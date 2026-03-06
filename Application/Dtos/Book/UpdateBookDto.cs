namespace Application.Dtos.Book
{
    public class UpdateBookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

       // public string ISBN { get; set; }

        public string Description { get; set; }


        public int PageCount { get; set; }

        public string Language { get; set; }


        public int _authorId { get; set; }



        public int _publisherId { get; set; }

        public int _userId { get; set; }

        public int _categoryId { get; set; }
    }
}
