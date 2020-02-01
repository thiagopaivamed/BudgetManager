using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, ErrorMessage = "Name too big")]
        [Remote("CategoryExists", "Categories", AdditionalFields = "CategoryId")]
        public string Name { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}
