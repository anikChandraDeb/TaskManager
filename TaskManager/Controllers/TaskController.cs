using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Models;
namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks.ToListAsync();
            var viewModel = new TaskViewModel
            {
                NewTask = new TaskManager.Models.Task(),
                Tasks = tasks
            };
            ViewData["message"] = "All Task loaded..";
            ViewData["status"] = true;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskViewModel model)
        {

            if (ModelState.IsValid)
            {
                ViewData["status"]= true;
                ViewData["message"] = "Task added successfully!";
                _context.Tasks.Add(model.NewTask);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); 
            }
            ViewData["status"] = false;
            ViewData["message"] = "Task not added(Name Required)!";
            model.Tasks = await _context.Tasks.ToListAsync();
            return View("Index", model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);

            if (task == null)
            {
                return NotFound(id);
            }
            var viewModel = new TaskViewModel
            {
                NewTask = task,
                Tasks = await _context.Tasks.ToListAsync()
            };
            ViewData["status"] = true;
            ViewData["message"] = "Task added successfully!";
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TaskViewModel model)
        {
            var existingTask = await _context.Tasks.FindAsync(model.NewTask.Id);
            if (existingTask == null)
            {
                Console.WriteLine("not found");
                return NotFound(model.NewTask.Id); // Return 404 if task does not exist
            }


            // Update properties manually
            existingTask.Name = model.NewTask.Name;
            existingTask.IsComplete = model.NewTask.IsComplete;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Edit()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (task == null)
            {
                return NotFound(id);
            }

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Toggle(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            task.IsComplete = (bool)task.IsComplete ? false : true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
