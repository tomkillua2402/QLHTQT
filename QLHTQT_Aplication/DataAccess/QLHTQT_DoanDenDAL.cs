using Dapper;
using Newtonsoft.Json;
using QLHTQT_Aplication.DBConnections;
using QLHTQT_Aplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLHTQT_Aplication.DataAccess
{
    public class QLHTQT_DoanDenDAL:BaseDAL
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
        public List<STU_TinhModels> SelectAllTinh()
        {
            var model = DBConnection.Select<STU_TinhModels>("QLHTQT_Tinh_GetAll", null).ToList();
            return model;
        }
        public List<DonViModels> SelectAllDonVi()
        {
            var model = DBConnection.Select<DonViModels>("QLHTQT_DonVi_GetAll", null).ToList();
            return model;
        }
        public List<LoaiToChucModel> SelectAllLoaiToChuc()
        {
            var model = DBConnection.Select<LoaiToChucModel>("QLHTQT_LoaiToChuc_GetAll", null).ToList();
            return model;
        }
        public List<Continents> SelectAllContinents()
        {
            var model = DBConnection.Select<Continents>("QLHTQT_ChauLuc_GetAll", null).ToList();
            return model;
        }
        public List<Country> SelectCountryByContinents(string id)
        {
            var param = new DynamicParameters();
            param.Add("@id", id, DbType.String, ParameterDirection.Input);
            var model = DBConnection.Select<Country>("QLHTQT_Country_ByContinents", param).ToList();
            return model;
        }
        public QLDoanDenModel GetById(int Id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            var model = DBConnection.Select<QLDoanDenModel>("QLHTQT_DoanDen_GetById", param).FirstOrDefault();
            return model;
        }
        public QLDoanDenPaging SelectAll(string keyword, DateTime? FromDate, DateTime? ToDate, int pageIndex, int pageSize)
        {
            var param = new DynamicParameters();
            param.Add("@keyword", keyword, DbType.String, ParameterDirection.Input);
            param.Add("@pageIndex", pageIndex, DbType.Int64, ParameterDirection.Input);
            param.Add("@pageSize", pageSize, DbType.Int64, ParameterDirection.Input);
            param.Add("@fromDate", FromDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@toDate", ToDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@totalCount", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            var model = new QLDoanDenPaging();
            //var result = DBConnection.SelectMultiple("QLHTQT_DoanDen_SelectAll", param);
            //model.lst = result.Read<QLDoanDenModel>().ToList();
            //model.totalCount = param.Get<int>("@totalCount");
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = conn.QueryMultiple("QLHTQT_DoanDen_SelectAll", param, commandType: System.Data.CommandType.StoredProcedure);
                model.lst = result.Read<QLDoanDenModel>().ToList();
                model.totalCount = param.Get<int>("@totalCount");
            }
            return model;
        }
        public int SaveItem(QLDoanDenModel model, int saveType)
        {
            var param = new DynamicParameters();
            param.Add("@saveType", saveType, DbType.Int32, ParameterDirection.Input);
            param.Add("@Id", model.Id, DbType.Int32, ParameterDirection.Input);
            param.Add("@Ten_to_chuc", model.Ten_to_chuc, DbType.String, ParameterDirection.Input);
            param.Add("@Loai_to_chuc", model.Loai_to_chuc, DbType.Int32, ParameterDirection.Input);
            param.Add("@Dien_thoai", model.Dien_thoai, DbType.String, ParameterDirection.Input);
            param.Add("@Email", model.Email, DbType.String, ParameterDirection.Input);
            param.Add("@Dia_chi", model.Dia_chi, DbType.String, ParameterDirection.Input);
            param.Add("@Chau_luc", model.Chau_luc, DbType.Int32, ParameterDirection.Input);
            param.Add("@Quoc_tich", model.Quoc_tich, DbType.Int32, ParameterDirection.Input);
            param.Add("@Don_vi_de_nghi", model.Don_vi_de_nghi,DbType.Int32, ParameterDirection.Input);
            param.Add("@Email_lien_lac", model.Email_lien_lac, DbType.String, ParameterDirection.Input);
            param.Add("@So_cong_van", model.So_cong_van, DbType.Int32, ParameterDirection.Input);
            param.Add("@Ngay_cong_van", model.Ngay_cong_van, DbType.DateTime, ParameterDirection.Input);
            param.Add("@Chuong_trinh_hoat_dong", model.Chuong_trinh_hoat_dong, DbType.String, ParameterDirection.Input);
            param.Add("@Du_kien_tam_tru", model.Du_kien_tam_tru, DbType.String, ParameterDirection.Input);
            param.Add("@Tinh_thanh_pho_tam_tru", model.Tinh_thanh_pho_tam_tru, DbType.Int32, ParameterDirection.Input);
            param.Add("@Don_vi_don_tiep", model.Don_vi_don_tiep, DbType.Int32, ParameterDirection.Input);
            param.Add("@Nhan_cong_van", model.Nhan_cong_van, DbType.Int32, ParameterDirection.Input);
            param.Add("@Ly_do_nhan_thi_thuc", model.Ly_do_nhan_thi_thuc, DbType.Int32, ParameterDirection.Input);
            param.Add("@File_upload", model.FileUpload, DbType.Int32, ParameterDirection.Input);
            param.Add("@result", 0, DbType.Int32, ParameterDirection.InputOutput);
            DBConnection.Excute("QLHTQT_DoanDen_SaveItem", param);
            return param.Get<int>("@result");
        }
        public bool Delete(string Id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id, DbType.String, ParameterDirection.Input);
                DBConnection.Excute("QLHTQT_DoanDen_Delete", param);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}