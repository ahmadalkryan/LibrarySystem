using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.WalletTransaction
{
    public class UpdateWalletTransactionDto
    {
        public int Id { get; set; }
        public string? TransactionNumber { get; set; }

        public decimal Amount { get; set; }

        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }


        public string? TransactionType { get; set; }   // Charge, Borrow, Fine, Refund

        public string Status { get; set; } = "Completed"; // Pending, Completed, Failed

        public string? Description { get; set; }

        public string? Notes { get; set; }


      



        public int _walletId { get; set; }




        public int _memberId { get; set; }

        public int _borrowingRecordId { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
