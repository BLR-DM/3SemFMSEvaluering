using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Commands.CommandDto.VoteDto
{
    public record HandleVoteDto
    {
        public bool VoteType { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
