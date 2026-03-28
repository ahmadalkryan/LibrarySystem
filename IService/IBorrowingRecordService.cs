using Application.Dtos.Book;
using Application.Dtos.BorrowingRecord;
using Application.Dtos.Statistics;
using Domain.Entities;
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
        Task<bool> DeleteBorrowingRecord(int id);
        Task<BorrowingRecordDto> GetBorrowingRecordById(int id);


       
        Task<bool> BorrowPayment(BorrowingRecord borrow);
        Task<bool> FinePayment(CreateBorrowingRecordDto createBorrowingRecordDto);


        // set status to Available and return date to now
        Task<BorrowingRecordDto> ReturnBookAsync(int BorrowingId);

        // set status to lost and return date to now
        Task<bool> MarkCopyAsLostAsync(int BorrowingId);
        // set status to in repair 
        Task<BorrowingRecordDto> RenewBorrowingAsync( int BorrowingId);




        Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByUserId(int userId);

        Task<IEnumerable<BorrowingRecordDto>> GetOverdueBorrowingRecords();


        Task<IEnumerable<BorrowingRecordDto>> GetActiveBorrowingRecords();



        Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByBookCopyId(int bookCopyId);

        Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByDateRange(DateTime startDate, DateTime endDate);

        Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByMemberId(int memberId);

        Task<IEnumerable<BorrowingRecordDto>> GetOverdueBorrowingRecordsAndNotLost();
        Task<BorrowingRecordDto> GetBorrowingRecordByTransaction(string transaction);

       
        Task<IEnumerable<MostBorrowedBookDto>> GetMostBorrowedBooksAsyncLastMounth(int topN , int numdays);



        Task<decimal> CalculateFineAsync(int borrowingId);
        Task<int> GetActiveBorrowingsCountAsync(int memberId);
        Task<bool> CanMemberBorrowAsync(int memberId);


    }
}
