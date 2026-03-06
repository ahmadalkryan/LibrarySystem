using Application.IService;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class PasswordHasher : IPaswordHasher
    {
        public string HashPassword(string password)
        {
          return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
           return BCrypt.Net.BCrypt.Verify(password , hashedPassword);
        }
    }
}
