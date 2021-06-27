using Demo.Core.Entities;
using Demo.Core.Models;
using Demo.Data.DatabaseContext;
using Demo.Data.Repositories.Base;
using Demo.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DemoContext context) : base(context)
        {
        }

        public Task<User> GetUserByLoginInfo(LoginRequestModel loginRequest)
        {
            return _set.Where(x => x.Username == loginRequest.Username && x.Password == loginRequest.Password).Include(x => x.Roles).FirstOrDefaultAsync();
        }

        public Task<User> GetUserById(int id)
        {
            return _set.Where(x => x.Id == id).Include(x => x.Roles).FirstOrDefaultAsync();
        }
    }
}
