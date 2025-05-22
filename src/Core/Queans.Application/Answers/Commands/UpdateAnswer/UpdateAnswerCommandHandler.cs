using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;

namespace Queans.Application.Answers.Commands.UpdateAnswer
{
    public class UpdateAnswerCommandHandler : ICommandHandler<UpdateAnswerCommand, ErrorOr<Success>>
    {
        private readonly IAnswerRepository _answerRepository;

        public UpdateAnswerCommandHandler(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var (answerId, text) = request;
            
            var answer = await _answerRepository.GetAnswerByIdAsync(answerId, cancellationToken);
            if (answer is null)
            {
                return ApplicationErrors.NotFoundAnswerError;
            }

            var changingTextResult = answer.ChangeText(text);
            if (changingTextResult.IsError)
            {
                return changingTextResult.Errors;
            }

            _answerRepository.UpdateAnswer(answer);
            await _answerRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}
