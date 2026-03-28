using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Statistics
{
    public class BookCopyStutsStatistics
    {
        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public int BorrowedCopies { get; set; }

        public int LostCopies { get; set; }

        public int InRepairCopies { get; set; }
    }
}
