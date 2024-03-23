using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Controllers
{
    public class ProjectController : Controller
    {
        private readonly DbContextApplication _context;

        public ProjectController(DbContextApplication context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects
                                         .Include(p => p.Comments)
                                         .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
    }
}
