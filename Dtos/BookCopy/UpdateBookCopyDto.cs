namespace Application.Dtos.BookCopy
{
    public class UpdateBookCopyDto
    {
        public int Id { get; set; }

        public string Barcode { get; set; }

        /// <summary>
        /// Status: Available, Borrowed, Damaged, Lost
        /// </summary>
      
        public string Location { get; set; } // e.g., Shelf A3, Section B

       
        public int _userId  { get; set; }
        public int _bookId { get; set; }
        public string Notes { get; set; }

       


    }
}
