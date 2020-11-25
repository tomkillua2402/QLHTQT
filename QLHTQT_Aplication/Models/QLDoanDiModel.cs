using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHTQT_Aplication.Models
{
    public class QLDoanDiPaging
    {
        public QLDoanDiPaging()
        {
            lst = new List<QLDoanDiModel>();
        }
        public List<QLDoanDiModel> lst { get; set; }
        public int totalCount { get; set; }
    }
    public class QLDoanDiModel
    {
        public int Id { get; set; }
        public string Quyet_dinh_so { get; set; }
        public string Ho_ten { get; set; }
        public int Doi_tuong_di { get; set; }
        public string Don_vi { get; set; }
        public int Quoc_tich { get; set; }
        public string So_ho_chieu { get; set; }
        public DateTime Ngay_di { get; set; }
        public DateTime Ngay_ve { get; set; }
        public string Noi_di { get; set; }
        public string Tong_thoi_gian { get; set; }
        public int Muc_dich_di { get; set; }
        public string Muc_dich_di_text { get; set; }
        public int Nguon_kinh_phi { get; set; }
        public string Nguon_kinh_phi_text { get; set; }
        public string Doi_tac_nuoc_ngoai { get; set; }
        public string Ket_qua_chuyen_di { get; set; }
    }
    public class QLDoanDiSearch
    {
        public string Keyword { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class QLDoanDiTemplate
    {
        public string Quyet_dinh_so { get; set; }
        public string Ho_ten { get; set; }
        public string Don_vi { get; set; }
        public string So_ho_chieu { get; set; }
        public string Ngay_di { get; set; }
        public string Ngay_ve { get; set; }
        public string Noi_di { get; set; }
    }
}