using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using lifu.Models;

namespace lifu.Areas.Admin.Controllers
{
    public class CasePicturesController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;
        //
        // GET: /Admin/CasePictures/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var casepictures = db.CasePictures.Include(c => c.Case).OrderByDescending(p=>p.ListNum);
            ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p=>p.InitDate), "Id", "Subject");
            return View(casepictures.OrderByDescending(p=>p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject,System.Int32? CaseId, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var casepictures = db.CasePictures.Include(c => c.Case).OrderByDescending(p => p.ListNum).AsQueryable();
            ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p => p.InitDate), "Id", "Subject");
            if (!string.IsNullOrEmpty(Subject)) 
            { 
                casepictures = casepictures.Where(w => w.Subject.Contains(Subject)); 
            } 
 
            if (CaseId.HasValue) 
            { 
                casepictures = casepictures.Where(w => w.CaseId == CaseId); 
            } 

            ViewBag.Subject = Subject;
            return View(casepictures.OrderByDescending(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Sort(string sortData, int Id)
        {
            if (!string.IsNullOrEmpty(sortData))
            {
                string[] tempDatas = sortData.TrimEnd(',').Split(',');
                foreach (string tempData in tempDatas)
                {
                    string[] itemDatas = tempData.Split(':');
                    CasePictures casepictures = db.CasePictures.Find(Convert.ToInt16(itemDatas[0]));
                    casepictures.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            return RedirectToAction("Edit", "Cases", new { Id = Id, type = "1" });
        }
        

        //
        // GET: /Admin/CasePictures/Details/5

        public ActionResult Details(int id = 0)
        {
            CasePictures casepictures = db.CasePictures.Find(id);
            if (casepictures == null)
            {
                return HttpNotFound();
            }
            return View(casepictures);
        }

        //
        // GET: /Admin/CasePictures/Create

        public ActionResult Create(int caseId)
        {
            ViewBag.CaseId = caseId;
            return View();
        }

        //
        // POST: /Admin/CasePictures/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(CasePictures casepictures ,HttpPostedFileBase UpPicUrls)
        {
            if (ModelState.IsValid)
            {
                if (UpPicUrls != null){ 
                    if (UpPicUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p => p.InitDate), "Id", "Subject", casepictures.CaseId);
                        return View(casepictures); 
                    } 
 
                    casepictures.UpPicUrl = Utility.SaveUpImage(UpPicUrls); 
                    Utility.GenerateThumbnailImage(casepictures.UpPicUrl, UpPicUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 90, 60); 
                } 
                System.Threading.Thread.Sleep(1000); 

                db.CasePictures.Add(casepictures);
                casepictures.Create(db,db.CasePictures);
                //return RedirectToAction("Index");
            }

            ViewBag.CaseId = casepictures.CaseId;
            ViewBag.close = "true";
            return View(casepictures);
        }

        //
        // GET: /Admin/CasePictures/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CasePictures casepictures = db.CasePictures.Find(id);
            if (casepictures == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p=>p.InitDate), "Id", "Subject", casepictures.CaseId);
            return View(casepictures);
        }

        //
        // POST: /Admin/CasePictures/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(CasePictures casepictures,HttpPostedFileBase UpPicUrls)
        {
            if (ModelState.IsValid)
            {
                if (UpPicUrls != null){ 
                    if (UpPicUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!"; 
                        return View(casepictures); 
                    } 
 
                    casepictures.UpPicUrl = Utility.SaveUpImage(UpPicUrls); 
                    Utility.GenerateThumbnailImage(casepictures.UpPicUrl, UpPicUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

               //db.Entry(casepictures).State = EntityState.Modified;
                casepictures.Update();
                //return RedirectToAction("Index");
            }
            ViewBag.close = "true";
            return View(casepictures);
        }

        //
        // GET: /Admin/CasePictures/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CasePictures casepictures = db.CasePictures.Find(id);
            if (casepictures == null)
            {
                return HttpNotFound();
            }
            return View(casepictures);
        }

        //
        // POST: /Admin/CasePictures/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Pictureid)
        {
            CasePictures casepictures = db.CasePictures.Find(Pictureid);
            db.CasePictures.Remove(casepictures);
            db.SaveChanges();
            return RedirectToAction("Edit", "Cases", new { Id = casepictures.CaseId, type = "1" });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

}
