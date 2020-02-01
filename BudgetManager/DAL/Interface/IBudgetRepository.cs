using BudgetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Interface
{
    public interface IBudgetRepository : IGenericRepository<Budget>
    {
        Task<IEnumerable<Budget>> GetAll(string userId);

        Task<double> GetSumOfBudgets(string userId);
        Task<double> GetSumOfBudgetsByMonthAndYear(string userId, string month, int year);

        object GetBudgetsData(string userId, string month, int year);

        Task<IEnumerable<double>> GetBudgetsValuesByYear(string userId, int year);
    }
}
