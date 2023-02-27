using Microsoft.EntityFrameworkCore;
using Preezie.Presentation.Model;

namespace Preezie.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;
        private readonly ILogger<UserRepository> _logger;
        private readonly int PageSize = 10;

        public UserRepository(UserDbContext userDbContext, ILogger<UserRepository> logger)
        {
            _userDbContext = userDbContext ?? throw new ArgumentNullException(nameof(userDbContext));
            _logger = logger;
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                await _userDbContext.Users.AddAsync(user).ConfigureAwait(false);

                await _userDbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is an issue saving data to User table. Unhandled Error: {error}", ex.Message);

                throw new Exception("Failed to save user");
            }
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var user = await _userDbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogWarning("User with ID {userId} not found.", userId);
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string? email = null, int page = 1)
        {
            var query = _userDbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(u => u.Email == email);
            }

            return await query.Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        public async Task UpdateUserAsync(User user, string displayName, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.DisplayName = displayName;
            user.Password = password;

            await _userDbContext.SaveChangesAsync();
        }
    }
}