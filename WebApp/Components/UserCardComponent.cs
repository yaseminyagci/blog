using Application.Services;
using AutoMapper;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Resources.User;
using System.Threading.Tasks;
namespace Presentation.Components
{
    public class UserCardComponent : ViewComponent
    {

        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UserCardComponent(IMapper mapper, UserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserByNameAsync(User.Identity.Name).ConfigureAwait(false);

            var model = _mapper.Map<User, UserGetData>(user);

            return View("~/PartialViews/User/_PartialUserCard.cshtml", model);
        }

    }
}
