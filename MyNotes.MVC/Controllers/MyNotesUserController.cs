using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyNotes.BusinessLayer;
using MyNotes.DataAccessLayer;
using MyNotes.EntityLayer;

namespace MyNotes.MVC.Controllers
{
    public class MyNotesUserController : Controller
    {
        private readonly MyNotesUserManager mum = new MyNotesUserManager();

        // GET: MyNotesUser
        public ActionResult Index()
        {
            return View(mum.List());
        }

        // GET: MyNotesUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyNotesUser myNotesUser = mum.Find(s => s.Id == id);
            if (myNotesUser == null)
            {
                return HttpNotFound();
            }
            return View(myNotesUser);
        }

        // GET: MyNotesUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyNotesUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyNotesUser myNotesUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<MyNotesUser> res = mum.Insert(myNotesUser);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(myNotesUser);
                }
                return RedirectToAction("Index");
            }

            return View(myNotesUser);
        }

        // GET: MyNotesUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyNotesUser myNotesUser = mum.Find(s => s.Id == id);
            if (myNotesUser == null)
            {
                return HttpNotFound();
            }
            return View(myNotesUser);
        }

        // POST: MyNotesUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MyNotesUser myNotesUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<MyNotesUser> res = mum.Update(myNotesUser);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(s => ModelState.AddModelError("", s.Message));

                }
                return RedirectToAction("Index");
            }
            return View(myNotesUser);
        }

        // GET: MyNotesUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyNotesUser myNotesUser = mum.Find(s => s.Id == id);
            if (myNotesUser == null)
            {
                return HttpNotFound();
            }
            return View(myNotesUser);
        }

        // POST: MyNotesUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyNotesUser myNotesUser = mum.Find(s => s.Id == id);
            mum.Delete(myNotesUser);

            return RedirectToAction("Index");
        }


    }
}
