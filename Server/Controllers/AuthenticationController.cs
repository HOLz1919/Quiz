using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quiz.Server.Data;
using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public AuthenticationController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _db = db;
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
           
        }


        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationModel registrationModel)
        {
            if (registrationModel == null || !ModelState.IsValid)
                return BadRequest();

            var user = new ApplicationUser { UserName = registrationModel.Username, Email = registrationModel.Email };

            var result = await _userManager.CreateAsync(user, registrationModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            return StatusCode(201);


        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel lognModel)
        {
            var user = await _userManager.FindByNameAsync(lognModel.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, lognModel.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }





        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["validIssuer"],
                audience: _jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }

    }
}
