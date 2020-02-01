using BudgetManager.Models;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Interface
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> CategoryExists(string categoryName);
        Task<bool> CategoryExists(string categoryName, int categoryId);
    }
}
