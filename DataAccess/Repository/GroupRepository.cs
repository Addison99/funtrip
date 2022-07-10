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
    public class GroupRepository : IGroupRepository
    {
        public void Create(Group Group)=> GroupDAO.Instance.Create(Group);
        public void Delete(int id)
        {
            Group group = Get(id);
            group.Status = "Inactive";
            Update(group);
        }
        void AddRelations(Group group)
        {
            if (group == null) return;
            if (group.AreaGroups.Count == 0) group.AreaGroups = AreaGroupDAO.Instance.GetAll(x => x.GroupId == group.Id).ToList();
            if (group.Drivers.Count == 0) group.Drivers = DriverDAO.Instance.GetAll(x=> x.GroupId == group.Id).ToList();
        }
        public Group Get(int id)
        {
            Group group = GroupDAO.Instance.Get(x => x.Id == id);
            AddRelations(group);
            return group;
        }

        public IEnumerable<Group> GetList(Expression<Func<Group, bool>> func)
        {
            IEnumerable<Group> groups = GroupDAO.Instance.GetAll(func);
            foreach (Group group in groups) AddRelations(group);
            return groups;
        }

        public void Update(Group Group)=>GroupDAO.Instance.Update(Group);
    }
}
