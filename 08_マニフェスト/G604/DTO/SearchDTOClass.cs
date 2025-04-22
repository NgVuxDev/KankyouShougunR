using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu
{
    internal class JissekiHokokuDto
    {
        /// <summary>
        /// 拠部署コード
        /// </summary>
        public string BUSHO_CD { get; set; }
    }

    public class DTOClass
    {
        public SqlInt64 SYSTEM_ID { get; set; }

        public SqlInt32 SEQ { get; set; }

        public string MANIFEST_ID { get; set; }

        public string KANRI_ID { get; set; }

        public SqlInt32 DEN_SEQ { get; set; }

        public string SHORI_SHISETSU_NAME { get; set; }

        public SqlInt16 NEXT_KBN { get; set; }

        public SqlInt64 NEXT_SYSTEM_ID { get; set; }

        public string HOUKOKUSHO_BUNRUI_CD { get; set; }

        public string SBN_AFTER_HAIKI_NAME { get; set; }

        public string SHORI_SHISETSU_CD { get; set; }

        public string HAIKI_SHURUI_CD { get; set; }

        public string HAIKI_SHURUI_NAME { get; set; }

        public SqlDecimal KANSAN_SUU { get; set; }

        public string UNIT_NAME { get; set; }

        public SqlDecimal GENNYOU_SUU { get; set; }

        public string SHOBUN_HOUHOU_CD { get; set; }

        public string SHOBUN_HOUHOU_NAME { get; set; }

        public string HST_JOU_CHIIKI_CD { get; set; }

        public SqlInt16 HST_KEN_KBN { get; set; }

        public SqlInt16 HST_JOU_TODOUFUKEN_CD { get; set; }

        public SqlInt16 HAIKI_KBN_CD { get; set; }
    }
}