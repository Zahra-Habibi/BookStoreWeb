using BulkyBook.Models;
using BulkyBook.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _db;
        public ProductController(IProductRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> objcategory = _db.GetAll();
            return View(objcategory);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
          

            if (ModelState.IsValid)
            {
                _db.Add(obj);
                TempData["success"] = "Product create successfully.";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category category = _db.categories.Find(id);
            Product CategoryFromDbFirst = _db.GetFirstOrDefault(c => c.Id == id);
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {
           
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                TempData["success"] = "Product Update successfully.";
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _db.GetFirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            TempData["success"] = "product Delete successfully.";
            return RedirectToAction("Index");
        }

    }
}
