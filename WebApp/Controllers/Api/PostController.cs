
using Application.Services;
using AutoMapper;
using AutoWrapper.Extensions;
using Core.Enums;
using Core.Utilities;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Process.Repository;
using Shared;
using Shared.Resources.Post;
using Shared.Resources.Tag;
using Shared.Resources.TagPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Post> _repository;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly IRepository<TagPost> _tagPostRepository;
        private readonly IRepository<Tag> _tagRepository;
        public PostController(IUnitOfWork unitOfWork, IRepository<Tag> tagRepository, IRepository<Post> repository, IMapper mapper, UserService userService, IRepository<TagPost> tagPostRepository)
        {
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository;
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
            _tagPostRepository = tagPostRepository;
        }

        [AllowAnonymous]
        [HttpGet("Get/{id}")]
        public async Task<ActionResponse> Get(int id)
        {
            var item = await _repository.GetSingleFirstAsync(c => c.Id == id).ConfigureAwait(false);
            item.Content = System.Web.HttpUtility.UrlEncode(item.Content);
            // item.Content= HttpUtility.JavaScriptStringEncode(item.Content); 
            if (item != null)
                return new ActionResponse(_mapper.Map<Post, PostGetData>(item), StatusCodes.Status200OK);
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);

        }

        [HttpGet("GetAll")]
        public async Task<ActionResponse> GetAll()
        {
            var data = _repository.GetAll();
            data = data.Where(x => x.Status == (byte)RecordStatus.Active);
            foreach (var item in data)
            {
                //item.Content = Uri.EscapeDataString(item.Content);  
                item.Content = System.Web.HttpUtility.UrlEncode(item.Content);
            }
            var resultData = _mapper.Map<List<Post>, List<PostGetData>>(await data.ToListAsync().ConfigureAwait(false));
            return new ActionResponse(resultData, StatusCodes.Status200OK);

        }

        [HttpGet("GetAllGuest/{page}")]
        public ActionResponse GetAllGuest(int page,int count=12)
        {
            var data = _repository.GetAll();
            data = data.Where(x => x.Status == (byte)RecordStatus.Active).Skip(page*count).Take(count);
            
            foreach (var item in data)
            {
                //item.Content = Uri.EscapeDataString(item.Content);  
                item.Content = System.Web.HttpUtility.UrlEncode(item.Content);
            }
            var dat = data.ToList();
            var resultData = _mapper.Map<List<Post>, List<PostGetData>>(dat);
            return new ActionResponse(resultData, StatusCodes.Status200OK);

        }

        [HttpPost("Add")]
        public async Task<ActionResponse> Add([FromBody] PostData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors(), StatusCodes.Status400BadRequest);

            var item = _mapper.Map<PostData, Post>(data);

            item.UserId = await _userService.GetAuthorizedUserIdAsync(User).ConfigureAwait(false);

            var isLabelExist = await _repository
                              .IsExistAsync(c => c.Title.ToUpper() == data.Title.ToUpper()).ConfigureAwait(false);
            if (isLabelExist)
            {
                var result = new ActionResponse(MessageBuilder.AlreadyExist(data.Title), StatusCodes.Status400BadRequest);

                return result;
            }

            try
            {
                await _repository.AddAsync(item).ConfigureAwait(false);

                await _unitOfWork.CompleteAsync().ConfigureAwait(false);
                List<TagPost> tagPosts = new List<TagPost>();

                for (int i = 0; i < data.Tags.Length; i++)
                {
                    tagPosts.Add(new TagPost { TagId = data.Tags[i], PostId = item.Id });
                }

                await _tagPostRepository.AddRangeAsync(tagPosts).ConfigureAwait(false);
                await _unitOfWork.CompleteAsync().ConfigureAwait(false);

                var responseData = await Get(item.Id).ConfigureAwait(false);

                return responseData;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpPost("Edit")]
        public async Task<ActionResponse> Edit([FromBody] PostData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors(), StatusCodes.Status400BadRequest);


            var item = await _repository.GetSingleFirstAsync(c => c.Id == data.Id).ConfigureAwait(false);
            if (item == null)
                return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);


            var isLabelExist = data.Title.ToUpper() != item.Title.ToUpper() && await _repository
                              .IsExistAsync(c => c.Title.ToUpper() == data.Title.ToUpper()).ConfigureAwait(false);
            if (isLabelExist)
                return new ActionResponse(MessageBuilder.AlreadyExist(data.Title), StatusCodes.Status400BadRequest);

            _mapper.Map(data, item);

            var tagitems = _tagPostRepository.GetAll().Where(x => x.PostId == data.Id).ToList();
            tagitems.RemoveAll(x => x.PostId == data.Id);
            _tagPostRepository.DeleteWhere(x => x.PostId == data.Id);

            for (int i = 0; i < data.Tags.Length; i++)
            {

                tagitems.Add(new TagPost { TagId = data.Tags[i], PostId = item.Id });
            }

            await _tagPostRepository.AddRangeAsync(tagitems).ConfigureAwait(false);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);

            return await Get(item.Id).ConfigureAwait(false);
        }

        [HttpPost("Delete/{id}")]
        public async Task<ActionResponse> Delete(int id)
        {
            var item = await _repository.GetSingleFirstAsync(c => c.Id == id).ConfigureAwait(false);
            if (item != null)
            {
                _tagPostRepository.DeleteWhere(x=>x.PostId==id);
                //_repository.Delete(item);
                item.Status = (byte)RecordStatus.Deleted;
                await _unitOfWork.CompleteAsync().ConfigureAwait(false);
                return new ActionResponse(MessageBuilder.Deleted(), StatusCodes.Status200OK);
            }
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);
        }

        [HttpGet("GetTag/{id}")]
        public async Task<ActionResponse> GetTagsByPostId(int id)
        {
            var selectedTags = _tagPostRepository.GetAll().Where(c => c.PostId == id).Select(x => x.TagId).ToList();
            var allTags = _tagRepository.GetAll().Where(c => c.Status == (byte)RecordStatus.Active).Select(x => new TagPostGetData
            {
                Selected = selectedTags.Contains(x.Id) == true ? true : false,
                Tag= _mapper.Map<Tag, TagGetData>(x),
                
            }).ToList();
            if (allTags != null)
                return new ActionResponse(allTags, StatusCodes.Status200OK);
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);

        }
    }
}