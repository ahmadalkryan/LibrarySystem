using Application.Dtos.Author;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<CreateAuthorDto , Author>();
            CreateMap<UpdateAuthorDto , Author>();
            CreateMap<Author , AuthorDto>();
        }
    }
}
