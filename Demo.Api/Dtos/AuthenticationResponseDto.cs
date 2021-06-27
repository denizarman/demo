using AutoMapper;
using Demo.Api.Dtos.Base;
using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Dtos
{
    public class AuthenticationResponseDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }

    public class LoginResponseToAuthenticationResponseDtoProfile : Profile
    {
        public LoginResponseToAuthenticationResponseDtoProfile()
        {
            CreateMap<LoginResponseModel, AuthenticationResponseDto>();
        }
    }
}
