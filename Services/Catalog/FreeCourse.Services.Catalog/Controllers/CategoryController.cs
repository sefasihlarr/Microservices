using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CategoryDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.CustomBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService=categoryService;
            _mapper=mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsycn();
            return CreateActionResultInstance(response);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryDto)
        {
            var response = await _categoryService.CreateAsync(_mapper.Map<Category>(categoryDto));
            return CreateActionResultInstance(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var values = await _categoryService.GetByIdAsync(id);
            if (values!=null)
            {
                return CreateActionResultInstance(values);
            }

            return NotFound();
        }

        //[HttpPost("[action]/{id}")]
        //public async Task<IActionResult> Remove(string id)
        //{
            
        //}
    }
}
