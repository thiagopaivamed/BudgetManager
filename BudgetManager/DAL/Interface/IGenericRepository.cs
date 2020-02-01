using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task AddEntity(TEntity entity);
        Task UpdateEntity(TEntity entity);
        Task DeleteEntity(TEntity entity);
    }
}
