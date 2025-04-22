using System.Data.SqlTypes;
namespace r_framework.Entity
{
    public class M_KYOYUSAKI : SuperEntity
    {
        public SqlInt16 KYOYUSAKI_CD { get; set; }
        public string KYOYUSAKI_CORP_NAME { get; set; }
        public string KYOYUSAKI_NAME { get; set; }
        public string KYOYUSAKI_MAIL_ADDRESS { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}