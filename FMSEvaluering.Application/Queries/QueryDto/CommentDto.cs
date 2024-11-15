using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Queries.QueryDto
{
    public record CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
