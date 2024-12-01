using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Queries.QueryDto
{
    public record ForumDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ForumType { get; set; }

    }

    public record PublicForumDto : ForumDto
    {
    }
    public record ClassForumDto : ForumDto
    {
        public string ClassId { get; set; }
    }
    public record SubjectForumDto : ForumDto
    {
        public string SubjectId { get; set; }
    }
}
