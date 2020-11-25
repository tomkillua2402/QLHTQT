using Aspose.Cells;
using Newtonsoft.Json;
using QLHTQT_Aplication.DataAccess;
using QLHTQT_Aplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static QLHTQT_Aplication.Util.EnumConstants;

namespace QLHTQT_Aplication.Controllers
{
    public class QLDoanController : Controller
    {
        //private static readonly string Path = "~/Content/Template/BaoCaoDoanDen.xls";
        // GET: QuanLyDoanDen
        public ActionResult DSDoanDen()
        {
            return View();
        }
        public ActionResult _DanhSachDoanDen(string obj)
        {
            DateTime? fromDate = null, toDate = null;
            var res = JsonConvert.DeserializeObject<QLDoanDenSearch>(obj);
            if (!string.IsNullOrEmpty(res.FromDate))
            {
                fromDate = Convert.ToDateTime(res.FromDate);
            }
            if (!string.IsNullOrEmpty(res.ToDate))
            {
                toDate = Convert.ToDateTime(res.ToDate);
            }
            var model = new QLHTQT_DoanDenDAL().SelectAll(res.Keyword, fromDate, toDate, res.PageIndex, res.PageSize);
            var jsModel = JsonConvert.SerializeObject(model);
            return Json(new { Result = "SUCCESS", Data = model }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _ChiTietDoanDen(int id = 0)
        {
            ViewBag.DSTinh = new QLHTQT_DoanDenDAL().SelectAllTinh();
            ViewBag.DSLoaiToChuc = new QLHTQT_DoanDenDAL().SelectAllLoaiToChuc();
            ViewBag.DSDonVi = new QLHTQT_DoanDenDAL().SelectAllDonVi();
            ViewBag.DSChauLuc = new QLHTQT_DoanDenDAL().SelectAllContinents();
            var model = new QLDoanDenModel();
            if (id > 0)
            {
                model = new QLHTQT_DoanDenDAL().GetById(id);
                return PartialView("~/Views/QLDoan/_ChiTietDoanDen.cshtml", model);
            }
            else
            {
                return PartialView("~/Views/QLDoan/_ThemDoanDen.cshtml");
            }

        }
        public JsonResult GetCountryByContinents(string id)
        {
            var model = new QLHTQT_DoanDenDAL().SelectCountryByContinents(id);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult _LuuDoanDen(QLDoanDenModel obj)
        {
            try
            {
                int result;
                if (obj.Id > 0)
                {
                    result = new QLHTQT_DoanDenDAL().SaveItem(obj, (int)SaveType.CapNhat);
                }
                else
                {
                    result = new QLHTQT_DoanDenDAL().SaveItem(obj, (int)SaveType.ThemMoi);
                }
                if (result.Equals(1))
                {
                    return Json(new { type = "success", msg = "Thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { type = "error", msg = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { type = "error", msg = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        [HttpPost]
        public ActionResult _DeleteDoanDen(string lstIDs = "")
        {
            new QLHTQT_DoanDenDAL().Delete(lstIDs);
            return Json(new { type = "success", msg = "Thành công" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExportTemplate(string fromDate, string toDate)
        {
            DateTime? fromDates = null, toDates = null;
            if (string.IsNullOrEmpty(fromDate))
            {
                return Json(new { type = "failed", msg = "Vui lòng lựa chọn từ ngày nhập" }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(toDate))
            {
                return Json(new { type = "failed", msg = "Vui lòng lựa chọn đến ngày nhập" }, JsonRequestBehavior.AllowGet);
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                fromDates = Convert.ToDateTime(fromDate);
                toDates = Convert.ToDateTime(toDate);
                if (toDates.Value.AddMonths(-3) > fromDates)
                {
                    return Json(new { type = "failed", msg = "Vui lòng chỉ tìm kiếm trong khoảng thời gian 3 tháng" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { type = "success" }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Export(string keyword, string fromDate, string toDate)
        //{
        //    DateTime? fromDates = null, toDates = null;
        //    fromDates = Convert.ToDateTime(fromDate);
        //    toDates = Convert.ToDateTime(toDate);
        //    var res = new QLHTQT_DoanDenDAL().SelectAll(keyword, fromDates, toDates, 1, int.MaxValue);
        //    var model = res.lst.Select(x => new QLDoanDenTemplate
        //    {
        //        Co_quan = x.Co_quan == null ? "" : x.Co_quan,
        //        Email = x.Email == null ? "" : x.Email,
        //        Ngay_nhap = x.Ngay_nhap == DateTime.MinValue ? "" : x.Ngay_nhap.ToString("dd/MM/yyyy"),
        //        SDT = x.SDT == null ? "" : x.SDT,
        //        Ten_doan_vao = x.Ten_doan_den == null ? "" : x.Ten_doan_den
        //    });
        //    //var res = JsonConvert.DeserializeObject<List<Error>>(model);
        //    var path = System.Web.Hosting.HostingEnvironment.MapPath(Path);
        //    var workbook = new Workbook(path);
        //    var worksheet = workbook.Worksheets[0];

        //    worksheet.Cells[3, 0].PutValue("Từ khóa: " + keyword + "");
        //    worksheet.Cells[4, 0].PutValue("Ngày nhập từ ngày: " + fromDate + "");
        //    worksheet.Cells[5, 0].PutValue("Ngày nhập đến ngày: " + toDate + "");

        //    var firstRow = 12;
        //    var stt = 1;
        //    foreach (var item in model)
        //    {
        //        worksheet.Cells.InsertRow(firstRow + 1);
        //        worksheet.Cells[firstRow, 0].PutValue(stt);
        //        worksheet.Cells[firstRow, 1].PutValue(item.Co_quan);
        //        worksheet.Cells[firstRow, 2].PutValue(item.Ten_doan_vao);
        //        worksheet.Cells[firstRow, 3].PutValue(item.Email);
        //        worksheet.Cells[firstRow, 4].PutValue(item.SDT);
        //        worksheet.Cells[firstRow, 5].PutValue(item.Ngay_nhap);
        //        stt++;
        //        firstRow += 1;
        //    }


        //    var dstStream = new MemoryStream();
        //    workbook.Save(dstStream, SaveFormat.Excel97To2003);
        //    dstStream.Position = 0;

        //    return File(dstStream, "application/octet-stream", "Báo cáo đoàn vào.xls");
        //}
        public ActionResult DSDoanDi()
        {
            return View();
        }
        public ActionResult _DanhSachDoanDi(string obj)
        {
            DateTime? fromDate = null, toDate = null;
            var res = JsonConvert.DeserializeObject<QLDoanDiSearch>(obj);
            if (!string.IsNullOrEmpty(res.FromDate))
            {
                fromDate = Convert.ToDateTime(res.FromDate);
            }
            if (!string.IsNullOrEmpty(res.ToDate))
            {
                toDate = Convert.ToDateTime(res.ToDate);
            }
            var model = new QLHTQT_DoanDiDAL().SelectAll(res.Keyword, fromDate, toDate, res.PageIndex, res.PageSize);
            var jsModel = JsonConvert.SerializeObject(model);
            return Json(new { Result = "SUCCESS", Data = model }, JsonRequestBehavior.AllowGet);
        }
    }
}