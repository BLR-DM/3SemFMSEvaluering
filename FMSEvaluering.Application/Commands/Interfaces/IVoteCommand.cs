using FMSEvaluering.Application.Commands.CommandDto.VoteDto;

namespace FMSEvaluering.Application.Commands.Interfaces
{
    public interface IVoteCommand
    {
        void CreateVote(CreateVoteDto vote);
    }
}