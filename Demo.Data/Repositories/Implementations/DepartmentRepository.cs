using Demo.Core.Entities;
using Demo.Data.DatabaseContext;
using Demo.Data.Repositories.Base;
using Demo.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories.Implementations
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DemoContext context) : base(context)
        {

        }

        public Department AddDepartment(Department department)
        {
            _set.Add(department);
            _context.SaveChanges();
            return department;
        }

        public IEnumerable<Department> GetDepartments()
        {
            var res = _set.ToList();
            return _set.ToList();
        }
    }
}
