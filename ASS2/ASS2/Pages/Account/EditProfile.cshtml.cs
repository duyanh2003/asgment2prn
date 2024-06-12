using System.Threading.Tasks;
using ASS2.Models;
using ASS2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ASS2.Pages
{
    using System.Security.Claims;


    public class EditProfileModel : PageModel
    {
        private readonly RepositoryManager _repo;

        public EditProfileModel(RepositoryManager repo)
        {
            _repo = repo;
            Staff = new Staff(); 
        }

        [BindProperty]
        public Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            System.Diagnostics.Debug.WriteLine($"IsAuthenticated: {User?.Identity?.IsAuthenticated}");
            System.Diagnostics.Debug.WriteLine($"StaffId: {User?.FindFirstValue(ClaimTypes.NameIdentifier)}");

            var staffIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(staffIdStr))
            {
                return RedirectToPage("/Login");
            }

            if (int.TryParse(staffIdStr, out var staffId))
            {
                Staff = await _repo.StaffRepository.GetByIdAsync(staffId);

                if (Staff == null)
                {
                    return NotFound();
                }
            }
            else
            {

                Staff = new Staff { Name = staffIdStr };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var staffIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(staffIdStr))
            {
                return RedirectToPage("/Login");
            }

            if (int.TryParse(staffIdStr, out var staffId))
            {
                Staff.StaffId = staffId; 

                var updated = await _repo.StaffRepository.UpdateAsync(Staff);

                if (!updated)
                {
                    return NotFound();
                }
            }
            return RedirectToPage("/Account/EditProfile");


        }
    }
}
