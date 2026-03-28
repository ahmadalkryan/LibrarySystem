using Application.Dtos.Author;
using Application.Dtos.Wallet;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class WalletProfile:Profile
    {
        public WalletProfile()
        {
            CreateMap<CreateWalletDto, Wallet>().ForMember(des => des.CreatedAt, opt => opt.MapFrom(x => DateTime.Now))

              
               .ForMember
                (des => des.IsActive, opt => opt.MapFrom(x => true));
               



            CreateMap<UpdateWalletDto , Wallet>();

            CreateMap<Wallet, WalletDto>().ForMember(des => des.MemberName, opt => opt.MapFrom(src => src._member.FullName));
        }
    }
}
