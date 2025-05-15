using Mamba.DAL;
using Mamba.Models;
using Mamba.ViewModels.PositionVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mamba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _context.Positions.Include(p => p.Employees)
                .ToListAsync();
            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionCreateVM positionCreateVM)
        {
            if (!ModelState.IsValid) return View();

            bool result = await _context.Positions.AnyAsync(p => p.Name == positionCreateVM.Name);
            if (result)
            {
                ModelState.AddModelError(positionCreateVM.Name, $"this position:{positionCreateVM.Name} is already exist");
                return View();
            }

            Position position = new() { Name = positionCreateVM.Name };

            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Position? position = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);

            if (position is null) return NotFound();

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
