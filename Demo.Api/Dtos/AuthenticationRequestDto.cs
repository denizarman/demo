using AutoMapper;
using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Dtos
{
    public class AuthenticationRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationRequestDtoToLoginRequestProfile : Profile
    {
        public AuthenticationRequestDtoToLoginRequestProfile()
        {
            CreateMap<AuthenticationRequestDto, LoginRequestModel>();
        }
    }
}
