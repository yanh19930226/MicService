using AutoMapper;
using MicService.Abstractions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<Models.Domain.User, UserDto>()
                .ForMember(p => p.UserName, src => src.MapFrom(p => p.Name));
        }
    }
}
