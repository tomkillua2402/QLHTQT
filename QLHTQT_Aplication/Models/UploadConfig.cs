using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static QLHTQT_Aplication.Util.EnumConstants;

namespace QLHTQT_Aplication.Models
{
    public class UploadConfig
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool isMultiple { get; set; }
        public UploadType DisplayType { get; set; }
        public bool Overwrite { get; set; }
        public string Accept { get; set; }
    }
}