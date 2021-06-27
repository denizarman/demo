using Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service.Services.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetDepartments();
        Department AddDepartment(Department department);
    }
}
