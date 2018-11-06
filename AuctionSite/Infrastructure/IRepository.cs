using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<TEntity> GetAsync(int id);

        Task<TEntity> GetAsync(int id, params string[] includes);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
    }
}
