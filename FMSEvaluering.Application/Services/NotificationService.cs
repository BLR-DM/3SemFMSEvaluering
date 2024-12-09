using FMSEvaluering.Domain.Entities.ForumEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.MailService;
using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IMail _mail;
        private readonly ITeacherApplicationService _teacherApplicationService;

        public NotificationService(IEmailSender emailSender, IMail mail, ITeacherApplicationService teacherApplicationService)
        {
            _emailSender = emailSender;
            _mail = mail;
            _teacherApplicationService = teacherApplicationService;
        }
        async Task INotificationService.NotifyTeacherOnPostDesiredLikes(Forum forum, int upvotes)
        {
            if (forum is SubjectForum subjectForum)
            {
                var teacher = await _teacherApplicationService.GetTeacherForSubjectAsync(subjectForum.SubjectId.ToString());

                generateAndSendEmail(teacher, upvotes, forum);
            }

            if (forum is ClassForum classForum)
            {
                var teachers = await _teacherApplicationService.GetTeachersForClassAsync(classForum.ClassId.ToString());
                foreach(var teacher in teachers)
                {
                    generateAndSendEmail(teacher, upvotes, forum);
                }
            }

            if (forum is PublicForum)
            {
                var teachers = await _teacherApplicationService.GetTeachersAsync();
                foreach (var teacher in teachers)
                {
                    generateAndSendEmail(teacher, upvotes, forum);
                }
            }
        }

        void generateAndSendEmail(TeacherValue teacher, int upvotes, Forum forum)
        {
            var message = _mail.GetMessagePostOverDesiredLikes(teacher.FirstName, upvotes, forum.Name);
            _emailSender.SendEmail(teacher.Email, message);
        }
    }
}
