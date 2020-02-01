using BudgetManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Mapping
{
    public class AppUsersMap : IEntityTypeConfiguration<AppUsers>
    {
        public void Configure(EntityTypeBuilder<AppUsers> builder)
        {
            builder.Property(u => u.Photo).IsRequired();

            builder.HasMany(u => u.Budgets).WithOne(u => u.AppUsers);
            builder.HasMany(u => u.Expenses).WithOne(u => u.AppUsers);

            builder.ToTable("AppUsers");
        }
    }
}
