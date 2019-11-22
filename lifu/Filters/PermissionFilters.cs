using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using lifu.Models;
using Newtonsoft.Json;

namespace lifu.Filters
{

    public class PermissionFilters : ActionFilterAttribute
    {
        public string Module { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.ViewBag.menu = "";
                return;
            }
            string controllerName= string.IsNullOrEmpty(Module) ? filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString() : Module;
            string actionName = string.IsNullOrEmpty(Module) ? filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString() : Module;


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/Config/Menu.xml"));

            //取得UserData
            string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            Member member = JsonConvert.DeserializeObject<Member>(strUserData);

          

            //if (controllerName != "Home")
            //{
               
            //    var selectNode = xmlDoc.DocumentElement.SelectSingleNode("//Modules[@Controller='" + controllerName + "' and @Action='" + actionName + "']");
            //    string FunctionValue = selectNode.Attributes["Value"].Value;
            //    if (member == null || member.Permission.ToLower().IndexOf(FunctionValue.ToLower()) == -1)
            //    {
            //        filterContext.Controller.ViewBag.message = "你沒有權限使用，請重新登入!";
            //        filterContext.Result = new HttpUnauthorizedResult();
            //        return;
            //    }
            //}
            filterContext.Controller.ViewBag.menu = Utility.GetLeftMenu(member, controllerName, xmlDoc);

        }

     


    }
}
