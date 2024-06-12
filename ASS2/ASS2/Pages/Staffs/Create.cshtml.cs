using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASS2.Models;
using ASS2.Repositories;
using System.Threading.Tasks;
namespace ASS2.Pages
{
    public class CreateStaffModel : PageModel
    {
        private readonly RepositoryManager _repo;

        public CreateStaffModel(RepositoryManager repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public Staff Staff { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Staff.Role = 1;
            _repo.StaffRepository.Add(Staff);
            await _repo.StaffRepository.SaveAsync();

            return RedirectToPage("/Staffs/Index");
        }
    }
}
