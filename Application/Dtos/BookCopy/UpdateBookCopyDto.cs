namespace Application.Dtos.BookCopy
{
    public class UpdateBookCopyDto
    {
        public int Id { get; set; }

        public string Barcode { get; set; }

        public string Status { get; set; } // e.g., Available, Borrowed, Damaged 
        public string Location { get; set; } // e.g., Shelf A3, Section B

        public int _userId  { get; set; }
        public string Notes { get; set; }

       


    }
}
