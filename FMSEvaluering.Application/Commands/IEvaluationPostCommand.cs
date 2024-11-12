using FMSEvaluering.Application.Commands.CommandDto.EvaluationPostDto;

namespace FMSEvaluering.Application.Commands;

public interface IEvaluationPostCommand
{
    Task CreateEvaluationPost(CreateEvaluationPostDto dto);
}