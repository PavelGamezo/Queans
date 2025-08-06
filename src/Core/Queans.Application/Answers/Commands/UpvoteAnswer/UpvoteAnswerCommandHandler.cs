using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Domain.Questions;
using Queans.Domain.Votes.Entities;
using Queans.Domain.Votes.Enums;

namespace Queans.Application.Answers.Commands.UpvoteAnswer
{
    public class UpvoteAnswerCommandHandler : ICommandHandler<UpvoteAnswerCommand, ErrorOr<Success>>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IVoteRepository _voteRepository;

        public UpvoteAnswerCommandHandler(
            IAnswerRepository answerRepository,
            IVoteRepository voteRepository)
        {
            _answerRepository = answerRepository;
            _voteRepository = voteRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpvoteAnswerCommand request, CancellationToken cancellationToken)
        {
            var (answerId, userId) = request;

            var answer = await _answerRepository.GetAnswerByIdAsync(answerId, cancellationToken);
            if (answer is null)
            {
                return ApplicationErrors.NotFoundAnswerError;
            }

            var user = answer.Author;
            if (userId != user.Id)
            {
                return ApplicationErrors.NotFoundUser;
            }

            var isVoteExist = await _voteRepository.IsVoteExistAsync(
                userId,
                answerId,
                VoteTargetType.Answer,
                cancellationToken);
            if (isVoteExist)
            {
                return ApplicationErrors.UserAlreadyVotedError;
            }

            var voteFactoryResult = Vote.CreateVote(user, answerId, VoteTargetType.Answer, VoteType.Upvote);
            if (voteFactoryResult.IsError)
            {
                var errors = voteFactoryResult.Errors;

                return errors;
            }

            var vote = voteFactoryResult.Value;

            _voteRepository.AddVote(vote);

            answer.ApplyVote(VoteType.Upvote);

            await _voteRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}
