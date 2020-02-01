using BudgetManager.DAL.Interface;
using BudgetManager.Models;
using BudgetManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManager.DAL.Repository
{
    public class UserRepository : GenericRepository<AppUsers>, IUserRepository
    {

        private readonly Context _context;
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;

        public UserRepository(Context context, UserManager<AppUsers> userManager, SignInManager<AppUsers> signInManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUser(AppUsers user, string password)
        {
            try
            {
                return await _userManager.CreateAsync(user, password);
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        public async Task<AppUsers> FindUserByName(string name)
        {
            try
            {
                return await _userManager.FindByNameAsync(name);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<AppUsers> FindUserByEmail(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task SignInUser(AppUsers user, bool remember)
        {
            try
            {
                await _signInManager.SignInAsync(user, remember);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task SignOutUser()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool VerifyPassword(AppUsers user, string password)
        {
            try
            {
                PasswordHasher<AppUsers> passwordHasher = new PasswordHasher<AppUsers>();
                return passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<AuthenticatedUserDataViewModel> GetAuthenticatedUserData(string Name)
        {
            try
            {
                return _context.AppUsers.Where(u => u.UserName == Name)
                         .Select(u => new AuthenticatedUserDataViewModel { UserName = u.UserName, UserPhoto = u.Photo }).First();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
