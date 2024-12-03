﻿using System;
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
            return await _db.ExitSlips
                .Include(e => e.Questions)
                    .ThenInclude(q => q.Responses)
                .SingleAsync(e => e.Id == id);
        }

        async Task<IEnumerable<ExitSlip>> IExitSlipRepository.GetExitSlipsAsync()
        {
            return await _db.ExitSlips.ToListAsync();
        }

        void IExitSlipRepository.UpdateResponse(Response response, byte[] rowVersion)
        {
            _db.Entry(response).Property(nameof(response.RowVersion)).OriginalValue = rowVersion;
        }

        void IExitSlipRepository.DeleteResponse(Response response, byte[] rowVersion)
        {
            _db.Entry(response).Property(nameof(response.RowVersion)).OriginalValue = rowVersion;
            _db.Responses.Remove(response);
        }

        void IExitSlipRepository.PublishExitSlip(ExitSlip exitSlip, byte[] rowVersion)
        {
            _db.Entry(exitSlip).Property(nameof(exitSlip.RowVersion)).OriginalValue = rowVersion;
            _db.ExitSlips.Update(exitSlip);
        }

        void IExitSlipRepository.UpdateQuestion(Question question, byte[] rowVersion)
        {
            _db.Entry(question).Property(nameof(question.RowVersion)).OriginalValue = rowVersion;
        }

        void IExitSlipRepository.DeleteQuestion(Question question, byte[] rowVersion)
        {
            _db.Entry(question).Property(nameof(question.RowVersion)).OriginalValue = rowVersion;
            _db.Questions.Remove(question);
        }
    }
}
