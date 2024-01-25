using FinalExam_B14.Areas.Admin.ViewModels.Common;
using FinalExam_B14.Areas.Admin.ViewModels.DepartmentVMs;
using FinalExam_B14.Models;
using FinalExam_B14.Repositories.Interfaces;
using FinalExam_B14.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace FinalExam_B14.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateDepartmentAsync(DepartmentCreateVM vm, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            var isExist = await _repository.AnyAsync(x => x.Name.ToLower() == vm.Name.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Department is already exist");
                return false;

            }
            Department department = new() { Name = vm.Name.Trim() };
            await _repository.CreateAsync(department);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _repository.GetSingleAsync(x => x.Id == id);
            if (department == null)
            {
                return false;
            }
            _repository.Delete(department);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<PaginationVM<Department>> GetAllDepartmentsAsync(int page = 1)
        {
            if (page < 1)
                return new();

            var departments = await _repository.GetAll().Skip((page - 1) * 10).Take(10).ToListAsync();

            return new() { Items = departments, CurrentPage = page, PageCount = (int)Math.Ceiling((decimal)_repository.GetAll().Count() / 10) };
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _repository.GetSingleAsync(x => x.Id == id, "Employees");
            return department;
        }

        public async Task<DepartmentUpdateVM> GetUpdatedDepartmentAsync(int id)
        {
            var department = await GetDepartmentByIdAsync(id);
            if (department is null)
                return null;

            DepartmentUpdateVM vm = new()
            { Id = id, Name = department.Name };
            return vm;
        }

        public async Task<bool> UpdateDepartmentAsync(DepartmentUpdateVM vm, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            var existedDepartment = await _repository.GetSingleAsync(x => x.Id == vm.Id);
            if (existedDepartment == null)
            {
                return false;
            }
            var isExist = await _repository.AnyAsync(x => x.Name.ToLower() == vm.Name.ToLower().Trim() && vm.Id != x.Id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Department is already exist");
                return false;
            }

            existedDepartment.Name = vm.Name.Trim();
            _repository.Update(existedDepartment);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
