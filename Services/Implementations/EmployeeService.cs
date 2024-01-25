using FinalExam_B14.Areas.Admin.ViewModels.Common;
using FinalExam_B14.Areas.Admin.ViewModels.DepartmentVMs;
using FinalExam_B14.Areas.Admin.ViewModels.EmployeeVMs;
using FinalExam_B14.Models;
using FinalExam_B14.Repositories.Interfaces;
using FinalExam_B14.Services.Interfaces;
using FinalExam_B14.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var employee = await _repository.GetSingleAsync(x => x.Id == id);
            if (employee is null)
                return false;
            employee.ImagePath.DeleteFile(_environment.ContentRootPath, "wwwroot", "assets", "img");
            _repository.Delete(employee);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<PaginationVM<Employee>> GetAllEmployeesAsync(int page = 1)
        {
            if (page < 1)
                return new();

            var employees = await _repository.GetAll(null, "Department").Skip((page - 1) * 10).Take(10).ToListAsync();


            return new() { Items = employees, CurrentPage = page, PageCount = (int)Math.Ceiling((decimal)_repository.GetAll().Count() / 10) };
        }

        public EmployeeCreateVM GetCreatedEmployee(EmployeeCreateVM vm)
        {



            vm.Departments = new SelectList(_departmentRepository.GetAll(), nameof(Department.Id), nameof(Department.Name));

            return vm;
        }


        public EmployeeUpdateVM GetUpdatedEmployee(EmployeeUpdateVM vm)
        {



            vm.Departments = new SelectList(_departmentRepository.GetAll(), nameof(Department.Id), nameof(Department.Name));

            return vm;
        }
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _repository.GetSingleAsync(x => x.Id == id, "Department");
            return employee;
        }


        public async Task<EmployeeUpdateVM> GetUpdatedEmployeeAsync(int id)
        {
            var employee = await GetEmployeeByIdAsync(id);
            if (employee is null)
                return null;
            EmployeeUpdateVM vm = new()
            {
                Id = employee.Id,
                DepartmentId = employee.DepartmentId,
                Description = employee.Description,
                ImagePath = employee.ImagePath,
                LinkedInLink = employee.LinkedInLink,
                TwitterLink = employee.TwitterLink,
                InstagramLink = employee.InstagramLink,
                FullName = employee.FullName,
                FacebookLink = employee.FacebookLink,
                Departments = new SelectList(_departmentRepository.GetAll(), nameof(Department.Id), nameof(Department.Name))

            };


            return vm;
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeUpdateVM vm, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            var existedEmployee = await _repository.GetSingleAsync(x => x.Id == vm.Id);
            if (existedEmployee is null)
            {
                return false;
            }

            var isExistCategory = await _departmentRepository.AnyAsync(x => x.Id == vm.DepartmentId);
            if (!isExistCategory)
            {
                ModelState.AddModelError("DepartmentId", "Department not found");
                return false;
            }

            if (vm.Image is not null)
            {

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

                existedEmployee.ImagePath.DeleteFile(_environment.ContentRootPath, "wwwroot", "assets", "img");
                existedEmployee.ImagePath = await vm.Image.CreateFile(_environment.ContentRootPath, "wwwroot", "assets", "img");
            }

            existedEmployee.FullName = vm.FullName.Trim();
            existedEmployee.DepartmentId = vm.DepartmentId;
            existedEmployee.Description = vm.Description.Trim();
            existedEmployee.FacebookLink = vm.FacebookLink.Trim();
            existedEmployee.InstagramLink = vm.InstagramLink.Trim();
            existedEmployee.LinkedInLink = vm.LinkedInLink.Trim();
            existedEmployee.TwitterLink = vm.TwitterLink.Trim();

            _repository.Update(existedEmployee);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
