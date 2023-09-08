using System.Data.Common;

namespace BlogEngine.Domain.Intefaces
{
    public interface IDatabase
    {
        Task<DbConnection> CreateAndOpenConnection(CancellationToken stoppingToken = default);
        Task ExecuteInTransaction(Func<DbConnection, DbTransaction, Task> action,
            CancellationToken cancellationToken = default);
        void UpgradeIfNecessary();
    }
}
