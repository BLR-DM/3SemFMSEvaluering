using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Application.Helpers
{
    public interface IUnitOfWork
    {
        Task Commit();
        Task Rollback();
        Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable);
    }
}
