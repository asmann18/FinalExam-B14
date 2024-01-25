using FinalExam_B14.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam_B14.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _service;

        public HomeController(IEmployeeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var employees=await _service.GetAllEmployeesAsync();
            return View(employees);
        }
    }
}