﻿using FinalExam_B14.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinalExam_B14.Areas.Admin.ViewModels.EmployeeVMs
{
    public class EmployeeCreateVM
    {

        [MaxLength(100)]
        [MinLength(3)]
        public string FullName { get; set; } = null!;

        [MaxLength(1000)]
        [MinLength(3)]
        public string Description { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;

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

        public int DepartmentId { get; set; }
        public SelectList Departments { get; set; }
    }
}
