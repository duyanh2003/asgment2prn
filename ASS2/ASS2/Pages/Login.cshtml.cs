using ASS2.Models;
using ASS2.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace ASS2.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IConfiguration _configuration;

        public LoginModel(IStaffRepository staffRepository, IConfiguration configuration)
        {
            _staffRepository = staffRepository;
            _configuration = configuration;
            Staff = new Staff(); // Initialize Staff in the constructor
        }

        [BindProperty]
        public Staff Staff { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var defaultName = _configuration["Account:Name"];
            var defaultPassword = _configuration["Account:Password"];

            if (Staff.Name == defaultName && Staff.Password == defaultPassword)
            {
                var defaultClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, defaultName), // Add this line
                    new Claim(ClaimTypes.Name, defaultName),
                    new Claim(ClaimTypes.Role, "1") // Change the admin role to "1"
                };

                var defaultClaimsIdentity = new ClaimsIdentity(
                    defaultClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(defaultClaimsIdentity));

                return LocalRedirect(Url.Content("~/Index"));
            }

            var staff = _staffRepository.Login(Staff.Name, Staff.Password);

            if (staff == null)
            {
                ModelState.AddModelError(string.Empty, "Wrong Password or Name");
                return Page();
            }

            var staffClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, staff.StaffId.ToString()), // Add this line
                new Claim(ClaimTypes.Name, staff.Name),
                new Claim(ClaimTypes.Role, staff.Role.ToString())
            };

            System.Diagnostics.Debug.WriteLine($"StaffId: {staff.StaffId}");

            var staffClaimsIdentity = new ClaimsIdentity(
                staffClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(staffClaimsIdentity));

            return LocalRedirect(Url.Content("~/Index"));
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Login");
        }
    }
}
