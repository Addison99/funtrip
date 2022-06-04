using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface ICategoryRepository
    {
        void Create(Category Category);
        void Update(Category Category);
        void Delete(int id);
        Category Get(int id);
        IEnumerable<Category> GetList(Expression<Func<Category, bool>> func);
    }
}
