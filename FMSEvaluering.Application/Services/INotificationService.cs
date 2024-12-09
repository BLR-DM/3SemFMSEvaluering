using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Application.Services;

public interface INotificationService
{
    Task NotifyTeacherOnPostDesiredLikes(Forum forum, int upvotes);
}