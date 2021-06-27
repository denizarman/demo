using AutoMapper;
using Demo.Api.Dtos.Base;
using Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Dtos
{
    public class StudentDetailDto : BaseDto
    {
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public int DepartmantId { get; set; }
    }

    public class StudentToStudentDetailDtoProfile : Profile
    {
        public StudentToStudentDetailDtoProfile()
        {
            CreateMap<Student, StudentDetailDto>();
        }
    }

    public class StudentDetailDtoToStudentProfile : Profile
    {
        public StudentDetailDtoToStudentProfile()
        {
            CreateMap<StudentDetailDto, Student>();
        }
    }
}
