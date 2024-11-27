using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.CommandDto.QuestionDto;
using FMSExitSlip.Application.Commands.CommandDto.ResponseDto;

namespace FMSExitSlip.Application.Commands.Interfaces;

public interface IExitSlipCommand
{
    Task AddQuestion(CreateQuestionDto questionDto, string appUserId);
    Task CreateExitSlipAsync(CreateExitSlipDto exitSlipDto, string appUserId);
    Task AddResponseAsync(CreateResponseDto responseDto, int exitSlipId);
    Task UpdateResponseAsync(UpdateResponseDto updateResponseDto, int exitSlipId);
}