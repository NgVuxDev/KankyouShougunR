using System.Data.SqlTypes;
namespace Shougun.Core.Master.CourseIchiran
{
    public class DTOClass
    {
        public string DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string HINMEI_CD { get; set; }
    }

    public class SummaryKeyCode
    {
        public string DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
    }
}
