using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHTQT_Aplication.DBConnections
{
    public static class Common
    {
        public static int ToInt32(this object obj)
        {
            try
            {

                return Int32.Parse(obj.ToString().RemoveSpace());
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string RemoveSpace(this string input)
        {
            do
            {
                input.Replace(" ", "");
            }
            while (input.IndexOf(" ") >= 0);
            return input;
        }
        public static int MapInt(this object obj)
        {
            try
            {
                if (obj == null) return 0;
                if (!obj.ToString().IsNumeric()) return 0;
                return obj.ToInt32();
            }
            catch
            {
                return 0;
            }
        }

        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }
}
