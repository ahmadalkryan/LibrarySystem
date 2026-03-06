using Application.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IBookService
    {
        Task<BookDto> AddBookAsync(CreateBookDto createBookDto);

        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> UpdateBookAsync( UpdateBookDto updateBookDto);


        Task<IEnumerable<BookDto>> GetAllBooksAsync();

        Task<IEnumerable<BookDto>> GetBooksByAuthorIdAsync(int authorId);

        Task<IEnumerable<BookDto>> GetBooksByCategoryNameAsync(string categoryName);

        Task<IEnumerable<BookDto>> GetBooksByUserID(int userId);
        Task<IEnumerable<BookDto>> GetBooksByPublisherIdAsync(int publisherId);
        Task<BookDto> GetBookByTitle (string title);    
        Task<IEnumerable<BookDto>> GetBooksByLanguageAsync(string language);

        Task<BookDto> GetBooksByISBN(string isbn);

        Task<int> GetBookCountForCategory(string categoryName);

        //Task<IEnumerable<BookDto>>GetBooksByCategory(string categoryName);
        Task<bool> DeleteBook(int id);







    }

}
