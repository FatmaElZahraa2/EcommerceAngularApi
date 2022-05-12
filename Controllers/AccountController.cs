using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerceAngularProject.DTOs;
using Microsoft.AspNetCore.Identity;
using EcommerceAngularProject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EcommerceAngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> usrerManager , IConfiguration configuration , RoleManager<IdentityRole> roleManager)
        {
            _userManager = usrerManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser userModel = new ApplicationUser();
            userModel.UserName = registerDTO.Username;
            userModel.Email = registerDTO.Email;
            IdentityResult result = await _userManager.CreateAsync(userModel, registerDTO.Password);
            if (result.Succeeded)
                return Ok("User Added Successfully");
            else
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
                return BadRequest(ModelState);
            } 


        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser userModel = await _userManager.FindByNameAsync(loginDTO.Username);

            if (userModel != null)
            {
                if (await _userManager.CheckPasswordAsync(userModel, loginDTO.Password) == true)
                {
                    var mytoken = await GenerateToke(userModel);
                    return Ok(

                    new {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo
                    });
                }
                else
                    return Unauthorized();
            }
            else
                return Unauthorized();
        }

        [NonAction]
        public async Task<JwtSecurityToken> GenerateToke(ApplicationUser userModel)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("intakeNo", "42"));//Custom
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
            var roles = await _userManager.GetRolesAsync(userModel);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecrtKey"]));

            var Token = new JwtSecurityToken(
                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials:
                       new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return Token;
        }
    }
}
