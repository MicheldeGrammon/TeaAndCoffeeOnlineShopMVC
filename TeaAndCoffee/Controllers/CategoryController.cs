﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TeaAndCoffee_DataAccess;
using TeaAndCoffee_DataAccess.Repository.IRepository;
using TeaAndCoffee_Models;
using TeaAndCoffee_Utility;

namespace TeaAndCoffee.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _catRepo;

        public CategoryController(ICategoryRepository catRepo)
        {
            _catRepo = catRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _catRepo.GetAll();
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Add(obj);
                _catRepo.Save();
                TempData[WC.Success] = "Category created successfully";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error while creating category";
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _catRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Update(obj);
                _catRepo.Save();
                TempData[WC.Success] = "Category edit successfully";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error while deleting category";

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _catRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _catRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            _catRepo.Remove(obj);
            _catRepo.Save();

            TempData[WC.Warning] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
