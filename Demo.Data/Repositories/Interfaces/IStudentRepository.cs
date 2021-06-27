using Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Data.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetStudents();
        Student GetStudentById(int id);
        Student AddStudent(Student student);
        Student UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
