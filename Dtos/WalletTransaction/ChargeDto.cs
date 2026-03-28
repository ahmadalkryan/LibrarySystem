using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.WalletTransaction
{
    public class ChargeDto
    {

        public decimal Amount { get; set; }

      


        //public string TransactionType { get; set; }   // Charge, Borrow, Fine 

       

        public string Description { get; set; }

        public string Notes { get; set; }


       



        public int _walletId { get; set; }

        public int _memberId { get; set; }

      

        public int CreatedByUserId { get; set; }
    }
}
