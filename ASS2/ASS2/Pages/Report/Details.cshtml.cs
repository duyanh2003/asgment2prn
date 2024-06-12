using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASS2.Models;
using ASS2.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASS2.Pages

    {
        public class DetailsModel : PageModel
        {
            private readonly ASS2.Models.MyStoreContext _context;
            private readonly RepositoryManager _repo;
            private int _orderId; 

            public DetailsModel(ASS2.Models.MyStoreContext context, RepositoryManager repo)
            {
                _context = context;
                _repo = repo;
            }

            public List<OrderDetail> GetOrderDetail { get; set; } = new List<OrderDetail>();


        public async Task<IActionResult> OnGetAsync(int? id)
        {
          
            if (id == null)
            {
                return NotFound(); 
            }

            

           
            var order = await _context.Orders.FindAsync(id);

           
            if (order == null)
            {
                return NotFound(); 
            }

            
             LoadOrderDetailAsync(id);

            return Page();
        }



        private async Task LoadOrderDetailAsync(int ?id)
        {

            var orderDetails = await Task.Run(() => _repo.OrderRepository.GetOrderDetail((int)id));
            GetOrderDetail = orderDetails.ToList();
        }
    }
    }


/* public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
            }
            return Page();
        }
       */