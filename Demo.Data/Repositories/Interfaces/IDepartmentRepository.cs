using Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department AddDepartment(Department deptartment);
    }
}
