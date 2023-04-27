using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiAppMusic.Models;
using ApiAppMusic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiAppMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly MusicDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        UserService _userService;
        public AccountController(MusicDBContext dbContext,UserService userService,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
        
                var result = await _userManager.CreateAsync(user, model.Password);
        
                if (result.Succeeded)
                {
                    // Tạo token
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
        
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        
                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Issuer"],
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
        
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
        
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        
            return BadRequest(ModelState);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.UtcNow.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }
        [Authorize]
        [HttpGet("my-profile")]
        public async Task<ApplicationUser> getUser(){
            
            var user = await _userService.getUserLogin(_userManager,_configuration,HttpContext);
            return user;      
            
            
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult> updateProfile([FromForm] string name, [FromForm] string email,[FromForm] string phoneNumber,[FromForm] string dob){
            var user = await _userService.getUserLogin(_userManager,_configuration,HttpContext);
            var id = user.Id;
            var userdb = await _userManager.FindByIdAsync(id);
            if(userdb != null){
                userdb.Name = name;
                userdb.Email = email;
                user.PhoneNumber = phoneNumber;
                user.DateOfBirth = dob;
                 var result = await _userManager.UpdateAsync(user);
            }

            return Ok("Update successfully");
            
        }
        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult> changePassword([FromForm] string pw,[FromForm] string cfpw,[FromForm] string password){
            var user = await _userService.getUserLogin(_userManager,_configuration,HttpContext);
            var id = user.Id;
            var userdb = await _userManager.FindByIdAsync(id);
            if (user != null && await _userManager.CheckPasswordAsync(userdb, password)){
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, password, cfpw);
                if(changePasswordResult.Succeeded){
                    return Ok("Change password sucessfully");
                }
                else {
                    return Ok("Change password failed");
                }
            }
            else
                return Ok("Find user failed");
        }

    }      
}