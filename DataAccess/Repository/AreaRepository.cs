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
    public class AreaRepository : IAreaRepository
    {
        public void Create(Area Area)=> AreaDAO.Instance.Create(Area);
        public void Delete(int id)
        {
            Area area = Get(id);
            area.Status = "Inactive";
            AreaDAO.Instance.Update(area);
        }
        private void AddRelations(Area area)
        {
            if (area.District==null) area.District = DistrictDAO.Instance.Get(x => x.Id == area.DistrictId);
            if (area.AreaGroups==null) area.AreaGroups = AreaGroupDAO.Instance.GetAll(x => x.AreaId == area.Id).ToList();
        }
        public Area Get(int id)
        {
            Area area = AreaDAO.Instance.Get(x => x.Id == id);
            if (area!=null) AddRelations(area);
            return area;
        }

        public IEnumerable<Area> GetList(Expression<Func<Area, bool>> func)
        {
            IEnumerable<Area> areas = AreaDAO.Instance.GetAll(func);
            foreach(Area area in areas) AddRelations(area);
            return areas;
        }

        public void Update(Area Area)=> AreaDAO.Instance.Update(Area);

    }
}
