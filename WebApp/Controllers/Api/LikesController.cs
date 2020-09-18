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
using Shared.Resources.Likes;

namespace WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Likes> _repository;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public LikesController(IUnitOfWork unitOfWork, IRepository<Likes> repository, IMapper mapper, UserService userService)
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
                return new ActionResponse(_mapper.Map<Likes, LikesGetData>(item), StatusCodes.Status200OK);
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);

        }

        [HttpGet("GetByPost/{id}")]
        public async Task<ActionResponse> GetByPost(int id)
        {
            var userId = await _userService.GetAuthorizedUserIdAsync(User).ConfigureAwait(false);
            var data = await _repository.GetSingleFirstAsync(x => x.PostId == id && x.UserId == userId, x => x.UserDetail).ConfigureAwait(false);
            
            var getData = _mapper.Map<Likes, LikesGetData>(data);
            if (getData == null)
                getData = new LikesGetData() {Liked=false,Id=0 };
            int count= _repository.GetAll().Where(x => x.PostId == id&&x.Liked==true).Count();
            getData.LikeCount = count;

            return new ActionResponse(getData, StatusCodes.Status200OK);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResponse> GetAll()
        {
            var data = _repository.GetAll();
            return new ActionResponse(_mapper.Map<List<Likes>, List<LikesGetData>>(await data.ToListAsync().ConfigureAwait(false)), StatusCodes.Status200OK);

        }

        [HttpPost("Add")]
        public async Task<ActionResponse> Add([FromBody] LikesData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors(), StatusCodes.Status400BadRequest);
            data.UserId = await _userService.GetAuthorizedUserIdAsync(User).ConfigureAwait(false);
            var item = _mapper.Map<LikesData, Likes>(data);

            //var isLabelExist = await _repository
            //                  .IsExistAsync(c => c.Content.ToUpper() == data.Content.ToUpper()).ConfigureAwait(false);
            //if (isLabelExist)
            //    return new ActionResponse(MessageBuilder.AlreadyExist(data.Content), StatusCodes.Status400BadRequest);

            await _repository.AddAsync(item).ConfigureAwait(false);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);

            return await Get(item.Id).ConfigureAwait(false);
        }
        [HttpPost("Edit")]
        public async Task<ActionResponse> Edit([FromBody] LikesData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors(), StatusCodes.Status400BadRequest);
            data.UserId = await _userService.GetAuthorizedUserIdAsync(User).ConfigureAwait(false);
            var item = await _repository.GetSingleFirstAsync(c => c.Id == data.Id).ConfigureAwait(false);
            if (item == null)
                return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);

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