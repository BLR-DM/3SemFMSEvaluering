namespace FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.CommentDto
{
    public record UpdateCommentDto()
    {
        public string Text { get; set; }
        public byte[] RowVersion { get; set; }
    };
}