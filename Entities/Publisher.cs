using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public ICollection<Book> _books { get; set; } = new HashSet<Book>();
    }
}
