using Preezie.Presentation.Model;

namespace Preezie.UnitTests.Theories
{
    public class GetUserTheory
    {
        public string? EmailKeyword { get; set; }

        public List<UserInfo> ExpectedUserResponse { get; set; }
    }
}