using BLL.Interfaces;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext _dbContext;

        public GenericRepository(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await _dbContext.Employees.Include(E => E.department).ToListAsync();
            }
            else
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _dbContext.Employees
                                 .Include(E => E.department)
                                 .FirstOrDefaultAsync(E => E.Id == id) as T;
            }

            return await _dbContext.Set<T>().FindAsync(id);
        }


        public void Update(T entity)
        {
             _dbContext.Set<T>().Update(entity);
        }
    }
}
