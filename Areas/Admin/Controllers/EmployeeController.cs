using FinalExam_B14.Areas.Admin.ViewModels.EmployeeVMs;
using FinalExam_B14.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FinalExam_B14.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]

public class EmployeeController : Controller
{

    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var paginationVM = await _service.GetAllEmployeesAsync(page);
        if (paginationVM.Items == null) return NotFound();

        return View(paginationVM);
    }


    public IActionResult Create()
    {
        EmployeeCreateVM vm = new();
        vm = _service.GetCreatedEmployee(vm);
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreateVM vm)
    {
        var result = await _service.CreateEmployeeAsync(vm, ModelState);
        if (result)
            return RedirectToAction("Index");

        return View(_service.GetCreatedEmployee(vm));
    }

    public async Task<IActionResult> Update(int id)
    {
        var vm = await _service.GetUpdatedEmployeeAsync(id);
        if (vm == null) return NotFound();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(EmployeeUpdateVM vm)
    {
        var result = await _service.UpdateEmployeeAsync(vm, ModelState);
        if (result)
            return RedirectToAction("Index");
        return View(_service.GetUpdatedEmployee(vm));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if(result)
            return RedirectToAction("Index");
        return NotFound();
    }


    public async Task<IActionResult> Detail(int id)
    {
        var employee=await _service.GetEmployeeByIdAsync(id);
        if (employee == null) return NotFound();
        return View(employee);
    }
}
