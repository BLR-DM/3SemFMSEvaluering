namespace FMSExitSlip.Application.Commands.CommandDto.ResponseDto;

public record DeleteResponseDto (int ResponseId, string AppUserId, byte[] RowVersion, int QuestionId);