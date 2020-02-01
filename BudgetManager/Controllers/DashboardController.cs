using System;
using System.Threading.Tasks;
using BudgetManager.DAL.Interface;
using BudgetManager.Models;
using BudgetManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Controllers
{
    public class DashboardController : Controller
    {        
        private readonly IMonthRepository _monthRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IUserRepository _userRepository;        

        public DashboardController(IMonthRepository monthRepository, IExpenseRepository expenseRepository, IBudgetRepository budgetRepository, IUserRepository userRepository)
        {   
            _monthRepository = monthRepository;
            _expenseRepository = expenseRepository;
            _budgetRepository = budgetRepository;
            _userRepository = userRepository;            

        }

        public async Task<IActionResult> Index()
        {
            ViewData["Months"] = new SelectList(await _monthRepository.GetAllMonthsNames());
            ViewData["Years"] = new SelectList(await _expenseRepository.GetYears());

            CardDataViewModel model = new CardDataViewModel();
            var user = await _userRepository.FindUserByName(User.Identity.Name);

            model.SumOfBudgets = await _budgetRepository.GetSumOfBudgets(user.Id);
            model.SumOfExpense = await _expenseRepository.GetSumOfExpenses(user.Id);
            model.Total = model.SumOfBudgets - model.SumOfExpense;          

            return View(model);
        }

        public async Task<JsonResult> BudgetsAndExpensesData(string month, int year)
        {
            var user = await _userRepository.FindUserByName(User.Identity.Name);

            BudgetsAndExpensesDataViewModel model = new BudgetsAndExpensesDataViewModel
            {
                SumOfBudgets = await _budgetRepository.GetSumOfBudgetsByMonthAndYear(user.Id, month, year),
                SumOfExpenses = await _expenseRepository.GetSumOfExpensesByMonthAndYear(user.Id, month, year)
            };

            return Json(model);

        }

        public async Task<JsonResult> GetExpensesData(string month, int year)
        {
            var user = await _userRepository.FindUserByName(User.Identity.Name);
            return Json(_expenseRepository.GetExpenseData(user.Id, month, year));
        }

        public async Task<JsonResult> GetBudgetsData(string month, int year)
        {
            var user = await _userRepository.FindUserByName(User.Identity.Name);
            return Json(_budgetRepository.GetBudgetsData(user.Id, month, year));
        }

        public async Task<JsonResult> GetBudgetsAndExpensesByYear(int year)
        {
            var user = await _userRepository.FindUserByName(User.Identity.Name);
            BudgetsAndExpensesViewModel model = new BudgetsAndExpensesViewModel
            {
                ExpenseValues = await _expenseRepository.GetExpensesValuesByYear(user.Id, year),
                BudgetValues = await _budgetRepository.GetBudgetsValuesByYear(user.Id, year),
                MonthNames = await _monthRepository.GetAllMonthsNames()
            };

            return Json(model);
        }
    }
}