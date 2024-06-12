using ASS2.Models;

namespace ASS2.Repositories
{
    public class RepositoryManager
    {
        private MyStoreContext _context;
        public ICategoryRepository CategoryRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IStaffRepository StaffRepository { get; set; }
        public RepositoryManager()
        {
            _context = new MyStoreContext();
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context);
            OrderRepository = new OrderRepository(_context);
            StaffRepository = new StaffRepository(_context);
        }
    }
}
