using FMSEvaluering.Application.Commands.CommandDto.PostDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreateEvaluationPost(CreatePostDto dto);
}