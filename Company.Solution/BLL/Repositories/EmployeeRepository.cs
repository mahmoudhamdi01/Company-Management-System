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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _dbContext;

        public EmployeeRepository(CompanyDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmpByName(string name)
        {
            return _dbContext.Employees.Include(e => e.department)
                                       .Where(e => e.Name.ToLower()
                                       .Contains(name.ToLower()))
                                       .ToList().AsQueryable();
        }
    }
}
