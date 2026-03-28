using Application.Dtos.BookCopy;
using Application.Dtos.Statistics;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class BookCopyService : IBookCopyService
    {

        private readonly IRepository<BookCopy> _bookCopyRepository;

        private readonly IMapper _mapper;


        public BookCopyService(IRepository<BookCopy> repository , IMapper mapper)
        {
            
            _bookCopyRepository = repository;
            _mapper = mapper;

        }
        public async Task<BookCopyDto> CreateBookCopyAsync(CreateBookCopyDto createBookCopyDto)
        {
            var existingCopies = await _bookCopyRepository.GetAllWitAllIncludeAsync(x => x.Barcode == createBookCopyDto.Barcode);

            if(existingCopies.Any())
            {
                throw new Exception($"A copy with barcode {createBookCopyDto.Barcode} already exists.");
            }

            var copy = _mapper.Map<BookCopy>(createBookCopyDto);

            await _bookCopyRepository.AddAsync(copy);

            return _mapper.Map<BookCopyDto>(copy);
        }

        public async Task<bool> DeleteBookCopyAsync(int id)
        {
           await _bookCopyRepository.DeleteAsync(id);

            return true;
        }

        public async Task<IEnumerable<BookCopyDto>> GetAllBookCopiesAsync()
        {
            return _mapper.Map<IEnumerable<BookCopyDto>>(await _bookCopyRepository.GetAllAsync());
        }

        public async Task<IEnumerable<BookCopyDto>> GetBookCopiesByBookIdAsync(int bookId)
        {
            return _mapper.Map<IEnumerable<BookCopyDto>>(await _bookCopyRepository.GetAllWitAllIncludeAsync(x=>x._bookId==bookId));
        }



        public async Task<int> GetBookCopiesCountForStatusAsync(string status)
        {
            return (await _bookCopyRepository.GetAllWitAllIncludeAsync(x=>x.Status==status)).Count();
        }


        public async Task<IEnumerable<BookCopyDto>> GetBookCopiesThatAddedAfterDate(DateTime dateTime)
        {
            return _mapper.Map<IEnumerable<BookCopyDto>>(await _bookCopyRepository.GetAllWitAllIncludeAsync(x => x.AddedDate >= dateTime));
        }

        public async Task<BookCopyDto> GetBookCopyByIdAsync(int id)
        {
              return _mapper.Map<BookCopyDto>(await _bookCopyRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<BookCopyDto>> GetBookCopyByStatusAsync(string status)
        {

            return _mapper.Map<IEnumerable<BookCopyDto>>(await _bookCopyRepository.GetAllWitAllIncludeAsync(x => x.Status == status));

           
        }

        public async Task<IEnumerable<BookCopyDto>> GetBookCopyByUserId(int userId)
        {
           return _mapper.Map<IEnumerable<BookCopyDto>>(await _bookCopyRepository.GetAllWitAllIncludeAsync(x=>x._userId == userId));
        }

        public async Task<int> GetTotalBookCopiesCountAsync()
        {
            return (await _bookCopyRepository.GetAllAsync()).Count();
        }

        public async Task<BookCopyDto> UpdateBookCopyAsync(UpdateBookCopyDto updateBookCopyDto)
        {

            var existingCopy = await _bookCopyRepository.GetByIdAsync(updateBookCopyDto.Id);

            var terminalStates = new[] { "Lost", "Withdrawn", "PermanentlyDamaged" };

            if(terminalStates.Contains(existingCopy.Status))
            {
              
                throw new Exception($"Cannot change status of a copy in terminal state: {existingCopy.Status}");
            }
           
            if (updateBookCopyDto.Location != null)
            {
                existingCopy.Location = updateBookCopyDto.Location;
            }
            if (updateBookCopyDto.Notes != null)
            {
                existingCopy.Notes = updateBookCopyDto.Notes;
            }
            if (updateBookCopyDto.Barcode != null)
            {
                existingCopy.Barcode = updateBookCopyDto.Barcode;
            }
            // 5. الحقول الإجبارية (int)
            //existingCopy._bookId = updateBookCopyDto._bookId;
            //existingCopy._userId = updateBookCopyDto._userId;

            await _bookCopyRepository.UpdateAsync(existingCopy);
            //  var copy = _mapper.Map<BookCopy>(updateBookCopyDto);
            //await _bookCopyRepository.UpdateAsync(copy);

            return _mapper.Map<BookCopyDto>(existingCopy);
        }


        //  this method Not used 
        public async Task<BookCopyDto> UpdateStatusForCopy(int copyid, string newStatus)
        {
            var copy = await _bookCopyRepository.GetByIdAsync(copyid);

            copy.Status = newStatus;

            await _bookCopyRepository.UpdateAsync(copy);
            return _mapper.Map<BookCopyDto>(copy);

        }


        public async Task<IEnumerable<BookCopyDto>> GetBookCopyByCategoryName(string categoryName)
        {
            return _mapper.Map<IEnumerable<BookCopyDto>>(await _bookCopyRepository.GetAllWitAllIncludeAsync(x=>x._book._category.Name == categoryName));
        }

       public  async Task<IEnumerable<BookCopyDto>>GetBookCopiesByTitle(string title)
        {
           return _mapper.Map<IEnumerable<BookCopyDto>>(await _bookCopyRepository.GetAllWitAllIncludeAsync(x=>x._book.Title== title));

        }

        public async Task<int> GetBookCopiesCountByBookId(int bookId)
        {
            var result =await _bookCopyRepository.GetAllWitAllIncludeAsync(x => x._bookId == bookId);
            return result.Count();
        }

       public async Task<BookCopyStutsStatistics>GetStatisticsStuts(int bookId)
        {

            var copies = await _bookCopyRepository.GetAllWitAllIncludeAsync(x => x._bookId == bookId);
            var statistics = new BookCopyStutsStatistics
            {
                TotalCopies = copies.Count(),
                AvailableCopies = copies.Count(x => x.Status == "Available"),
                BorrowedCopies = copies.Count(x => x.Status == "Borrowed"),
                LostCopies = copies.Count(x => x.Status == "Lost"),
                InRepairCopies = copies.Count(x => x.Status == "InRepair")
            };
            return statistics;

        }

       public async Task<bool>SetAvailable(int copyId)
        {
           var copy = await _bookCopyRepository.GetByIdAsync(copyId);

           if(copy.Status== "Lost" || copy.Status == "Withdrawn" || copy.Status == "PermanentlyDamaged")
            {
                throw new Exception($"Cannot set status to Available for a copy in terminal state: {copy.Status}");
            }
           if(copy.Status== "Borrowed" || copy.Status== "InRepair")
            {
                copy.Status = "Available";
                await _bookCopyRepository.UpdateAsync(copy);
                return true;
            }
           return false;
        }

      public  async Task<bool> SetBorrowed(int copyId)
        {
            var copy = await _bookCopyRepository.GetByIdAsync(copyId);

            if(copy.Status== "Lost" || copy.Status == "Withdrawn" || copy.Status == "PermanentlyDamaged")
            {
                throw new Exception($"Cannot set status to Borrowed for a copy in terminal state: {copy.Status}");
            }
            if(copy.Status== "Available")
            {
                copy.Status = "Borrowed";

                await _bookCopyRepository.UpdateAsync(copy);
                return true;
            }
                return false;

        }

       public async Task<bool> SetInRepair(int copyId)
        {
            var copy = await _bookCopyRepository.GetByIdAsync(copyId);

            if(copy.Status== "Lost" || copy.Status == "Withdrawn" || copy.Status == "PermanentlyDamaged")
            {
                throw new Exception($"Cannot set status to InRepair for a copy in terminal state: {copy.Status}");
            }
          if(copy.Status== "Borrowed") {
                copy.Status = "InRepair";
                await _bookCopyRepository.UpdateAsync(copy);
                return true;
            }
          return false;
        }
                
        


       public async Task<bool> SetLost(int copyId)
        {
            var copy = await  _bookCopyRepository.GetByIdAsync(copyId);

            if (copy.Status == "Lost" || copy.Status == "Withdrawn" || copy.Status == "PermanentlyDamaged")
            {
                throw new Exception($"Cannot set status to Lost for a copy that is currently Borrowed.");
            }

            if(copy.Status == "Borrowed")
            {
                copy.Status = "Lost";
                await _bookCopyRepository.UpdateAsync(copy);
                return true;
            }
            return false;

        }

          public async Task<IEnumerable<BookCopyDto>> GetAvilableCopyForBook(int bookId)
        {
            var av = await _bookCopyRepository.GetAllWitAllIncludeAsync(x=>x._bookId==bookId &&x.Status== "Available");

            return _mapper.Map<IEnumerable<BookCopyDto>>(av);
        }
    }
}
