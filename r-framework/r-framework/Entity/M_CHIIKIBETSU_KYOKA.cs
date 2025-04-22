using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CHIIKIBETSU_KYOKA : SuperEntity
    {
        public SqlInt16 KYOKA_KBN { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string CHIIKI_CD { get; set; }
        public string FUTSUU_KYOKA_NO { get; set; }
        public SqlDateTime FUTSUU_KYOKA_BEGIN { get; set; }
        public string SEARCH_FUTSUU_KYOKA_BEGIN { get; set; }
        public SqlDateTime FUTSUU_KYOKA_END { get; set; }
        public string SEARCH_FUTSUU_KYOKA_END { get; set; }
        public string FUTSUU_KYOKA_FILE_PATH { get; set; }
        public string TOKUBETSU_KYOKA_NO { get; set; }
        public SqlDateTime TOKUBETSU_KYOKA_BEGIN { get; set; }
        public string SEARCH_TOKUBETSU_KYOKA_BEGIN { get; set; }
        public SqlDateTime TOKUBETSU_KYOKA_END { get; set; }
        public string SEARCH_TOKUBETSU_KYOKA_END { get; set; }
        public string TOKUBETSU_KYOKA_FILE_PATH { get; set; }
        public string CHIIKIBETSU_KYOKA_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}