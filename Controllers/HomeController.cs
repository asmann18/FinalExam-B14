using Microsoft.AspNetCore.Mvc;

namespace FinalExam_B14.Controllers
{
    public class HomeController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }
    }
}