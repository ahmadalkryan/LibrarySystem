using Application.Dtos.Member;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIApplication.Dtos.Member;

namespace Infrastructure
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Domain.Entities.Member> repo;
        private readonly IMapper _mapper;
        public MemberService(IMapper mapper, IRepository<Domain.Entities.Member> repository)
        {

            _mapper = mapper;
            repo = repository;
            
        }

        public async Task<MemberDto> CreateMember(CreateMemberDto createMemberDto)
        {
           var member = _mapper.Map<Domain.Entities.Member>(createMemberDto);
            await repo.AddAsync(member);
            return _mapper.Map<MemberDto>(member);
        }


        public async Task<bool> DeleteMember(int id)
        {
           await repo.DeleteAsync(id);

            return true;
        }

        public async Task<IEnumerable<MemberDto>> GetAllMembers()
        {
            return _mapper.Map<IEnumerable<MemberDto>>(await repo.GetAllAsync());

        }

        public async Task<MemberDto> UpdateMember(UpdateMemberDto updateMemberDto)
        {
            var mb = _mapper.Map<Domain.Entities.Member>(updateMemberDto);

            await repo.UpdateAsync(mb);

            return _mapper.Map<MemberDto>(mb);

        }

        public async Task<MemberDto> GetMemberById(int id)
        {
           var mbr = await repo.GetByIdAsync(id);
            return _mapper.Map<MemberDto>(mbr);
        }

       public async  Task<MemberDto> GetMemberByCode(string memberCode)
        {
             var mbr = await repo.GetAllWitAllIncludeAsync(x=>x.MemberCode == memberCode);
            return _mapper.Map<MemberDto>(mbr);
        }

        Task<IEnumerable<MemberDto>> IMemberService.GetMemberByType(string memberType)
        {
           var mbr =  repo.GetAllWitAllIncludeAsync(x => x.MembershipType == memberType);
            return _mapper.Map<Task<IEnumerable<MemberDto>>>(mbr);
        }
    }
}
