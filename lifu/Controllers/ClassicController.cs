using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lifu.Models;

namespace lifu.Controllers
{
    public class ClassicController : Controller
    {
        private BackendContext db = new BackendContext();
        //
        // GET: /Classic/

        public ActionResult Index()
        {
            var Area = db.Areas.OrderBy(x => x.ListNum).ToList();
            var newCase = db.Cases.Where(x => x.Status == CaseStatus.經典個案).OrderByDescending(x => x.EndDate).FirstOrDefault();
            if (newCase != null)
            {

                return RedirectToActionPermanent("Service", new {id=newCase.Id});
            }
            return View(Area);
        }

        public ActionResult Diagram(int id = 0)
        {

            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            var areas = db.Areas.OrderBy(x => x.ListNum).ToList();
            ViewBag.Areas = areas;
            return View(area);
        }

        public ActionResult Service(int id = 0)
        {
            Cases cases = db.Cases.Find(id);
            if (cases == null)
            {
                return HttpNotFound();
            }
            var areas = db.Areas.OrderBy(x => x.ListNum).ToList();
            ViewBag.Areas = areas;
            return View(cases);
        }

    }
}
