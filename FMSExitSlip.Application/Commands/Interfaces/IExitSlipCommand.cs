using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.CommandDto.QuestionDto;
using FMSExitSlip.Application.Commands.CommandDto.ResponseDto;

namespace FMSExitSlip.Application.Commands.Interfaces;

public interface IExitSlipCommand
{
    Task AddQuestion(CreateQuestionDto questionDto, string appUserId);
    Task UpdateQuestion(UpdateQuestionDto questionDto, int exitSlipId);
    Task CreateExitSlipAsync(CreateExitSlipDto exitSlipDto, string appUserId);
    Task CreateResponseAsync(CreateResponseDto responseDto, int exitSlipId);
    Task UpdateResponseAsync(UpdateResponseDto responseDto, int exitSlipId);
    Task DeleteResponseAsync(DeleteResponseDto responseDto, int exitSlipId);
    Task PublishExitSlip(int id, string appUserId);
}