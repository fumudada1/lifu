using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lifu.Filters;
using lifu.Models;

namespace lifu.Areas.Admin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class RoleController : Controller
    {
        private BackendContext db = new BackendContext();

        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        //
        // GET: /Role/Details/5

        public ActionResult Details(int id = 0)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            ViewBag.Members = db.Members.ToList();
            return View();
        }

        //
        // POST: /Role/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role, string MemberListSelect)
        {
            if (ModelState.IsValid)
            {
                string aa = Request["MemberListSelect"];
                string[] strArray = MemberListSelect.Split(',');

                var members = db.Members.ToList().Where(c => strArray.Contains(c.Id.ToString()));

                foreach (var member in members)
                {
                    role.Members.Add(member);
                }
                role.Create(db, db.Roles);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Role role = db.Roles.Find(id);
            ViewBag.Members = db.Members.ToList().Where(p => !(role.Members.Contains(p)));


            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role, string MemberListSelect)
        {
            if (ModelState.IsValid)
            {
                //取得資料庫裏面原來的值
                var roleItem = db.Roles.Single(r => r.Id == role.Id);

                //套用新的值
                db.Entry(roleItem).CurrentValues.SetValues(role);


                //放入新的值
                roleItem.Members.Clear();
                string[] strArray = MemberListSelect.Split(',');
                var memberLists = db.Members.ToList().Where(c => strArray.Contains(c.Id.ToString()));
                foreach (var m in memberLists)
                {
                    roleItem.Members.Add(m);
                }

                db.Entry(roleItem).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        //
        // GET: /Role/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Role/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //輸出treeView javascript Code 
        public JavaScriptResult TreeScript(int id = 0)
        {
            Role role = db.Roles.Find(id);
            string strPermission = "";
            if (role != null)
            {
                strPermission = role.Permission;
            }
            string strMenu = string.Format("var treeData =[{0}]", Utility.GetMenu(strPermission));
            string treeScript = System.IO.File.ReadAllText(Server.MapPath("~/Config/PermissionTree.js"));

            return JavaScript(strMenu + treeScript);

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}