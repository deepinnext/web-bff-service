using Deepin.Domain;
using Deepin.WebBff.API.Models.Identity;
using Deepin.WebBff.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deepin.WebBff.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController(
        IIdentityService identityService,
        IUserContext userContext) : ControllerBase
    {
        private readonly IIdentityService _identityService = identityService;
        private readonly IUserContext _userContext = userContext;

        [HttpGet]
        public async Task<ActionResult<UserProfile>> Get()
        {
            var user = await _identityService.GetUserAsync(_userContext.UserId);
            return Ok(user);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> Get(string id)
        {
            var user = await _identityService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
