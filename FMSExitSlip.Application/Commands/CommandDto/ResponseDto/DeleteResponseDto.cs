namespace FMSExitSlip.Application.Commands.CommandDto.ResponseDto;

public record DeleteResponseDto (int ResponseId, byte[] RowVersion, int QuestionId);