using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Application.Services;

public interface INotificationService
{
    void NotifyTeacherOnPostDesiredLikes(Forum forum, int upvotes);
}