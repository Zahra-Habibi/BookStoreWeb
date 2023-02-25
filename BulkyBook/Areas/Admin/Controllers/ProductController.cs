
using BulkyBook.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Migrations;
using BulkyBook.Models;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }

        [HttpGet]
        public IActionResult Index()
        {

            IEnumerable<Product> objCategoryList = _unitOfWork.Product.GetAll();
            return View(objCategoryList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            if(id == null || id == 0)
            {
                //product = new Product();
                return View(product);
            }
            else
            {

            }
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {

            return View(product);
        }


    }
}
