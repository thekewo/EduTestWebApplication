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
        public string Name { get; set; }
        [Required]
        [Range(1, 8)]
        public int YearGroup { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long? PhoneNumber { get; set; }
    }
}
