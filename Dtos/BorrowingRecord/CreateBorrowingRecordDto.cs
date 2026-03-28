namespace Application.Dtos.BorrowingRecord
{
    public class CreateBorrowingRecordDto
    {
      
        public DateTime DueDate { get; set; }


        public int _memberId { get; set; }


        public int _bookCopyId { get; set; }


        public int _userId { get; set; }

        // daily
        public decimal BorrowingCost { get; set; } // determine for wach day 
        public decimal FinePaid { get; set; }   // determine for wach day 

      

        public bool IsValidDueDate()
        {
            return DueDate > DateTime.Now;
        }

    }
}




