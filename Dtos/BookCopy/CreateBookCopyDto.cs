namespace Application.Dtos.BookCopy
{
    public class CreateBookCopyDto
    {

        public string Barcode { get; set; }

        public string Status { get; set; } // e.g., Available, Borrowed, Lost ,In Repair
        public string Location { get; set; } // e.g., Shelf A3, Section B

        public int _userId { get; set; }
        public int _bookId { get; set; }

        public string Notes { get; set; } 

        
    }
}
