using BudgetManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Mapping
{
    public class ExpensesMap : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.ExpenseId);

            builder.Property(e => e.Value).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasMaxLength(40);
            builder.Property(e => e.CategoryId).IsRequired();
            builder.Property(b => b.AppUsersId).IsRequired();
            builder.Property(e => e.Day).IsRequired();
            builder.Property(e => e.MonthId).IsRequired();
            builder.Property(e => e.Year).IsRequired();

            builder.HasOne(e => e.Category).WithMany(e => e.Expenses).HasForeignKey(e => e.CategoryId);
            builder.HasOne(e => e.Month).WithMany(e => e.Expenses).HasForeignKey(e => e.MonthId);
            builder.HasOne(b => b.AppUsers).WithMany(b => b.Expenses).HasForeignKey(b => b.AppUsersId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Expenses");
        }
    }
}
