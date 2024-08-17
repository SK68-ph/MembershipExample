using AutoMapper;
using MembershipExample.Application.DTOs;
using MembershipExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Membership, MembershipDto>();
            CreateMap<Plan, PlanDto>();
        }
    }

}
