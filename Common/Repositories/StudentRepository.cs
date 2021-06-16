using EduTestWebApplication.Data;
using EduTestWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.Common.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddStudent(Student student)
        {
            _context.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            _context.Remove(student);
        }

        public async Task<Student> GetStudentByIdAsync(Guid? id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public void MigrateDatabase()
        {
            _context.Database.Migrate();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        public void UpdateStudent(Student student)
        {
            _context.Update(student);
        }
    }
}
