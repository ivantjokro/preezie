using Preezie.Application.Extensions;
using Preezie.Domain.Repositories;
using Preezie.Presentation.Model;

namespace Preezie.Application
{
    public class UserMediator : IUserMediator
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserMediator> _logger;

        public UserMediator(IUserRepository userRepository, ILogger<UserMediator> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<UserResponse> AddUserAsync(CreateUserRequest request)
        {
            var user = new User(request.Email, request.Password.EncryptPassword(), request.DisplayName);

            await _userRepository.AddUserAsync(user);

            _logger.LogInformation("Added user {Email} with ID {UserId}.", request.Email, user.Id);

            return new UserResponse()
            {
                Id = user.Id
            };
        }

        public async Task<IEnumerable<UserInfo>> GetUserAsync(string? email = null, int page = 1)
        {
            _logger.LogInformation("Getting users with email {Email} and page number {Page}.", email, page);

            var users = await _userRepository.GetUsersAsync(email, page);

            _logger.LogInformation("Retrieved {Count} users.", users.Count());

            return users.ToUserInfo();
        }

        public async Task<UserResponse> UpdateUserAsync(int userId, UserRequest request)
        {
            _logger.LogInformation("Updating user with ID {UserId}.", userId);

            // Validate input parameters
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                _logger.LogError("User with ID {UserId} not found.", userId);

                throw new ArgumentException($"User with ID {userId} not found.");
            }

            await _userRepository.UpdateUserAsync(user, request.DisplayName, request.Password.EncryptPassword());

            _logger.LogInformation("Updated user with ID {UserId}.", userId);

            // Create UserResponse object
            return CreateUserResponse(user);
        }

        private UserResponse CreateUserResponse(User user)
        {
            var response = new UserResponse()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email
            };

            return response;
        }
    }
}
