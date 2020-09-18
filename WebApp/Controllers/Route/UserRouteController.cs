using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using AutoMapper;
using Core.Constants;
using Core.Utilities;
using Data.Entities;
using Data.Entities.Identity;
using Data.Entities.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Process.Repository;
using Shared.Resources.Post;
using Shared.Resources.Tag;
using Shared.Resources.TagPost;
using Shared.Resources.User;

namespace WebApp.Controllers.Route
{
    [Authorize]
    [Route("")]
    public class UserRouteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserDetail> _repository;
        private readonly IRepository<Post> _postRepository;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserRouteController(UserService userService, IMapper mapper,IUnitOfWork unitOfWork,IRepository<UserDetail> repository, IRepository<Post> postRepository)
        {
            _postRepository = postRepository;   
            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }    


        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet("UserList")]
        public IActionResult UserList()
        {
            return View();
        }
        [HttpGet("UserAdd")]
        public IActionResult UserAdd()
        {
            return View();
        }

        [HttpGet("UserEdit/{id}")]
        public async Task<IActionResult> UserEdit(string id)
        {
            var includeParams = IncludeStringConstants.UserRolePermissionIncludeArray.ToList();
             includeParams.Add("Detail");
            var item =await _userService.GetUserByIdAsync(id, includeParams.ToArray()).ConfigureAwait(false);
           
            var result = _mapper.Map<User, UserEditData>(item);

            return View(result);
        }
        [HttpGet("UserDetail/{id}")]
        public async Task<IActionResult> UserDetail(string id)
        {
            var includeParams = IncludeStringConstants.UserRolePermissionIncludeArray.ToList();
            includeParams.Add("Detail");
            var item = await _repository.GetSingleFirstAsync(x=>x.UserId==id).ConfigureAwait(false);

            var result = _mapper.Map<UserDetail, UserDetailGetData>(item);

            return View(result);
        }

        [HttpGet("Index")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var item =await  _postRepository.GetSingleFirstAsync(x=>x.Id==id).ConfigureAwait(false);
            var result = _mapper.Map<Post, PostGetData>(item);
            return View(result);
        }

        [HttpGet("Contact")]
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpGet("About")]
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }
        [HttpGet("Tags")]
        public IActionResult Tags()
        {
            return View();
        }    
        [HttpGet("Comment")]
        public IActionResult Comment()
        {
            return View();
        }


        [HttpGet("Vehicles")]
        public IActionResult Vehicles()
        {
            return View();
        }


    }
}