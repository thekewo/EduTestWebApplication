using EduTestWebApplication.Data;
using EduTestWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.Common.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDbContext _context;
        public GradeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddGrade(Grade grade)
        {
            _context.Add(grade);
        }

        public void DeleteGrade(Grade grade)
        {
            _context.Remove(grade);
        }

        public async Task<Grade> GetGradeByIdAsync(Guid? id)
        {
            return await _context.Grades.FindAsync(id);
        }

        public async Task<List<Grade>> GetGradesAsync()
        {
            return await _context.Grades.ToListAsync();
        }

        public void MigrateDatabase()
        {
            _context.Database.Migrate();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool GradeExists(Guid id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }

        public void UpdateGrade(Grade grade)
        {
            _context.Update(grade);
        }
    }
}
