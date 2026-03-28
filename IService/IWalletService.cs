using Application.Dtos.Wallet;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IWalletService
    {


            /// <summary>
            /// الحصول على محفظة العضو
            /// </summary>
            Task<WalletDto> GetWalletByMemberIdAsync(int memberId);

            Task<WalletDto> GetWalletById(int id);

            Task<IEnumerable<WalletDto>> GetActiveWallets();

             Task<IEnumerable<WalletDto>> GetDeavtivateWallets();
              
             Task<bool> DeleteWallet(int id);

            /// <summary>
            /// الحصول على رصيد العضو
            /// </summary>
            Task<decimal> GetBalanceAsync(int memberId);


        // ============ عمليات المحفظة ============

        /// <summary>
        /// شحن المحفظة (إيداع)
        /// </summary>
            Task<WalletDto> CreateWallet (CreateWalletDto createWalletDto);

                Task<bool> HasSufficientBalanceAsync(int memberId, decimal amount);

                // 📌 شحن المحفظة
                Task<bool> ChargeWalletAsync(
                    int memberId,
                    decimal amount
                  );
                /// <summary>
                /// خصم من المحفظة
                /// </summary>
                Task<bool> DeductFromWalletAsync(int memberId, decimal deductamount);
               

                 void UpddatetransactionPramater(UpdateWalletDto updateWalletDto);

        
                    // 📌 تقارير
                    Task<WalletDto> GetWalletSummaryAsync(int memberId);
                 //   Task<FinancialReport> GetFinancialReportAsync(DateTime startDate, DateTime endDate);


                    // 📌 إدارة المحفظة
                    Task<WalletDto> ActivateWalletAsync(int memberId);
                    Task<WalletDto> DeactivateWalletAsync(int memberId);
                    Task<WalletDto> UpdateWalletLimitsAsync(int memberId, decimal minBalance, decimal maxBalance);








        // 📌 سجل المعاملات
        //Task<IEnumerable<WalletTransaction>> GetTransactionsAsync(
        //    int memberId,
        //    DateTime? from = null,
        //    DateTime? to = null);

        //Task<IEnumerable<WalletTransaction>> GetAllTransactionsAsync(
        //    DateTime? from = null,
        //    DateTime? to = null);

        /// <summary>
        /// استرداد مبلغ إلى المحفظة
        /// </summary>
        //  Task<WalletDto> RefundToWalletAsync(int memberId, decimal amount);

        // ============ سجل المعاملات ============

        /// <summary>
        /// الحصول على معاملات المحفظة
        /// </summary>
        // Task<List<WalletTransaction>> GetWalletTransactionsAsync(int memberId, DateTime? from = null, DateTime? to = null);

        /// <summary>
        /// الحصول على آخر المعاملات
        /// </summary>
        //  Task<List<WalletTransaction>> GetRecentTransactionsAsync(int memberId, int count = 10);

        /// <summary>
        /// الحصول على معاملة برقمها
        /// </summary>
        //Task<WalletTransaction> GetTransactionByNumberAsync(string transactionNumber);

        // ============ تقارير ============

        /// <summary>
        /// ملخص المحفظة
        /// </summary>
        //Task<WalletSummary> GetWalletSummaryAsync(int memberId);

        /// <summary>
        /// إحصائيات المحفظة
        /// </summary>
        //Task<WalletStatistics> GetWalletStatisticsAsync(int memberId);

    }
}
