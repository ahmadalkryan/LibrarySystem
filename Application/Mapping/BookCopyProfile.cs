using Application.Dtos.Author;
using Application.Dtos.BookCopy;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class BookCopyProfile:Profile
    {
        public BookCopyProfile()
        {

            CreateMap<BookCopy, BookCopyDto>().ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src._book.Title));
            CreateMap<CreateBookCopyDto, BookCopy>().ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.Now)).
                ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Available"));
            CreateMap<UpdateBookCopyDto, BookCopy>();
            
        }
    }
}
