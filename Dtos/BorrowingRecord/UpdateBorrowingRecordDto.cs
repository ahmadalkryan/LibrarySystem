namespace Application.Dtos.BorrowingRecord
{
    public class UpdateBorrowingRecordDto
    {
        public int Id { get; set; }

        public string TransactionNumber { get; set; }      // Unique code for each borrowing transaction

        public DateTime? ReturnDate { get; set; }
        public DateTime? BorrowDate { get; set; }

        public DateTime? DueDate { get; set; }

       
        public int? _memberId { get; set; }


        public int? _bookCopyId { get; set; }


        public int? _userId { get; set; }





        public decimal BorrowingCost { get; set; }
        public decimal? FinePaid { get; set; }

     

    }
}
