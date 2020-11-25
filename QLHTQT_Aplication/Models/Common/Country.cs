using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHTQT_Aplication.Models
{
    public class Country
    {
        public int country_id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public string iso3 { get; set; }
        public int number { get; set; }
        public string continent_code { get; set; }
        public int display_order { get; set; }
    }
}