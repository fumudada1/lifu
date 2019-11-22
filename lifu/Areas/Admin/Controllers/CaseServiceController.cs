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
    public class CaseServiceController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;
        //
        // GET: /Admin/CaseService/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var caseservices = db.CaseServices.Include(c => c.Cost).Include(c => c.Area).Include(c => c.Type).OrderByDescending(p=>p.InitDate);
            ViewBag.CostId = new SelectList(db.CaseServiceCosts.OrderBy(p=>p.InitDate), "Id", "Subject");
            ViewBag.AreaId = new SelectList(db.CaseServiceAreas.OrderBy(p=>p.InitDate), "Id", "Subject");
            ViewBag.TypeId = new SelectList(db.CaseServiceTypes.OrderBy(p=>p.InitDate), "Id", "Subject");
            return View(caseservices.OrderByDescending(p=>p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(System.Int32? CostId,System.Int32? AreaId,System.Int32? TypeId,string Name,lifu.Models.MoreInformation? MoerInformation, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var caseservices = db.CaseServices.Include(c => c.Cost).Include(c => c.Area).Include(c => c.Type).OrderByDescending(p => p.InitDate).AsQueryable();
            ViewBag.CostId = new SelectList(db.CaseServiceCosts.OrderBy(p=>p.InitDate), "Id", "Subject");
            ViewBag.AreaId = new SelectList(db.CaseServiceAreas.OrderBy(p=>p.InitDate), "Id", "Subject");
            ViewBag.TypeId = new SelectList(db.CaseServiceTypes.OrderBy(p=>p.InitDate), "Id", "Subject");
            if (CostId.HasValue) 
            { 
                caseservices = caseservices.Where(w => w.CostId == CostId); 
            } 
            if (AreaId.HasValue) 
            { 
                caseservices = caseservices.Where(w => w.AreaId == AreaId); 
            } 
            if (TypeId.HasValue) 
            { 
                caseservices = caseservices.Where(w => w.TypeId == TypeId); 
            } 
            if (!string.IsNullOrEmpty(Name)) 
            { 
                caseservices = caseservices.Where(w => w.Name.Contains(Name)); 
            } 
 
            if (MoerInformation.HasValue) 
            { 
                caseservices = caseservices.Where(w => w.MoerInformation == MoerInformation); 
            } 

            ViewBag.Name = Name;
            return View(caseservices.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        

        //
        // GET: /Admin/CaseService/Details/5

        public ActionResult Details(int id = 0)
        {
            CaseService caseservice = db.CaseServices.Find(id);
            if (caseservice == null)
            {
                return HttpNotFound();
            }
            return View(caseservice);
        }

        //
        // GET: /Admin/CaseService/Create

        public ActionResult Create()
        {
            ViewBag.CostId = new SelectList(db.CaseServiceCosts.OrderBy(p=>p.ListNum), "Id", "Subject");
            ViewBag.AreaId = new SelectList(db.CaseServiceAreas.OrderBy(p=>p.ListNum), "Id", "Subject");
            ViewBag.TypeId = new SelectList(db.CaseServiceTypes.OrderBy(p=>p.ListNum), "Id", "Subject");
            return View();
        }

        //
        // POST: /Admin/CaseService/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(CaseService caseservice )
        {
            if (ModelState.IsValid)
            {

                db.CaseServices.Add(caseservice);
                caseservice.Create(db,db.CaseServices);
                return RedirectToAction("Index");
            }

            ViewBag.CostId = new SelectList(db.CaseServiceCosts.OrderBy(p=>p.ListNum), "Id", "Subject", caseservice.CostId);
            ViewBag.AreaId = new SelectList(db.CaseServiceAreas.OrderBy(p=>p.ListNum), "Id", "Subject", caseservice.AreaId);
            ViewBag.TypeId = new SelectList(db.CaseServiceTypes.OrderBy(p=>p.ListNum), "Id", "Subject", caseservice.TypeId);
            return View(caseservice);
        }

        //
        // GET: /Admin/CaseService/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CaseService caseservice = db.CaseServices.Find(id);
            if (caseservice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CostId = new SelectList(db.CaseServiceCosts.OrderBy(p=>p.ListNum), "Id", "Subject", caseservice.CostId);
            ViewBag.AreaId = new SelectList(db.CaseServiceAreas.OrderBy(p=>p.ListNum), "Id", "Subject", caseservice.AreaId);
            ViewBag.TypeId = new SelectList(db.CaseServiceTypes.OrderBy(p=>p.ListNum), "Id", "Subject", caseservice.TypeId);
            return View(caseservice);
        }

        //
        // POST: /Admin/CaseService/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(CaseService caseservice)
        {
            if (ModelState.IsValid)
            {

               //db.Entry(caseservice).State = EntityState.Modified;
                caseservice.Update();
                return RedirectToAction("Index");
            }
            ViewBag.CostId = new SelectList(db.CaseServiceCosts.OrderBy(p=>p.ListNum), "Id", "Subject", caseservice.CostId);
            ViewBag.AreaId = new SelectList(db.CaseServiceAreas.OrderBy(p=>p.ListNum), "Id", "Subject", caseservice.AreaId);
            ViewBag.TypeId = new SelectList(db.CaseServiceTypes.OrderBy(p=>p.ListNum), "Id", "Subject", caseservice.TypeId);
            return View(caseservice);
        }

        //
        // GET: /Admin/CaseService/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CaseService caseservice = db.CaseServices.Find(id);
            if (caseservice == null)
            {
                return HttpNotFound();
            }
            return View(caseservice);
        }

        //
        // POST: /Admin/CaseService/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaseService caseservice = db.CaseServices.Find(id);
            db.CaseServices.Remove(caseservice);
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
