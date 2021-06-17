using EduTestWebApplication.Common.Repositories;
using EduTestWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.Common.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public void AddGrade(Grade grade, Guid userId)
        {
            grade.Id = Guid.NewGuid();
            grade.CreatedAt = DateTime.Now;
            grade.CreatedBy = userId;
            _gradeRepository.AddGrade(grade);
        }

        public void DeleteGrade(Grade grade)
        {
            _gradeRepository.DeleteGrade(grade);
        }

        public async Task<Grade> GetGradeByIdAsync(Guid? id)
        {
            return await _gradeRepository.GetGradeByIdAsync(id);
        }

        public async Task<List<Grade>> GetGradesAsync()
        {
            return await _gradeRepository.GetGradesAsync();
        }

        public void MigrateDatabase()
        {
            _gradeRepository.MigrateDatabase();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _gradeRepository.SaveChangesAsync();
        }

        public bool GradeExists(Guid id)
        {
            return _gradeRepository.GradeExists(id);
        }

        public void UpdateGrade(Grade grade, Guid userId)
        {
            grade.ModifiedAt = DateTime.Now;
            grade.ModifiedBy = userId;
            _gradeRepository.UpdateGrade(grade);
        }
    }
}
