using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.DomainServices;
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
            var studentDomainService = _serviceProvider.GetRequiredService<IStudentDomainService>();
            var studentDto = await studentDomainService.GetStudentAsync(appUserId);

            validatedForums.AddRange(forums.Where(forum => forum.ValidateStudentAccessAsync(studentDto)));
        }
        else if (role == "teacher")
        {
            var teacherDomainService = _serviceProvider.GetRequiredService<ITeacherDomainService>();
            var teacherDto = await teacherDomainService.GetTeacherAsync(appUserId);

            validatedForums.AddRange(forums.Where(forum => forum.ValidateTeacherAccessAsync(teacherDto)));
        }

        return validatedForums;
    }

    async Task<bool> IForumAccessHandler.ValidateAccessSingleForumAsync(string appUserId, string role, Forum forum)
    {
        if (role == "student")
        {
            var studentDomainService = _serviceProvider.GetRequiredService<IStudentDomainService>();
            var studentDto = await studentDomainService.GetStudentAsync(appUserId);

            return forum.ValidateStudentAccessAsync(studentDto);
        }
        else if (role == "teacher")
        {
            var teacherDomainService = _serviceProvider.GetRequiredService<ITeacherDomainService>();
            var teacherDto = await teacherDomainService.GetTeacherAsync(appUserId);

            return forum.ValidateTeacherAccessAsync(teacherDto);
        }

        return false;
    }
}