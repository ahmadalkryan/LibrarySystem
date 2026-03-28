using APIApplication.Dtos.Member;
using Application.Dtos.Member;
using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IMemberService
    {

        Task<MemberDto> CreateMember(CreateMemberDto createMemberDto);

        Task<MemberDto> UpdateMember(UpdateMemberDto updateMemberDto);

        Task<MemberDto> GetMemberById(int id);
        Task<MemberDto> GetMemberByCode(string memberCode);
        Task<IEnumerable<MemberDto>> GetMemberByType(string memberType);


        Task<bool> DeleteMember(int id);

        Task<IEnumerable<MemberDto>> GetAllMembers();
    }
}
