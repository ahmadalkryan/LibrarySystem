 using Application.Dtos.BorrowingRecord;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using AutoMapper.Execution;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class BorrowingRecordService : IBorrowingRecordService
    {

        private readonly IRepository<BorrowingRecord> _repo;

        private readonly IMapper _mapper;
        private readonly IBookCopyService _bookCopyService;
        private readonly IMemberService _memberService;

        public BorrowingRecordService(IMapper mapper,IMemberService memberService ,IRepository<BorrowingRecord> repository ,IBookCopyService bookCopyService)
        {
            _mapper=mapper;
            _repo=repository;
            _bookCopyService=bookCopyService;
                _memberService=memberService;
        }

        public async Task<BorrowingRecordDto> CreateBorrowing(CreateBorrowingRecordDto createBorrowingRecordDto)
        {

            var copy = await _bookCopyService.GetBookCopyByIdAsync(createBorrowingRecordDto._bookCopyId);
            var member = await _memberService.GetMemberById(createBorrowingRecordDto._memberId);

            if (copy.Status == " Borrowed" ||copy.Status == "Lost")
            {
                throw new Exception("Not valid");

            }
            var borrow = _mapper.Map<BorrowingRecord>(createBorrowingRecordDto);

            await _bookCopyService.UpdateStatusForCopy(createBorrowingRecordDto._bookCopyId, "Borrowed");

            await _repo.AddAsync(borrow);

            return _mapper.Map<BorrowingRecordDto>(borrow);
            
            
        }

        public async Task<bool> DeleteBorrowingRecord(int id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetActiveBorrowingRecords()
        {
            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllWitAllIncludeAsync(x => x.DueDate > DateTime.Now));
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetAllBorrowingRecords()
        {
            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllAsync());
        }

        public async Task<BorrowingRecordDto> GetBorrowingRecordById(int id)
        {
           return  _mapper.Map<BorrowingRecordDto>(await _repo.GetByIdAsync(id));
        }

        public async Task<BorrowingRecordDto> GetBorrowingRecordByTransaction(string transaction)
        {
            return  _mapper.Map<BorrowingRecordDto>(await _repo.GetAllWitAllIncludeAsync(x => x.TransactionNumber==transaction));
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByBookCopyId(int bookCopyId)
        {
            var borrow = await _repo.GetAllWitAllIncludeAsync(x=>x._bookCopyId == bookCopyId);

            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(borrow);
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllWitAllIncludeAsync(x => x.BorrowDate == startDate && x.DueDate == endDate));
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByMemberId(int memberId)
        {
           return _mapper.Map<IEnumerable<BorrowingRecordDto>>( await _repo.GetAllWitAllIncludeAsync(x=>x._memberId == memberId));
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByUserId(int userId)
        {
           var borrow =  _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllWitAllIncludeAsync(x => x._userId == userId));
            return borrow;
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetOverdueBorrowingRecords()
        {
            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllWitAllIncludeAsync(x => x.ReturnDate >= x.DueDate ));

        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetOverdueBorrowingRecordsAndNotLost()
        {
            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllWitAllIncludeAsync(x => x.ReturnDate >= x.DueDate && x._bookCopy.Status != "Lost"));
        }

        public async  Task<bool> IsBookCopyAvailableAsync(int bookCopyId)
        {
           var copy =  await _bookCopyService.GetBookCopyByIdAsync(bookCopyId);
            if(copy.Status == "Available")
            {
                return true;
            }
            return false;
        }

       

        public Task<BorrowingRecordDto> SetStatusLostForCopy()
        {
            throw new NotImplementedException();
        }

       

       public async Task<bool> SetLost(int copyId)
        {
          var ressult =  await _bookCopyService.UpdateStatusForCopy(copyId, "Lost");
            if (ressult != null)
            {
                return true;
            }
            return false;

        }

         public   async Task<BorrowingRecordDto> UpdateBorrowingRecord(string transaction)
        { 

            var record = (await _repo.GetAllWitAllIncludeAsync(x=>x.TransactionNumber == transaction)).FirstOrDefault();

            record.ReturnDate= DateTime.Now;

            await _repo.UpdateAsync(record); 

            return _mapper.Map<BorrowingRecordDto>(record);

        }
    }
}
