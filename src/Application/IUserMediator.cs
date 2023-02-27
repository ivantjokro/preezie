using Preezie.Presentation.Model;

namespace Preezie.Application
{
    public interface IUserMediator
    {
        Task<UserResponse> AddUserAsync(CreateUserRequest request);

        Task<UserResponse> UpdateUserAsync(int userId, UserRequest request);

        Task<IEnumerable<UserInfo>> GetUserAsync(string? email, int page);
    }
}
