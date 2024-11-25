using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Commands.CommandDto.ForumDto
{
    public record CreatePublicForumDto
    {
        public string Name { get; set; }
    }
    public record CreateClassForumDto : CreatePublicForumDto
    {
        public int ClassId { get; set; }
    }
    public record CreateSubjectForumDto : CreatePublicForumDto
    {
        public int SubjectId { get; set; }
    }

}
