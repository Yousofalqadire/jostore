using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Models;
using AutoMapper;

namespace Api.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser,MemberDto>()
            .ForMember(m => m.FullName,opt => opt.MapFrom(x => x.FullName))
            .ForMember(m => m.UserName , opt => opt.MapFrom(x => x.UserName))
            .ForMember(m => m.Id,opt => opt.MapFrom(x => x.Id))
            .ForMember(m => m.PhoneNumber , opt => opt.MapFrom(x => x.PhoneNumber))
            .ForMember(m => m.Age ,opt => opt.MapFrom(x => x.Age))
            .ForMember(m => m.Country,opt => opt.MapFrom(x => x.Country));
        }
    }
}