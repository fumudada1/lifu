using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using lifu.Filters;
using lifu.Models;

namespace lifu.Areas.Admin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class MemberController : Controller
    {
        private BackendContext db = new BackendContext();

        //
        // GET: /Member/

        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }

        //
        // GET: /Member/Details/5

        public ActionResult Details(int id = 0)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        //
        // GET: /Member/Create
        public ActionResult Create()
        {
            ViewBag.Units = db.Units.ToList();

            return View();
        }

        //
        // POST: /Member/Create

        [HttpPost]
        public ActionResult Create(Member member, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                //上傳檔案
                if (upfile != null)
                {
                    member.MyPic = Utility.SaveUpFile(upfile);
                }
                member.PasswordSalt = Utility.CreateSalt();
                member.Password = Utility.GenerateHashWithSalt(member.Password, member.PasswordSalt);

                member.Permission = member.Permission ?? "";
                member.JobTitle = member.JobTitle ?? "";
                if (!member.AddMember())
                {
                    ViewBag.Units = db.Units.ToList();
                    ViewBag.Message = "帳號重複!";
                    member.Password = "";
                    return View(member);
                }
                return RedirectToAction("Index");
            }
            //db.Members.Add(member);
            //db.SaveChanges();


            return View(member);
        }




        //
        // GET: /Member/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewBag.Units = db.Units.ToList();
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            string strMenu = Utility.GetMenu(member.Permission);



            ViewBag.TreeScript = strMenu;
            return View(member);
        }

        //
        // POST: /Member/Edit/5

        [HttpPost]
        public ActionResult Edit(Member member)
        {
            //移除驗證
            ModelState.Remove("Account");
            ModelState.Remove("Password");
            member.Password = Request["NewPassword"] != "" ? Utility.GenerateHashWithSalt(Request["NewPassword"], member.PasswordSalt) : Request["hash"];
            member.Permission = member.Permission ?? "";
            if (ModelState.IsValid)
            {
                member.Update();
                return RedirectToAction("Index");
            }
            ViewBag.Units = db.Units.ToList();
            string strMenu = Utility.GetMenu(member.Permission);
            ViewBag.TreeScript = strMenu.Trim();
            return View(member);
        }

        //
        // GET: /Member/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        //
        // POST: /Member/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
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
                    return RedirectToAction("Index","Home");
                }
                ViewBag.message = "登入失敗!";
                return View();
            }
            ViewBag.message = "登入失敗!";
            return View();

        }




        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string newPassword)
        {
            Member member = db.Members.SingleOrDefault(o => o.Account == User.Identity.Name);
            if (member != null)
            {

                member.Password = Utility.GenerateHashWithSalt(newPassword, member.PasswordSalt);
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.message = "修改成功";
                return View();
            }
            ViewBag.message = "修改失敗!請重新登入試試!!";
            return View();
        }
        public ActionResult Default()
        {
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
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        //輸出treeView javascript Code 
        public JavaScriptResult TreeScript(int id = 0)
        {
            Member member = db.Members.Find(id);
            string strPermission = "";
            if (member != null)
            {
                strPermission = member.Permission;
            }
            string strMenu = string.Format("var treeData =[{0}]", Utility.GetMenu(strPermission));
            string treeScript = System.IO.File.ReadAllText(Server.MapPath("~/Config/PermissionTree.js"));

            return JavaScript(strMenu + treeScript);

        }
        public ActionResult CheckAccount(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return Content("參數錯誤");
            }
            Member member = db.Members.SingleOrDefault(o => o.Account == userName);
            if (member == null)
            {
                return Content("這個帳號尚未使用!");
            }
            return Content("這個帳號已使用!");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}