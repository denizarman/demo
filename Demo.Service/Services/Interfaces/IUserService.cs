using Demo.Core.Entities;
using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseModel> Authenticate(LoginRequestModel loginRequest);
        Task<User> GetUserById(int id);
    }
}
