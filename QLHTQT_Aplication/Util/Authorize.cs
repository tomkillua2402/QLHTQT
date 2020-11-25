using QLHTQT_Aplication.DBConnections;
using QLHTQT_Aplication.Models;
using QLHTQT_Aplication.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLHTQT_Aplication.Util
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = SessionControl.GetObject(Constants.User) as UserModel;
            if (user == null)
            {
                var requestType = filterContext.HttpContext.Request.ContentType;
                if (!string.IsNullOrEmpty(requestType))
                {
                }
                else
                {
                    SessionControl.DelAll();
                    filterContext.HttpContext.Response.Redirect("/Account/Login", true);              
                }
            }
            return;
        }
    }
}