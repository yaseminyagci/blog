using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using AutoMapper;
using AutoWrapper.Extensions;
using Core.Constants;
using Core.Utilities;
using Data.Entities;
using Data.Entities.Identity;
using Data.Entities.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Resources.User;

namespace Web.Controllers.Api.Identity
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("Get")]
        public async Task<ActionResponse> Get()
        {
            var includeParams = IncludeStringConstants.UserRolePermissionIncludeArray.ToList();
            includeParams.Add("Detail");
            var item = await _userService.GetUserByNameAsync(User.Identity.Name, includeParams.ToArray()).ConfigureAwait(false);

            if (item != null)
                return new ActionResponse(_mapper.Map<User, UserGetData>(item), StatusCodes.Status200OK);
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);

        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResponse> Get(string id)
        {
            var includeParams = IncludeStringConstants.UserRolePermissionIncludeArray.ToList();
            includeParams.Add("Detail");
            var item = await _userService.GetUserByIdAsync(id, includeParams.ToArray()).ConfigureAwait(false);
            if (item != null)
                return new ActionResponse(_mapper.Map<User, UserGetData>(item), StatusCodes.Status200OK);
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);

        }
        [HttpGet("GetAll")]
        public async Task<ActionResponse> GetAll()
        {
            var includeParams = IncludeStringConstants.UserRolePermissionIncludeArray.ToList();
            includeParams.Add("Detail");
            var data = _userService.FindBy(c => c.IsEditable, includeParams.ToArray());
            return new ActionResponse(_mapper.Map<List<User>, List<UserGetData>>(await data.ToListAsync().ConfigureAwait(false)), StatusCodes.Status200OK);

        }

        [HttpPost("Add")]
        public async Task<ActionResponse> Add([FromBody] UserAddData data)
        {

            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors());

            if (!await _userService.IsExistAsync(c => c.UserName == data.UserName || c.Email == data.Email)
                .ConfigureAwait(false))
            {
                var user = _mapper.Map<UserAddData, User>(data);
                user.Id = Guid.NewGuid().ToString();
                user.EmailConfirmed = true;
                
                try
                {
                    var result = await _userService.CreateAsync(user, data.Password).ConfigureAwait(false);
                    if (result.Succeeded)
                        return await Get(user.Id).ConfigureAwait(false);
                    var errorMessage = result.Errors.FirstOrDefault();
                    return new ActionResponse(errorMessage, StatusCodes.Status400BadRequest);
                }
                catch (Exception e) { 
                    
                }
              
     
            }
            return new ActionResponse("Username veya Email zaten mevcut!", StatusCodes.Status400BadRequest);

        }
        [HttpPost("Edit")]
        public async Task<ActionResponse> Edit([FromBody] UserEditData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors());

            var includeParams = IncludeStringConstants.UserRolePermissionIncludeArray.ToList();
            includeParams.Add("Detail");
            var user = await _userService.GetUserByIdAsync(data.Id, includeParams.ToArray()).ConfigureAwait(false);

            if (user == null)
                return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);
            if (!user.IsEditable)
                return new ActionResponse(MessageBuilder.NotEditable, StatusCodes.Status400BadRequest);

            //update
            _mapper.Map<UserEditData, User>(data, user);
            if (user.Detail == null)
                user.Detail = new UserDetail();
            //user.Detail.Title = data.Title;
            //user.Detail.Departman = data.Departman;
            user.Detail.FullName = data.FullName;
            await _userService.UpdateAsync(user).ConfigureAwait(false);

            return await Get(user.Id).ConfigureAwait(false);
        }


        [HttpPost("ChangePassword")]
        public async Task<ActionResponse> ChangePassword([FromBody] UserChangePasswordData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors());

            var user = await _userService.GetUserByIdAsync(data.Id).ConfigureAwait(false);
            if (user == null)
                return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);
            if (!user.IsEditable)
                return new ActionResponse(MessageBuilder.NotEditable, StatusCodes.Status400BadRequest);


            var changePasswordResult =
                await _userService.ChangePasswordAsync(user, data.OldPassword, data.Password).ConfigureAwait(false);
            if (!changePasswordResult.Succeeded)
            {
                var errorMessage = changePasswordResult.Errors.FirstOrDefault();
                return new ActionResponse(errorMessage.Description, StatusCodes.Status400BadRequest);
            }
            return new ActionResponse(StatusCodes.Status200OK);

        }


        [HttpPost("Delete/{id}")]
        public async Task<ActionResponse> Delete(string id)
        {
            var user = await _userService.GetUserByIdAsync(id).ConfigureAwait(false);
            if (!user.IsEditable)
                return new ActionResponse(MessageBuilder.NotEditable, StatusCodes.Status400BadRequest);
            if (user != null)
            {
                await _userService.DeleteAsync(user).ConfigureAwait(false);
                return new ActionResponse(MessageBuilder.Deleted(), StatusCodes.Status200OK);

            }
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);
        }

    }
}
