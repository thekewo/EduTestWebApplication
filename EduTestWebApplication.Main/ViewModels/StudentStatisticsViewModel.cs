using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.ViewModels
{
    public class StudentStatisticsViewModel
    {
        public string StudentName { get; set; }
        public double Avarage { get; set; }
        public int NumberOfFailGrades { get; set; }
        public int BestGrade { get; set; }
    }
}
