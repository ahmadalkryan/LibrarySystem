using Application.Dtos.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IPublisherService
    {

        Task<PublisherDto> CreatePublisher(CreatePublisherDto publisherDto);
        Task<PublisherDto> GetPublisherById(int id);

        Task<PublisherDto> UpdatePublisher(UpdatePublisherDto publisherDto);

        Task<bool> DeletePublisher(int id);

        Task<IEnumerable<PublisherDto>> GetAllPublishers();
    }
}
