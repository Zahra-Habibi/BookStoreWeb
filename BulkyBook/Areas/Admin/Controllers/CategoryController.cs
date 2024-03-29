﻿using BulkyBook.Models;
using BulkyBook.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _appc;
        public CategoryController(ApplicationDbContext db)
        {
            _appc = db;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Category> obg=_appc.categories.ToList();
            //IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            //return View(objCategoryList);
            return View(obg);

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
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            //if (ModelState.IsValid)
            //{
            //    _unitOfWork.Category.Add(obj);
            //    _unitOfWork.Save();
            //    TempData["success"] = "Category created successfully";
            //    return RedirectToAction("Index");
            //}
            if (ModelState.IsValid)
            {
                _appc.categories.Add(obj);
                _appc.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
          

        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            ////var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            //if (categoryFromDbFirst == null)
            //{
            //    return NotFound();
            //}
            Category categoryFromDbFirst = _appc.categories.Find(id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _appc.categories.Update(obj);
                _appc.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            ////var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            //if (categoryFromDbFirst == null)
            //{
            //    return NotFound();
            //}

            Category categoryFromDbFirst = _appc.categories.Find(id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            Category obj = _appc.categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _appc.categories.Remove(obj);
            _appc.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
