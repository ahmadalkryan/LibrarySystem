using Application.Dtos.User;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _appRepository;

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly  IPaswordHasher passwordHasher;

        private readonly IConfiguration _configuration;

        public UserService(IJwtService jwtService, IConfiguration configuration, IRepository<User> appRepostory, IMapper mapper, IPaswordHasher passwordHash1, IHttpContextAccessor httpContextAccessor)
        {
            _appRepository = appRepostory;
            _contextAccessor = httpContextAccessor;
            passwordHasher = passwordHash1;
            _configuration = configuration;
            _jwtService = jwtService;
            _mapper = mapper;

        }
        public async Task<IEnumerable<UserDto>> GetAllUser()
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _appRepository.GetAllAsync());
        }

        public async Task<UserDto> GetCurrentUser()
        {
            var userID = GetCurrentUSerID();

            var user = await _appRepository.GetByIdAsync(userID);
            return _mapper.Map<UserDto >(user);
            

        }
        private int GetCurrentUSerID()
        {
            var userclaim = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userclaim != null && int.TryParse(userclaim.Value, out int userId))
            {
                return userId;




            }
            throw new Exception("user ID claim not found or invalid");
        }

        public async Task<User> GetUser(int id)
        {

            return await _appRepository.GetByIdAsync(id);
           
        }

        
        public async Task<LoginResponse> Login(LoginDto loginDto)
        {
            var user = (await _appRepository.GetAllWitAllIncludeAsync(x => x.Email == loginDto.Email)).FirstOrDefault();

            if (user != null && passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                var token = _jwtService.GenerateToken(user);
                return new LoginResponse
               {
                   Token= token,   
                   Id =user.Id
                  ,
                   Role =user.Role
                   ,
                   UserName=user.Username,
                   Expires=DateTime.Now.AddMinutes(60.0)
               };
            }
            throw new Exception("Invalid Email Or Password");

        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var check = await _appRepository.GetAllAsync();
            if (check.Any(x => x.Email.Equals(registerDto.Email)))
            {
                throw new Exception("Email Already Exists");
            }



            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHasher.HashPassword(registerDto.Password)
                ,
                Role = "Librarian"
            };
            await _appRepository.AddAsync(user);
            return _mapper.Map<UserDto>(user);
        }

      public  async Task<UserDto> UpdateUSerRole(UpdateUserRole updateUserRole)
        {
           var user =await _appRepository.GetByIdAsync(updateUserRole.Id);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            user.Role = updateUserRole.Role;
          await  _appRepository.UpdateAsync(user);
            return _mapper.Map<UserDto>(user);
        }


        
    }
}
