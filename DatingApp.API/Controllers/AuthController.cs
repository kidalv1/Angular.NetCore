﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this.repo = repo;
            this.config = config;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            var userFromDb = await repo.Login(user.Name.ToLower(), user.Password);
            if (userFromDb == null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()),
                new Claim(ClaimTypes.Name , userFromDb.Name)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
        // GET: api/<AuthController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto user)
        {
            
            user.Name = user.Name.ToLower();
            if (await repo.UserExsists(user.Name))
            {
                return BadRequest("User is exists");
            }
            var userToCreate = new User { Name = user.Name };
            var createdUser = await repo.Register(userToCreate, user.Password);

            return StatusCode(201);
        }

    }
}
