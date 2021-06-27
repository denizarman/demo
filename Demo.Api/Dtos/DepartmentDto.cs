using AutoMapper;
using Demo.Api.Dtos.Base;
using Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Dtos
{
    public class DepartmentDto : BaseDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class DepartmentToDepartmentDtoProfile : Profile
    {
        public DepartmentToDepartmentDtoProfile()
        {
            CreateMap<Department, DepartmentDto>();
        }
    }

    public class DepartmentDtoToDepartmentProfile : Profile
    {
        public DepartmentDtoToDepartmentProfile()
        {
            CreateMap<DepartmentDto, Department>();
        }
    }
}
