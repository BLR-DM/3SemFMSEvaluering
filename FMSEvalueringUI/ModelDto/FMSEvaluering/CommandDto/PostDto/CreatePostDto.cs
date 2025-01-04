namespace FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.PostDto;

public record CreatePostDto
{
    public string Description { get; set; }
    public string Solution { get; set; }
}