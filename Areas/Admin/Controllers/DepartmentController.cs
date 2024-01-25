using FinalExam_B14.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam_B14.Areas.Admin.Controllers
{
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
            return View(departments);
        }
    }
}
