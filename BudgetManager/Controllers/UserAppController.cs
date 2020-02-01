using System;
using System.IO;
using System.Threading.Tasks;
using BudgetManager.DAL.Interface;
using BudgetManager.Models;
using BudgetManager.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Controllers
{
    public class UserAppController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserRepository _userRepository;

        public UserAppController(IHostingEnvironment hostingEnvironment, IUserRepository userRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _userRepository = userRepository;
        }        

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                string fileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "-" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    await model.Photo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }

                AppUsers userApp = new AppUsers
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = model.Password,
                    Photo = fileName
                };

                var createdUser = await _userRepository.CreateUser(userApp, model.Password);

                if (createdUser.Succeeded)
                {
                    await _userRepository.SignInUser(userApp, false);

                    if (createdUser.Succeeded)
                    {

                        return RedirectToAction("Index", "Dashboard");
                    }

                    else
                    {
                        foreach (var error in createdUser.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }
                return View(model);
            }
            return View(model);
        }    

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var userApp = await _userRepository.FindUserByEmail(loginViewModel.Email);

                if (userApp != null)
                {
                    if (_userRepository.VerifyPassword(userApp, loginViewModel.Password))
                    {
                        await _userRepository.SignInUser(userApp, false);

                        return RedirectToAction("Index", "Dashboard");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Invalid email and/or password");
                        return View(loginViewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email and/or password");
                    return View(loginViewModel);
                }
            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _userRepository.SignOutUser();

            return RedirectToAction("Login");
        }
    }
}