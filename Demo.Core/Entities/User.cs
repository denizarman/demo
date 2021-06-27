using Demo.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Role> Roles { get; set; }
    }
}
