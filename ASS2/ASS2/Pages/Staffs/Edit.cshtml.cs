using System.Threading.Tasks;
using ASS2.Models;
using ASS2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASS2.Pages
{
    public class EditModel : PageModel
    {
        private readonly RepositoryManager _repo;

        public EditModel(RepositoryManager repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync(int staffId)
        {
            Staff = await _repo.StaffRepository.GetByIdAsync(staffId);

            if (Staff == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updated = await _repo.StaffRepository.UpdateAsync(Staff);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToPage("/Staffs/Index");
        }
    }
}
