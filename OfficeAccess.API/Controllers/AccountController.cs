using Application.Dtos.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OfficeAccess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody] AuthRequest authRequest)
        {
            var token = await _userService.AuthenticateUser(authRequest.UserId);

            return Ok(token);
        }
    }
}
