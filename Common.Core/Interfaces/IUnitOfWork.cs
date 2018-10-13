using System;
using System.Threading;
using System.Threading.Tasks;

namespace Conquistador.Common.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken token = default(CancellationToken));
    }
}
