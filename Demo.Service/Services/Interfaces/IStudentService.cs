using Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Service.Services.Interfaces
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        Student GetStudentById(int id);
        Student AddStudent(Student student);
        Student UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
