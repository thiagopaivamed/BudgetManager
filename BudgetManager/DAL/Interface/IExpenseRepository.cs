using BudgetManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Interface
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetAll(string userId);

        Task<double> GetSumOfExpenses(string userId);

        object GetExpenseData(string userId, string month, int year);

        Task<IEnumerable<int>> GetYears();

        Task<double> GetSumOfExpensesByMonthAndYear(string userId, string month, int year);

        Task<IEnumerable<double>> GetExpensesValuesByYear(string userId, int year);
    }
}
