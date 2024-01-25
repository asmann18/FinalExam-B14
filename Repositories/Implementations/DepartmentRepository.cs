using FinalExam_B14.DAL;
using FinalExam_B14.Models;
using FinalExam_B14.Repositories.Implementations.Generic;
using FinalExam_B14.Repositories.Interfaces;

namespace FinalExam_B14.Repositories.Implementations
{
    public class DepartmentRepository:Repository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
