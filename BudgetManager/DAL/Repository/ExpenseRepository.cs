using BudgetManager.DAL.Interface;
using BudgetManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Repository
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        private readonly Context _context;
        public ExpenseRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAll(string userId)
        {
            try
            {
                return await _context.Expenses.Where(e => e.AppUsersId == userId).Include(e => e.Category).Include(e => e.Month)
                    .Include(e => e.AppUsers).OrderBy(e => e.MonthId).ThenBy(e => e.Year).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<int>> GetYears()
        {
            try
            {
                return await _context.Expenses.Select(b => b.Year).Distinct().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<double> GetSumOfExpensesByMonthAndYear(string userId, string month, int year)
        {
            try
            {
                return await _context.Expenses.Where(e => e.AppUsersId == userId && e.Month.Name == month && e.Year == year).SumAsync(e => e.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public object GetExpenseData(string userId, string month, int year)
        {
            try
            {
                return _context.Expenses.Where(e => e.AppUsersId == userId && e.Month.Name == month && e.Year == year).GroupBy(e => e.Category.Name)
                        .Select(e => new { listOfCategories = e.Key, listOfValues = e.Sum(x => x.Value) })
                        .OrderByDescending(b => b.listOfValues);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<double>> GetExpensesValuesByYear(string userId, int year)
        {
            try
            {
                return await _context.Expenses.Where(e => e.AppUsersId == userId && e.Year == year).OrderBy(e => e.MonthId).GroupBy(e => e.Month.Name)
                        .Select(e => e.Sum(x => x.Value)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<double> GetSumOfExpenses(string userId)
        {
            try
            {
                return await _context.Expenses.Where(e => e.AppUsersId == userId).SumAsync(e => e.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
