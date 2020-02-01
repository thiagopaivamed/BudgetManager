using BudgetManager.DAL.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BudgetManager.ViewComponents
{
    public class UserAutheticatedViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;

        public UserAutheticatedViewComponent(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string name = User.Identity.Name;
            return View(await _userRepository.GetAuthenticatedUserData(name));
        }
    }
}
