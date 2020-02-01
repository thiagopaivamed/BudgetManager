using BudgetManager.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Models
{
    public class Context : IdentityDbContext<AppUsers>
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<AppUsers> AppUsers { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new BudgetMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new ExpensesMap());
            builder.ApplyConfiguration(new MonthMap());
            builder.ApplyConfiguration(new AppUsersMap());
        }
    }
}
