using Preezie.Presentation.Model;
using Xunit;

namespace Preezie.UnitTests.Theories
{
    public class GetUsersTheories : TheoryData<string, GetUserTheory>
    {
        public GetUsersTheories()
        {
            GetAllUsers();
            GetUserFromSpecificEmail();
        }

        private void GetAllUsers()
        {
            var expectedUserResponse = new List<UserInfo>()
            {
                new UserInfo()
                {
                    DisplayName = "Test Dummy",
                    Email = "test1@dummy.local"
                },
                new UserInfo()
                {
                    DisplayName = "Test Honky",
                    Email = "test2@dummy.local"
                },
                new UserInfo()
                {
                    DisplayName = "Test",
                    Email = "test3@dummy.local"
                }
            };

            BuildTheoryData(null, expectedUserResponse, nameof(GetAllUsers));
        }

        private void GetUserFromSpecificEmail()
        {
            var expectedUserResponse = new List<UserInfo>()
            {
                new UserInfo()
                {
                    DisplayName = "Test Honky",
                    Email = "test2@dummy.local"
                }
            };

            BuildTheoryData("test2@dummy.local", expectedUserResponse, nameof(GetUserFromSpecificEmail));
        }


        private void BuildTheoryData(string? emailKeyword, List<UserInfo> expectedUserResponse, string description)
        {
            var theoryData = new GetUserTheory()
            {
                EmailKeyword = emailKeyword,
                ExpectedUserResponse = expectedUserResponse
            };

            Add(description, theoryData);
        }
    }
}