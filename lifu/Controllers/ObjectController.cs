using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lifu.Models;

namespace lifu.Controllers
{
    public class ObjectController : Controller
    {
        private BackendContext db = new BackendContext();
        //
        // GET: /Object/

        public ActionResult Index(int ? id)
        {
            var cases = db.Cases.Include(c => c.Area).Where(x => x.Status == CaseStatus.新案推薦).OrderByDescending(p => p.InitDate).ToList();
            var content = cases.FirstOrDefault(x => x.Id==id) ?? cases.FirstOrDefault();
            if (content == null)
            {
                return RedirectToAction("NoData");
            }
            ViewBag.cases = cases;

            return View(content);
        }
        public ActionResult Diagram(int? id)
        {
            var cases = db.Cases.Include(c => c.Area).Where(x => x.Status == CaseStatus.新案推薦).OrderByDescending(p => p.InitDate).ToList();
            var content = cases.FirstOrDefault(x => x.Id == id) ?? cases.FirstOrDefault();
            if (content == null)
            {
                return RedirectToAction("NoData");
            }
            
            ViewBag.cases = cases;
            return View(content);
        }

        public ActionResult Service()
        {
            var cases = db.Cases.Include(c => c.Area).Where(x => x.Status == CaseStatus.新案推薦).OrderByDescending(p => p.InitDate).ToList();
            ViewBag.cases = cases;
            ViewBag.CostId = new SelectList(db.CaseServiceCosts.OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.AreaId = new SelectList(db.CaseServiceAreas.OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.TypeId = new SelectList(db.CaseServiceTypes.OrderBy(p => p.ListNum), "Id", "Subject");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Service(CaseServiceView caseserviceView, string[] Information)
        {
            if (ModelState.IsValid)
            {
                var informations = "";
                foreach (var s in Information)
                {
                    informations += s + ",";
                }
                CaseService caseservice=new CaseService();
                caseservice.Data = caseserviceView.Data.Trim(',');
                caseservice.Reserved = caseserviceView.Reserved.Trim(',');
                caseservice.CostId = caseserviceView.CostId;
                caseservice.AreaId = caseserviceView.AreaId;
                caseservice.TypeId = caseserviceView.TypeId;
                caseservice.Name = caseserviceView.Name;
                caseservice.Gender = caseserviceView.Gender;
                caseservice.Birthday = caseserviceView.Birthday;
                caseservice.AreaCode = caseserviceView.AreaCode;
                caseservice.Telphone = caseserviceView.Telphone;
                caseservice.Mobile = caseserviceView.Mobile;
                caseservice.Email = caseserviceView.Email;
                caseservice.City = caseserviceView.City;
                caseservice.Division = caseserviceView.Division;
                caseservice.Zip = caseserviceView.Zip;
                caseservice.Address = caseserviceView.Address;
                caseservice.Information = informations.Trim(',');

                db.CaseServices.Add(caseservice);
                caseservice.Create(db, db.CaseServices);
                return RedirectToAction("Index");
            }

            var cases = db.Cases.Include(c => c.Area).Where(x => x.Status == CaseStatus.新案推薦).OrderByDescending(p => p.InitDate).ToList();
            ViewBag.cases = cases;
            ViewBag.CostId = new SelectList(db.CaseServiceCosts.OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.AreaId = new SelectList(db.CaseServiceAreas.OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.TypeId = new SelectList(db.CaseServiceTypes.OrderBy(p => p.ListNum), "Id", "Subject");
            return View();
        }

        public ActionResult NoData()
        {
            return View();
        }
    }
}
