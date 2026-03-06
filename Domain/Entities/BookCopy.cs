using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookCopy
    {
        public int Id { get; set; }
       
        public string  Barcode { get; set; }

        public string Status { get; set; } = "Available"; // e.g., Available, Borrowed, Lost 
        public string Location { get; set; } // e.g., Shelf A3, Section B

         public string Notes { get; set; }

        public DateTime AddedDate { get; set; }


        // Foreign Key to Book
        public int _bookId { get; set; }
        public Book _book   { get; set; }

        public User _user { get; set; }
        public int _userId { get; set; }
        public ICollection<BorrowingRecord> _borrowRecords { get; set; }

    }
}
