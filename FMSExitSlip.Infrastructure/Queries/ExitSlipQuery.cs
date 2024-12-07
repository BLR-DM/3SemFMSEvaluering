﻿using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Queries.Interfaces;
using FMSExitSlip.Application.Queries.QueryDto;
using Microsoft.EntityFrameworkCore;

namespace FMSExitSlip.Infrastructure.Queries;

public class ExitSlipQuery : IExitSlipQuery
{
    private readonly ExitSlipContext _db;
    private readonly IServiceProvider _serviceProvider;
    private readonly IExitSlipAccessHandler _exitSlipAccessHandler;

    public ExitSlipQuery(ExitSlipContext db, IServiceProvider serviceProvider, IExitSlipAccessHandler exitSlipAccessHandler)
    {
        _db = db;
        _serviceProvider = serviceProvider;
        _exitSlipAccessHandler = exitSlipAccessHandler;
    }

    async Task<ExitSlipDto> IExitSlipQuery.GetExitSlipAsync(int exitSlipId, string appUserId, string role)
    {
        var exitSlip = await _db.ExitSlips
            .AsNoTracking()
            .Include(e => e.Questions)
            .ThenInclude(q => q.Responses)
            .SingleOrDefaultAsync(e => e.Id == exitSlipId);

        if (exitSlip == null)
            throw new InvalidOperationException("Not found");

        // Validate Access
        await _exitSlipAccessHandler.ValidateExitslipAccess(appUserId, role, exitSlip);


        var exitSlipDto = new ExitSlipDto
        {
            Id = exitSlip.Id,
            AppUserId = exitSlip.AppUserId,
            IsPublished = exitSlip.IsPublished,
            LectureId = exitSlip.LectureId,
            MaxQuestions = exitSlip.MaxQuestions,
            RowVersion = exitSlip.RowVersion,
            Title = exitSlip.Title,
            Questions = exitSlip.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                RowVersion = q.RowVersion,
                Text = q.Text,
                AppUserId = q.AppUserId,
                Responses = q.Responses.Select(r => new ResponseDto
                {
                    Id = r.Id,
                    RowVersion = r.RowVersion,
                    Text = r.Text,
                    AppUserId = r.AppUserId
                })
            })
        };

        return exitSlipDto;
    }
}