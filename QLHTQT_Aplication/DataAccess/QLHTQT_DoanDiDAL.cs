using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using QLHTQT_Aplication.DBConnections;
using QLHTQT_Aplication.Models;
using Dapper;
using Newtonsoft.Json;

namespace QLHTQT_Aplication.DataAccess
{
    public class QLHTQT_DoanDiDAL : BaseDAL
    {
        public string Login(string username, string password)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@username", username);
                param.Add("@password", password);
                return JsonConvert.SerializeObject(DBConnection.Select<UserModel>("[dbo].[QLHTQT_Login]", param).FirstOrDefault());
            }
            catch (Exception)
            {
                return null;
            }
        }
        public QLDoanDiModel GetById(int Id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            var model = DBConnection.Select<QLDoanDiModel>("QLHTQT_DoanDi_GetById", param).FirstOrDefault();
            return model;
        }
        public List<Country> SelectAllCountry()
        {
            var model = DBConnection.Select<Country>("QLHTQT_Country_GetAll", null).ToList();
            return model;
        }
      

        public QLDoanDiPaging SelectAll(string keyword, DateTime? FromDate, DateTime? ToDate, int pageIndex, int pageSize)
        {
            var param = new DynamicParameters();
            param.Add("@keyword", keyword, DbType.String, ParameterDirection.Input);
            param.Add("@pageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            param.Add("@pageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            param.Add("@fromDate", FromDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@toDate", ToDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@totalCount", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            var model = new QLDoanDiPaging();
            using(var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = conn.QueryMultiple("[dbo].[QLHTQT_DoanDi_SelectAll]", param, commandType: System.Data.CommandType.StoredProcedure);
                model.lst = result.Read<QLDoanDiModel>().ToList();
                model.totalCount = param.Get<int>("@totalCount");
            }
            return model;
        }
        public int SaveItem(QLDoanDiModel obj, int saveType)
        {
            var param = new DynamicParameters();
            param.Add("@saveType", saveType, DbType.String, ParameterDirection.Input);
            param.Add("@Id", obj.Id, DbType.String, ParameterDirection.Input);
            param.Add("@Quyet_dinh_so", obj.Quyet_dinh_so);
            param.Add("@Ho_ten", obj.Ho_ten);
            param.Add("@Doi_tuong_di", obj.Doi_tuong_di);
            param.Add("@Don_vi", obj.Don_vi);
            param.Add("@Quoc_tich", obj.Quoc_tich);
            param.Add("@So_ho_chieu", obj.So_ho_chieu);
            param.Add("@Ngay_di", obj.Ngay_di);
            param.Add("@Ngay_ve", obj.Ngay_ve);
            param.Add("@Noi_di", obj.Noi_di);
            param.Add("@Tong_thoi_gian", obj.Tong_thoi_gian);
            param.Add("@Muc_dich_di", obj.Muc_dich_di);
            //param.Add("@Muc_dich_di_text", obj.Muc_dich_di_text);
            param.Add("@Nguon_kinh_phi", obj.Nguon_kinh_phi);
            //param.Add("@Nguon_kinh_phi_text", obj.Nguon_kinh_phi_text);
            param.Add("@Doi_tac_nuoc_ngoai", obj.Doi_tac_nuoc_ngoai);
            param.Add("@Ket_qua_chuyen_di", obj.Ket_qua_chuyen_di);
            param.Add("@result", 0, DbType.Int32, ParameterDirection.InputOutput);
            DBConnection.Excute("QLHTQT_DoanDi_SaveItem", param);
            return param.Get<int>("@result");
        }
        public bool Delete(string Id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
                DBConnection.Excute("QLHTQT_DoanDi_Delete", param);
                return true;
            }
            catch(Exception)
            {
                return false;
                throw;
            }
        }
    }
    
} 