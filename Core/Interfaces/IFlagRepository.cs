using FlagX0.Web.Core.Entities;

namespace FlagX0.Web.Core.Interfaces
{
    public class IFlagRepository : IGenericRepository<FlagEntity>
    {
        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(FlagEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(FlagEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(FlagEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(FlagEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
