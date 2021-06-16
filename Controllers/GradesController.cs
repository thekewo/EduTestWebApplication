using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduTestWebApplication.Data;
using EduTestWebApplication.ViewModels;
using EduTestWebApplication.Common.Services;
using AutoMapper;
using EduTestWebApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace EduTestWebApplication.Controllers
{
    public class GradesController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public GradesController(
            IGradeService gradeService,
            IStudentService studentService,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _gradeService = gradeService;
            _studentService = studentService;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Grades
        public async Task<IActionResult> Index()
        {
            _gradeService.MigrateDatabase();
            var grades = await _gradeService.GetGradesAsync();
            return View(_mapper.Map<List<GradeViewModel>>(grades));
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<GradeViewModel>(grade));
        }

        // GET: Grades/Create/{Id}
        public IActionResult Create(Guid Id)
        {
            return View(new GradeViewModel() { StudentId = Id });
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
                var grade = _mapper.Map<Grade>(gradeViewModel);

                var user = await _userManager.GetUserAsync(User);

                _gradeService.AddGrade(grade, Guid.Parse(user.Id));
                await _gradeService.SaveChangesAsync();
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

            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<GradeViewModel>(grade));
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
                    var grade = _mapper.Map<Grade>(gradeViewModel);

                    var user = await _userManager.GetUserAsync(User);

                    _gradeService.UpdateGrade(grade, Guid.Parse(user.Id));
                    await _gradeService.SaveChangesAsync();
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

            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<GradeViewModel>(grade));
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gradeViewModel = await _gradeService.GetGradeByIdAsync(id);
            _gradeService.DeleteGrade(gradeViewModel);
            await _gradeService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeViewModelExists(Guid id)
        {
            return _gradeService.GradeExists(id);
        }
    }
}
