using AutoMapper;
using Demo.Api.Dtos.Base;
using Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Dtos
{
    public class StudentSummaryDto : BaseDto
    {
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmantId { get; set; }
    }

    public class StudentSummartyDtoProfile : Profile
    {
        public StudentSummartyDtoProfile()
        {
            CreateMap<Student, StudentSummaryDto>();
        }
    }
}
