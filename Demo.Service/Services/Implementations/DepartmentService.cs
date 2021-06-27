using Demo.Core.Entities;
using Demo.Data.Repositories.Interfaces;
using Demo.Service.Services.Base;
using Demo.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service.Services.Implementations
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Department AddDepartment(Department department)
        {
            _departmentRepository.AddDepartment(department);
            return department;
        }

        public IEnumerable<Department> GetDepartments()
        {
            var departments =  _departmentRepository.GetDepartments();
            return departments;
        }
    }
}
