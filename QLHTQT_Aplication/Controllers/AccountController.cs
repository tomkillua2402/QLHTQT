using Newtonsoft.Json;
using QLHTQT_Aplication.DataAccess;
using QLHTQT_Aplication.Models;
using QLHTQT_Aplication.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace QLHTQT_Aplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(string redirectUrl)
        {
            TempData["url"] = redirectUrl;
            return View();
        }
        
        [HttpPost]
        public JsonResult do_Login(string user, string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                {
                    return Json(new { Result = "ERROR", Message = "Tài khoản hoặc mật khẩu không được để trống!" });
                }
                else
                {

                    //Mã hóa mật khẩu theo MD5
                    var _md5 = new MD5CryptoServiceProvider();
                    var rawData = System.Text.ASCIIEncoding.ASCII.GetBytes(pass);
                    var result = _md5.ComputeHash(rawData);
                    pass = System.Convert.ToBase64String(result, 0, result.Length);

                    string userinfo = new AccountDAL().Login(user, pass);
                    if ((!string.IsNullOrEmpty(userinfo)) && (userinfo != "null"))
                    {
                        var obj = JsonConvert.DeserializeObject<UserModel>(userinfo);
                        SessionControl.AddObject(Constants.User, obj);
                        return Json(new { Result = "SUCCESS", Message = "Đăng nhập thành công!", Url = TempData["url"] });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = "Tài khoản hoặc mật khẩu không chính xác!" });
                    }
                }

            }
            catch
            {
                return Json(new { Result = "ERROR", Message = Constants.MSG_ERR });
            }
        }

        public ActionResult Logout()
        {
            SessionControl.DelAll();
            return RedirectToAction("Login");
        }
    }
}