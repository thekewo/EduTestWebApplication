using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.ViewModels
{
    public class GradeViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public int Value { get; set; }
    }
}
