namespace FMSEvaluering.Application.Commands.CommandDto.VoteDto;

public record HandleVoteDto
{
    public bool VoteType { get; set; }
    public byte[] RowVersion { get; set; }
}