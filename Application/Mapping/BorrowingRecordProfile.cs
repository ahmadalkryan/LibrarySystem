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
            CreateMap<BorrowingRecord, BorrowingRecordDto>();
            CreateMap<CreateBorrowingRecordDto, BorrowingRecord>().ForMember(dest=>dest.TransactionNumber ,opt=>opt.MapFrom(src=>Guid.NewGuid().ToString())).
                ForMember(dest=>dest.BorrowDate ,opt=>opt.MapFrom(src=>DateTime.Now));
            CreateMap<UpdateBorrowingRecordDto, BorrowingRecord>().ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src=>DateTime.Now));
        }
    }
}
