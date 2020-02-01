using BudgetManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Interface
{
    public interface IMonthRepository : IGenericRepository<Month>
    {
        new Task<IEnumerable<Month>> GetAll();

        Task<IEnumerable<string>> GetAllMonthsNames();
    }
}
