using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Linq;
using System;

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
            var users = new[]
            {
                new { Username = "demo", Password = "demo", Role = Constants.USER_ROLE},
                new { Username = "admin", Password = "demo", Role = Constants.ADMIN_ROLE}
            };

            var user = users.SingleOrDefault(x => x.Username.Equals(Username, StringComparison.CurrentCultureIgnoreCase));

            if (user != null && user.Password.Equals(Password))
            {
                var claims = new List<Claim>
                {
                    new Claim("user", user.Username),
                    new Claim("role", user.Role)
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
