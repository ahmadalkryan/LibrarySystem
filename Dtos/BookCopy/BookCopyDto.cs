namespace Application.Dtos.BookCopy
{
    public class BookCopyDto
    {

        public int Id { get; set; }

        public string Barcode { get; set; }

        public string Status { get; set; } // e.g., Available, Borrowed, lost ,InRepair 
        public string Location { get; set; } // e.g., Shelf A3, Section B

        public string Notes { get; set; }

        public DateTime AddedDate { get; set; }

        public int _bookId { get; set; }
        public int _userId { get; set; }
        public string BookTitle { get; set; } = string.Empty;


        }
}
