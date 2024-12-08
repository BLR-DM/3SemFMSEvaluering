using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Queries.Interfaces;
using FMSExitSlip.Application.Queries.QueryDto;
using FMSExitSlip.Application.Services;
using FMSExitSlip.Application.Services.ProxyInterface;
using FMSExitSlip.Domain.Values.DataServer;
using Microsoft.EntityFrameworkCore;

namespace FMSExitSlip.Infrastructure.Queries;

public class ExitSlipQuery : IExitSlipQuery
{
    private readonly ExitSlipContext _db;
    private readonly IExitSlipAccessHandler _exitSlipAccessHandler;
    private readonly ITeacherApplicationService _teacherApplicationService;

    public ExitSlipQuery(ExitSlipContext db, IExitSlipAccessHandler exitSlipAccessHandler, ITeacherApplicationService teacherApplicationService)
    {
        _db = db;
        _exitSlipAccessHandler = exitSlipAccessHandler;
        _teacherApplicationService = teacherApplicationService;
    }

    async Task<ExitSlipDto> IExitSlipQuery.GetExitSlipAsync(int exitSlipId, string appUserId, string role)
    {
        var exitSlip = await _db.ExitSlips
            .AsNoTracking()
            .Include(e => e.Questions)
            .ThenInclude(q => q.Responses)
            .SingleOrDefaultAsync(e => e.Id == exitSlipId);

        if (exitSlip == null)
            throw new InvalidOperationException("Exitslip not found");

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

    async Task<IEnumerable<ExitSlipDto>> IExitSlipQuery.GetExitSlipsAsync(RequestLectureDto requestDto, string role)
    {
        // GET teacher 
        var teacher = await _teacherApplicationService.GetTeacherAsync(requestDto.TeacherAppUserId);

        var lectures = teacher.TeacherSubjects
            .Where(ts => ts.Class.Id == requestDto.ClassId.ToString() && ts.Subject.Id == requestDto.SubjectId)
            .SelectMany(ts => ts.Lectures
            .Where(l => l.Date >= requestDto.StartDate && l.Date <= requestDto.EndDate));
        
        var exitSlips = _db.ExitSlips
            .AsNoTracking()
            .Include(e => e.Questions)
                .ThenInclude(q => q.Responses)
            .Where(e =>
                lectures.Any(l => l.Id == e.LectureId.ToString()));

        if (exitSlips is null || !exitSlips.Any())
            throw new InvalidOperationException("Exitslips not found");

        // Validate Access
        //await _exitSlipAccessHandler.ValidateExitslipAccess(appUserId, role, exitSlips.First()); // test


        var exitSlipsDto = exitSlips.Select(exitSlip => new ExitSlipDto
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
        });

        return exitSlipsDto;
    }
}