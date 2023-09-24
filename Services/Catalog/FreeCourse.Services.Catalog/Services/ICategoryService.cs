using FreeCourse.Services.Catalog.Dtos.CategoryDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsycn();
        Task<ResponseDto<CategoryDto>> CreateAsync(Category category);

        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
    }
}
