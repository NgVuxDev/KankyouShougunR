using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using r_framework.Entity;

namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Entity
{
    /// <summary>
    /// 実績売上支払確定画面検索結果レコード
    /// </summary>
    [Serializable()]
    public class T_SELECT_RESULT
    {
        // 定期実績明細から取得
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public byte[] DETAIL_TIME_STAMP { get; set; }

        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlDecimal SUURYOU { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal KANSAN_SUURYOU { get; set; }
        public SqlInt16 KANSAN_UNIT_CD { get; set; }
        public int TSUKIGIME_KBN { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlInt64 UR_SH_NUMBER { get; set; }
        public SqlInt32 ROUND_NO { get; set; }
        public SqlInt16 KEIYAKU_KBN { get; set; }

        // 定期実績入力から取得
        public long TEIKI_JISSEKI_NUMBER { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        //[2014/01/29] DENPYOU_DATE->SAGYOU_DATE--
        public SqlDateTime SAGYOU_DATE { get; set; }
        public byte[] ENTRY_TIME_STAMP { get; set; }
        // [2014/02/14] 車輌系データ
        public string SHARYOU_CD { get; set; }
        public string SHARYOU_NAME { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHASHU_NAME { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNTENSHA_NAME { get; set; }

        // 定期実績荷降から取得
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }

        // 現場マスタから取得
        public string GENBA_NAME_RYAKU { get; set; }
        public string NIOROSHI_GENBA_NAME_RYAKU { get; set; }

        // 業者マスタから取得
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_NAME_RYAKU { get; set; }
        public string NIOROSHI_GYOUSHA_NAME_RYAKU { get; set; }

        // 品名マスタから取得
        public string HINMEI_NAME_RYAKU { get; set; }
        public SqlInt16 HINMEI_ZEI_KBN_CD { get; set; }
        
        // 取引先マスタから取得
        public string TORIHIKISAKI_NAME_RYAKU { get; set; }
        public string TORIHIKISAKI_FURIGANA { get; set; }

        // 取引先_請求情報マスタから取得
        public SqlInt16 TORIHIKI_KBN_CD { get; set; }
        public SqlInt16 ZEI_KBN_CD { get; set; }
        public SqlInt16 TAX_HASUU_CD { get; set; }
        public SqlInt16 ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 KINGAKU_HASUU_CD { get; set; }

        // 取引先_支払い情報マスタから取得
        public SqlInt16 SHIHARAI_TORIHIKI_KBN_CD { get; set; }
        public SqlInt16 SHIHARAI_ZEI_KBN_CD { get; set; }
        public SqlInt16 SHIHARAI_TAX_HASUU_CD { get; set; }
        public SqlInt16 SHIHARAI_ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 SHIHARAI_KINGAKU_HASUU_CD { get; set; }

        // 現場_定期品名マスタから取得
        public string TEIKI_HINMEI_CD { get; set; }
        public SqlDecimal TANKA { get; set; }

        // 現場_定期品名マスタから取得
        public string TSUKI_HINMEI_CD { get; set; }
        public SqlDecimal CHOUKA_LIMIT_AMOUNT { get; set; }
        public string CHOUKA_HINMEI_NAME { get; set; }

        // 消費税マスタから取得
        public SqlDecimal SHOUHIZEI_RATE { get; set; }

        public string UNPAN_GYOUSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_NAME { get; set; }
    }
    
}
