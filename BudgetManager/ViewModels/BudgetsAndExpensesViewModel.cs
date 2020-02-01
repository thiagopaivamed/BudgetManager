using System.Collections.Generic;

namespace BudgetManager.ViewModels
{
    public class BudgetsAndExpensesViewModel
    {
        public IEnumerable<double> BudgetValues { get; set; }
        public IEnumerable<double> ExpenseValues { get; set; }
        public IEnumerable<string> MonthNames { get; set; }
    }
}
