using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using static Dapper.SqlMapper;

namespace QLHTQT_Aplication.DBConnections
{
    public class DBConnection : DBConnectionString
    {  
        private static SqlConnection con = new SqlConnection(_strCONNECTION_SYSTEM);

        public enum connectionState { open, close };
        public static void setStateConnection(connectionState state)
        {
            if (state == connectionState.open)
            {
                con.Open();
            }
            else
            {
                con.Close();
            }
        }

        public static int Excute(String sql)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            return cmd.ExecuteScalar().MapInt();
        }
        public static IDbConnection getConnection()
        {
            IDbConnection _db = new SqlConnection(_strCONNECTION_SYSTEM);
            return _db;
        }
        /// <summary>
        /// Thực thi store procedure
        /// </summary>
        /// <param name="StoreProcedureName">Tên store</param>
        /// <param name="param">DynamicParameters</param>
        public static void Excute(string StoreProcedureName, DynamicParameters param)
        {
            getConnection().Execute(StoreProcedureName, param, commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// Thực thi querry
        /// </summary>
        /// <param name="StoreProcedureName">Tên store</param>
        /// <param name="param">DynamicParameters</param>
        public static void ExcuteQuerry(string Querry)
        {
            getConnection().Execute(Querry, null, commandType: CommandType.Text);
        }
        /// <summary>
        /// Thực thi store procedure và trả về return value của store (Thường dùng để lấy ID tự tăng -> Store phải return value)
        /// </summary>
        /// <param name="StoreProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="returnValueParamName"></param>
        /// <returns></returns>
        public static int ExcuteScalar(string StoreProcedureName, DynamicParameters param, string returnValueParamName)
        {
            getConnection().Execute(StoreProcedureName, param, commandType: CommandType.StoredProcedure);
            return param.Get<int>(returnValueParamName);
        }
        /// <summary>
        /// Thực thi store procedure truy vấn và trả về list object được mapping theo fieldname
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoreProcedueName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<T> Select<T>(string StoreProcedueName, DynamicParameters param = null)
          where T : new()
        {
            var result = SqlMapper.Query<T>(getConnection(), StoreProcedueName, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
            return result;
        }

        public static GridReader SelectMultiple(string StoreProcedueName, DynamicParameters param = null)
        {
            GridReader result;
            using (var conn = new SqlConnection(_strCONNECTION_SYSTEM))
            {
                conn.Open();
                result = conn.QueryMultiple(
                    StoreProcedueName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
            return result;
        }

        public static List<T> SelectByQuery<T>(string Querry)
         where T : new()
        {
            var result = SqlMapper.Query<T>(getConnection(), Querry, commandType: System.Data.CommandType.Text).ToList();
            return result;
        }
        public static int ExcuteStoreProc(string StoreProcName, SqlParameter[] param = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(StoreProcName);
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed) { con.Open(); }
                cmd.CommandType = CommandType.StoredProcedure;
                if (param != null)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        cmd.Parameters.Add(param[i]);
                    }
                }
                return cmd.ExecuteScalar().MapInt();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static DataTable SelectTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception)
            {
                return dt;
                throw;
            }
            finally
            {
            }
        }
        public static DataTable SelectTableByStoreProc(string StoreProcName, SqlParameter[] param = null)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(StoreProcName);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                if (param != null)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        cmd.Parameters.Add(param[i]);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception)
            {
                return dt;
                throw;
            }
            finally
            {
            }
        }
    }
}
