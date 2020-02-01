using BudgetManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Mapping
{
    public class MonthMap : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            builder.HasKey(m => m.MonthId);

            builder.Property(m => m.MonthId).ValueGeneratedNever();
            builder.Property(m => m.Name).IsRequired().HasMaxLength(20);
            builder.HasIndex(m => m.Name).IsUnique();

            builder.HasData(
                new Month
                {
                    MonthId = 1,
                    Name = "January"
                },

                new Month
                {
                    MonthId = 2,
                    Name = "February"
                },

                new Month
                {
                    MonthId = 3,
                    Name = "March"
                },

                new Month
                {
                    MonthId = 4,
                    Name = "April"
                },

                  new Month
                  {
                      MonthId = 5,
                      Name = "May"
                  },

                new Month
                {
                    MonthId = 6,
                    Name = "June"
                },

                new Month
                {
                    MonthId = 7,
                    Name = "July"
                },

                new Month
                {
                    MonthId = 8,
                    Name = "August"
                }, new Month
                {
                    MonthId = 9,
                    Name = "September"
                }, new Month
                {
                    MonthId = 10,
                    Name = "October"
                }, new Month
                {
                    MonthId = 11,
                    Name = "November"
                }, new Month
                {
                    MonthId = 12,
                    Name = "December"
                });

            builder.HasMany(m => m.Budgets).WithOne(m => m.Month);
            builder.HasMany(m => m.Expenses).WithOne(m => m.Month);

            builder.ToTable("Months");
        }
    }
}
