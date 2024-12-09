using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.MailService;
using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Infrastructure.MailService
{
    public class Mail : IMail
    {
        public string GetMessagePostOverDesiredLikes(string teacherFirstName, int likes, string forumName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Kaere {teacherFirstName}");
            sb.AppendLine($"Et post på {forumName} har opnaaet {likes} likes.");
            sb.AppendLine("Og er nu tilgaengligt for dig");
            sb.AppendLine("Med Venlig hilsen");
            sb.AppendLine("FMSEvaluering");

            return sb.ToString();
        }
    }

}
