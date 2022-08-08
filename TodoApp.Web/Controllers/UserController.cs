using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Core.DBEntities;
using TodoApp.Core.DBEntities.Authentication;
using TodoApp.Core.Interfaces;
using TodoApp.Web.EnpointModel;
using TodoApp.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TodoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IResponseGeneric _responseGeneric;
        private readonly ILogService _logService;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IConfiguration configuration,
            IResponseGeneric responseGeneric,
            ILogService logService)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
            _responseGeneric = responseGeneric;
            _logService = logService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest cusModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "Registering User", JsonSerializer.Serialize(cusModel));

                    #region VALIDATIONS
                    var isAlreadyExist = await _userService.FindByEmailAsync(cusModel.Email) != null;
                    if (isAlreadyExist)
                    {
                        return BadRequest(_responseGeneric.Error("User already exist"));
                    }
                    #endregion

                    #region OPERATIONS
                    ApplicationUser user = new ApplicationUser()
                    {
                        Email = cusModel.Email,
                        UserName = cusModel.Name,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await _userService.CreateAsync(user, cusModel.Password);
                    if (!result.Succeeded)
                    {
                        List<string> errors = new List<string>();
                        if (result.Errors != null)
                        {
                            foreach (var error in result.Errors)
                            {
                                errors.Add(error.Description.ToString());
                            }
                        }

                        return BadRequest(_responseGeneric.Error(result: errors));

                    }

                    //Adding roles
                    await _userService.AddRolesAsync(user, "User");

                    //Create an entry in User Table along with ASPNET Identity tables:
                    await _userService.AddAsync(new User()
                    {
                        ApplicationUser = user,
                        Name = cusModel.Name
                    });

                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "Registering Success User", JsonSerializer.Serialize(cusModel));

                    return Ok(_responseGeneric.Success());
                    #endregion

                    
                }
                else
                {
                    return BadRequest(_responseGeneric.Error(result: ModelState));

                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginModel)
        {
            //Logging
            await _logService.InsertLog(LogLevel.Information, "Try Logging", JsonSerializer.Serialize(loginModel));

            var user = await _userService.FindByEmailAsync(loginModel.Email);
            if (user != null && await _userService.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    //new Claim("UserID", user.User.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                // Add roles as multiple claims
                var userRoles = await _userService.GetUserRoles(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(_responseGeneric.Success(result: new
                {
                    userId = user.Id,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    role = userRoles.ToList().FirstOrDefault() 
                }));
            }
            return Unauthorized(_responseGeneric.Error("Wrong credentials"));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
       
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.ListOfUsersAsync();
            return Ok(users.Select(m => new { name = m.Name, id = m.ApplicationUserId }));
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("CanAdminOnly")]
        public async Task<IActionResult> CanAdminOnly()
        {
            return Ok("success");
        }

        [Authorize(Roles = "User")]
        [HttpGet("CanUserOnly")]
        public async Task<IActionResult> CanUserOnly()
        {
            return Ok("success");
        }

    }
}
