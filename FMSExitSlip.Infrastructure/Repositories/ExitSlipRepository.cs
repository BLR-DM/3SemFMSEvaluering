using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Repositories;

namespace FMSExitSlip.Infrastructure.Repositories
{
    public class ExitSlipRepository : IExitSlipRepository
    {
        private readonly ExitSlipContext _db;

        public ExitSlipRepository(ExitSlipContext db)
        {
            _db = db;
        }
    }
}
