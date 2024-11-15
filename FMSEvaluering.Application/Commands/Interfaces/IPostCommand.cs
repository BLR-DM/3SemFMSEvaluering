using FMSEvaluering.Application.Commands.CommandDto.PostDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreatePost(CreatePostDto dto);
}