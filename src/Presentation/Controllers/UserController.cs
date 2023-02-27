using Microsoft.AspNetCore.Mvc;
using Preezie.Application;
using Preezie.Presentation.Model;

namespace Preezie.Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private const string UserRoute = "users";
        private readonly IUserMediator _userMediator;

        public UserController(IUserMediator userMediator)
        {
            _userMediator = userMediator;
        }

        [HttpPost]
        [Route(UserRoute)]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest user, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(user.DisplayName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("All fields are mandatory.");
            }

            var response = await _userMediator.AddUserAsync(user);

            return Ok(response);
        }

        [HttpPut]
        [Route(UserRoute + "/{userId}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public async Task<IActionResult> UpdateUser(int userId, UserRequest userRequest, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userMediator.UpdateUserAsync(userId, userRequest);

            return Ok(response);
        }

        [HttpGet]
        [Route(UserRoute)]
        [ProducesResponseType(typeof(IEnumerable<UserInfo>), 200)]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken, [FromQuery] string? email = null, [FromQuery] int page = 1)
        {
            var response = await _userMediator.GetUserAsync(email, page);

            return Ok(response);
        }
    }
}