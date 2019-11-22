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
    public class CaseServiceTypeController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 2000;
        //
        // GET: /Admin/CaseServiceType/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.CaseServiceTypes.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.CaseServiceTypes.OrderByDescending(p=>p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
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
                    CaseServiceType caseservicetype = db.CaseServiceTypes.Find(Convert.ToInt16(itemDatas[0]));
                    caseservicetype.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /Admin/CaseServiceType/Details/5

        public ActionResult Details(int id = 0)
        {
            CaseServiceType caseservicetype = db.CaseServiceTypes.Find(id);
            if (caseservicetype == null)
            {
                return HttpNotFound();
            }
            return View(caseservicetype);
        }

        //
        // GET: /Admin/CaseServiceType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/CaseServiceType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(CaseServiceType caseservicetype )
        {
            if (ModelState.IsValid)
            {

                db.CaseServiceTypes.Add(caseservicetype);
                caseservicetype.Create(db,db.CaseServiceTypes);
                return RedirectToAction("Index");
            }

            return View(caseservicetype);
        }

        //
        // GET: /Admin/CaseServiceType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CaseServiceType caseservicetype = db.CaseServiceTypes.Find(id);
            if (caseservicetype == null)
            {
                return HttpNotFound();
            }
            return View(caseservicetype);
        }

        //
        // POST: /Admin/CaseServiceType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(CaseServiceType caseservicetype)
        {
            if (ModelState.IsValid)
            {

               //db.Entry(caseservicetype).State = EntityState.Modified;
                caseservicetype.Update();
                return RedirectToAction("Index");
            }
            return View(caseservicetype);
        }

        //
        // GET: /Admin/CaseServiceType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CaseServiceType caseservicetype = db.CaseServiceTypes.Find(id);
            if (caseservicetype == null)
            {
                return HttpNotFound();
            }
            return View(caseservicetype);
        }

        //
        // POST: /Admin/CaseServiceType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaseServiceType caseservicetype = db.CaseServiceTypes.Find(id);
            db.CaseServiceTypes.Remove(caseservicetype);
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
