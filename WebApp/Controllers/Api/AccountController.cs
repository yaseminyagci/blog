using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using AutoMapper;
using AutoWrapper.Extensions;
using Core.Constants;
using Core.Extensions;
using Core.Utilities;
using Data.Entities.Identity;
using Data.Entities.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Process;
using Shared;
using Shared.Resources.User;

namespace WebApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper,UserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userService = userService;
        }
         
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResponse> Login([FromBody] UserLoginData data)
        {

            var isEmail = data.EmailOrUsername.isEmail();
            var isUsername = data.EmailOrUsername.isUsername();

            if (!(isEmail || isUsername))
                ModelState.AddModelError("emailOrUsername", "Enter valid Email/Username");

            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors(), StatusCodes.Status400BadRequest);
            var includeAll = IncludeStringConstants.UserRolePermissionIncludeArray.ToList();
            includeAll.Add("Detail");
            var userQuery = _userManager.Users.IncludeAll(includeAll.ToArray());
            

            User user;
            switch (isEmail)
            {
                case true:
                    user = await userQuery.FirstOrDefaultAsync(c => c.Email == data.EmailOrUsername)
                        .ConfigureAwait(false);
                    break;
                case false:
                    user = await userQuery.FirstOrDefaultAsync(c => c.UserName == data.EmailOrUsername)
                        .ConfigureAwait(false);
                    break;
            }

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, data.Password, false, false).ConfigureAwait(false);

                if (result.Succeeded)
                    return new ActionResponse(user, StatusCodes.Status200OK);
                return new ActionResponse(MessageBuilder.LoginFault, StatusCodes.Status400BadRequest);
            }

            return new ActionResponse(MessageBuilder.LoginFault, StatusCodes.Status400BadRequest);

        }
        //normal kullanıcı için
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResponse> Register([FromBody] UserAddData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState, StatusCodes.Status409Conflict);


            var user = _mapper.Map<UserRegisterData, User>(data);
            user.Id = Guid.NewGuid().ToString();
            user.DateCreated = DateTime.Now;
            user.EmailConfirmed = true;
            

            var result = await _userService.CreateAsync(user, data.Password).ConfigureAwait(false);
            if (result.Succeeded)
                return new ActionResponse(result, StatusCodes.Status200OK);
            
            var errorMessage = result.Errors.FirstOrDefault();
            return new ActionResponse(MessageBuilder.LoginFault, StatusCodes.Status400BadRequest);

        }
        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResponse> Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return new ActionResponse(MessageBuilder.Logout, StatusCodes.Status200OK);
        }
    }
}