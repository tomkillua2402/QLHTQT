using Dapper;
using Newtonsoft.Json;
using QLHTQT_Aplication.DBConnections;
using QLHTQT_Aplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QLHTQT_Aplication.DataAccess
{
    public class AccountDAL
    {
        public string Login(string user, string Pass)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@username", user);
                param.Add("@password", Pass);
                return JsonConvert.SerializeObject(DBConnection.Select<UserModel>("[dbo].[QLHTQT_Login]", param).FirstOrDefault());                              
            }
            catch (Exception)
            {        
                return null;
            }
        }

        public List<UserModel> SelectAll()
        {
            return DBConnection.Select<UserModel>("Plan_GiaoVien_SelectAll");
        }

        public UserModel SelectbyID(int ID_cb)
        {
            var param = new DynamicParameters();
            param.Add("@ID_cb", ID_cb);
            return DBConnection.Select<UserModel>("[QLKH_PLAN_GiaoVien_byID]", param).FirstOrDefault();
        }

        public DataTable SelectDanhSachDanhBaDonVi()
        {
            return DBConnection.SelectTableByStoreProc("QLKH_SelectDanhSachDanhBaDonVi");
        }
    }
}