using Application.Dtos.Category;
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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repo;
        private readonly IMapper _mapper;


        public CategoryService(IMapper mapper , IRepository<Category> repository)
        {
            _mapper = mapper;
            _repo= repository;      
            
        }

        public async Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
        {
           
            var cat = _mapper.Map<Category>(createCategoryDto);

            await _repo.AddAsync(cat);

            return _mapper.Map<CategoryDto>(cat);



        }

        public async Task<bool> DeleteCategory(int id)
        {
           await _repo.DeleteAsync(id);

            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {

            return _mapper.Map<IEnumerable<CategoryDto>>(await _repo.GetAllAsync());
            
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
           var cat = await _repo.GetByIdAsync(id);

            return _mapper.Map<CategoryDto>(cat);
        }

        public async Task<CategoryDto> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var caat = _mapper.Map<Category>(updateCategoryDto);

            await _repo.UpdateAsync(caat);

            return _mapper.Map<CategoryDto>(caat);
        }
    }
}
