using FMSExitSlip.Application.Queries.QueryDto;

namespace FMSExitSlip.Application.Queries.Interfaces;

public interface IExitSlipQuery
{
    Task<ExitSlipDto> GetExitSlipAsync(int exitSlipId, string appUserId, string role);
}