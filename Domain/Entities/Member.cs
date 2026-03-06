using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Member
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string MembershipType { get; set; } // e.g., Student , Public ,Researcher ,GovernmentEmployee

        public string MemberCode { get; set; } // Unique code for each member

        public ICollection<BorrowingRecord> _borrowingRecords { get; set; } = new HashSet<BorrowingRecord>();
    }
}
