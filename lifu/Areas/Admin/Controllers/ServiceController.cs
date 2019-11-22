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
    public class ServiceController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;
        //
        // GET: /Admin/Service/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.Services.OrderByDescending(p=>p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Name, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.Services.OrderByDescending(p=>p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        

        //
        // GET: /Admin/Service/Details/5

        public ActionResult Details(int id = 0)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // GET: /Admin/Service/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Service/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(Service service )
        {
            if (ModelState.IsValid)
            {

                db.Services.Add(service);
                service.Create(db,db.Services);
                return RedirectToAction("Index");
            }

            return View(service);
        }

        //
        // GET: /Admin/Service/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // POST: /Admin/Service/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {

               //db.Entry(service).State = EntityState.Modified;
                service.Update();
                return RedirectToAction("Edit", null, new { Id = service.Id });
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SendMail(int Id, string Email, string Replay)
        {
            Utility.SystemSendMail("service@lfb.com.tw ", Email, "利富建設客戶服務回覆信件", Replay);

            return RedirectToAction("index");
        }

        //
        // GET: /Admin/Service/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // POST: /Admin/Service/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
