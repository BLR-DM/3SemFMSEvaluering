using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Commands.CommandDto.VoteDto
{
    public record DeleteVoteDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
    }
}
