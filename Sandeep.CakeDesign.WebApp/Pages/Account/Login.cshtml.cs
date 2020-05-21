using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sandeep.CakeDesign.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public IActionResult OnPost()
        {
            var isValidUser = (EmailAddress == "abc@gmail.com") && (Password == "abc@123");

            if (!isValidUser)
            {
                ModelState.AddModelError("", "Invalid username or password");
            }

            if (!ModelState.IsValid) return Page();

            const string scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, EmailAddress)},
                scheme));

            return SignIn(user, null,scheme);

        }

        public async Task<IActionResult> OnPostSignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage(("/Index"));

        }
    }
}