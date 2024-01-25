using FinalExam_B14.Areas.Admin.ViewModels.DepartmentVMs;
using FinalExam_B14.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam_B14.Areas.Admin.Controllers;

[Area("Admin")]
public class DepartmentController : Controller
{
    private readonly IDepartmentService _service;

    public DepartmentController(IDepartmentService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var departments = await _service.GetAllDepartmentsAsync(page);
        if (departments.Items is null || departments.Items.Count==0)
            return NotFound();
        return View(departments);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateVM vm)
    {
        var result = await _service.CreateDepartmentAsync(vm, ModelState);
        if (result)
            return RedirectToAction("Index");

        return View(vm);
    }

    public async Task<IActionResult> Update(int id)
    {
        var vm = await _service.GetUpdatedDepartmentAsync(id);
        if (vm == null)
            return NotFound();
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Update(DepartmentUpdateVM vm)
    {
        var result = await _service.UpdateDepartmentAsync(vm, ModelState);
        if (result)
            return RedirectToAction("Index");

        return View(vm);
    }

    public async Task<ActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (result)
            return RedirectToAction("Index");
        return NotFound();

    }

    public async Task<IActionResult> Detail(int id)
    {
        var department=await _service.GetDepartmentByIdAsync(id);
        if(department == null)
            return NotFound();
        return View(department);
    }
}
