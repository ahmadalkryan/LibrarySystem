using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // e.g., Admin, Librarian, Staff

        public ICollection<BookCopy> _bookCopies { get; set; } = new HashSet<BookCopy>();
        public ICollection<Book > _books { get; set; } = new HashSet<Book>();
        public ICollection<BorrowingRecord> _borrowingRecords { get; set; } = new HashSet<BorrowingRecord>();
    }
}
