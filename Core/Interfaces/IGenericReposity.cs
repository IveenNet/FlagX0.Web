namespace FlagX0.Web.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        #region Métodos Síncronos

        public bool Insert(T entity);
        public bool Update(T entity);
        public bool Delete(string id);
        //public T Get(string id);
        //public IEnumerable<T> GetAll();
        //public IEnumerable<T> GetAllWithPagination(int pageNumber, int pageSize);
        //public int Count();


        #endregion

        #region Métodos Asíncronos

        public Task<bool> InsertAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(string id);
        //public Task<T> GetAsync(string id);
        //public Task<T> GetAsync(string id, CancellationToken cancellationToken);
        //public Task<IEnumerable<T>> GetAllAsync();
        //public Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        //public Task<int> CountAsync();

        #endregion


    }
}
