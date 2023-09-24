using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.CustomBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService=courseService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsycn();
            return CreateActionResultInstance(response);
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _courseService.GetByUserId(userId);
            return CreateActionResultInstance(response);
        }
    }
}
