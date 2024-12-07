using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.Entities;

namespace FMSExitSlip.Application.Helpers
{
    public interface IExitSlipAccessHandler
    {
        Task ValidateExitslipAccess(string appUserId, string role, ExitSlip exitSlip);
    }
}
