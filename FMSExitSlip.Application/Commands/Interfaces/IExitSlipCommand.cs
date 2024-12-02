using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.CommandDto.QuestionDto;
using FMSExitSlip.Application.Commands.CommandDto.ResponseDto;

namespace FMSExitSlip.Application.Commands.Interfaces;

public interface IExitSlipCommand
{
    Task AddQuestionAsync(CreateQuestionDto questionDto, string appUserId);
    Task UpdateQuestionAsync(UpdateQuestionDto questionDto, int exitSlipId);
    Task CreateExitSlipAsync(CreateExitSlipDto exitSlipDto, string appUserId);
    Task CreateResponseAsync(CreateResponseDto responseDto, int exitSlipId);
    Task UpdateResponseAsync(UpdateResponseDto responseDto, int exitSlipId);
    Task DeleteResponseAsync(DeleteResponseDto responseDto, int exitSlipId);
    Task PublishExitSlip(int id, string appUserId, PublishExitSlipDto exitSlipDto);
}