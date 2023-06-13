namespace BLueCodeChanllenge.Interfaces
{
    public interface IDbService<TEntity> where TEntity : class
    {
      
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        int GetNextId();
    }
}
