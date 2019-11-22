using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using lifu.Filters;
using lifu.Models;

namespace lifu.Areas.Admin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class UnitController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 2000;
        //
        // GET: /Unit/

        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            //    IQueryable<Unit> custs =
            //from cust in db.Units select cust;
            //一定要先經過排序才可

            return View(db.Units.OrderBy(c => c.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        //
        // GET: /Unit/Details/5

        public ActionResult Details(int id = 0)
        {
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        //
        // GET: /Unit/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Unit/Create

        [HttpPost]
        public ActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                unit.Create(db, db.Units);
                return RedirectToAction("Index");
            }

            return View(unit);
        }

        //
        // GET: /Unit/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        //
        // POST: /Unit/Edit/5

        [HttpPost]
        public ActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                unit.Update();
                return RedirectToAction("Index");
            }
            return View(unit);
        }

        //
        // GET: /Unit/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        //
        // POST: /Unit/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Unit unit = db.Units.Find(id);
            db.Units.Remove(unit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}