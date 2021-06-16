using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.Models
{
    public class Grade
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        [Range(1,5)]
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
