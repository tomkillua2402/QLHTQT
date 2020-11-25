using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHTQT_Aplication.Models
{
    public class ThanhVienDenModel
    {
        public int Id { get; set; }
        public string Ho { get; set; }
        public string Ten_dem { get; set; }
        public string Ten { get; set; }
        public DateTime Ngay_sinh { get; set; }
        public string Ma_doan_den { get; set; }
    }
    public class ThanhVienDenPaging
    {
        public ThanhVienDenPaging()
        {
            lst = new List<ThanhVienDenModel>();
        }
        public List<ThanhVienDenModel> lst { get; set; }
        public int totalCount { get; set; }
    }
    public class ThanhVienSearch
    {
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int  PageSize { get; set; }
        public int MaDoanDen { get; set; }
    }
}