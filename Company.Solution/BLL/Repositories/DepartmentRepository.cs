using BLL.Interfaces;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
	public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
	{
        public DepartmentRepository(CompanyDbContext dbContext) :base(dbContext)
        {
            
        }
    }
}
