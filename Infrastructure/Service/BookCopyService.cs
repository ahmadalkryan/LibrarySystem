using Application.Dtos.BookCopy;
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
           var copy = _mapper.Map<BookCopy>(updateBookCopyDto);
            await _bookCopyRepository.UpdateAsync(copy);

            return _mapper.Map<BookCopyDto>(copy);
        }

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
    }
}
