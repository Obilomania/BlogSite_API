using BlogSite_API.Data;
using BlogSite_API.DTOs.AuthenticationDTOs;
using BlogSite_API.Models;
using BlogSite_API.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogSite_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private string secretKey;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public AuthController(
            ApplicationDbContext context, 
            IConfiguration configuration, 
            UserManager<ApplicationUser> usermanager, 
            RoleManager<IdentityRole> rolemanager
            )
        {
            _context = context;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _usermanager = usermanager;
            _rolemanager = rolemanager;
        }

        //CONTROLLER TO REGISTER A USER
        [HttpPost]
        [Route("Register User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            ApplicationUser userFromDb = await _context.AppUser
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (userFromDb != null)
            {
                throw new Exception("Username already exists");

            }
            ApplicationUser newUser = new()
            {
                UserName = model.UserName,
                Email = model.UserName,
                NormalizedEmail = model.UserName.ToUpper(),
                NickName = model.Name
            };
            try
            {
                var result = await _usermanager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    if (!_rolemanager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                    {
                        //create role in DB
                        await _rolemanager.CreateAsync(new IdentityRole(SD.Role_Admin));
                        await _rolemanager.CreateAsync(new IdentityRole(SD.Role_User));
                    }
                    if (model.Role.ToLower() == SD.Role_Admin)
                    {
                        await _usermanager.AddToRoleAsync(newUser, SD.Role_Admin);
                    }
                    else
                    {
                        await _usermanager.AddToRoleAsync(newUser, SD.Role_User);
                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
            }
                return BadRequest();
        }


        //CONTROLLER TO LOGIN
        [HttpPost]
        [Route("Login User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            ApplicationUser userFromDb = _context.AppUser
                .FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            bool isValid = await _usermanager.CheckPasswordAsync(userFromDb, model.Password);
            if (isValid == false)
            {
                throw new Exception("Username or password is incorrect");
            }


            //we have to generate JWT Token
            var roles = await _usermanager.GetRolesAsync(userFromDb);
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim("fullName", userFromDb.NickName),
                new Claim("id", userFromDb.Id.ToString()),
                new Claim(ClaimTypes.Email, userFromDb.UserName.ToString()),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponse loginResponse = new()
            {
                Email = userFromDb.Email,
                Token = tokenHandler.WriteToken(token)
            };
            if (loginResponse.Email == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                throw new Exception("Username or password is incorrect");
            }
            return Ok(loginResponse);
        }
    }
}
