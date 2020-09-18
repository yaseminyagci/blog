
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Shared.Resources.Tag;
using Microsoft.AspNetCore.Identity;
using Application.Services;
using System;

namespace WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]

    public class TagsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Tag> _repository;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public TagsController(IUnitOfWork unitOfWork, IRepository<Tag> repository, IMapper mapper, UserService userService)
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
            var item = await _repository.GetSingleFirstAsync(c => c.Id == id).ConfigureAwait(false);
            if (item != null)
                return new ActionResponse(_mapper.Map<Tag, TagGetData>(item), StatusCodes.Status200OK);
            return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);

        }

        [HttpGet("GetAll")]
        public async Task<ActionResponse> GetAll()
        {
            var data = _repository.GetAll().Where(p=>p.Status==(byte)RecordStatus.Active);
            var result = new ActionResponse(_mapper.Map<List<Tag>, List<TagGetData>>(await data.ToListAsync().ConfigureAwait(false)), StatusCodes.Status200OK);
            return result;

        }

        [HttpPost("Add")]
        public async Task<ActionResponse> Add([FromBody] TagData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors(), StatusCodes.Status400BadRequest);


            try
            {

                var item = _mapper.Map<TagData, Tag>(data);

                var isLabelExist = await _repository
                                  .IsExistAsync(c => c.TagName.ToUpper() == data.TagName.ToUpper()).ConfigureAwait(false);
                if (isLabelExist)
                    return new ActionResponse(MessageBuilder.AlreadyExist(data.TagName), StatusCodes.Status400BadRequest);

                await _repository.AddAsync(item).ConfigureAwait(false);
                await _unitOfWork.CompleteAsync().ConfigureAwait(false);

                return await Get(item.Id).ConfigureAwait(false);
            }
            catch (Exception e)
            {

                throw;
            }



        }
        [HttpPost("Edit")]
        public async Task<ActionResponse> Edit([FromBody] TagData data)
        {
            if (!ModelState.IsValid)
                return new ActionResponse(ModelState.AllErrors(), StatusCodes.Status400BadRequest);


            var item = await _repository.GetSingleFirstAsync(c => c.Id == data.Id).ConfigureAwait(false);
            if (item == null)
                return new ActionResponse(MessageBuilder.NotFound, StatusCodes.Status404NotFound);


            var isLabelExist = data.TagName.ToUpper() != item.TagName.ToUpper() && await _repository
                              .IsExistAsync(c => c.TagName.ToUpper() == data.TagName.ToUpper()).ConfigureAwait(false);
            if (isLabelExist)
                return new ActionResponse(MessageBuilder.AlreadyExist(data.TagName), StatusCodes.Status400BadRequest);

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