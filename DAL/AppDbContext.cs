using FinalExam_B14.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalExam_B14.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions opt):base(opt)
        {
            
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees{ get; set; }
    }
}
