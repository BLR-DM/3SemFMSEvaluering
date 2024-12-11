namespace FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;

public record PublishExitSlipDto
{
    public byte[] RowVersion { get; set; }
}