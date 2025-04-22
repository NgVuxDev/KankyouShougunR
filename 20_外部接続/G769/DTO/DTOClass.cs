
namespace Shougun.Core.ExternalConnection.SmsResult.DTO
{
    public class SearchDTO
    {
        public string KYOTEN_CD { get; set; }
        public string DATE_FROM { get; set; }
        public string DATE_TO { get; set; }
        public string DATE_SHURUI { get; set; }
        public int SMS_DENPYOU_SHURUI { get; set; }
        public int SMS_RECEIVER_STATUS { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
    }
}
