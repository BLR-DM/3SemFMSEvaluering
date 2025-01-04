namespace FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.PostDto;

public class UpdatePostDto()
{
    public string Description { get; set; }
    public string Solution { get; set; }
    public byte[] RowVersion { get; set; }
}