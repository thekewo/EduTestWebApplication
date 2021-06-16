using EduTestWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.Common.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsAsync();
        Task<List<Student>> GetStudentsOrderByNameAsync();
        Task<Student> GetStudentByIdAsync(Guid? id);
        bool StudentExists(Guid id);
        void AddStudent(Student student, Guid userId);
        void UpdateStudent(Student student, Guid userId);
        void DeleteStudent(Student student);
        Task<int> SaveChangesAsync();
        void MigrateDatabase();
    }
}
