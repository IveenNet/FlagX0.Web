using FlagX0.Web.Core.Interfaces;

namespace FlagX0.Web.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IFlagRepository Flags { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public UnitOfWork(IFlagRepository flags, ApplicationDbContext applicationDbContext)
        {

            Flags = flags;
            _context = applicationDbContext;

        }
    }
}
