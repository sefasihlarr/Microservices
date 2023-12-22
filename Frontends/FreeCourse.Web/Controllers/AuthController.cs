using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SingIn(SingInInput singInInput)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            var response = await _identityService.SingIn(singInInput);
            if (!response.IsSuccessful)
            {
                response.Errors.ForEach(e =>
                {
                    ModelState.AddModelError(string.Empty, e);
                });

                return View();
                
            }

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
