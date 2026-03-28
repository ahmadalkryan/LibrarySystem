using Application.Dtos.Book;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class BookService : IBookService
    {

        private readonly IRepository<Book> _repository;

        private readonly IMapper _mapper;


        public BookService(IMapper mapper , IRepository<Book> repository)
        {
             
            _mapper = mapper;
            _repository = repository;

        }
        public async Task<BookDto> AddBookAsync(CreateBookDto createBookDto)
        {
            var bppk = _mapper.Map<Book>(createBookDto);
            bppk.ISBN = $"978-{DateTime.Now.Year}-{Guid.NewGuid().ToString().Substring(0, 8)}";
            await _repository.AddAsync(bppk);

            return _mapper.Map<BookDto>(bppk);
        }

        public async Task<bool> DeleteBook(int id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return _mapper.Map<IEnumerable<BookDto>>((await _repository.GetAllAsync()).OrderBy(x=>x.PageCount));
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book =await _repository.GetByIdAsync(id);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByAuthorIdAsync(int authorId)
        {
            var books = await _repository.GetAllWitAllIncludeAsync(x=>x._authorId==authorId);


            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByCategoryNameAsync(string categoryName)
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _repository.GetAllWitAllIncludeAsync(x => x._category.Name == categoryName));
        }

        public async Task<BookDto> GetBooksByISBN(string isbn)
        {

            return _mapper.Map<BookDto>((await _repository.GetAllWitAllIncludeAsync(x => x.ISBN == isbn)).FirstOrDefault());
            
        }

        public async Task<IEnumerable<BookDto>> GetBooksByLanguageAsync(string language)
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _repository.GetAllWitAllIncludeAsync(x => x.Language==language));
        }

        public async Task<IEnumerable<BookDto>> GetBooksByPublisherIdAsync(int publisherId)
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _repository.GetAllWitAllIncludeAsync(x => x._publisherId==publisherId));
        }

        public async Task<IEnumerable<BookDto>> GetBooksByUserID(int userId)
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _repository.GetAllWitAllIncludeAsync(x =>x._userId==userId));
        }

        public async Task<BookDto> UpdateBookAsync(UpdateBookDto updateBookDto)
        {
          var book= _mapper.Map<Book>(updateBookDto);

            await _repository.UpdateAsync(book);

            return _mapper.Map<BookDto>(book);
        }

         public async Task<BookDto> GetBookByTitle(string title)
        {
            var book = await _repository.GetAllWitAllIncludeAsync(x => x.Title == title);

            return _mapper.Map<BookDto>(book.FirstOrDefault());


        }

            public async Task<int> GetBookCountForCategory(string categoryName)
        {
           var result = await _repository.GetAllWitAllIncludeAsync(x => x._category.Name == categoryName);
            return result.Count();
        }
    }
}
