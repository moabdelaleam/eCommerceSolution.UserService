using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _userService;

        public AuthController(IUsersService userService)
        {
            this._userService = userService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Core.DTO.RegisterRequest registerRequest)
        {
            if (registerRequest == null)
                return BadRequest("This is invalid request.");

            var user = await _userService.Register(registerRequest);

            if (user is null || user.Sucess == false)
                return BadRequest("Invalid Credentials.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Core.DTO.LoginRequest loginRequest)
        {
            if (loginRequest == null)
                return BadRequest("this is an Inavalid Credentials");

            var user = await _userService.Login(loginRequest);

            if (user is null || user.Sucess == false)
                return Unauthorized("This user is not authenticated");

            return Ok(user);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRequest updateRequest)
        {
            var result = await _userService.UpdateUser(updateRequest);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser([FromQuery] string userId)
        {
            var result = await _userService.DeleteUser(Guid.Parse(userId));
            return Ok(result);
        }
    }
}