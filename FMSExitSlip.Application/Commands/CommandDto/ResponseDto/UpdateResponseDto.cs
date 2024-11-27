namespace FMSExitSlip.Application.Commands.CommandDto.ResponseDto;

public record UpdateResponseDto (int ResponseId, string Text, string AppUserId, int QuestionId, byte[] RowVersion);