using BudgetManager.DAL.Interface;
using BudgetManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExists(string categoryName)
        {
            try
            {
                return await _context.Categories.AnyAsync(c => c.Name == categoryName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CategoryExists(string categoryName, int categoryId)
        {
            try
            {
                return await _context.Categories.AnyAsync(c => c.Name == categoryName && c.CategoryId != categoryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}
