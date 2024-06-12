
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASS2.Models;
using ASS2.Repositories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASS2.Pages
{

    public class ReportModel : PageModel
    {
        private readonly RepositoryManager _repo;

        public ReportModel(RepositoryManager repo)
        {
            _repo = repo;
        }

        public IList<Order> Order { get; set; } = new List<Order>();
        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDate { get; set; }
        public DateTime? ToDate { get; set; }

        public async Task OnGetAsync()
        {
            
            if (FilterDate == null)
            {
                FilterDate = DateTime.Now.Date.AddDays(-30);
                ToDate = DateTime.Now.Date;
            }

            await LoadOrderListAsync(FilterDate.Value, ToDate.Value);
        }

        private async Task LoadOrderListAsync(DateTime fromDate, DateTime toDate)
        {
            Order =  _repo.OrderRepository.ReportOrders(fromDate , toDate);
        }
    }
}

/*public async Task Filter()
       {
           if(_staff.Role == 1)
           {
               Order = await _context.Orders
           .Where(o => o.OrderDate.Date >= FilterDate && o.OrderDate.Date <= toDate)
           .Include(o => o.Staff)
           .ToListAsync();
           }
           else
           {
               Order = await _context.Orders
                   .Where(o => o.StaffId == _staff.StaffId)
                   .ToListAsync();
           }
       }
       */