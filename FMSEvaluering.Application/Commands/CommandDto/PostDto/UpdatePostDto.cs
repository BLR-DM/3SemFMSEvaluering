namespace FMSEvaluering.Application.Commands.CommandDto.PostDto;

public record UpdatePostDto(int PostId, string Content, byte[] RowVersion);