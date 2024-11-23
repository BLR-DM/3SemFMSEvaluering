using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FMSEvaluering.Infrastructure.Helpers
{
    public class UnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private readonly DbContext _db;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(T db)
        {
            _db = db;
        }
        async Task IUnitOfWork.Commit()
        {
            if (_transaction == null) throw new Exception("You must call 'BeginTransaction' before Commit is called");

            try
            {
                await _db.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
            
        }

        async Task IUnitOfWork.Rollback()
        {
            if(_transaction == null) throw new Exception("You must call 'BeginTransaction' before Rollback is called");

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        async Task IUnitOfWork.BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_db.Database.CurrentTransaction != null) return;
            _transaction = await _db.Database.BeginTransactionAsync(isolationLevel);
        }
    }
}
