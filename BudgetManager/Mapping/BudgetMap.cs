using BudgetManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Mapping
{
    public class BudgetMap : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.HasKey(b => b.BudgetId);

            builder.Property(b => b.Value).IsRequired();
            builder.Property(b => b.Description).IsRequired().HasMaxLength(50);
            builder.Property(b => b.AppUsersId).IsRequired();
            builder.Property(b => b.Day).IsRequired();
            builder.Property(b => b.MonthId).IsRequired();
            builder.Property(b => b.Year).IsRequired();

            builder.HasOne(b => b.Month).WithMany(b => b.Budgets).HasForeignKey(b => b.MonthId);
            builder.HasOne(b => b.AppUsers).WithMany(b => b.Budgets).HasForeignKey(b => b.AppUsersId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Budgets");
        }
    }
}
