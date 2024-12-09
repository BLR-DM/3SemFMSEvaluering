namespace FMSEvaluering.Application.MailService;

public interface IMail
{
    string GetMessagePostOverDesiredLikes(string teacherFirstName, int likes, string forumName);
}