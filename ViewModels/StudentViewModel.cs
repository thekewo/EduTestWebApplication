using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.ViewModels
{
    public class StudentViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Prompt = "Enter name of the student", Description = "Name of the student")]
        public string Name { get; set; }
        [Required]
        [Range(1, 8)]
        public int YearGroup { get; set; }
        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
        public float Avarage { get; set; }
        public int NumberOfFailGrades { get; set; }
        public int BestGrade { get; set; }
    }
}
