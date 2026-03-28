using Application.Dtos.BorrowingRecord;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class BorrowingRecordProfile:Profile
    {
        public BorrowingRecordProfile()
        {
            CreateMap<BorrowingRecord, BorrowingRecordDto>().ForMember(des =>des.MemberFullName, opt => opt.MapFrom(src => src._member.FullName));

            CreateMap<CreateBorrowingRecordDto, BorrowingRecord>()
                .ForMember(dest => dest.TransactionNumber, opt => opt.MapFrom(src => GenerateTransactionNumber())).
                ForMember(dest => dest.BorrowDate, opt => opt.MapFrom(src => DateTime.Now)).
                ForMember(dest => dest.ReturnDate, opt =>opt.Ignore());



            CreateMap<UpdateBorrowingRecordDto, BorrowingRecord>().ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src=>DateTime.Now));
        }


        private string GenerateTransactionNumber()
        {
            return $"BRW-{DateTime.Now:yyyyMMdd-HHmmss}-{Guid.NewGuid():N}".Substring(0, 20);
        }
    }
}
