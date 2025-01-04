namespace FMSEvalueringUI.ModelDto.FMSEvaluering.QueryDto
{
    public record CommentDto
    {
        public int Id { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Text { get; set; }
        public string CreatedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public string AppUserId { get; set; }
    }
}
