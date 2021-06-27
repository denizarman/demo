using Demo.Core.Entities;
using Demo.Data.Repositories.Interfaces;
using Demo.Service.Services.Base;
using Demo.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Service.Services.Implementations
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public Student AddStudent(Student student)
        {
            _studentRepository.AddStudent(student);
            return student;
        }

        public void DeleteStudent(int id)
        {
            _studentRepository.DeleteStudent(id);
        }

        public Student GetStudentById(int id)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
                throw new Exception("User not found !!!");
            return student;
        }

        public List<Student> GetStudents()
        {
            return _studentRepository.GetStudents();
        }

        public Student UpdateStudent(Student student)
        {
            return _studentRepository.UpdateStudent(student);
        }
    }
}
