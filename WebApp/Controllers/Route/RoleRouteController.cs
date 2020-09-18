using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using AutoMapper;
using Core.Constants;
using Core.Enums;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Process.Repository;
using Shared.Poco;
using Shared.Resources.Role;

namespace WebApp.Controllers.Route
{
    [Authorize]
    [Route("")]
    public class RoleRouteController : Controller
    {
        private readonly IRepository<PermissionCategory> _categoryRepository;
        private readonly RoleService _roleService;
        private readonly IMapper _mapper;

        public RoleRouteController(
            IRepository<PermissionCategory> categoryRepository,
            RoleService roleService,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _roleService = roleService;
            _mapper = mapper;
        }

        private List<PermissionGroupPocoModel> GetPermissions()
        {

            var categoryPermissions = _categoryRepository.GetAll(
                "PossiblePermissions.Permission").ToList();


            var categoryPocoPermissions = categoryPermissions.Select(c => new PermissionGroupPocoModel()
            {
                Title = c.Label,
                VisibleTitle = c.VisibleLabel,
                PossiblePermissionsList = c.PossiblePermissions.Select(c => new PermissionPocoModel()
                {
                    Title = c.Permission.Label,
                    VisibleTitle = c.Permission.VisibleLabel,
                    RelationId = c.Id,
                    PermissionId = c.PermissionId,
                    CategoryId = c.CategoryId
                }).ToList()
            }).ToList();

            var list = new List<PermissionGroupPocoModel>();
            list.AddRange(categoryPocoPermissions);
            return list;
        }

        [Authorize("role_list")]
        [HttpGet("Roles")]
        public IActionResult Roles()
        {
            return View();
        }

        [HttpGet("AddRole")]
        public IActionResult AddRole()
        {
            ViewBag.Permissions = GetPermissions();

            return View();
        }

        [HttpGet("EditRole/{id}")]
        public async Task<IActionResult> EditRole(string id)
        {
            ViewBag.Permissions = GetPermissions();
            var includeParams = IncludeStringConstants.RolePermissionIncludeList;
            var role = await _roleService.GetRoleByIdAsync(id, includeParams.ToArray()).ConfigureAwait(false);

            if (role == null)
                return RedirectToAction("Roles");
            var result = _mapper.Map<Role, RoleGetData>(role);

            return View(result);
        }

    }
}