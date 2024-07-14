using System;
using System.Threading.Tasks;

namespace FlagX0.Web.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFlagRepository Flags { get; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}