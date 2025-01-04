namespace FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.PostDto;

public record UpdatePostDto(string Description, string Solution, byte[] RowVersion);