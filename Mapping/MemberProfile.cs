using APIApplication.Dtos.Member;
using Application.Dtos.Category;
using Application.Dtos.Member;
using AutoMapper;
using Domain.Entities;
using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
     public class MemberProfile:Profile
    {
        public MemberProfile()
        {
            CreateMap<Domain.Entities.Member, MemberDto>();
            CreateMap<CreateMemberDto ,Domain.Entities.Member>();
            CreateMap<UpdateMemberDto,Domain.Entities.Member>();
        }

    }
}
