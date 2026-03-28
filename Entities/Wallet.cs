using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Wallet
    {
        public int Id { get; set; }

        public decimal Balance { get; set; } = 0;

        public decimal TotalCharge { get; set; } = 0;

        public decimal TotalSpent { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public decimal MinBalance { get; set; } = 0;

        public decimal MaxBalance { get; set; } = 5000;

        public DateTime CreatedAt { get; set; }
        public DateTime? LastTransactionAt { get; set; }

        public Member? _member { get; set; }
        public int _memberId { get; set; }

        public ICollection<WalletTransaction> _walletTransactions { get; set; } =new HashSet<WalletTransaction>();




    }
}
