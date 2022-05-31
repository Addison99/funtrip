using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.IRepository;
using BusinessObject.Models;
using DataAccess.DAO;
namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public User CheckLogin(string username, string password) 
            => UserDAO.Instance.Get(x=> x.Username == username && x.Password == password); 

        public User CheckLoginByMail(string mail)=>UserDAO.Instance.Get(x=> x.Gmail == mail);

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
