
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SNServiceAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using SNServiceAPI.Services;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace SNServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IDapper _dapper;

        public LoginController(IDapper dapper, IConfiguration config)
        {
            _config = config;
            _dapper = dapper;
        }

        //[AllowAnonymous]
        //[HttpPost("AuthenticateOfficial")]
        //public IActionResult AuthenticateParent([FromBody] UserModel login)
        //{
        //    IActionResult response = Unauthorized();
        //    var user = AuthenticateUserAsync(login);

        //    if (user.Result != null)
        //    {
        //        var tokenString = GenerateJSONWebToken(user.Result);
        //        response = Ok(new { token = tokenString });
        //    }

        //    return response;            
        //}
        [AllowAnonymous]
        [HttpPost("AuthenticateOfficial")]
        public Task<UserModel> AuthenticateUser([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUserAsync(login);

            if (user.Result != null)
            {
                var tokenString = GenerateJSONWebToken(user.Result);
                response = Ok(new { token = tokenString });
            }
            return user;
        }

        [AllowAnonymous]
        [HttpPost("AuthenticateSGB")]
        public Task<UserModel> AuthenticateSGB([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateParentAsync(login);

            if (user.Result != null)
            {
                var tokenString = GenerateJSONWebToken(user.Result);
                response = Ok(new { token = tokenString });
            }
            return user;
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserModel> AuthenticateUserAsync(UserModel login)
        {
            UserModel user = null;
            var result = await Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where Persal = '{login.Persal}' and Password = '{login.Password}'", null, commandType: CommandType.Text));
            
            if(result == null)
                return null;

            //Validate the User Credentials    
            if (login.Persal == result.Persal && login.Password == result.Password)
            {
                user = result;
            }
            return user;
        }

        private async Task<UserModel> AuthenticateParentAsync(UserModel login)
        {
            UserModel user = null;
            var result = await Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where IDNumber = '{login.IDNumber}' and Password = '{login.Password}'", null, commandType: CommandType.Text));

            if (result == null)
                return null;

            //Validate the User Credentials    
            if (login.IDNumber == result.IDNumber && login.Password == result.Password)
            {
                user = result;
            }
            return user;
        }



        public static string GenerateRandomAlphanumericString(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!$%_@^*()";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        [AllowAnonymous]
        [HttpGet("GeneratePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<int> GeneratePassword(int Id)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Credentials", GenerateRandomAlphanumericString());
            dbparams.Add("@Id", Id);

            var result = await Task.FromResult(_dapper.Update<int>("[dbo].[SP_GenerateTempPassword]"
                , dbparams,
                commandType: CommandType.StoredProcedure));

            return result;

        }
    }
}
