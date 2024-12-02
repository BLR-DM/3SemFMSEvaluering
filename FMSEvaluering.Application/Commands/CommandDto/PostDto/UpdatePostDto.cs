namespace FMSEvaluering.Application.Commands.CommandDto.PostDto;

public record UpdatePostDto(int PostId, string Description, string Solution, byte[] RowVersion);