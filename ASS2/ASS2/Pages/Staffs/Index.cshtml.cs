using ASS2.Models;
using ASS2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASS2.Pages
{
    public class StaffModel : PageModel
    {
        private readonly RepositoryManager _repo;
        public StaffModel(RepositoryManager repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public List<Staff> StaffList { get; set; }

        [BindProperty]
        public Staff Staff { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            await LoadStaffListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int staffId)
        {
            int result = _repo.StaffRepository.Delete(staffId);
            if (result > 0)
            {
                Message = "Deleted successfully.";
                await LoadStaffListAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadStaffListAsync();
            }
            else
            {
                StaffList = (List<Staff>)_repo.StaffRepository.FindByName(SearchText);
            }
            return Page();
        }

        private async Task LoadStaffListAsync()
        {
            StaffList = (List<Staff>)_repo.StaffRepository.FindAll();
        }
    }
}
