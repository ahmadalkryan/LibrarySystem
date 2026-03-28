using Application.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Statistics
{
    public class MostBorrowedBookDto
    {
        public BookDto BookDto { get; set; }
        public int BorrowCount { get; set; }

    }
}
