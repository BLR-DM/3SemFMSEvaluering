namespace FMSEvaluering.Application.Commands.CommandDto.PostDto;

public record CreatePostDto(string Description, string Solution, string AppUserId, string ForumId);