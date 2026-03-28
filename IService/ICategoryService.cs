using Application.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ICategoryService
    {

        Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto);

        Task<CategoryDto> UpdateCategory(UpdateCategoryDto updateCategoryDto);

        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task< CategoryDto> GetCategoryById(int id);
        Task<bool> DeleteCategory(int id);

    }
}
