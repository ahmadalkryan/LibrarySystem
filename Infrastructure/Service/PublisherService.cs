using Application.Dtos.Publisher;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepository<Publisher> _repostory;

        private readonly IMapper _mapper;

        public PublisherService(IRepository<Publisher> repository ,IMapper mapper)
        {
            _mapper=mapper;
            _repostory=repository;
        }


        public async Task<PublisherDto> CreatePublisher(CreatePublisherDto publisherDto)
        {   
            var  pub  = _mapper.Map<Publisher>(publisherDto);

            await _repostory.AddAsync(pub);

            return _mapper.Map<PublisherDto>(pub);
            
        }


        public async Task<bool> DeletePublisher(int id)
        {
           await _repostory.DeleteAsync(id);

            return true;
        }

        public async Task<IEnumerable<PublisherDto>> GetAllPublishers()
        {
            return _mapper.Map<IEnumerable<PublisherDto>>(await _repostory.GetAllAsync());
        }

        public async Task<PublisherDto> UpdatePublisher(UpdatePublisherDto publisherDto)
        {
            var pub = _mapper.Map<Publisher>(publisherDto);
                     await _repostory.UpdateAsync(pub);
            return _mapper.Map<PublisherDto>(publisherDto);

        }

       public async   Task<PublisherDto> GetPublisherById(int id)
        { 
             var pub = await _repostory.GetByIdAsync(id);
            return _mapper.Map<PublisherDto>(pub);
        }
    }
}
