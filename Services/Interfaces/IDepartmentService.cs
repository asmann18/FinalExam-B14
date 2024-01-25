using FinalExam_B14.Areas.Admin.ViewModels.Common;
using FinalExam_B14.Areas.Admin.ViewModels.DepartmentVMs;
using FinalExam_B14.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FinalExam_B14.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<bool> CreateDepartmentAsync(DepartmentCreateVM vm,ModelStateDictionary ModelState);
        Task<bool> UpdateDepartmentAsync(DepartmentUpdateVM vm, ModelStateDictionary ModelState);
        Task<bool> DeleteAsync(int id);
        Task<PaginationVM<Department>> GetAllDepartmentsAsync(int page = 1);
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<DepartmentUpdateVM> GetUpdatedDepartmentAsync(int id);
    }
}
