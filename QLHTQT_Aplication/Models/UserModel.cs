using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHTQT_Aplication.Models
{
    public class UserModel
    {
        public int ID_cb { get; set; }
        public int ID_khoa { get; set; }
        public string Ma_cb { get; set; }
        public string Ho_ten { get; set; }
        public DateTime Ngay_sinh { get; set; }
        public int ID_gioi_tinh { get; set; }
        public string Gioi_tinh { get; set; }
        public int UserGroup { get; set; }
    } 

}