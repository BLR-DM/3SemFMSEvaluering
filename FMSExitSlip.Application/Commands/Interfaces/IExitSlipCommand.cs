using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;

namespace FMSExitSlip.Application.Commands.Interfaces
{
    public interface IExitSlipCommand
    {
        Task CreateQuestion(CreateExitSlipDto exitSlipDto);
    }
}
