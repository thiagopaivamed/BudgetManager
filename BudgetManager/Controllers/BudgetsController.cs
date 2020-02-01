using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BudgetManager.Models;
using BudgetManager.DAL.Interface;

namespace BudgetManager.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IMonthRepository _monthRepository;

        public BudgetsController(IUserRepository userRepository, IBudgetRepository budgetRepository, IMonthRepository monthRepository)
        {
            _userRepository = userRepository;
            _budgetRepository = budgetRepository;
            _monthRepository = monthRepository;
        }

        // GET: Budgets
        public async Task<IActionResult> Index()
        {
            var user = await _userRepository.FindUserByName(User.Identity.Name);

            return View(await _budgetRepository.GetAll(user.Id));
        }

        public async Task<IActionResult> Create()
        {
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name");
            AppUsers appUsers = await _userRepository.FindUserByName(User.Identity.Name);
            Budget budget = new Budget { AppUsersId = appUsers.Id };
            return View(budget);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BudgetId,Value,Description,AppUsersId,Day,MonthId,Year")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                await _budgetRepository.AddEntity(budget);
                TempData["Action"] = $"{budget.Value} budget was successfully registered";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name", budget.MonthId);
            return View(budget);
        }
        
        public async Task<IActionResult> Edit(int id)
        {

            var budget = await _budgetRepository.GetById(id);
            if (budget == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name", budget.MonthId);
            return View(budget);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("BudgetId,Value,Description,AppUsersId,Day,MonthId,Year")] Budget budget)
        {
            if (id != budget.BudgetId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _budgetRepository.UpdateEntity(budget);
                TempData["Action"] = $"{budget.Value} budget was successfully updated";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name", budget.MonthId);

            return View(budget);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int rowId)
        {
            var budget = await _budgetRepository.GetById(rowId);
            await _budgetRepository.DeleteEntity(budget);
            TempData["Action"] = $"{budget.Value} budget was successfully deleted";
            return Json("Budget deleted");
        }    
    }
}
