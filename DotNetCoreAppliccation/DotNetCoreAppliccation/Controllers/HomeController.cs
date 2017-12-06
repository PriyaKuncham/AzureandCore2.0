using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreAppliccation.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DotNetCoreAppliccation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IConfiguration _iconfiguration;
        public HomeController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public async Task<IActionResult> Index()
        {
            var principal = HttpContext.User;

            ClaimsExtension.UpdateClaim(principal, ClaimTypes.Email, "test@gmail.com");

            ClaimsExtension.UpdateClaim(principal, ClaimTypes.Name, "test");

            ClaimsExtension.UpdateClaim(principal, "FullName", "test.test");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            var s = ClaimsExtension.GetClaimValue(principal, "FullName");

            var r = ClaimsExtension.GetClaimValue(principal, ClaimTypes.Email);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            var principal = HttpContext.User;

            ViewData["Message"] = ClaimsExtension.GetClaimValue(principal, ClaimTypes.Email) + ";  " +
                ClaimsExtension.GetClaimValue(principal, ClaimTypes.Name) + ";  " +
                ClaimsExtension.GetClaimValue(principal, "FullName");


            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public static class CustomCookieAuthenticationEvents
    {
        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            ClaimsExtension.UpdateClaim(userPrincipal, ClaimTypes.Email, "test@gmail.com");

            ClaimsExtension.UpdateClaim(userPrincipal, ClaimTypes.Name, "test");

            ClaimsExtension.UpdateClaim(userPrincipal, "FullName", "test.test");

            context.ShouldRenew = true;

             context.ReplacePrincipal(userPrincipal);

            var s = ClaimsExtension.GetClaimValue(userPrincipal, "FullName");

            var r = ClaimsExtension.GetClaimValue(userPrincipal, ClaimTypes.Email);

            
        }
    }
}
