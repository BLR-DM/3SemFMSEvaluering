﻿using FMSExitSlip.Application.Queries.QueryDto;
using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Queries.Interfaces;

public interface IExitSlipQuery
{
    Task<ExitSlipDto> GetExitSlipAsync(int exitSlipId, string appUserId, string role);
    Task<IEnumerable<ExitSlipDto>> GetExitSlipsAsync(DateTime startDate, DateTime endDate, string appUserId, string role);
}