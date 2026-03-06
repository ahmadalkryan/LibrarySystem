using Application.Dtos.BookCopy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopyDto>> GetAllBookCopiesAsync();
        Task<BookCopyDto> GetBookCopyByIdAsync(int id);
        Task<BookCopyDto> CreateBookCopyAsync(CreateBookCopyDto createBookCopyDto);
        Task<BookCopyDto> UpdateBookCopyAsync(UpdateBookCopyDto updateBookCopyDto);
        Task<bool> DeleteBookCopyAsync(int id);

        Task<BookCopyDto> UpdateStatusForCopy(int copyid ,string newStatus);
        Task<IEnumerable<BookCopyDto>> GetBookCopiesByBookIdAsync(int bookId);
        
        Task<IEnumerable<BookCopyDto>> GetBookCopyByStatusAsync(string status);

        // Task<IEnumerable<BookCopyDto>> GetBookCopiesByLocationAsync(string location);
        Task<IEnumerable<BookCopyDto>> GetBookCopyByUserId(int userId);
        Task<int> GetTotalBookCopiesCountAsync();
        Task<int> GetBookCopiesCountByBookId(int bookId);
        Task<IEnumerable<BookCopyDto>>GetBookCopiesByTitle(string title);
        Task<IEnumerable<BookCopyDto>> GetBookCopyByCategoryName(string categoryName);
        Task<int> GetBookCopiesCountForStatusAsync(string status);

        Task<IEnumerable<BookCopyDto>> GetBookCopiesThatAddedAfterDate(DateTime dateTime);
    }
}
