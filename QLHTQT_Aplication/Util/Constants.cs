using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHTQT_Aplication.Util
{
    public class Constants
    {
        #region Message
        public static string User = "User";
        public static string MSG_ERR = "Có lỗi xảy ra trong quá trình thực hiện. Vui lòng thực hiện lại sau.";
        #endregion

        #region Group Roles
        public static int Quan_tri = 0;
        public static int Can_bo_duyet = 1;
        public static int Can_bo_dang_ky = 2;
        #endregion

        #region Status
        public static int Cho_duyet = 0;
        public static int Da_duyet = 1;
        public static int Khong_duyet = 2;
        #endregion

        #region Vai tro NCKH
        public static int Chu_nhiem = 1;
        #endregion

        #region Gioi Tinh
        public static string GioiTinh(int ID_gioi_tinh)
        {
            string Gioi_tinh = "Nam";
            if (ID_gioi_tinh == 1)
            {
                Gioi_tinh = "Nữ";
            }
            return Gioi_tinh;
        }
        #endregion
    }
}