using Application.Dtos.BorrowingRecord;
using Application.Dtos.WalletTransaction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IWalletTransactionService
    {
        Task<IEnumerable<WalletTransactionDto>> GetAllTransaction();

        Task<IEnumerable<WalletTransactionDto>> GetAllBorrowTransaction();

        Task<bool> ChargeWallet(ChargeDto chargeDto);
        Task<WalletTransactionDto> CreationTransaction(CreateWalletTransactionDto createWalletTransactionDto);

        Task<bool> DeletionTransaction(int id);

        Task<bool> UpdateStatusForTransaction(int id, string newStatus);

        Task<IEnumerable<WalletTransactionDto>> GetTRansactionGreaterThan(decimal amount);

        Task<IEnumerable<WalletTransactionDto>> GetTopNTransactions(int topN =10);
        Task<WalletTransactionDto> GetTransactionByIdAsync(int id);
        Task<WalletTransactionDto> GetTransactionByNumberAsync(string transactionNumber);
        Task<IEnumerable<WalletTransactionDto>> GetTransactionsByMemberIdAsync(int memberId);
        Task<IEnumerable<WalletTransactionDto>> GetTransactionsByUserIdAsync(int userId);
        Task<IEnumerable<WalletTransactionDto>> GetTransactionsByWalletIdAsync(int walletId);
        Task<IEnumerable<WalletTransactionDto>> GetTransactionsByBorrowingIdAsync(int borrowingRecordId);

        // 📌 فلترة المعاملات
        Task<IEnumerable<WalletTransactionDto>> GetTransactionsByDateRangeAsync(
            DateTime startDate,
            DateTime endDate);

        Task<IEnumerable<WalletTransactionDto>> GetTransactionsByTypeAsync(string transactionType);
        Task<IEnumerable<WalletTransactionDto>> GetTransactionsByStatusAsync(string status);

        // 📌 تقارير المعاملات
       // Task<TransactionReport> GetTransactionReportAsync(DateTime startDate, DateTime endDate);
        //Task<TransactionSummary> GetTransactionSummaryAsync(int memberId);

        // 📌 تحليلات
        Task<Dictionary<string, decimal>> GetTransactionTotalsByTypeAsync(DateTime? from = null, DateTime? to = null);
        Task<Dictionary<DateTime, decimal>> GetDailyTransactionTotalsAsync(DateTime startDate, DateTime endDate);

       
      
    }
}
