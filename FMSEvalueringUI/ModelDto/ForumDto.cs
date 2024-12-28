namespace FMSEvalueringUI.ModelDto
{
    public class ForumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ForumType { get; set; }
        public byte[] RowVersion { get; set; }
        public IEnumerable<PostDto> Posts { get; set; } = null!;
    }

    public record PostDto() {}
}