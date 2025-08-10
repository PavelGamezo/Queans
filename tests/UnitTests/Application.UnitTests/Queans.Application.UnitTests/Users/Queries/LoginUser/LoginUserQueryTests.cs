using FluentAssertions;
using Moq;
using Queans.Application.Common.Authentications;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Application.Common.Services;
using Queans.Application.UnitTests.Users.TestUtils;
using Queans.Application.Users.Queries.LoginUser;
using Queans.Domain.Users;

namespace Queans.Application.UnitTests.Users.Queries.LoginUser
{
    public class LoginUserQueryTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly LoginUserQueryHandler _handler;

        public LoginUserQueryTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();

            _handler = new LoginUserQueryHandler(
                _mockUserRepository.Object,
                _mockJwtTokenGenerator.Object,
                _mockPasswordHasher.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenUserNotFound()
        {
            // Arrange
            var query = LoginUserQueryUtils.CreateQuery();

            _mockUserRepository.Setup(r => r.GetUserByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(User?));

            // Act
            var result = await _handler.Handle(query, default);

            // Access
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(ApplicationErrors.NotFoundUser);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenPasswordIsNotValid()
        {
            // Arrange
            var query = LoginUserQueryUtils.CreateQuery();

            _mockPasswordHasher.Setup(q => q.VerifyHashedPassword(
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(false);

            // Act
            var result = await _handler.Handle(query, default);

            // Access
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(ApplicationErrors.NotFoundUser);
        }

        [Fact]
        public async Task Handle_ShouldReturnString_WhenDataIsValid()
        {
            // Arrange
            var query = LoginUserQueryUtils.CreateQuery();

            var user = User.Create(
                UnitTests.TestUtils.Constants.Constants.User.UserName,
                query.Email,
                query.Password,
                0).Value;

            _mockUserRepository.Setup(q => q.GetUserByEmailAsync(
                query.Email,
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _mockPasswordHasher.Setup(q => q.VerifyHashedPassword(
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(true);

            // Act
            var result = await _handler.Handle(query, default);

            // Access
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeEmpty();
        }
    }
}
