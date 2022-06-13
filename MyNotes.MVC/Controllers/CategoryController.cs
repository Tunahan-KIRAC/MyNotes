using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNotes.BusinessLayer;
using MyNotes.BusinessLayer.Models;
using MyNotes.BusinessLayer.ValueObject;

namespace MyNotes.MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        CategoryManager cm = new CategoryManager();
        // GET: Category
        public ActionResult Index()
        {
            var cat = cm.IndexCat();
            return View(cat);
            //return View(cm.List());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }

            //Category cat = cm.Find(s => s.Id==id);
            CategoryViewModel cvm = cm.FindCat(id);
            if (cvm == null)
            {
                return HttpNotFound();

            }
            //CategoryViewModel cvm = new CategoryViewModel();
            //cvm.Id = cat.Id;
            //cvm.Title = cat.Tittle;
            //cvm.Description = cat.Description;
            //cvm.ModifiedUserName = cat.ModifiedUserName;
            //cvm.CreatedOn = cat.CreatedOn;
            //cvm.ModifiedOn = cat.ModifiedOn;
            return View(cvm);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel cat)
        {
            ModelState.Remove("Category.CreatedOn");
            ModelState.Remove("Category.ModifiedOn");
            ModelState.Remove("Category.ModifiedUserName");
            if (ModelState.IsValid)
            {
                cm.InsertCat(cat);
                CacheHelper.RemoveCategoriesFromCache();
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            CategoryViewModel category = cm.GetEditCat(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel cat)
        {
            ModelState.Remove("Category.CreatedOn");
            ModelState.Remove("Category.ModifiedOn");
            ModelState.Remove("Category.ModifiedUserName");
            if (ModelState.IsValid)
            {
                cm.UpdateCat(cat);
                CacheHelper.RemoveCategoriesFromCache();
                return RedirectToAction("Index");
            }

            return View(cat);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            CategoryViewModel category = cm.GetEditCat(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            cm.DeleteCat(id);
            CacheHelper.RemoveCategoriesFromCache();
            return RedirectToAction("Index");
        }
    }
}