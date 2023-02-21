using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    public class CoverController : Controller
    {
        private readonly ICoverTypeRepository _db;
        public CoverController(ICoverTypeRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
