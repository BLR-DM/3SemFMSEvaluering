namespace FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.VoteDto;

public record HandleVoteDto
{
    public bool VoteType { get; set; }
    public byte[] RowVersion { get; set; }
}