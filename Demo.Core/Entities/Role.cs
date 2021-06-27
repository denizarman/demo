using Demo.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
