using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.DAO;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Create(Category Category)=> CategoryDAO.Instance.Create(Category);

        public void Delete(int id)
        {
            Category cate = Get(id);
            cate.Status = "Inactive";
            Update(cate);
        }
        void AddRelations(Category category)
        {
            if(category.Vehicles==null) category.Vehicles = VehicleDAO.Instance.GetAll(x => x.CategoryId == category.Id).ToList();
        }
        public Category Get(int id)
        {
            Category category = CategoryDAO.Instance.Get(x => x.Id == id);
            if (category!=null) AddRelations(category);
            return category;
        }

        public IEnumerable<Category> GetList(Expression<Func<Category, bool>> func)
        {
            IEnumerable<Category> categories = CategoryDAO.Instance.GetAll(func);
            foreach(Category category in categories) AddRelations(category);
            return categories;
        }

        public void Update(Category Category)=> CategoryDAO.Instance.Update(Category);
    }
}
