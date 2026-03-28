using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Wallet
{

    public class WalletDto
    {

        public int Id { get; set; }

        // money exists 
        public decimal Balance { get; set; } 

        //  for each charge transaction ++ 
        public decimal TotalCharge { get; set; } 

        // for each withdraw trnsaction ++ 
        public decimal TotalSpent { get; set; } 



        public bool IsActive { get; set; }

        // Max 
        public decimal MinBalance { get; set; }
        // Min 
        public decimal MaxBalance { get; set; } 


        public DateTime CreatedAt { get; set; }

        // update it on transaction 
        public DateTime? LastTransactionAt { get; set; }

       // Owner for account 
       public string MemberName { get; set; }
        public int _memberId { get; set; }





    }
}
