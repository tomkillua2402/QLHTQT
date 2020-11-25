using Dapper;
using QLHTQT_Aplication.Models;
using QLHTQT_Aplication.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLHTQT_Aplication.Controllers
{
    public class UploadFileController : Controller
    {
        // GET: UploadFile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult UploadFiles(string type, string accept)
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                List<string> fileUploads = new List<string>();
                try
                {
                    var arrExt = accept.Split(',');
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    bool isValid = true;
                    for (int i = 0; i < files.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(accept))
                        {
                            if (!arrExt.Contains(Path.GetExtension(files[i].FileName)) && accept != "underfined")
                            {
                                isValid = false;
                            }
                        }


                    }
                    if (!isValid)
                    {
                        return Json(new { status = false, message = "Định dạng không hợp lệ" });
                    }
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        var name_wo_ext = Path.GetFileNameWithoutExtension(fname).Replace(" ", "_");
                        var ext = Path.GetExtension(fname);
                        var new_name = $"{name_wo_ext}_{DateTime.Now.Ticks}{ext}";
                        // Get the complete folder path and store the file inside it.  
                        if (!Directory.Exists(Server.MapPath($"~/Uploads/{type}")))
                        {
                            Directory.CreateDirectory(Server.MapPath($"~/Uploads/{type}"));
                        }
                        fname = Path.Combine(Server.MapPath(($"~/Uploads/{type}").Replace(@"\", "/")), new_name);
                        file.SaveAs(fname);
                        //Extract Scorm File
                        
                        fileUploads.Add(Path.Combine($"/Uploads/{type}", new_name).Replace(@"\", "/"));
                    }
                    // Returns message that successfully uploaded  
                    return Json(new { status = true, message = "Finished", files = fileUploads }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { status = false, message = ex.Message, files = fileUploads });
                }
            }
            else
            {
                return Json("Chưa chọn file upload.");
            }
        }
        //[HttpPost]
        //public JsonResult SaveFile(List<UploadFile> listfile, string attachment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Dictionary<string, string> param = new Dictionary<string, string>();
        //        if (listfile != null)
        //        {
        //            foreach (var item in listfile)
        //            {
        //                item.ID = Guid.NewGuid().ToString();
        //            }
        //            param.Add("XML", HelperXML.SerializeXML<List<UploadFile>>(listfile));
        //        }
        //        else
        //        {
        //            param.Add("XML", "");
        //        }
        //        param.Add("Attachment", attachment);
        //        var result = HelperRestSharp.CallApi("/api/UploadFile/Add", RestSharp.Method.POST, User.Token, param);
        //        return Json(listfile, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            Error = true,
        //            Message = "Lưu thất bại"
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //[HttpPost]
        //public JsonResult ListFileAttachment(string attachment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Dictionary<string, string> parameters = new Dictionary<string, string>();
        //        parameters.Add("Attachment", attachment);
        //        var result = HelperRestSharp.CallApi("/api/UploadFile/ListData", RestSharp.Method.POST, User.Token, parameters);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new Result()
        //        {
        //            Error = true,
        //            Message = "Thất bại"
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //[HttpPost]
        //public JsonResult DeleteFile(string fileID)
        //{
        //    var result = HelperRestSharp.CallApi("/api/UploadFile/Delete/" + fileID, RestSharp.Method.POST, User.Token);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
    }
}