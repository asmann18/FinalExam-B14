using FinalExam_B14.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace FinalExam_B14.Models;

public class Department:BaseEntity
{
    
    [MaxLength(100)]
    [MinLength(1)]
    public string Name { get; set; }
    public ICollection<Employee> Employees{ get; set; }
}
