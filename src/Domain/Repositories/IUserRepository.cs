using Preezie.Presentation.Model;

namespace Preezie.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user, string displayName, string password);

        Task<User?> GetUserByIdAsync(int userId);

        Task<IEnumerable<User>> GetUsersAsync(string? email, int page);
    }
}