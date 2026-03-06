using Application.Dtos.BorrowingRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IBorrowingRecordService
    {
        // Define methods related to borrowing records here

        Task<bool> IsBookCopyAvailableAsync(int bookCopyId);

       

        Task<BorrowingRecordDto> CreateBorrowing(CreateBorrowingRecordDto createBorrowingRecordDto);

        Task<IEnumerable<BorrowingRecordDto>> GetAllBorrowingRecords();

        Task<BorrowingRecordDto> GetBorrowingRecordById(int id);

        Task<BorrowingRecordDto> UpdateBorrowingRecord(string transaction );

        Task<bool> DeleteBorrowingRecord(int id);

        Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByUserId(int userId);

        Task<IEnumerable<BorrowingRecordDto>> GetOverdueBorrowingRecords();


        Task<IEnumerable<BorrowingRecordDto>> GetActiveBorrowingRecords();



        Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByBookCopyId(int bookCopyId);

        Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByDateRange(DateTime startDate, DateTime endDate);

        Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByMemberId(int memberId);

        Task<IEnumerable<BorrowingRecordDto>> GetOverdueBorrowingRecordsAndNotLost();
        Task<BorrowingRecordDto> GetBorrowingRecordByTransaction(string transaction);

        Task<BorrowingRecordDto> SetStatusLostForCopy();

        Task<bool> SetLost(int copyId );

    }
}
