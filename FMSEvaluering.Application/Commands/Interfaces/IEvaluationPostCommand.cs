using FMSEvaluering.Application.Commands.CommandDto.EvaluationPostDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IEvaluationPostCommand
{
    Task CreateEvaluationPost(CreateEvaluationPostDto dto);
}