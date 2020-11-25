using System;
using System.Web;

namespace QLHTQT_Aplication.Util
{
    public class SessionControl
    {
        public static void AddObject(string strSessionName, object objValue)
        {
            HttpContext.Current.Session.Remove(strSessionName);
            HttpContext.Current.Session[strSessionName] = objValue;
            HttpContext.Current.Session.Timeout = 1440;
        }

        public static object GetObject(string strSessionName)
        {
            try
            {
                return HttpContext.Current.Session[strSessionName];
            }
            catch (Exception)
            {                
                HttpContext.Current.Response.Redirect("/Account/Login", true);
                return null;
            }
        }
        public static void DelObject(string strSessionName)
        {
            HttpContext.Current.Session.Remove(strSessionName);
        }
        public static void DelAll()
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.RemoveAll();
        }
    }
}