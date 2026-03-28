using Application.Dtos;
using Application.Dtos.WalletTransaction;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class WalletTransactionProfile:Profile
    {
        public WalletTransactionProfile()
        {
            CreateMap<WalletTransaction, WalletTransactionDto>()
                .ForMember(des => des.MemberName, opt => opt.MapFrom(src => src._member.FullName));

            CreateMap<CreateWalletTransactionDto, WalletTransaction>()
                .ForMember(des => des.CreatedAt, opt => opt.MapFrom(x => DateTime.Now)).
                ForMember(des => des.Status , opt => opt.MapFrom(x=> "Completed")).
                ForMember(des => des.TransactionNumber, opt=> opt.MapFrom(src => GenerateTransaction(src._memberId)));



           //     .ForMember(des => des.CreatedAt, opt => opt.MapFrom(x => GenerateTransaction(x._memberId)));

        }
      
       private string GenerateTransaction( int memberId)
        {
            return $"TRANS-{DateTime.Now:yyyyMMdd-HHmmss}--{Guid.NewGuid()}-{memberId}-" .Substring (0, 20);
        }
    }
}
