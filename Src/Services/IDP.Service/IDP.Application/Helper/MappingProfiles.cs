using AutoMapper;
using IDP.Application.Command.Auth;
using IDP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, LoginMobileUser>().ReverseMap();
        }
    }
}
