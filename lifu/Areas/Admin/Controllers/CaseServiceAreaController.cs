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
    public class CaseServiceAreaController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 2000;
        //
        // GET: /Admin/CaseServiceArea/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.CaseServiceAreas.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.CaseServiceAreas.OrderByDescending(p=>p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
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
                    CaseServiceArea caseservicearea = db.CaseServiceAreas.Find(Convert.ToInt16(itemDatas[0]));
                    caseservicearea.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /Admin/CaseServiceArea/Details/5

        public ActionResult Details(int id = 0)
        {
            CaseServiceArea caseservicearea = db.CaseServiceAreas.Find(id);
            if (caseservicearea == null)
            {
                return HttpNotFound();
            }
            return View(caseservicearea);
        }

        //
        // GET: /Admin/CaseServiceArea/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/CaseServiceArea/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(CaseServiceArea caseservicearea )
        {
            if (ModelState.IsValid)
            {

                db.CaseServiceAreas.Add(caseservicearea);
                caseservicearea.Create(db,db.CaseServiceAreas);
                return RedirectToAction("Index");
            }

            return View(caseservicearea);
        }

        //
        // GET: /Admin/CaseServiceArea/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CaseServiceArea caseservicearea = db.CaseServiceAreas.Find(id);
            if (caseservicearea == null)
            {
                return HttpNotFound();
            }
            return View(caseservicearea);
        }

        //
        // POST: /Admin/CaseServiceArea/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(CaseServiceArea caseservicearea)
        {
            if (ModelState.IsValid)
            {

               //db.Entry(caseservicearea).State = EntityState.Modified;
                caseservicearea.Update();
                return RedirectToAction("Index");
            }
            return View(caseservicearea);
        }

        //
        // GET: /Admin/CaseServiceArea/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CaseServiceArea caseservicearea = db.CaseServiceAreas.Find(id);
            if (caseservicearea == null)
            {
                return HttpNotFound();
            }
            return View(caseservicearea);
        }

        //
        // POST: /Admin/CaseServiceArea/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaseServiceArea caseservicearea = db.CaseServiceAreas.Find(id);
            db.CaseServiceAreas.Remove(caseservicearea);
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
