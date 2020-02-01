using BudgetManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.HasMany(c => c.Expenses).WithOne(c => c.Category);

            builder.HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Food"
                },

                 new Category
                 {
                     CategoryId = 2,
                     Name = "Oil"
                 },

                  new Category
                  {
                      CategoryId = 3,
                      Name = "Internet"
                  },

                   new Category
                   {
                       CategoryId = 4,
                       Name = "Air Plane tickets"
                   },

                    new Category
                    {
                        CategoryId = 5,
                        Name = "Hotels"
                    },

                     new Category
                     {
                         CategoryId = 6,
                         Name = "Car Maintenance"
                     },

                      new Category
                      {
                          CategoryId = 7,
                          Name = "Energy bill"
                      },

                       new Category
                       {
                           CategoryId = 8,
                           Name = "Phone Bill"
                       },

                        new Category
                        {
                            CategoryId = 9,
                            Name = "Water bill"
                        },

                         new Category
                         {
                             CategoryId = 10,
                             Name = "Gas Bill"
                         },

                          new Category
                          {
                              CategoryId = 11,
                              Name = "Credit Card Bill"
                          }
                );

            builder.ToTable("Categories");
        }
    }
}
