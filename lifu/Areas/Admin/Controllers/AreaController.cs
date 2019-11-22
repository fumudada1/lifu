using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using lifu.Filters;
using MvcPaging;
using System.Web.Mvc;
using lifu.Models;

namespace lifu.Areas.Admin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class AreaController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 2000;
        //
        // GET: /Admin/Area/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.Areas.OrderBy(p=>p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.Areas.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Sort(string sortData)
        {
            if (!string.IsNullOrEmpty(sortData))
            {
                string[] tempDatas = sortData.TrimEnd(',').Split(',');
                foreach (string tempData in tempDatas)
                {
                    string[] itemDatas = tempData.Split(':');
                    Area area = db.Areas.Find(Convert.ToInt16(itemDatas[0]));
                    area.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /Admin/Area/Details/5

        public ActionResult Details(int id = 0)
        {
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        //
        // GET: /Admin/Area/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Area/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(Area area )
        {
            if (ModelState.IsValid)
            {

                db.Areas.Add(area);
                area.Create(db,db.Areas);
                return RedirectToAction("Index");
            }

            return View(area);
        }

        //
        // GET: /Admin/Area/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        //
        // POST: /Admin/Area/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(Area area)
        {
            if (ModelState.IsValid)
            {

               //db.Entry(area).State = EntityState.Modified;
                area.Update();
                return RedirectToAction("Index");
            }
            return View(area);
        }

        //
        // GET: /Admin/Area/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        //
        // POST: /Admin/Area/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Area area = db.Areas.Find(id);
            db.Areas.Remove(area);
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
