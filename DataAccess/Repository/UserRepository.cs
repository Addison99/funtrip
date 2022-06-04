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
    public class UserRepository : IUserRepository
    {
        public User CheckLogin(string username, string password) 
            => UserDAO.Instance.Get(x=> x.Username == username && x.Password == password); 

        public User CheckLoginByMail(string mail)=>UserDAO.Instance.Get(x=> x.Gmail == mail);

        public void Create(User User)=>UserDAO.Instance.Create(User);   

        public void Delete(int id)
        {
        }

        public User Get(int id)
        {
            return UserDAO.Instance.Get(x=>x.Id == id);
        }

        public IEnumerable<User> GetList(Expression<Func<User, bool>> func)
        {
            return UserDAO.Instance.GetAll(func);
        }

        public void Update(User User)
        {
            UserDAO.Instance.Update(User);
        }
    }
}
