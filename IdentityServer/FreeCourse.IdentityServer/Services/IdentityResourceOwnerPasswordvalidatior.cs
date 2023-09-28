
using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidatior : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidatior(UserManager<ApplicationUser> userManager)
        {
            _userManager=userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);

            if (existUser == null)
            {
                var errros = new Dictionary<string, object>();
                errros.Add("errors", new List<string> { "Email  yada şifreniz yanlış" });

                context.Result.CustomResponse = errros;

                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (passwordCheck == false)
            {
                var errros = new Dictionary<string, object>();
                errros.Add("errors", new List<string> { "Email  yada şifreniz yanlış" });

                context.Result.CustomResponse = errros;

                return;
            }

            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
