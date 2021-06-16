using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EduTestWebApplication.Data;
using EduTestWebApplication.ViewModels;
using AutoMapper;
using EduTestWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using EduTestWebApplication.Common.Services;

namespace EduTestWebApplication.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentsController(
            IStudentService studentService,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _studentService = studentService;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            _studentService.MigrateDatabase();
            var students = await _studentService.GetStudentsOrderByNameAsync();
            var studentViewModels = _mapper.Map<List<StudentViewModel>>(students);

            return View(studentViewModels);
        }

        // GET: Statistics
        public async Task<IActionResult> Statistics()
        {
            var students = await _studentService.GetStudentsOrderByNameAsync();
            students = students.Where(s => s.Grades.Count() > 0).ToList();

            var studentStatisticsViewModel = new List<StudentStatisticsViewModel>();

            foreach (var student in students)
            {
                studentStatisticsViewModel.Add(new StudentStatisticsViewModel() { 
                    StudentName = student.Name,
                    Avarage = student.Grades.Select(g => g.Value).Average(),
                    NumberOfFailGrades = student.Grades.Where(g => g.Value == 1).Count(),
                    BestGrade = student.Grades.Select(g => g.Value).Max()
                });
            }

            return View(studentStatisticsViewModel.OrderBy(svm => svm.Avarage));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<StudentViewModel>(student));
        }

        // GET: Students/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,YearGroup,DateOfBirth,PhoneNumber")] StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(studentViewModel);

                var user = await _userManager.GetUserAsync(User);

                _studentService.AddStudent(student, Guid.Parse(user.Id));
                await _studentService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<StudentViewModel>(student));
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,YearGroup,DateOfBirth,PhoneNumber")] StudentViewModel studentViewModel)
        {
            if (id != studentViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var student = _mapper.Map<Student>(studentViewModel);

                    var user = await _userManager.GetUserAsync(User);

                    _studentService.UpdateStudent(student, Guid.Parse(user.Id));
                    await _studentService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentViewModelExists(studentViewModel.Id))
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
            return View(studentViewModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<StudentViewModel>(student));
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            _studentService.DeleteStudent(student);
            await _studentService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentViewModelExists(Guid id)
        {
            return _studentService.StudentExists(id);
        }
    }
}
