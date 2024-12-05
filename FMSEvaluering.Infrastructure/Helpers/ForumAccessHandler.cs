using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Application.Services;
using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Infrastructure.Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Infrastructure.Helpers;

public class ForumAccessHandler : IForumAccessHandler
{
    private readonly IForumMapper _forumMapper;
    private readonly IServiceProvider _serviceProvider;

    public ForumAccessHandler(IForumMapper forumMapper, IServiceProvider serviceProvider)
    {
        _forumMapper = forumMapper;
        _serviceProvider = serviceProvider;
    }

    async Task<IEnumerable<Forum>> IForumAccessHandler.ValidateAccessMultipleForumsAsync(string appUserId, string role, List<Forum> forums)
    {
        List<Forum> validatedForums = new List<Forum>();

        if (role == "student")
        {
            var studentDomainService = _serviceProvider.GetRequiredService<IStudentApplicationService>();
            var studentDto = await studentDomainService.GetStudentAsync(appUserId);

            validatedForums.AddRange(forums.Where(forum => forum.ValidateStudentAccessAsync(studentDto)));
        }
        else if (role == "teacher")
        {
            var teacherDomainService = _serviceProvider.GetRequiredService<ITeacherApplicationService>();
            var teacherDto = await teacherDomainService.GetTeacherAsync(appUserId);

            validatedForums.AddRange(forums.Where(forum => forum.ValidateTeacherAccessAsync(teacherDto)));
        }

        if (validatedForums.Count <= 0)
        {
            throw new UnauthorizedAccessException("You do not have access");
        }

        return validatedForums;
    }

    async Task IForumAccessHandler.ValidateAccessSingleForumAsync(string appUserId, string role, Forum forum)
    {
        bool hasAccess = false;

        if (role == "student")
        {
            var studentDomainService = _serviceProvider.GetRequiredService<IStudentApplicationService>();
            var studentDto = await studentDomainService.GetStudentAsync(appUserId);

            hasAccess = forum.ValidateStudentAccessAsync(studentDto);
        }
        else if (role == "teacher")
        {
            var teacherDomainService = _serviceProvider.GetRequiredService<ITeacherApplicationService>();
            var teacherDto = await teacherDomainService.GetTeacherAsync(appUserId);

            hasAccess = forum.ValidateTeacherAccessAsync(teacherDto);
        }

        if (!hasAccess)
        {
            throw new UnauthorizedAccessException("You do not have access");
        }

    }
}