using Microsoft.AspNetCore.Mvc;

namespace backendAlquimia.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
