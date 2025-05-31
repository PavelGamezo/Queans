using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Domain.Votes.Entities;
using Queans.Domain.Votes.Enums;

namespace Queans.Application.Questions.Commands.UpvoteQuestion
{
    public class UpvoteQuestionCommandHandler : ICommandHandler<UpvoteQuestionCommand, ErrorOr<Success>>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IQuestionRepository _questionRepository;

        public UpvoteQuestionCommandHandler(
            IVoteRepository voteRepository,
            IQuestionRepository questionRepository)
        {
            _voteRepository = voteRepository;
            _questionRepository = questionRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpvoteQuestionCommand request, CancellationToken cancellationToken)
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
                userId, questionId, VoteTargetType.Question, cancellationToken);

            if (!isVoteExist)
            {
                return ApplicationErrors.UserAlreadyVotedError;
            }

            var voteFactoryResult = Vote.CreateVote(user, question.Id, VoteTargetType.Question, VoteType.Upvote);
            if (voteFactoryResult.IsError)
            {
                return voteFactoryResult.Errors;
            }
            
            var vote = voteFactoryResult.Value;

            _voteRepository.AddVote(vote);

            question.ApplyVote(VoteType.Upvote);

            await _voteRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}
