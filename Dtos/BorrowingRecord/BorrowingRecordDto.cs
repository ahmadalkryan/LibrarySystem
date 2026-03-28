namespace Application.Dtos.BorrowingRecord
{
    public class BorrowingRecordDto
    {
        public int Id { get; set; }

        public string TransactionNumber { get; set; } // Unique code for each borrowing transaction

        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; } = null;

        public int _memberId { get; set; }
       
        public string MemberFullName { get; set; } // new 


        public int _bookCopyId { get; set; }

       
        public int _userId { get; set; }



        public decimal BorrowingCost { get; set; }
        public decimal? FinePaid { get; set; }

      
    }
}
