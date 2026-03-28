using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Wallet
{
    public class CreateWalletDto
    {
       

        public decimal MinBalance { get; set; }

       public decimal MaxBalance { get; set; }


      

        public int _memberId { get; set; }
    }
}
