using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Process.Repository;
using Shared.Resources.Post;
using Shared.Resources.TagPost;

namespace WebApp.Controllers.Route
{

    [Authorize]
    [Route("")]

    public class PostRouteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Post> _repository;
        private readonly IRepository<TagPost> _repositoryTagPost;
        private readonly IMapper _mapper;
        public PostRouteController(IRepository<Post> repository, IMapper mapper, IUnitOfWork unitOfWork, IRepository<TagPost> repositoryTagPost)
        {
            _repositoryTagPost = repositoryTagPost;
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("PostList")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Post")]
        public IActionResult Post()
        {
            return View();
        }

        [HttpGet("PostEdit/{id}")]
        public async Task<IActionResult> PostEdit(int id)
        {
            var resultTag = _repositoryTagPost.GetAll().Where(x => x.PostId == id).ToList();

            var items = _mapper.Map<List<TagPost>, List<TagPostGetData>>(resultTag);
            ViewBag.Tags = items;

            var item = await _repository.GetSingleFirstAsync(c => c.Id == id).ConfigureAwait(false);
            var result = _mapper.Map<Post, PostGetData>(item);

            return View(result);
        }

        [HttpGet("PostAdd")]
        public IActionResult PostAdd()
        {
            return View();
        }
        
        [HttpGet("PostDetail")]
        public async Task<IActionResult> PostDetail(int id)
        {
            var resultTag = await _repository.GetSingleFirstAsync(x => x.Id == id).ConfigureAwait(false);

            var item = _mapper.Map<Post, PostGetData>(resultTag);

            return View(item);
        }
    }
}