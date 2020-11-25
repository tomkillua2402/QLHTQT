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
    public class QLHTQT_ThanhVienDenDAL:BaseDAL
    {
        public ThanhVienDenPaging SelectAll(string keyword, int pageIndex, int pageSize)
        {
            var param = new DynamicParameters();
            param.Add("@keyword", keyword, DbType.String, ParameterDirection.Input);
            param.Add("@pageIndex", pageIndex, DbType.Int64, ParameterDirection.Input);
            param.Add("@pageSize", pageSize, DbType.Int64, ParameterDirection.Input);
            param.Add("@totalCount", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            //param.Add("@MaDoanDen", MaDoanDen, DbType.Int32, ParameterDirection.Input);
            var model = new ThanhVienDenPaging();
            //var result = DBConnection.SelectMultiple("QLHTQT_ThanhVienDen_SelectAll", param);
            //model.lst = result.Read<ThanhVienDenModel>().ToList();
            //model.totalCount = param.Get<int>("@totalCount");
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = conn.QueryMultiple("QLHTQT_ThanhVienDen_SelectAll", param, commandType: System.Data.CommandType.StoredProcedure);
                model.lst = result.Read<ThanhVienDenModel>().ToList();
                model.totalCount = param.Get<int>("@totalCount");
            }
            return model;
        }
        public ThanhVienDenModel SelectById(int MaDoanDen)
        {
            var param = new DynamicParameters();
            param.Add("@MaDoanDen", MaDoanDen, DbType.Int32, ParameterDirection.Input);
            var model = DBConnection.Select<ThanhVienDenModel>("QLHTQT_ThanhVienDen_GetById", param).FirstOrDefault();
            return model;
        }
    }
}