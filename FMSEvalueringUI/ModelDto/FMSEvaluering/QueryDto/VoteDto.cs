namespace FMSEvalueringUI.ModelDto.FMSEvaluering.QueryDto
{
    public record VoteDto
    {
        public bool VoteType { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
