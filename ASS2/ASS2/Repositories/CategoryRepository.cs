
using ASS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASS2.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyStoreContext _context;

        public CategoryRepository(MyStoreContext context)
        {
            _context = context;
        }
        public int Add(Category category)
        {
            _context.Categories.Add(category);
            int result = _context.SaveChanges();
            return result;
        }

        public int Delete(int id)
        {
            _context.Categories.Remove(FindById(id));
            int result = _context.SaveChanges();
            return result;
        }

        public Category FindById(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            return category;
        }

        public IList<Category> FindAll()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public int Update(Category category)
        {
            _context.Categories.Update(category);
            int result = _context.SaveChanges();
            return result;
        }
    }
}
