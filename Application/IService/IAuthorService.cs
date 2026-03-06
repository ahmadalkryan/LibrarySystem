using Application.Dtos.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public  interface IAuthorService
    {
        Task<AuthorDto> CreateAuthor(CreateAuthorDto createAuthorDto);

        Task<AuthorDto> GetAuthorById(int id);

        Task<AuthorDto> UpdateAuthor(UpdateAuthorDto updateAuthorDto);
        Task<AuthorDto> DeleteAuthor(int id);
        Task<IEnumerable<AuthorDto>> GetAllAuthor();
        

    }
}
