using ErrorOr;
using FluentAssertions;
using Moq;
using Queans.Application.Common.DTOs;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Application.Common.Services;
using Queans.Application.UnitTests.Users.TestUtils;
using Queans.Application.Users.Commands.RegisterUser;
using Queans.Domain.Users;
using Queans.Domain.Users.Entities;

namespace Queans.Application.UnitTests.Users.Commands.RegisterUser
{
    public class RegisterUserCommandTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly RegisterUserCommandHandler _handler;

        public RegisterUserCommandTests()
        {
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new RegisterUserCommandHandler(
                    _mockUserRepository.Object,
                    _mockPasswordHasher.Object
                );
        }

        [Fact]
        public async Task Handle_ShouldReturnUserDto_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var command = RegisterUserCommandUtils.CreateCommand();
            Role fakeRole = new Role(0);

            var user = User.Create(command.UserName, command.UserEmail, command.Password, 0).Value;

            _mockUserRepository.Setup(r => r.IsUserExistAsync(command.UserEmail, command.UserName, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mockPasswordHasher.Setup(h => h.GenerateHashedPassword(command.Password))
                .Returns(command.Password);

            _mockUserRepository.Setup(r => r.GetUserRoleAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeRole);

            _mockUserRepository.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeOfType<UserDto>();
            result.Value.UserName.Should().Be(command.UserName);
            result.Value.Email.Should().Be(command.UserEmail);
            result.Value.Rating.Should().Be(0);

            _mockUserRepository.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenUserExist()
        {
            // Arrange
            var command = RegisterUserCommandUtils.CreateCommand();
            Role fakeRole = new Role(0);

            var user = User.Create("User", "Name", "user@gmail.com", 0).Value;

            _mockUserRepository.Setup(r => r.IsUserExistAsync(
                command.UserEmail,
                command.UserName,
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().NotBeEmpty();

        }
    }
}
