using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class BorrowingRecord
    {
        public int Id { get; set; }

        public string TransactionNumber { get; set; } // Unique code for each borrowing transaction

        public DateTime BorrowDate  { get; set; }

        public DateTime DueDate  { get; set; }

        public DateTime? ReturnDate  { get; set; } = null;
        
        public int _memberId  { get; set; }
        public Member _member  { get; set; }

        public BookCopy _bookCopy  { get; set; }

        public int _bookCopyId { get; set; }

        public User _user { get; set; }

        public int _userId { get; set; }

        // new !!!!!!!!
        public decimal BorrowingCost { get; set; }
        public decimal? FinePaid {  get; set; }
      
        public ICollection<WalletTransaction>? _transactions { get; set; } =new HashSet<WalletTransaction>();

    }
}
