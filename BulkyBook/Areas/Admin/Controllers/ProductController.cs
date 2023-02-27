
using BulkyBook.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Migrations;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using BulkyBook.Models.ViewModels;
using Microsoft.Extensions.Hosting;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = db;
            _hostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {

            IEnumerable<Product> objCategoryList = (IEnumerable<Product>)_unitOfWork.Product.GetAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                  u => new SelectListItem
                  {
                      Text = u.Name,
                      Value = u.Id.ToString()
                  });
            IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                  u => new SelectListItem
                  {
                      Text = u.Name,
                      Value = u.Id.ToString()
                  });

            if (id == null || id == 0)
            {
                //create product
                ViewBag.CategoryList = CategoryList;
                ViewBag.CoverTypeList = CoverTypeList;
                return View(product);
            }
            else
            {
                Product product1 = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(product1);

                //update product
            }


        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.ImgUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.ImgUrl = @"\images\products\" + fileName + extension;

                }
                if (obj.Id == 0)
                {
                    _unitOfWork.Product.Add(obj);
                }
                else
                {
                    _unitOfWork.Product.Update(obj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");

            }

            return View(obj);
        }


    }
}
