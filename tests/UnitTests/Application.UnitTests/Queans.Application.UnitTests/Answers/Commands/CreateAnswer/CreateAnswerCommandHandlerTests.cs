using ErrorOr;
using FluentAssertions;
using Moq;
using Queans.Application.Answers.Commands.CreateAnswer;
using Queans.Application.Common.Persistence;
using Queans.Application.UnitTests.Answers.TestUtilies;
using Queans.Domain.Questions;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Users;

namespace CreateAnswerCommandHandlerTests
{
    public class CreateAnswerCommandHandlerTests
    {
        private readonly CreateAnswerCommandHandler _handler;
        private readonly Mock<IQuestionRepository> _mockQuestionRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IAnswerRepository> _mockAnswerRepository;

        public CreateAnswerCommandHandlerTests()
        {
            _mockAnswerRepository = new Mock<IAnswerRepository>();
            _mockQuestionRepository = new Mock<IQuestionRepository>();
            _mockUserRepository = new Mock<IUserRepository>();

            _handler = new CreateAnswerCommandHandler(
                    _mockQuestionRepository.Object,
                    _mockUserRepository.Object,
                    _mockAnswerRepository.Object
                );
        }

        [Fact]
        public async Task HandleCreateAnswerCommand_ShouldReturnSuccess_WhenDataIsValid()
        {
            // Arrange
            // Get hold of a valid answer
            var user = User.Create("User Name", "user@gmail.com", "hashed_password", 0).Value;
            var question = Question.CreateQuestion(
                0,
                user,
                "Title",
                "Description",
                new List<Tag> { Tag.CreateTag("Tag Name").Value },
                DateTime.UtcNow).Value;

            var createAnswerCommand = new CreateAnswerCommand("Text", question.Id, user.Id);

            _mockQuestionRepository.Setup(r => r.GetQuestionByIdAsync(question.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(question);

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(user.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            // Invoke the handler
            var result = await _handler.Handle(createAnswerCommand, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeOfType<Success>();
            _mockAnswerRepository.Verify(r => r.AddAnswer(It.IsAny<Answer>()), Times.Once);
            _mockAnswerRepository.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void HandleCreateAnswerCommand_ShouldReturnError_WhenQuestionNotFound()
        {
            //Arrange


            //Act


            //Assert
        }

        [Fact]
        public void HandleCreateAnswerCommand_ShouldReturnError_WhenUserNotFound()
        {
            //Arrange


            //Act


            //Assert
        }

        [Fact]
        public void HandleCreateAnswerCommand_ShouldReturnError_WhenAnswerCreationFails()
        {
            //Arrange


            //Act


            //Assert
        }
    }
}
