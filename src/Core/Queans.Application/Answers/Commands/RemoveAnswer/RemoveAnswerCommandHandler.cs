using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;

namespace Queans.Application.Answers.Commands.RemoveAnswer
{
    public class RemoveAnswerCommandHandler : ICommandHandler<RemoveAnswerCommand, ErrorOr<Success>>
    {
        private readonly IAnswerRepository _answerRepository;

        public RemoveAnswerCommandHandler(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<ErrorOr<Success>> Handle(RemoveAnswerCommand request, CancellationToken cancellationToken)
        {
            var answerId = request.AnswerId;

            var answer = await _answerRepository.GetAnswerByIdAsync(answerId, cancellationToken);
            
            if (answer is null)
            {
                return ApplicationErrors.NotFoundAnswerError;
            }

            _answerRepository.DeleteAnswer(answer);
            await _answerRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}
