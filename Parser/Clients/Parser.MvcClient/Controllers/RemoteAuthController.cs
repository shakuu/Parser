using System.Threading.Tasks;
using System.Web.Mvc;

using Microsoft.AspNet.Identity.Owin;

using Parser.Auth.Contracts;
using Parser.MvcClient.Controllers.Base;

namespace Parser.MvcClient.Controllers
{
    public class RemoteAuthController : LoggingController
    {
        private readonly IIdentityAuthAccountService identityAuthAccountService;

        public RemoteAuthController(IIdentityAuthAccountService identityAuthAccountService)
        {
            this.identityAuthAccountService = identityAuthAccountService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            var signInResult = await this.identityAuthAccountService.PasswordSignInAsync(username, password, false);
            if (signInResult == SignInStatus.Success)
            {
                return this.Json(new { result = "success" });
            }
            else
            {
                return this.Json(new { result = string.Empty });
            }
        }
    }
}