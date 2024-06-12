using ASS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASS2.Repositories
{
    public interface IProductRepository
    {
        Product FindById(int id);
        IList<Product> FindAll();
        IList<Product> SearchByName(string searchText);
        int Add(Product product);
        int Update(Product product);
        int Delete(int id);
        List<Product> GetByCategory(int categoryId);
    }
}
