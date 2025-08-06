using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Domain.Votes.Entities;
using Queans.Domain.Votes.Enums;

namespace Queans.Application.Questions.Commands.DownvoteQuestion
{
    public class DownvoteQuestionCommandHandler : ICommandHandler<DownvoteQuestionCommand, ErrorOr<Success>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IVoteRepository _voteRepository;

        public DownvoteQuestionCommandHandler(
            IQuestionRepository questionRepository,
            IVoteRepository voteRepository)
        {
            _questionRepository = questionRepository;
            _voteRepository = voteRepository;
        }

        public async Task<ErrorOr<Success>> Handle(DownvoteQuestionCommand request, CancellationToken cancellationToken)
        {
            var (questionId, userId) = request;
            
            var question = await _questionRepository.GetQuestionByIdAsync(questionId, cancellationToken);
            if (question is null)
            {
                return ApplicationErrors.NotFoundQuestionError;
            }

            var user = question.Author;
            if (userId != user.Id)
            {
                return ApplicationErrors.NotFoundUser;
            }

            var isVoteExist = await _voteRepository.IsVoteExistAsync(
                userId,
                questionId,
                VoteTargetType.Question,
                cancellationToken);

            if (isVoteExist)
            {
                return ApplicationErrors.UserAlreadyVotedError;
            }

            var voteFactoryResult = Vote.CreateVote(user, questionId, VoteTargetType.Question, VoteType.Downvote);
            if (voteFactoryResult.IsError)
            {
                return voteFactoryResult.Errors;
            }

            var vote = voteFactoryResult.Value;

            _voteRepository.AddVote(vote);

            question.ApplyVote(VoteType.Downvote);

            await _voteRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}
