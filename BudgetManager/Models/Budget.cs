using BudgetManager.Validation;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models
{
    public class Budget
    {
        public int BudgetId { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Invalid Value")]
        [DataType(DataType.Currency)]
        public double Value { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [StringLength(40, ErrorMessage = "Description too big")]
        public string Description { get; set; }

        public string AppUsersId { get; set; }
        public AppUsers AppUsers { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Range(1, 31, ErrorMessage = "Invalid value")]
        public int Day { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public int MonthId { get; set; }
        public Month Month { get; set; }

        [MaxYear(ErrorMessage = "Invalid Year")]
        [Required(ErrorMessage = "Required Field")]
        public int Year { get; set; }
    }
}
