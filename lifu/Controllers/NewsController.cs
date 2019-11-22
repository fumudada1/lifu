using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lifu.Models;
using MvcPaging;

namespace lifu.Controllers
{
    public class NewsController : Controller
    {

        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;

        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            ViewBag.count = db.News.Count();
            return View(db.News.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        public ActionResult Show(int id = 0)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

    }
}
