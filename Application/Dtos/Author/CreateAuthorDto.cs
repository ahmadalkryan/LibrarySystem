namespace Application.Dtos.Author
{
    public class CreateAuthorDto
    {
       

        public string Name { get; set; }

        public string Biography { get; set; }

        public string Nationality { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }
    }
}
