using ErrorOr;

namespace Queans.Domain.Votes.Errors
{
    public static class VoteDomainErrors
    {
        public static Error InvalidTargetIdentifierError = Error.NotFound(
            code: "General.NotFound", 
            description: "Invalid target identifier");
    }
}
