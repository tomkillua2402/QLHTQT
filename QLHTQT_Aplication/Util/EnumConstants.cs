using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHTQT_Aplication.Util
{
    public class EnumConstants
    {
        public enum SaveType
        {
            ThemMoi = 1,
            CapNhat = 2
        }
        public enum UploadType
        {
            Image = 1,
            File = 2,
            Video = 3,
            Scorm = 4,
            Sound = 5,
            Youtube = 6,
            Meeting = 7
        }

        public enum CourseStatus
        {
            InProgress = 1,
            Cancel = 0,
            Completed = 2
        }
    }
}