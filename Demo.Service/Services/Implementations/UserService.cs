using Demo.Core.Config;
using Demo.Core.Entities;
using Demo.Core.Models;
using Demo.Data.Repositories.Interfaces;
using Demo.Service.Services.Base;
using Demo.Service.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        #endregion

        #region Ctor

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        #endregion

        #region Public Methods

        public async Task<LoginResponseModel> Authenticate(LoginRequestModel loginRequest)
        {
            var user = await _userRepository.GetUserByLoginInfo(loginRequest);
            if (user is null)
            {
                throw new Exception("User not found !!!");
            }
            var token = generateJwtToken(user);

            return new LoginResponseModel(user, token);
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null)
            {
                throw new Exception("User not found !!!");
            }

            return user;
        }

        #endregion

        #region Private Methods

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecret);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("user_id", user.Id.ToString()));
            user.Roles.ForEach(x => claims.AddClaim(new Claim("role", x.Name)));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
