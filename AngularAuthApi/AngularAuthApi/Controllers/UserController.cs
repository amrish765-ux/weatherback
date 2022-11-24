using AngularAuthApi.Context;
using AngularAuthApi.Helpers;
using AngularAuthApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbcontext _appDbcontext;
        public UserController(AppDbcontext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();
            var user = await _appDbcontext.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username && x.Password == x.Password);
            if (user == null)
                return NotFound(new
                {
                    Message = "User not Found"
                });
            if(!PasswordHasher.VerifyPassword(userObj.Password,user.Password))
            {
                return BadRequest(new
                {
                    Message = "password is incorrect"
                });
            }

            user.Token = CreateJWT(user);
            return Ok(new
            {
                Token=user.Token,
                Message = "Login Success"
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();
            //check usrname
            if (await CheckUserNameExistsAsync(userObj.Username))
                return BadRequest(new
                {
                    Message = "username already exists"
                });

            //check  email
            if (await CheckEmailExistsAsync(userObj.Email))
                return BadRequest(new
                {
                    Message = "email already exists"
                });
            //check password strength
            var pass = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new
                {
                    Message = pass
                });

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            await _appDbcontext.Users.AddAsync(userObj);
            await _appDbcontext.SaveChangesAsync();
            return Ok(new
            {
                Message = "Registered Successfully"
            });
        }
        private Task<bool> CheckUserNameExistsAsync(string username)
            =>_appDbcontext.Users.AnyAsync(x=>x.Username==username);
        private Task<bool> CheckEmailExistsAsync(string email)
            => _appDbcontext.Users.AnyAsync(x => x.Email == email);

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length == 0)
                sb.Append("minimum length should be 8" + Environment.NewLine);
            if ((!Regex.IsMatch(password, "[a-z]") && !Regex.IsMatch(password, "[A-z]") && !Regex.IsMatch(password, "[0-9]")))
                sb.Append("password should be alpha numeric" + Environment.NewLine);
            if(!Regex.IsMatch(password,"[<,>,0,!,,@,#,$,%,*,&,+,(,),_,^,\\[,\\],\\,:,;,',`,~,/,?.\",=]"))
                sb.Append("Password should contains special characters" + Environment.NewLine);
            return sb.ToString();
        }

        private string CreateJWT(User user) 
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecretKey....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,$"{user.FirstName},{user.LastName}")
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenScriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenScriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(
                await _appDbcontext.Users.ToListAsync());
        }
    }
}
