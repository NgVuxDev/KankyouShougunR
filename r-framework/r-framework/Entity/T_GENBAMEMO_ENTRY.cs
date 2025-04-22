using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_GENBAMEMO_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlBoolean HIHYOUJI_FLG { get; set; }
        public SqlDateTime HIHYOUJI_DATE { get; set; }
        public string HIHYOUJI_TOUROKUSHA_NAME { get; set; }
        public SqlInt64 GENBAMEMO_NUMBER { get; set; }
        public SqlBoolean HENKOU_KANOU_FLG { get; set; }
        public string SHOKAI_TOUROKUSHA_CD { get; set; }
        public string GENBAMEMO_BUNRUI_CD { get; set; }
        public string GENBAMEMO_BUNRUI_NAME { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string TORIHIKISAKI_NAME { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string HYOUDAI { get; set; }
        public string NAIYOU1 { get; set; }
        public string NAIYOU2 { get; set; }
        public string HASSEIMOTO_CD { get; set; }
        public string HASSEIMOTO_NAME { get; set; }
        public SqlInt64 HASSEIMOTO_SYSTEM_ID { get; set; }
        public SqlInt64 HASSEIMOTO_DETAIL_SYSTEM_ID { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
