using Preezie.Presentation.Model;

namespace Preezie.Application.Extensions
{
    public static class UserExtensions
    {
        public static IEnumerable<UserInfo> ToUserInfo(this IEnumerable<User> users)
        {
            return users.Select(u => new UserInfo
            {
                DisplayName = u.DisplayName,
                Email = u.Email
            });
        }
    }
}
