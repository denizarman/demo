using Demo.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Entities
{
    public class Student : BaseEntity
    {
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public int DepartmantId { get; set; }
    }
}
