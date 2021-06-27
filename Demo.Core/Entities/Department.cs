using Demo.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Student> Students { get; set; }
    }
}
