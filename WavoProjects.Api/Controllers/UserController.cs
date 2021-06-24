using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.Helpers;
using WavoProjects.Api.Models;
using WavoProjects.Api.Models.UserController;

namespace WavoProjects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> m_logger;
        private WavoContext m_db;
        private AuthenticationHelper m_auth;
        private User? m_user;
        public UserController(ILogger<UserController> logger, WavoContext context, AuthenticationHelper authHelper)
        {
            m_logger = logger;
            m_db = context;
            m_auth = authHelper;
            var x = HttpContext;
            
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel login)
        {
            m_logger.LogInformation($"Login - email: {login.Email}");
            User user = await m_db.Users.SingleOrDefaultAsync(i => i.Email == login.Email);

            if(user == null)
            {
                m_logger.LogError($"Could not find user with email {login.Email}");
                return Unauthorized("Username or password incorrect");
            }

            bool validated = await m_auth.ValidateCredentials(user, login.Password);
            
            if(!validated)
            {
                m_logger.LogError($"Invalid login for email {login.Email}");
                return Unauthorized("Username or password incorrect");
            }

            if(!user.Enabled || !user.Confirmed) {
                m_logger.LogError($"Invalid login for {user.FullName} - Enabled? {user.Enabled}, Confirmed? {user.Confirmed}");
                return Unauthorized("User Account disabled or not confirmed"); 
            }

            string token = await m_auth.MintToken(user.Id);

            return new LoginResponse
            {
                Token = token,
                User = user
            };
        }

        [HttpGet("CheckEmail")]
        [AllowAnonymous]
        public async Task<ActionResult<CheckEmailResponse>> CheckEmail(string email)
        {
            bool exists = await m_db.Users.AnyAsync(i => i.Email == email);
            return new CheckEmailResponse(exists);
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            m_logger.LogInformation($"GetUser - id: {id}");
            if (!IsCurrentUserOrAdmin(id))
            {
                m_logger.LogWarning($"GetUser called from someone without acccess! Perpetrator: {m_user?.FullName}");
                return Unauthorized();
            }

            return await m_db.Users
                                .Include(i => i.Tidbits)
                                    .ThenInclude(i => i.TidbitType)
                                .SingleAsync(i => i.Id == id);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserModel newUser)
        {
            m_logger.LogInformation($"Creating User with email {newUser.Email}");

            if(newUser.Password != newUser.ConfirmPassword) { return ValidationProblem("Passwords do not match"); }

            User user = new User
            {
                Email = newUser.Email.Trim().ToLower(),
                Password = m_auth.HashPassword(newUser.Password),
                FirstName = newUser.Firstname,
                LastName = newUser.Lastname,
                Enabled = true,
                Confirmed = false,

            };

            await m_db.AddAsync(user);
            return user.Id;
        }


        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            m_logger.LogInformation($"GetAllUsers");
            if (!IsAdmin())
            {
                m_logger.LogWarning($"GetAllUsers called from someone without acccess! Perpetrator: {m_user?.FullName}");
                return Unauthorized();
            }

            return await m_db.Users.Where(i => i.Enabled == true).ToListAsync();
        }

        private bool IsCurrentUserOrAdmin(int accessedUserId)
        {
            if (m_user == null) return false;

            return m_user.Id == accessedUserId || m_user.RoleId == 1;
        }

        private bool IsAdmin()
        {
            if (m_user == null) return false;

            return m_user.RoleId == 1;
        }
    }
}
