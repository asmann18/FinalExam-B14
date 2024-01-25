using FinalExam_B14.Areas.Admin.ViewModels.Common;
using FinalExam_B14.Areas.Admin.ViewModels.DepartmentVMs;
using FinalExam_B14.Areas.Admin.ViewModels.EmployeeVMs;
using FinalExam_B14.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FinalExam_B14.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployeeAsync(EmployeeCreateVM vm, ModelStateDictionary ModelState);
        Task<bool> UpdateEmployeeAsync(EmployeeUpdateVM vm, ModelStateDictionary ModelState);
        Task<bool> DeleteAsync(int id);
        Task<PaginationVM<Employee>> GetAllEmployeesAsync(int page = 1);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<EmployeeUpdateVM> GetUpdatedEmployeeAsync(int id);
        EmployeeCreateVM GetCreatedEmployee(EmployeeCreateVM vm);
        EmployeeUpdateVM GetUpdatedEmployee(EmployeeUpdateVM vm);
    }
}
