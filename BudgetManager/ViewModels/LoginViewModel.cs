using System.ComponentModel.DataAnnotations;

namespace BudgetManager.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Required field")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
