using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Author
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Biography { get; set; }

        public string Nationality { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DeathDate { get; set; }

         public ICollection<Book> _books { get; set; }  =new HashSet<Book> ();

         


    }
}
