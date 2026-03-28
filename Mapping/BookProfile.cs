using Application.Dtos.Book;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ForMember(x=>x.AuthorName, opt=>opt.MapFrom(src=>src._author.Name)).ForMember(des =>des.UserName , opt=>opt.MapFrom(src=>src._user.Username))
                                       .ForMember(x=>x.PublisherName, opt=>opt.MapFrom(src=>src._publisher.Name))
                                       .ForMember(x=>x.CategoryName, opt=>opt.MapFrom(src=>src._category.Name));


            CreateMap<CreateBookDto, Book>();
                //.ForMember(des=>des.ISBN, opt=>opt.MapFrom(opt=>Guid.NewGuid().ToString()));
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
