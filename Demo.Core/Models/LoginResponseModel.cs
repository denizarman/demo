using Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Core.Models
{
    public class LoginResponseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }

        public LoginResponseModel(User user, string token)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.Password = user.Password;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Roles = user.Roles.Select(x => x.Name).ToList();
            this.Token = token;
        }
    }
}
