using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHTQT_Aplication.Models
{
    public class QLDoanDenPaging
    {
        public QLDoanDenPaging()
        {
            lst = new List<QLDoanDenModel>();
        }
        public List<QLDoanDenModel> lst { get; set; }
        public int totalCount { get; set; }
    }
    public class QLDoanDenRef
    {
        public QLDoanDenModel model { get; set; }
        //public List<LoaiDoiTacModel> ldt { get; set; }
    }
    public class QLDoanDenModel
    {
        public int Id { get; set; }
        public string Ten_to_chuc { get; set; }
        public int Loai_to_chuc { get; set; }
        public string Dien_thoai{ get; set; }
        public string Email { get; set; }
        public string Dia_chi { get; set; }
        public int Chau_luc  { get; set; }
        public int Quoc_tich { get; set; }
        public int Don_vi_de_nghi { get; set; }
        public string Email_lien_lac { get; set; }
        public string So_cong_van { get; set; }
        public DateTime Ngay_cong_van { get; set; }
        public string Chuong_trinh_hoat_dong  { get; set; }
        public string Du_kien_tam_tru { get; set; }
        public int Tinh_thanh_pho_tam_tru { get; set; }
        public int Don_vi_don_tiep { get; set; }
        public int Nhan_cong_van { get; set; }
        public string Ly_do_nhan_thi_thuc { get; set; }
        public string FileUpload { get; set; }
    }

    public class QLDoanDenTemplate
    {
        public string Ten_doan_vao { get; set; }
        public string Co_quan { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string Ngay_nhap { get; set; }
    }
    public class QLDoanDenSearch
    {
        public string Keyword { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}