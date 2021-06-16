using EduTestWebApplication.Common.Repositories;
using EduTestWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.Common.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void AddStudent(Student student, Guid userId)
        {
            student.Id = Guid.NewGuid();
            student.CreatedAt = DateTime.Now;
            student.CreatedBy = userId;
            _studentRepository.AddStudent(student);
        }

        public void DeleteStudent(Student student)
        {
            _studentRepository.DeleteStudent(student);
        }

        public async Task<Student> GetStudentByIdAsync(Guid? id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<List<Student>> GetStudentsOrderByNameAsync()
        {
            var students = await _studentRepository.GetStudentsAsync();
            return await Task.Run(() => students.OrderBy(s => s.Name).ToList());
        }

        public void MigrateDatabase()
        {
            _studentRepository.MigrateDatabase();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _studentRepository.SaveChangesAsync();
        }

        public bool StudentExists(Guid id)
        {
            return _studentRepository.StudentExists(id);
        }

        public void UpdateStudent(Student student)
        {
            _studentRepository.UpdateStudent(student);
        }
    }
}
