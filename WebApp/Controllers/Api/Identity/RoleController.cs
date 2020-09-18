using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using AutoMapper;
using AutoWrapper.Extensions;
using Core.Constants;
using Core.Utilities;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Resources.Role;

namespace WebApp.Controllers.Api.Identity
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(RoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }


        [HttpGet("Get/{id}")]
        public async Task<ActionResponse> Get(string id)
        {
            var includeParams = IncludeStringConstants.RolePermissionIncludeList;
            includeParams.Add("Users.User");
            var role = await _roleService.GetRoleByIdAsync(id, includeParams.ToArray()).ConfigureAwait(false);
            if (role != null)
            {
                var data = _mapper.Map<Role, RoleGetData>(role);
                return new ActionResponse(data);
            }

            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResponse> GetAll()
        {
            var includeParams = IncludeStringConstants.RolePermissionIncludeList;
            includeParams.Add("Users.User");
            var role = await _roleService.FindBy(c => c.IsEditable, includeParams.ToArray()).ToListAsync().ConfigureAwait(false);
            var data = _mapper.Map<List<Role>, List<RoleGetData>>(role);
            return new ActionResponse(data);
        }

        [HttpPost("Add")]
        public async Task<ActionResponse> Add([FromBody]RoleData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors());


            if (await _roleService.IsExistAsync(c => c.Name == data.Name).ConfigureAwait(false))
                return new ActionResponse(MessageBuilder.AlreadyExist(data.Name), StatusCodes.Status400BadRequest);

            var role = _mapper.Map<RoleData, Role>(data);
            role.Id = Guid.NewGuid().ToString();
            role.DateCreated = DateTime.Now;
            await _roleService.CreateAsync(role).ConfigureAwait(false);

            return await Get(role.Id).ConfigureAwait(false);
        }


        [HttpPost("Edit")]
        public async Task<ActionResponse> Edit([FromBody] RoleData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors());

            var role = await _roleService.GetRoleByIdAsync(data.Id, IncludeStringConstants.RolePermissionIncludeList.ToArray()).ConfigureAwait(false);
            if (role == null)
                return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);
            if (!role.IsEditable)
                return new ActionResponse(MessageBuilder.NotEditable, StatusCodes.Status400BadRequest);

            if (data.Name != role.Name &&
                await _roleService.IsExistAsync(c => c.Name == data.Name).ConfigureAwait(false))
                return new ActionResponse(MessageBuilder.AlreadyExist(data.Name), StatusCodes.Status400BadRequest);


            //update
            _mapper.Map<RoleData, Role>(data, role);
            role.DateModified = DateTime.Now;
            await _roleService.UpdateAsync(role).ConfigureAwait(false);

            return await Get(role.Id).ConfigureAwait(false);
        }

        [HttpPost("Delete/{id}")]
        public async Task<ActionResponse> Delete(string id)
        {

            var role = await _roleService.GetRoleByIdAsync(id).ConfigureAwait(false);
            if (!role.IsEditable)
                return new ActionResponse(MessageBuilder.NotEditable, StatusCodes.Status400BadRequest);


            if (role != null)
            {
                await _roleService.DeleteAsync(role).ConfigureAwait(false);
                return new ActionResponse(MessageBuilder.Deleted());
            }
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);

        }

    }
}
