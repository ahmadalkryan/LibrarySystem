using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string Description { get; set; }


        public int PageCount { get; set; }

        public string Language { get; set; }

        public Author _author { get; set; }

        public int _authorId { get; set; }

        public Publisher _publisher { get; set; }

        public int _publisherId { get; set; }

        public Category _category { get; set; }

        public int _categoryId { get; set; }

        public int _userId {  get; set; }
        public User _user { get; set; }

        public ICollection<BookCopy> _bookCopies { get; set; } = new HashSet<BookCopy>();
    }
}
