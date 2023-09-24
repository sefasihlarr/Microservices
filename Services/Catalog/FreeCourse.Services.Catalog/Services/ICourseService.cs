using FreeCourse.Services.Catalog.Dtos.CourseDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<ResponseDto<List<CourseDto>>> GetAllAsycn(); 
        Task<ResponseDto<CourseDto>> CreateAsync(Course course);
        Task<ResponseDto<CourseDto>> GetByIdAsync(string id);
        Task<ResponseDto<List<CourseDto>>> GetByUserId(string userId);
        Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<ResponseDto<NoContent>> DeleteAsycn(string id);
    }
}
