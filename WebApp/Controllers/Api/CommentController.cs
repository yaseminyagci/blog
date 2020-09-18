using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Shared.Resources.Comment;

namespace WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Comment> _repository;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public CommentController(IUnitOfWork unitOfWork, IRepository<Comment> repository, IMapper mapper, UserService userService)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("Get/{id}")]
        public async Task<ActionResponse> Get(int id)
        {
            var item = await _repository.GetSingleFirstAsync(c => c.Id == id, x => x.UserDetail).ConfigureAwait(false);
            if (item != null)
                return new ActionResponse(_mapper.Map<Comment, CommentGetData>(item), StatusCodes.Status200OK);
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);

        }

        [HttpGet("GetByPost/{id}")]
        public async Task<ActionResponse> GetByPost(int id)
        {
            var data = _repository.GetAll(x => x.UserDetail);
            data = data.Where(c => c.PostId == id);
            return new ActionResponse(_mapper.Map<List<Comment>, List<CommentGetData>>(await data.ToListAsync().ConfigureAwait(false)), StatusCodes.Status200OK);


        }

        [HttpGet("GetAll")]
        public async Task<ActionResponse> GetAll()
        {
            var data = _repository.GetAll().Where(x=>x.Status==1).Include(x=>x.UserDetail).Include(x=>x.Post);
            var result = _mapper.Map<List<Comment>, List<CommentGetData>>(await data.ToListAsync().ConfigureAwait(false));
            return new ActionResponse(result, StatusCodes.Status200OK);

        }

        [HttpPost("Add")]
        public async Task<ActionResponse> Add([FromBody] CommentData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors(), StatusCodes.Status400BadRequest);
            data.UserId = await _userService.GetAuthorizedUserIdAsync(User).ConfigureAwait(false);
            var item = _mapper.Map<CommentData, Comment>(data);

            //var isLabelExist = await _repository
            //                  .IsExistAsync(c => c.Content.ToUpper() == data.Content.ToUpper()).ConfigureAwait(false);
            //if (isLabelExist)
            //    return new ActionResponse(MessageBuilder.AlreadyExist(data.Content), StatusCodes.Status400BadRequest);

            await _repository.AddAsync(item).ConfigureAwait(false);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);

            return await Get(item.Id).ConfigureAwait(false);
        }
        [HttpPost("Edit")]
        public async Task<ActionResponse> Edit([FromBody] CommentData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors(), StatusCodes.Status400BadRequest);


            var item = await _repository.GetSingleFirstAsync(c => c.Id == data.Id).ConfigureAwait(false);
            if (item == null)
                return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);


            var isLabelExist = data.Content.ToUpper() != item.Content.ToUpper() && await _repository
                              .IsExistAsync(c => c.Content.ToUpper() == data.Content.ToUpper()).ConfigureAwait(false);
            if (isLabelExist)
                return new ActionResponse(MessageBuilder.AlreadyExist(data.Content), StatusCodes.Status400BadRequest);

            _mapper.Map(data, item);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);

            return await Get(item.Id).ConfigureAwait(false);
        }

        [HttpPost("Delete/{id}")]
        public async Task<ActionResponse> Delete(int id)
        {
            var item = await _repository.GetSingleFirstAsync(c => c.Id == id).ConfigureAwait(false);
            if (item != null)
            {
                //_repository.Delete(item);
                item.Status = (byte)RecordStatus.Deleted;
                await _unitOfWork.CompleteAsync().ConfigureAwait(false);
                return new ActionResponse(MessageBuilder.Deleted(), StatusCodes.Status200OK);
            }
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);
        }
    }
}