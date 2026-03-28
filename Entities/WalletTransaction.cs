using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WalletTransaction
    {

        public int Id { get; set; }
        public string TransactionNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }

        public string TransactionType { get; set; }   // Charge, Borrow, Fine 

        public string Status { get; set; } = "Completed";   // Pending,Completed,Failed

        public string Description { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }




        // navigations objects 
        public int _walletId { get; set; }
        public Wallet _wallet { get; set; }



        public int _memberId { get; set; }
        public Member _member { get; set; }



        public int? _borrowingRecordId { get; set; }
        public BorrowingRecord? _borrowingRecord { get; set; }

        public User CreatedByUser { get; set; }
        public int CreatedByUserId { get; set; }

    }
}
