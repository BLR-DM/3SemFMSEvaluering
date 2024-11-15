using FMSEvaluering.Application.Commands.CommandDto.EvaluationPostDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreateEvaluationPost(CreatePostDto dto);
}