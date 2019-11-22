using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lifu.Models;

namespace lifu.Controllers
{
    public class ServiceController : Controller
    {
        private BackendContext db = new BackendContext();
        //
        // GET: /Service/

        public ActionResult Index()
        {
            ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p => p.InitDate), "Subject", "Subject");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index(ServiceView serviceView, string CaseId)
        {
            if (ModelState.IsValid)
            {
                Service service=new Service();

                service.Name = serviceView.Name;
                service.AreaCodeAM = serviceView.AreaCodeAM;
                service.TelphoneAM = serviceView.TelphoneAM;
                service.AreaCodePM = serviceView.AreaCodePM;
                service.TelphonePM = serviceView.TelphonePM;
                service.Mobile = serviceView.Mobile;
                service.Email = serviceView.Email;
                if (serviceView.User == "是")
                {
                    serviceView.User = CaseId;
                }
                service.User = "否";
                service.ShortMemo = serviceView.ShortMemo;

                db.Services.Add(service);
                service.Create(db, db.Services);
                ViewBag.state = "success";
                ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p => p.InitDate), "Subject", "Subject");
                return View(serviceView);
            }
            ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p => p.InitDate), "Subject", "Subject");
            return View(serviceView);
        }

    }
}
