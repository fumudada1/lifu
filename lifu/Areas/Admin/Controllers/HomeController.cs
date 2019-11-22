using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using lifu.Filters;
using lifu.Models;
using Newtonsoft.Json;

namespace lifu.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private BackendContext db = new BackendContext();
        //
        // GET: /Admin/Home/

        [PermissionFilters]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                Member member = ValidateUser(userName, password);
                if (member != null)
                {
                    Utility.GetPerssion(member);
                    string userData = JsonConvert.SerializeObject(member);
                    Utility.SetAuthenTicket(userData, userName);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.message = "登入失敗!";
                return View();
            }
            ViewBag.message = "登入失敗!";
            return View();

        }

        private Member ValidateUser(string userName, string password)
        {
            Member member = db.Members.SingleOrDefault(o => o.Account == userName);
            if (member == null)
            {
                return null;
            }
            string saltPassword = Utility.GenerateHashWithSalt(password, member.PasswordSalt);
            return saltPassword == member.Password ? member : null;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return Redirect(FormsAuthentication.LoginUrl);


        }

    }
}
