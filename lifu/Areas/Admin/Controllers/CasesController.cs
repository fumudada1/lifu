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
    public class CasesController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;
        //
        // GET: /Admin/Cases/

        public ActionResult Index(int? page)
        {
            string Subject = Session["CasesSubject"] != null ? Session["CasesSubject"].ToString() : null;
            int? AreaId = Session["CasesAreaId"] != null ? (int?)Session["CasesAreaId"] : null;
            CaseStatus? Status = Session["CasesStatus"] != null ? (CaseStatus?) Session["CasesStatus"] : null;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var cases = db.Cases.Include(c => c.Area).OrderByDescending(p => p.InitDate).AsQueryable();
            ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.InitDate), "Id", "Subject");
            if (!string.IsNullOrEmpty(Subject))
            {
                cases = cases.Where(w => w.Subject.Contains(Subject));
            }

            if (AreaId.HasValue)
            {
                cases = cases.Where(w => w.AreaId == AreaId);
            }
            if (Status.HasValue)
            {
                cases = cases.Where(w => w.Status == Status);
            }
            ViewBag.Status = Status;
            ViewBag.Subject = Subject;
            return View(cases.Where(x => x.Status == CaseStatus.新案推薦).OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject, System.Int32? AreaId, CaseStatus? Status, int? page)
        {
            Session["CasesSubject"] = Subject;
            Session["CasesAreaId"] = AreaId;
            Session["CasesStatus"] = AreaId;


            return RedirectToAction("Index");
        }

        public ActionResult Old(int? page)
        {


            string Subject = Session["OldCasesSubject"] != null ? Session["OldCasesSubject"].ToString() : null;
            int? AreaId = Session["OldCasesAreaId"] != null ? (int?)Session["OldCasesAreaId"] : null;
            CaseStatus? Status = Session["OldCasesStatus"] != null ? (CaseStatus?)Session["OldCasesStatus"] : null;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var cases = db.Cases.Include(c => c.Area).OrderByDescending(p => p.InitDate).AsQueryable();
            ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.InitDate), "Id", "Subject");
            if (!string.IsNullOrEmpty(Subject))
            {
                cases = cases.Where(w => w.Subject.Contains(Subject));
            }

            if (AreaId.HasValue)
            {
                cases = cases.Where(w => w.AreaId == AreaId);
            }
            if (Status.HasValue)
            {
                cases = cases.Where(w => w.Status == Status);
            }
            ViewBag.Status = Status;
            ViewBag.Subject = Subject;
            return View(cases.Where(x => x.Status == CaseStatus.經典個案).OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Old(string Subject, System.Int32? AreaId, lifu.Models.CaseStatus? Status, int? page)
        {
            Session["OldCasesSubject"] = Subject;
            Session["OldCasesAreaId"] = AreaId;
            Session["OldCasesStatus"] = Status;


            return RedirectToAction("Index");
        }





        //
        // GET: /Admin/Cases/Details/5

        public ActionResult Details(int id = 0)
        {
            Cases cases = db.Cases.Find(id);
            if (cases == null)
            {
                return HttpNotFound();
            }
            return View(cases);
        }

        //
        // GET: /Admin/Cases/Create

        public ActionResult Create()
        {
            ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject");
            return View();
        }

        //
        // POST: /Admin/Cases/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Cases cases, HttpPostedFileBase UpImageUrls, HttpPostedFileBase ServerImageUrls, HttpPostedFileBase AreaImageUrls)
        {
            if (ModelState.IsValid)
            {
                if (UpImageUrls != null)
                {
                    if (UpImageUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject", cases.AreaId);
                        return View(cases);
                    }

                    cases.UpImageUrl = Utility.SaveUpImage(UpImageUrls);
                    Utility.GenerateThumbnailImage(cases.UpImageUrl, UpImageUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 225, 368);
                }
                System.Threading.Thread.Sleep(1000);

                if (ServerImageUrls != null)
                {
                    if (ServerImageUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject", cases.AreaId);
                        return View(cases);
                    }

                    cases.ServerImageUrl = Utility.SaveUpImage(ServerImageUrls);
                    Utility.GenerateThumbnailImage(cases.ServerImageUrl, ServerImageUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 167, 115);
                }
                System.Threading.Thread.Sleep(1000);

                if (AreaImageUrls != null)
                {
                    if (AreaImageUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject", cases.AreaId);
                        return View(cases);
                    }

                    cases.AreaImageUrl = Utility.SaveUpImage(AreaImageUrls);
                    Utility.GenerateThumbnailImage(cases.AreaImageUrl, AreaImageUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 167, 115);
                }
                System.Threading.Thread.Sleep(1000);

                db.Cases.Add(cases);
                cases.Create(db, db.Cases);
                return RedirectToAction("Edit", null, new { Id = cases.Id, type = "1" });
            }

            ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject", cases.AreaId);
            return View(cases);
        }

        //
        // GET: /Admin/Cases/Edit/5

        public ActionResult Edit(int id = 0, int type = 0)
        {
            Cases cases = db.Cases.Find(id);
            if (cases == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cid = id;
            ViewBag.type = type;
            ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject", cases.AreaId);
            return View(cases);
        }

        //
        // POST: /Admin/Cases/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Cases cases, HttpPostedFileBase UpImageUrls, HttpPostedFileBase ServerImageUrls, HttpPostedFileBase AreaImageUrls)
        {
            if (ModelState.IsValid)
            {
                if (UpImageUrls != null)
                {
                    if (UpImageUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject", cases.AreaId);
                        return View(cases);
                    }

                    cases.UpImageUrl = Utility.SaveUpImage(UpImageUrls);
                    Utility.GenerateThumbnailImage(cases.UpImageUrl, UpImageUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127);
                }
                System.Threading.Thread.Sleep(1000);

                if (ServerImageUrls != null)
                {
                    if (ServerImageUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject", cases.AreaId);
                        return View(cases);
                    }

                    cases.ServerImageUrl = Utility.SaveUpImage(ServerImageUrls);
                    Utility.GenerateThumbnailImage(cases.ServerImageUrl, ServerImageUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127);
                }
                System.Threading.Thread.Sleep(1000);
                if (AreaImageUrls != null)
                {
                    if (AreaImageUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject", cases.AreaId);
                        return View(cases);
                    }

                    cases.AreaImageUrl = Utility.SaveUpImage(AreaImageUrls);
                    Utility.GenerateThumbnailImage(cases.AreaImageUrl, AreaImageUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 307, 169);
                }
                System.Threading.Thread.Sleep(1000);
                //db.Entry(cases).State = EntityState.Modified;
                cases.Update();
                if (Request["type"] == "old")
                {
                    return RedirectToActionPermanent("Old", null,
                                        new { page = Request["page"] });
                }
                return RedirectToActionPermanent("Index", null,
                    new { page = Request["page"] });
            }
            ViewBag.AreaId = new SelectList(db.Areas.OrderBy(p => p.ListNum), "Id", "Subject", cases.AreaId);
            return View(cases);
        }

        //
        // GET: /Admin/Cases/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cases cases = db.Cases.Find(id);
            if (cases == null)
            {
                return HttpNotFound();
            }
            return View(cases);
        }

        //
        // POST: /Admin/Cases/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cases cases = db.Cases.Find(id);
            db.Cases.Remove(cases);
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
