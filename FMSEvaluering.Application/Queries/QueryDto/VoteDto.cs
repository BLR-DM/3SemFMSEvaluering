using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Queries.QueryDto
{
    public record VoteDto
    {
        public int Id { get; set; }
        public bool VoteType { get; set; }
    }
}
