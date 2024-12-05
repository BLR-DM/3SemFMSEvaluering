namespace FMSEvaluering.Application.Commands.CommandDto.PostDto;

public record UpdatePostDto(string Description, string Solution, byte[] RowVersion);