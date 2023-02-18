﻿using BulkyBook.Models;
using BulkyBook.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;
        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> objcategory = _db.GetAll();
            return View(objcategory);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The displayorder can not exactly match name.");
            }
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();
                TempData["success"] = "category create successfully.";
                return RedirectToAction("Index");
            }
            return View(obj);
           
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            //Category category = _db.categories.Find(id);
            Category CategoryFromDbFirst=_db.GetFirstOrDefault(c => c.Name == "id");
            if(CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The displayorder can not exactly match name.");
            }
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "category Update successfully.";
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
            Category category = _db.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj= _db.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
                _db.Remove(obj);
                _db.Save();
            TempData["success"] = "category Delete successfully.";
            return RedirectToAction("Index");
        }

    }
}
