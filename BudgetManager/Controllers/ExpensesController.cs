using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BudgetManager.Models;
using BudgetManager.DAL.Interface;

namespace BudgetManager.Controllers
{
    public class ExpensesController : Controller
    {        
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMonthRepository _monthRepository;
        private readonly IUserRepository _userRepository;

        public ExpensesController(IUserRepository userRepository, IExpenseRepository expenseRepository, ICategoryRepository categoryRepository, IMonthRepository monthRepository)
        {
            _userRepository = userRepository;
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _monthRepository = monthRepository;            
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var user = await _userRepository.FindUserByName(User.Identity.Name);

            return View(await _expenseRepository.GetAll(user.Id));
        }        

        // GET: Expenses/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name");
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name");
            AppUsers appUsers = await _userRepository.FindUserByName(User.Identity.Name);
            Expense expense = new Expense { AppUsersId = appUsers.Id };

            return View(expense);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,Value,Description,CategoryId,AppUsersId,Day,MonthId,Year")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expenseRepository.AddEntity(expense);
                TempData["Action"] = $"${expense.Value} expense was successfully registered";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _expenseRepository.GetAll(), "CategoryId", "Name", expense.CategoryId); ;
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name", expense.MonthId);            
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expenseRepository.GetById(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name", expense.CategoryId);
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name");
            return View(expense);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,Value,Description,CategoryId,AppUsersId,Day,MonthId,Year")] Expense expense)
        {
            if (id != expense.ExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _expenseRepository.UpdateEntity(expense);
                TempData["Action"] = $"${expense.Value} expense was successfully updated";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name", expense.CategoryId);
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name", expense.MonthId);
            return View(expense);
        }
       
        [HttpPost]
        public async Task<JsonResult> Delete(int rowId)
        {
            Expense expense = await _expenseRepository.GetById(rowId);
            await _expenseRepository.DeleteEntity(expense);
            TempData["Action"] = $"${expense.Value} expense was successfully deleted";
            return Json("Expense Deleted");
        }       
    }
}
