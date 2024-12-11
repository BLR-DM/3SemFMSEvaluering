using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.CommandDto.QuestionDto;
using FMSExitSlip.Application.Commands.CommandDto.ResponseDto;

namespace FMSExitSlip.Application.Commands.Interfaces;

public interface IExitSlipCommand
{
    Task AddQuestionAsync(CreateQuestionDto questionDto, int exitSlipId, string appUserId, string role);
    Task UpdateQuestionAsync(UpdateQuestionDto questionDto, int exitSlipId, int questionId, string appUserId, string role);
    Task DeleteQuestionAsync(DeleteQuestionDto questionDto, int exitSlipId, int questionId, string appUserId , string role);
    Task CreateExitSlipAsync(CreateExitSlipDto exitSlipDto);
    Task CreateResponseAsync(CreateResponseDto responseDto, int exitSlipId, int questionId, string appUserId, string role);
    Task UpdateResponseAsync(UpdateResponseDto responseDto, int exitSlipId, int responseId, int questionId, string appUserId, string role);
    Task DeleteResponseAsync(DeleteResponseDto responseDto, int exitSlipId, int responseId, int questionId, string appUserId, string role);
    Task PublishExitSlip(int id, string appUserId, PublishExitSlipDto exitSlipDto, string role);
}