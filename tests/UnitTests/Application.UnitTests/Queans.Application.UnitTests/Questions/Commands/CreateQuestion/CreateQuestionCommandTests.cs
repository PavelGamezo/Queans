using FluentAssertions;
using Moq;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Application.Questions.Commands.CreateQuestion;
using Queans.Application.UnitTests.Questions.TestUtils;
using Queans.Domain.Questions;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Users;

namespace Queans.Application.UnitTests.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandTests
    {
        private readonly Mock<IQuestionRepository> _mockQuestionRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ITagRepository> _mockTagRepository;
        private readonly CreateQuestionCommandHandler _handler;

        public CreateQuestionCommandTests()
        {
            _mockQuestionRepository = new Mock<IQuestionRepository>();
            _mockTagRepository = new Mock<ITagRepository>();
            _mockUserRepository = new Mock<IUserRepository>();

            _handler = new CreateQuestionCommandHandler(
                _mockQuestionRepository.Object,
                _mockUserRepository.Object,
                _mockTagRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenUserNotFound()
        {
            // Arrange
            var command = CreateQuestionCommandUtils.CreateCommand();

            var user = User.Create(
                UnitTests.TestUtils.Constants.Constants.User.UserName,
                UnitTests.TestUtils.Constants.Constants.User.UserEmail,
                UnitTests.TestUtils.Constants.Constants.User.Password,
                0).Value;

            _mockUserRepository.Setup(q => q.GetUserByIdAsync(
                UnitTests.TestUtils.Constants.Constants.User.UserId,
                It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)default);

            // Act
            var result = await _handler.Handle(command, default);

            // Assets
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenTagNotFound()
        {
            // Arrange
            var command = CreateQuestionCommandUtils.CreateCommand();

            var user = User.Create(
                UnitTests.TestUtils.Constants.Constants.User.UserName,
                UnitTests.TestUtils.Constants.Constants.User.UserEmail,
                UnitTests.TestUtils.Constants.Constants.User.Password,
                0).Value;

            _mockUserRepository.Setup(q => q.GetUserByIdAsync(
                UnitTests.TestUtils.Constants.Constants.User.UserId,
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _mockTagRepository.Setup(q => q.GetExistingTags(
                UnitTests.TestUtils.Constants.Constants.Tag.CreateUtilTagList(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Tag>());

            // Act
            var result = await _handler.Handle(command, default);

            // Assets
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(ApplicationErrors.NotFoundTagError);
        }

        [Fact]
        public async Task Handle_ShouldReturnDomainError_WhenDataIsNotValid()
        {
            // Arrange
            var command = CreateQuestionCommandUtils.CreateCommand();

            var user = User.Create(
                UnitTests.TestUtils.Constants.Constants.User.UserName,
                UnitTests.TestUtils.Constants.Constants.User.UserEmail,
                UnitTests.TestUtils.Constants.Constants.User.Password,
                0).Value;

            var question = Question.CreateQuestion(
                0,
                user,
                UnitTests.TestUtils.Constants.Constants.Question.Title,
                UnitTests.TestUtils.Constants.Constants.Question.Description,
                new List<Tag>(),
                DateTime.UtcNow).Value;

            _mockUserRepository.Setup(q => q.GetUserByIdAsync(
                UnitTests.TestUtils.Constants.Constants.User.UserId,
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _mockTagRepository.Setup(q => q.GetExistingTags(
                UnitTests.TestUtils.Constants.Constants.Tag.CreateUtilTagList(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Tag>
                    {
                        Tag.CreateTag(UnitTests.TestUtils.Constants.Constants.Tag.Name).Value
                    });

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.IsError.Should().BeFalse();
        }
    }
}
