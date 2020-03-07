using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OfferZoneAPI.Models;
using OfferZoneAPI.Services.AuthService;

namespace OfferZoneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authContext;
        public AuthController(AuthService context)
        {
            _authContext = context;
        }
        [HttpPost, Route("AuthorizeUser")]
        public IActionResult AuthorizeUser(LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request");
            }
            LoginModel isLogin = _authContext.AdminLogin(user);
            if (isLogin.UserType != "" && isLogin.UserType != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44318",
                    audience: "https://localhost:44318",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString , usertype= isLogin.UserType});
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}