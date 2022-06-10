using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IAreaGroupRepository
    {
        void Create(AreaGroup AreaGroup);
        void Update(AreaGroup AreaGroup);
        void Delete(int id);
        AreaGroup Get(int id);
        IEnumerable<AreaGroup> GetList(Expression<Func<AreaGroup, bool>> func);
    }
}
