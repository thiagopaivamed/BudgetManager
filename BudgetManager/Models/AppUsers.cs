using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BudgetManager.Models
{
    public class AppUsers : IdentityUser
    {
        public string Photo { get; set; }

        public ICollection<Budget> Budgets { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
