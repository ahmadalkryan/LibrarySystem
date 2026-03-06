namespace Application.Dtos.Author
{
    public class UpdateAuthorDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Biography { get; set; }

        public string? Nationality { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }
    }
}
