using Application.Dtos.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public  interface IUserService
    {

        Task<UserDto> Register(RegisterDto registerDto);


        Task<LoginResponse> Login(LoginDto loginDto);
        Task<UserDto> UpdateUSerRole(UpdateUserRole updateUserRole);

        Task<User> GetUser(int id);

        Task<UserDto> GetCurrentUser();

        Task<IEnumerable<UserDto>> GetAllUser();

    }
}
