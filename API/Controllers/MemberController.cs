using APIApplication.Dtos.Member;
using Application.Dtos.Action;
using Application.Dtos.BorrowingRecord;
using Application.Dtos.Member;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        private readonly IJsonFieldsSerializer _jsonFieldsSerializer ;
        private readonly IMemberService _memberService ;

        public MemberController(IMemberService memberService ,IJsonFieldsSerializer jsonFieldsSerializer )
        { 
            _jsonFieldsSerializer= jsonFieldsSerializer ;
            _memberService = memberService ;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MemberDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMembers()
        {
            var result = await _memberService.GetAllMembers();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
  new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MemberDto>), StatusCodes.Status200OK)]
        public async Task <IActionResult > CreateMember(CreateMemberDto createMemberDto)
        {
            var result = await _memberService.CreateMember(createMemberDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<MemberDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMember(UpdateMemberDto updateMemberDto)
        {
            var result= await _memberService.UpdateMember(updateMemberDto);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result =await _memberService.DeleteMember(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<MemberDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult > GetMemberById(int id)
        {
            var result = await _memberService.GetMemberById(id);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MemberDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> getMembersByType(string Type)
        {
            var result = await _memberService.GetMemberByType(Type);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<MemberDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> getMemberByCode(string code)
        {
            var result = await _memberService.GetMemberByCode(code);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }
    }
}
