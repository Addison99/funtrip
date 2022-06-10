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
    public class AreaGroupRepository : IAreaGroupRepository
    {
        public void Create(AreaGroup AreaGroup) => AreaGroupDAO.Instance.Create(AreaGroup);

        public void Delete(int id)
        {
            AreaGroup area = Get(id);
            area.Status = "Inactive";
            Update(area);   
        }

        public AreaGroup Get(int id) => AreaGroupDAO.Instance.Get(x => x.Id == id);

        public IEnumerable<AreaGroup> GetList(Expression<Func<AreaGroup, bool>> func) => AreaGroupDAO.Instance.GetAll(func);

        public void Update(AreaGroup AreaGroup)=> AreaGroupDAO.Instance.Update(AreaGroup);
    }
}
