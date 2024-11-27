using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto
{
    public record CreateExitSlipDto (string Title, int MaxQuestions, int LectureId){ }
}
