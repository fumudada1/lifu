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
    public class NewsController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;
        //
        // GET: /Admin/News/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.News.OrderByDescending(p=>p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var news = db.News.AsQueryable();
            if (!string.IsNullOrEmpty(Subject))
            {
                news = news.Where(w => w.Subject.Contains(Subject));
            }
            ViewBag.Subject = Subject;
            return View(news.OrderByDescending(p=>p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        

        //
        // GET: /Admin/News/Details/5

        public ActionResult Details(int id = 0)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // GET: /Admin/News/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(News news ,HttpPostedFileBase UpImageUrls)
        {
            if (ModelState.IsValid)
            {
                if (UpImageUrls != null){ 
                    if (UpImageUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!"; 
                        return View(news); 
                    } 
 
                    news.UpImageUrl = Utility.SaveUpImage(UpImageUrls); 
                    Utility.GenerateThumbnailImage(news.UpImageUrl, UpImageUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

                db.News.Add(news);
                news.Create(db,db.News);
                return RedirectToAction("Index");
            }

            return View(news);
        }

        //
        // GET: /Admin/News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /Admin/News/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(News news,HttpPostedFileBase UpImageUrls)
        {
            if (ModelState.IsValid)
            {
                if (UpImageUrls != null){ 
                    if (UpImageUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!"; 
                        return View(news); 
                    } 
 
                    news.UpImageUrl = Utility.SaveUpImage(UpImageUrls); 
                    Utility.GenerateThumbnailImage(news.UpImageUrl, UpImageUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

               //db.Entry(news).State = EntityState.Modified;
                news.Update();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        //
        // GET: /Admin/News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /Admin/News/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
