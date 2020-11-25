using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHTQT_Aplication.DBConnections
{
    public class DBConnectionString
    {
        public static string _strCONNECTION_SYSTEM
        {
            get
            {
                
                string l = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string s = Encryptor.Decrypt(l);
                return s;
            }
        }
    }
}
