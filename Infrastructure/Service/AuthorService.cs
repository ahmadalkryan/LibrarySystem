using Application.Dtos.Author;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class AuthorService : IAuthorService
    {

        private readonly IRepository<Author> _repository;

        private readonly IMapper _mapper;


        public AuthorService(IRepository<Author> repository ,IMapper mapper)
        {
             _mapper = mapper;
            _repository = repository;
        }
       public  async Task<AuthorDto> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
           var author = _mapper.Map<Author>(createAuthorDto);


            await _repository.AddAsync(author);

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> DeleteAuthor(int id)
        {
            var autor = _mapper.Map<Author>(id);

            await _repository.DeleteAsync(id);
            return _mapper.Map<AuthorDto>(autor);
        }

           public async Task<IEnumerable<AuthorDto>> GetAllAuthor()
        {
            return _mapper.Map<IEnumerable<AuthorDto>>(await _repository.GetAllAsync()); 
            
        }

         public async Task<AuthorDto> UpdateAuthor(UpdateAuthorDto updateAuthorDto)
        {

          var author = _mapper.Map<Author>(updateAuthorDto);
            await _repository.UpdateAsync(author);

            return _mapper.Map<AuthorDto>(author);
        }

       public async Task<AuthorDto> GetAuthorById(int id)
        {
           var author = await _repository.GetByIdAsync(id);
            return _mapper.Map<AuthorDto>(author);
        }
    }
}
