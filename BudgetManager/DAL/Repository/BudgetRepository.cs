using BudgetManager.DAL.Interface;
using BudgetManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Repository
{
    public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
    {
        private readonly Context _context;

        public BudgetRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Budget>> GetAll(string userId)
        {
            try
            {
                return await _context.Budgets.Where(b => b.AppUsersId == userId).Include(b => b.Month).Include(b => b.AppUsers).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        public object GetBudgetsData(string userId, string month, int year)
        {
            try
            {
                return _context.Budgets.Where(b => b.AppUsersId == userId && b.Month.Name == month && b.Year == year).GroupBy(b => b.Day)
                       .Select(b => new { listOfDays = b.Key, listOfValues = b.Sum(x => x.Value) })
                       .OrderByDescending(b => b.listOfDays);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<double>> GetBudgetsValuesByYear(string userId, int year)
        {
            try
            {
                return await _context.Budgets.Where(b=> b.AppUsersId == userId && b.Year == year).OrderBy(b => b.MonthId).GroupBy(b => b.Month.Name)
                      .Select(b => b.Sum(x => x.Value)).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<double> GetSumOfBudgetsByMonthAndYear(string userId, string month, int year)
        {
            try
            {
                return await _context.Budgets.Where(b => b.AppUsersId == userId && b.Month.Name == month && b.Year == year).SumAsync(b => b.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<double> GetSumOfBudgets(string userId)
        {
            try
            {
                return await _context.Budgets.Where(b => b.AppUsersId == userId).SumAsync(b => b.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
