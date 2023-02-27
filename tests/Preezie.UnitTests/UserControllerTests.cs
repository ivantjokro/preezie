using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Preezie.Application;
using Preezie.Presentation.Controllers;
using Preezie.Presentation.Model;
using Preezie.UnitTests.Fixtures;
using Preezie.UnitTests.TestDoubles;
using Preezie.UnitTests.Theories;
using Xunit;

namespace Preezie.UnitTests
{
    public class UserControllerTests
    {
        public class CreateUser
        {
            [Fact]
            public async void Should_Successfully_Add_User()
            {
                var dummyEmail = "test-success@email.local";

                var expectedUserMediatorLogMessages = new List<string>()
                {
                    $"Added user {dummyEmail} with ID 1."
                };

                var expectedUserResponse = new UserResponse()
                {
                    Id = 1
                };

                var customConfiguration = new Dictionary<string, string>();

                IConfiguration configuration = UnitTestFactory.CreateTestConfiguration(customConfiguration);

                var userMediatorLogger = new LoggerStub<UserMediator>();

                UserController sut = new UserControllerFixture()
                    .WithLogger(userMediatorLogger)
                    .CreateSut(configuration);

                var userRequest = new CreateUserRequest()
                {
                    DisplayName = "Test",
                    Email = dummyEmail,
                    Password = "Password654*"
                };

                var response = await sut.CreateUser(userRequest, CancellationToken.None).ConfigureAwait(false);

                var okResult = response as OkObjectResult;

                okResult.Should().NotBeNull();

                var userResponse = okResult.Value as UserResponse;

                okResult.StatusCode.Should().Be(200);

                userResponse.Should().BeEquivalentTo(expectedUserResponse);

                userMediatorLogger.LogEntries.Select(log => log.State.ToString())
                    .Should().BeEquivalentTo(expectedUserMediatorLogMessages);
            }

            [Fact]
            public async void Should_Throw_Bad_Request()
            {
                var expectedResponseMessage = "All fields are mandatory.";

                var customConfiguration = new Dictionary<string, string>();

                IConfiguration configuration = UnitTestFactory.CreateTestConfiguration(customConfiguration);

                var userMediatorLogger = new LoggerStub<UserMediator>();

                UserController sut = new UserControllerFixture()
                    .WithLogger(userMediatorLogger)
                    .CreateSut(configuration);

                var userRequest = new CreateUserRequest();

                var response = await sut.CreateUser(userRequest, CancellationToken.None).ConfigureAwait(false);

                var result = response as ObjectResult;

                result.Should().NotBeNull();

                var responseMessage = result.Value as string;

                result.StatusCode.Should().Be(400);

                responseMessage.Should().BeEquivalentTo(expectedResponseMessage);
            }
        }

        public class UpdateUser
        {
            [Fact]
            public async void Should_Successfully_Update_User()
            {
                var dummyName = "Testname";
                var dummyEmail = "test@email.local";
                var modifiedName = "Modified Testname";

                var expectedUserMediatorLogMessages = new List<string>()
                {
                    $"Added user {dummyEmail} with ID 1.",
                    "Updating user with ID 1.",
                    "Updated user with ID 1."
                };

                var expectedUserResponse = new UserResponse()
                {
                    Id = 1,
                    DisplayName = modifiedName,
                    Email = dummyEmail
                };

                var customConfiguration = new Dictionary<string, string>();

                IConfiguration configuration = UnitTestFactory.CreateTestConfiguration(customConfiguration);

                var userMediatorLogger = new LoggerStub<UserMediator>();

                UserController sut = new UserControllerFixture()
                    .WithLogger(userMediatorLogger)
                    .CreateSut(configuration);

                var userRequest = new CreateUserRequest()
                {
                    DisplayName = dummyName,
                    Email = "test@email.local",
                    Password = "Password654*"
                };

                var usermodifiedRequest = new CreateUserRequest()
                {
                    DisplayName = modifiedName,
                    Password = "Password987*"
                };

                // Creating a user
                await sut.CreateUser(userRequest, CancellationToken.None).ConfigureAwait(false);

                // Update a user stat
                var response = await sut.UpdateUser(1, usermodifiedRequest, CancellationToken.None).ConfigureAwait(false);

                var okResult = response as OkObjectResult;

                okResult.Should().NotBeNull();

                var userResponse = okResult.Value as UserResponse;

                okResult.StatusCode.Should().Be(200);

                userResponse.Should().BeEquivalentTo(expectedUserResponse);

                userMediatorLogger.LogEntries.Select(log => log.State.ToString())
                    .Should().BeEquivalentTo(expectedUserMediatorLogMessages);
            }
        }

        public class GetUsers
        {
            [Theory, ClassData(typeof(GetUsersTheories))]
            public async void Should_Successfully_Return_Users_List(string description, GetUserTheory theoryData)
            {
                var customConfiguration = new Dictionary<string, string>();

                IConfiguration configuration = UnitTestFactory.CreateTestConfiguration(customConfiguration);

                var userMediatorLogger = new LoggerStub<UserMediator>();

                UserController sut = new UserControllerFixture()
                    .WithLogger(userMediatorLogger)
                    .CreateSut(configuration);

                await sut.CreateUser(new CreateUserRequest()
                {
                    DisplayName = "Test Dummy",
                    Email = "test1@dummy.local",
                    Password = "Password654*"
                }, CancellationToken.None).ConfigureAwait(false);
                await sut.CreateUser(new CreateUserRequest()
                {
                    DisplayName = "Test Honky",
                    Email = "test2@dummy.local",
                    Password = "Password312*"
                }, CancellationToken.None).ConfigureAwait(false);
                await sut.CreateUser(new CreateUserRequest()
                {
                    DisplayName = "Test",
                    Email = "test3@dummy.local",
                    Password = "Password678*"
                }, CancellationToken.None).ConfigureAwait(false);

                var response = await sut.GetUsers(CancellationToken.None, theoryData.EmailKeyword);

                var okResult = response as OkObjectResult;

                okResult.StatusCode.Should().Be(200);

                var userResponse = okResult.Value as IEnumerable<UserInfo>;

                userResponse.Should().NotBeNull();
                userResponse.ToList().Should().HaveCount(theoryData.ExpectedUserResponse.Count);
                userResponse.ToList().Should().BeEquivalentTo(theoryData.ExpectedUserResponse);
            }
        }
    }
}