using BudgetManager.DAL.Interface;
using BudgetManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Repository
{
    public class MonthRepository : GenericRepository<Month>, IMonthRepository
    {
        private readonly Context _context;
        public MonthRepository(Context context) : base(context)
        {
            _context = context;
        }

        public new async Task<IEnumerable<Month>> GetAll()
        {
            try
            {
                return await _context.Months.OrderBy(m => m.MonthId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<string>> GetAllMonthsNames()
        {
            try
            {
                return await _context.Months.OrderBy(m => m.MonthId).Select(m => m.Name).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
