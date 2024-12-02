using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Application.Commands.CommandDto.QuestionDto
{
    public record UpdateQuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
