
using ASS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASS2.Repositories
{
    public interface IStaffRepository
    {
        Staff FindById(int id);
        Staff Login(string name, string password);
        IList<Staff> FindAll();
        int Add(Staff staff);
        int Update(Staff staff);
        int Delete(int id);
        IList<Staff> FindByName(string name);
        bool IsNameExists(string name);
        Task<Staff> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Staff staff);
        Task SaveAsync();
    }
}
