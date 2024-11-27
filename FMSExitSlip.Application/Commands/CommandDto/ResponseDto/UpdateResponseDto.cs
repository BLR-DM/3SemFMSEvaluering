namespace FMSExitSlip.Application.Commands.CommandDto.ResponseDto;

public record UpdateResponseDto (int ResponseId, string Text, int QuestionId, byte[] RowVersion);