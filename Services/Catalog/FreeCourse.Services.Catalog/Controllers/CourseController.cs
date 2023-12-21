using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CourseDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.CustomBases;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomBaseController
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService=courseService;
            _mapper=mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseDto)
        {
            var response = await _courseService.CreateAsync(_mapper.Map<Course>(courseDto));
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseDto)
        {
            var values = await _courseService.GetByIdAsync(courseDto.Id);
            if (values!=null)
            {
                await _courseService.UpdateAsync(courseDto);
            }

            return CreateActionResultInstance(values);
        }


        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> RemoveAsync(string id)
        {
            var response = await _courseService.DeleteAsycn(id);

            if (response.IsSuccessful)
            {
                return CreateActionResultInstance(response);
            }

            else
            {
                return CreateActionResultInstance(response);
            }
        }
    }
}
