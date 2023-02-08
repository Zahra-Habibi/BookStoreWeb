using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
