using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BudgetManager.Models;
using BudgetManager.DAL.Interface;

namespace BudgetManager.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.GetAll());
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddEntity(category);
                TempData["Action"] = $"{category.Name} was successfully registered";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateEntity(category);
                TempData["Action"] = $"{category.Name} was successfully updated";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        
        [HttpPost]
        public async Task<JsonResult> Delete(int rowId)
        {
            Category category = await _categoryRepository.GetById(rowId);
            await _categoryRepository.DeleteEntity(category);
            TempData["Action"] = $"{category.Name} was successfully deleted";
            return Json("Category deleted");
        }

        public async Task<JsonResult> CategoryExists(string Name, int CategoryId)
        {
            if(CategoryId == 0)
            {
                if (await _categoryRepository.CategoryExists(Name))
                    return Json("Category already registered");

                return Json(true);
            }

            else
            {
                if(await _categoryRepository.CategoryExists(Name, CategoryId))
                    return Json("Category already registered");

                return Json(true);
            }
        }
    }
}
