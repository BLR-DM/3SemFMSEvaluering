using System.Data;

namespace FMSEvaluering.Application.Helpers;

public interface IUnitOfWork
{
    Task Commit();
    Task Rollback();
    Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable);
}