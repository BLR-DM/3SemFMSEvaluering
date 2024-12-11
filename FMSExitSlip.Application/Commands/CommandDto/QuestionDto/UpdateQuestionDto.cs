namespace FMSExitSlip.Application.Commands.CommandDto.QuestionDto;

public record UpdateQuestionDto
{
    public string Text { get; set; }
    public byte[] RowVersion { get; set; }
}