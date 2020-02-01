using BudgetManager.Models;
using BudgetManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Interface
{
    public interface IUserRepository : IGenericRepository<AppUsers>
    {
        Task<IdentityResult> CreateUser(AppUsers user, string password);
        Task SignInUser(AppUsers user, bool remember);
        Task SignOutUser();
        Task<AppUsers> FindUserByName(string name);
        Task<AppUsers> FindUserByEmail(string email);
        bool VerifyPassword(AppUsers user, string password);
        Task<AuthenticatedUserDataViewModel> GetAuthenticatedUserData(string Name);
    }
}
