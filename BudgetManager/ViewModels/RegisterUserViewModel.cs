using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Required field")]       
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
        public IFormFile Photo { get; set; }
    }
}
