
using ASS2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASS2.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyStoreContext _context;
        public OrderRepository(MyStoreContext context)
        {
            _context = context;
        }

        public int Add(Order order)
        {
            _context.Orders.Add(order);
            _context.OrderDetails.AddRange(order.OrderDetails);
            if (order.OrderDetails.Count() != 0)
            {
                int result = _context.SaveChanges();
                return result;
            }
            return 0;
        }

        public int Delete(int id)
        {
            _context.Orders.Remove(FindById(id));
            int result = _context.SaveChanges();
            return result;
        }

        public Order FindById(int id)
        {
            var order = _context.Orders
           .FirstOrDefault(o => o.OrderId == id);
            return order;
        }

        public IList<Order> FindAll()
        {
            List<Order> orders = _context.Orders.Include(o => o.Staff).ToList();
            return orders;
        }

        public int Update(Order order)
        {
            _context.Orders.Update(order);
            int result = _context.SaveChanges();
            return result;
        }
        public List<Order> GetOrderByDate(DateTime date)
        {
            List<Order> orders = _context.Orders.Where(o=>o.OrderDate.Date == date.Date).ToList();
            return orders;
        }
        public List<OrderDetail> GetOrderDetail(int? orderId)
        {
            List<OrderDetail> orderDetails = _context.OrderDetails.Include(od => od.Product).Where(od => od.OrderId == orderId).ToList();
            return orderDetails;
        }
        public  List<Order> ReportOrders(DateTime form, DateTime to)
        {
            List<Order> orders = _context.Orders.Where(o => o.OrderDate.Date>= form.Date && o.OrderDate.Date<= to.Date).ToList();
            return orders;
        }
       
    }
}
