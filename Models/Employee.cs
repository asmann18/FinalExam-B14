using FinalExam_B14.Models.Common;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace FinalExam_B14.Models
{
    public class Employee:BaseEntity
    {
        
        [MaxLength(100)]
        [MinLength(3)]
        public string FullName { get; set; } = null!;

        [MaxLength(1000)]
        [MinLength(3)]
        public string Description { get; set; } = null!;
        public string ImagePath { get; set; } = null!;

        [MaxLength(200)]
        [MinLength(3)]
        public string TwitterLink { get; set; } = null!;
        [MaxLength(200)]
        [MinLength(3)]
        public string FacebookLink { get; set; } = null!;
        [MaxLength(200)]
        [MinLength(3)]
        public string InstagramLink { get; set; } = null!;
        [MaxLength(200)]
        [MinLength(3)]
        public string LinkedInLink { get; set; } = null!;
        public Department Department { get; set; } = null!;

        public int DepartmentId { get; set; }
    }
}
