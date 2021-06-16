using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduTestWebApplication.Data;
using EduTestWebApplication.ViewModels;

namespace EduTestWebApplication.Controllers
{
    public class GradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grades
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grades.ToListAsync());
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeViewModel = await _context.Grades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeViewModel == null)
            {
                return NotFound();
            }

            return View(gradeViewModel);
        }

        // GET: Grades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Value")] GradeViewModel gradeViewModel)
        {
            if (ModelState.IsValid)
            {
                gradeViewModel.Id = Guid.NewGuid();
                _context.Add(gradeViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gradeViewModel);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeViewModel = await _context.Grades.FindAsync(id);
            if (gradeViewModel == null)
            {
                return NotFound();
            }
            return View(gradeViewModel);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StudentId,Value")] GradeViewModel gradeViewModel)
        {
            if (id != gradeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeViewModelExists(gradeViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gradeViewModel);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeViewModel = await _context.Grades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeViewModel == null)
            {
                return NotFound();
            }

            return View(gradeViewModel);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gradeViewModel = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(gradeViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeViewModelExists(Guid id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
