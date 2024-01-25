using FinalExam_B14.Areas.Admin.ViewModels.Common;
using FinalExam_B14.Areas.Admin.ViewModels.DepartmentVMs;
using FinalExam_B14.Areas.Admin.ViewModels.EmployeeVMs;
using FinalExam_B14.Models;
using FinalExam_B14.Repositories.Interfaces;
using FinalExam_B14.Services.Interfaces;
using FinalExam_B14.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.AccessControl;

namespace FinalExam_B14.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _environment;

        public EmployeeService(IDepartmentRepository departmentRepository, IEmployeeRepository repository, IWebHostEnvironment environment)
        {
            _departmentRepository = departmentRepository;
            _repository = repository;
            _environment = environment;
        }

        public async Task<bool> CreateEmployeeAsync(EmployeeCreateVM vm, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
                return false;

            var isExistCategory = await _departmentRepository.AnyAsync(x => x.Id == vm.DepartmentId);
            if (!isExistCategory)
            {
                ModelState.AddModelError("DepartmentId", "Department not found");
                return false;
            }

            var validateImage = vm.Image.ValidateType();
            if (!validateImage)
            {
                ModelState.AddModelError("Image", "Please enter valid type");
                return false;
            }
            validateImage = vm.Image.ValidateSize(2);
            if (!validateImage)
            {
                ModelState.AddModelError("Image", "Please enter valid image,size is very big");
                return false;
            }

            Employee employee = new Employee()
            {
                FullName = vm.FullName.Trim(),
                ImagePath = await vm.Image.CreateFile(_environment.ContentRootPath, "wwwroot", "assets", "img"),
                DepartmentId = vm.DepartmentId,
                Description = vm.Description.Trim(),
                FacebookLink = vm.FacebookLink.Trim(),
                InstagramLink = vm.InstagramLink.Trim(),
                LinkedInLink = vm.LinkedInLink.Trim(),
                TwitterLink = vm.TwitterLink.Trim(),
            };

            await _repository.CreateAsync(employee);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
           var employee=await _repository.GetSingleAsync(x=>x.Id==id);
            if (employee is null)
                return false;

            _repository.Delete(employee);
        }

        public Task<PaginationVM<Employee>> GetAllEmployeesAsync(int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetEmployeeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentUpdateVM> GetUpdatedEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEmployeeAsync(EmployeeUpdateVM vm, ModelStateDictionary ModelState)
        {
            throw new NotImplementedException();
        }
    }
}
