using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Samples.Authentication.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() 
        {

            if (Username.Equals("demo") && Password.Equals("demo"))
            {
                var claims = new List<Claim>
                {
                    new Claim("user", Username),
                    new Claim("role", "Member")
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("AccessDenied");
            }
        }
    }
}
