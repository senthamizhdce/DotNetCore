using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTAuthController : ControllerBase
    {
        // GET: api/SampleAPI
        [HttpGet, Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values  
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request");
            }

            if (user.UserName == "johncitizen" && user.Password == "abc@123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    //issuer: "http://localhost:5000",
                    //audience: "http://localhost:5000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}