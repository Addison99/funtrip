using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class EntityDAO<T> where T : class
    {

        //Single-ton pattern
        private static EntityDAO<T> instance = null;
        private static readonly object instanceLock = new object();
        private readonly FunTripContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public EntityDAO(FunTripContext dBContext)
        {
            _dbContext = dBContext;
            _dbSet = dBContext.Set<T>();
        }

        public static EntityDAO<T> Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EntityDAO<T>(new FunTripContext());
                    }
                    return instance;
                }
            }
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            if (expression == null) return _dbSet;
            else
                return _dbSet.Where(expression);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).FirstOrDefault();
        }


        public void Create(T entity)
        {
            if (entity != null)
            {
                _dbSet.Add(entity);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
        }


        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }


    }
}
