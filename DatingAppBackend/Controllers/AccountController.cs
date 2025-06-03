using DatingAppBackend.Data;
using DatingAppBackend.DTOs;
using DatingAppBackend.Entities;
using DatingAppBackend.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingAppBackend.Controllers
{
    public class AccountController(DatingAppContext context, ITokenService tokenService) : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName))
            {
                return BadRequest("Username is already taken.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            var result = await context.SaveChangesAsync();

            return Ok(new UserDto()
            {
                UserName = registerDto.UserName,
                Token = tokenService.CreateToken(user)
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.UserName == loginDto.UserName);
            if (user == null)
                return Unauthorized("Invalid User Name");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid Password");
            }

            return Ok(new UserDto()
            {
                UserName = loginDto.UserName,
                Token = tokenService.CreateToken(user)
            });
        }

        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
