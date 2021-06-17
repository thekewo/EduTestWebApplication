using EduTestWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.Common.Repositories
{
    public interface IGradeRepository
    {
        Task<List<Grade>> GetGradesAsync();
        Task<Grade> GetGradeByIdAsync(Guid? id);
        bool GradeExists(Guid id);
        void AddGrade(Grade grade);
        void UpdateGrade(Grade grade);
        void DeleteGrade(Grade grade);
        Task<int> SaveChangesAsync();
        void MigrateDatabase();
    }
}
