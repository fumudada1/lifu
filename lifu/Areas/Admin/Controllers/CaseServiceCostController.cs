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
    public class CaseServiceCostController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 2000;
        //
        // GET: /Admin/CaseServiceCost/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.CaseServiceCosts.OrderBy(p=>p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.CaseServiceCosts.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
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
                    CaseServiceCost caseservicecost = db.CaseServiceCosts.Find(Convert.ToInt16(itemDatas[0]));
                    caseservicecost.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /Admin/CaseServiceCost/Details/5

        public ActionResult Details(int id = 0)
        {
            CaseServiceCost caseservicecost = db.CaseServiceCosts.Find(id);
            if (caseservicecost == null)
            {
                return HttpNotFound();
            }
            return View(caseservicecost);
        }

        //
        // GET: /Admin/CaseServiceCost/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/CaseServiceCost/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(CaseServiceCost caseservicecost )
        {
            if (ModelState.IsValid)
            {

                db.CaseServiceCosts.Add(caseservicecost);
                caseservicecost.Create(db,db.CaseServiceCosts);
                return RedirectToAction("Index");
            }

            return View(caseservicecost);
        }

        //
        // GET: /Admin/CaseServiceCost/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CaseServiceCost caseservicecost = db.CaseServiceCosts.Find(id);
            if (caseservicecost == null)
            {
                return HttpNotFound();
            }
            return View(caseservicecost);
        }

        //
        // POST: /Admin/CaseServiceCost/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(CaseServiceCost caseservicecost)
        {
            if (ModelState.IsValid)
            {

               //db.Entry(caseservicecost).State = EntityState.Modified;
                caseservicecost.Update();
                return RedirectToAction("Index");
            }
            return View(caseservicecost);
        }

        //
        // GET: /Admin/CaseServiceCost/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CaseServiceCost caseservicecost = db.CaseServiceCosts.Find(id);
            if (caseservicecost == null)
            {
                return HttpNotFound();
            }
            return View(caseservicecost);
        }

        //
        // POST: /Admin/CaseServiceCost/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaseServiceCost caseservicecost = db.CaseServiceCosts.Find(id);
            db.CaseServiceCosts.Remove(caseservicecost);
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
