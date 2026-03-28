using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Wallet
{
    public class UpdateWalletDto
    {
        public decimal TotalCharge { get; set; }

        public decimal TotalSpent { get; set; }

        public DateTime? LastTransactionAt { get; set; }

        public int _memberId { get; set; }
    }
}
