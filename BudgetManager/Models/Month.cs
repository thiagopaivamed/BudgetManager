using System.Collections.Generic;

namespace BudgetManager.Models
{
    public class Month
    {
        public int MonthId { get; set; }
        public string Name { get; set; }

        public ICollection<Budget> Budgets { get; set; }
        public ICollection<Expense> Expenses { get; set; }

    }
}
