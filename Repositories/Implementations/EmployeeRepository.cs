using FinalExam_B14.DAL;
using FinalExam_B14.Models;
using FinalExam_B14.Repositories.Implementations.Generic;
using FinalExam_B14.Repositories.Interfaces;

namespace FinalExam_B14.Repositories.Implementations
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {

        }
    }
}
