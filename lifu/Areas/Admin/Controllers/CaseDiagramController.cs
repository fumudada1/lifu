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
    public class CaseDiagramController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;
        //
        // GET: /Admin/CaseDiagram/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var casediagrams = db.CaseDiagrams.Include(c => c.Case).OrderByDescending(p=>p.ListNum);
            ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p=>p.InitDate), "Id", "Subject");
            return View(casediagrams.OrderByDescending(p=>p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject,System.Int32? CaseId, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var casediagrams = db.CaseDiagrams.Include(c => c.Case).OrderByDescending(p => p.ListNum).AsQueryable();
            ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p => p.InitDate), "Id", "Subject");
            if (!string.IsNullOrEmpty(Subject)) 
            { 
                casediagrams = casediagrams.Where(w => w.Subject.Contains(Subject)); 
            } 
 
            if (CaseId.HasValue) 
            { 
                casediagrams = casediagrams.Where(w => w.CaseId == CaseId); 
            } 

            ViewBag.Subject = Subject;
            return View(casediagrams.OrderByDescending(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
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
                    CaseDiagram casediagram = db.CaseDiagrams.Find(Convert.ToInt16(itemDatas[0]));
                    casediagram.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            return RedirectToAction("Edit", "Cases", new { Id = Id, type = "2" });
        }
        

        //
        // GET: /Admin/CaseDiagram/Details/5

        public ActionResult Details(int id = 0)
        {
            CaseDiagram casediagram = db.CaseDiagrams.Find(id);
            if (casediagram == null)
            {
                return HttpNotFound();
            }
            return View(casediagram);
        }

        //
        // GET: /Admin/CaseDiagram/Create

        public ActionResult Create(int caseId)
        {
            ViewBag.CaseId = caseId;
            return View();
        }

        //
        // POST: /Admin/CaseDiagram/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(CaseDiagram casediagram ,HttpPostedFileBase UpPicUrls)
        {
            if (ModelState.IsValid)
            {
                if (UpPicUrls != null){ 
                    if (UpPicUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.CaseId = new SelectList(db.Cases.OrderBy(p => p.InitDate), "Id", "Subject", casediagram.CaseId);
                        return View(casediagram); 
                    } 
 
                    casediagram.UpPicUrl = Utility.SaveUpImage(UpPicUrls); 
                    Utility.GenerateThumbnailImage(casediagram.UpPicUrl, UpPicUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 650, 522); 
                } 
                System.Threading.Thread.Sleep(1000); 

                db.CaseDiagrams.Add(casediagram);
                casediagram.Create(db,db.CaseDiagrams);
                //return RedirectToAction("Index");
            }

            ViewBag.CaseId = casediagram.CaseId;
            ViewBag.close = "true";
            return View(casediagram);
        }

        //
        // GET: /Admin/CaseDiagram/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CaseDiagram casediagram = db.CaseDiagrams.Find(id);
            if (casediagram == null)
            {
                return HttpNotFound();
            }
           
            return View(casediagram);
        }

        //
        // POST: /Admin/CaseDiagram/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(CaseDiagram casediagram,HttpPostedFileBase UpPicUrls)
        {
            if (ModelState.IsValid)
            {
                if (UpPicUrls != null){ 
                    if (UpPicUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!";
                        
                        return View(casediagram); 
                    } 
 
                    casediagram.UpPicUrl = Utility.SaveUpImage(UpPicUrls); 
                    Utility.GenerateThumbnailImage(casediagram.UpPicUrl, UpPicUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 650, 522); 
                } 
                System.Threading.Thread.Sleep(1000); 

               //db.Entry(casediagram).State = EntityState.Modified;
                casediagram.Update();
                //return RedirectToAction("Index");
            }
            ViewBag.close = "true";
            return View(casediagram);
        }

        //
        // GET: /Admin/CaseDiagram/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CaseDiagram casediagram = db.CaseDiagrams.Find(id);
            if (casediagram == null)
            {
                return HttpNotFound();
            }
            return View(casediagram);
        }

        //
        // POST: /Admin/CaseDiagram/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Diagramid)
        {
            CaseDiagram casediagram = db.CaseDiagrams.Find(Diagramid);
            db.CaseDiagrams.Remove(casediagram);
            db.SaveChanges();
            return RedirectToAction("Edit", "Cases", new { Id = casediagram.CaseId, type = "2" });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

}
