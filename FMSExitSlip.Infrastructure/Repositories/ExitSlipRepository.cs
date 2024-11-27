using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Repositories;
using FMSExitSlip.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMSExitSlip.Infrastructure.Repositories
{
    public class ExitSlipRepository : IExitSlipRepository
    {
        private readonly ExitSlipContext _db;

        public ExitSlipRepository(ExitSlipContext db)
        {
            _db = db;
        }

        async Task IExitSlipRepository.AddExitSlipAsync(ExitSlip exitSlip)
        {
            await _db.ExitSlips.AddAsync(exitSlip);
        }

        async Task<ExitSlip> IExitSlipRepository.GetExitSlipAsync(int id)
        {
            return await _db.ExitSlips.Include(e => e.Questions).SingleOrDefaultAsync(e => e.Id == id);
        }

        async Task<IEnumerable<ExitSlip>> IExitSlipRepository.GetExitSlipsAsync()
        {
            return await _db.ExitSlips.ToListAsync();
        }
    }
}
