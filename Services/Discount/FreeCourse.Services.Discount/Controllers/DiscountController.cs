using FreeCourse.Services.Discount.Models;
using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.CustomBases;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _identityService;

        public DiscountController(IDiscountService discountService, ISharedIdentityService identityService)
        {
            _discountService=discountService;
            _identityService=identityService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetById(id);
            return CreateActionResultInstance(discount);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _identityService.UserId;
            var discount = await _discountService.GetByCodeAnduserId(userId, code);
            if (discount!=null)
            {
                return CreateActionResultInstance(discount);
            }

            return (IActionResult)ResponseDto<NoContent>.Fail("Lütfen Bilgileri gözden geçiriniz ", 500);

        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Save(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Update(discount));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.Delete(id));
        }

    }
}
