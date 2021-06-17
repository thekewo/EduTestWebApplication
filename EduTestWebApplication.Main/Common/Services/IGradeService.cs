using EduTestWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.Common.Services
{
    public interface IGradeService
    {
        Task<List<Grade>> GetGradesAsync();
        Task<Grade> GetGradeByIdAsync(Guid? id);
        bool GradeExists(Guid id);
        void AddGrade(Grade grade, Guid userId);
        void UpdateGrade(Grade grade, Guid userId);
        void DeleteGrade(Grade grade);
        Task<int> SaveChangesAsync();
        void MigrateDatabase();
    }
}
