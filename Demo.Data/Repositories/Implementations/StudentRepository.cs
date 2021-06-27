using Demo.Core.Entities;
using Demo.Data.DatabaseContext;
using Demo.Data.Repositories.Base;
using Demo.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Data.Repositories.Implementations
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(DemoContext context) : base(context)
        {

        }

        public Student AddStudent(Student student)
        {
            _set.Add(student);
            _context.SaveChanges();
            return student;
        }

        public void DeleteStudent(int id)
        {
            _set.Remove(GetStudentById(id));
            _context.SaveChanges();
        }

        public Student GetStudentById(int id)
        {
            return _set.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Student> GetStudents()
        {
            return _set.ToList();
        }

        public Student UpdateStudent(Student student)
        {
            var _student = GetStudentById(student.Id);
            _student.PlaceOfBirth = student.PlaceOfBirth;
            _student.BirthDate = student.BirthDate;
            _student.DepartmantId = student.DepartmantId;
            _student.FirstName = student.FirstName;
            _student.LastName = student.LastName;
            _student.StudentNumber = student.StudentNumber;
            _context.SaveChanges();
            return student;
        }
    }
}
