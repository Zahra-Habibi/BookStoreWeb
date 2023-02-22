using BulkyBook.Models;
using BulkyBook.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverController : Controller
    {
        private readonly ICoverTypeRepository _db;
        public CoverController(ICoverTypeRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<CoverType> objcategory = _db.GetAll();
            return View(objcategory);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                TempData["success"] = "CoverType create successfully.";
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
            CoverType CategoryFromDbFirst = _db.GetFirstOrDefault(c => c.Id == id);
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                TempData["success"] = "CoverType Update successfully.";
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
            CoverType category = _db.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
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
            TempData["success"] = "Covertype Delete successfully.";
            return RedirectToAction("Index");
        }

    }
}
