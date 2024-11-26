using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Queries.QueryDto
{
    public record ForumWithPostDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ForumType { get; set; }
        public int ClassId { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
    }
}
