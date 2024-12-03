namespace FMSExitSlip.Application.Commands.CommandDto.QuestionDto;

public record DeleteQuestionDto
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; }
}