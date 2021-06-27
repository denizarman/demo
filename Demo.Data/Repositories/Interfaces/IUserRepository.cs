using Demo.Core.Entities;
using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByLoginInfo(LoginRequestModel loginRequest);
        Task<User> GetUserById(int id);
    }
}
