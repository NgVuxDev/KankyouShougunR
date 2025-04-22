using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.BusinessManagement.Const.Common
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class MitumorisyoConst
    {
        /// <summary>title</summary>
        public const string MITUMOTISYO_HEADER_TITLE = "見積書";
        /// <summary>見積書名称1</summary>
        public const string MITUMOTISYO_TANKA_YOKO_NAME = "単価見積書(横)";
        /// <summary>見積書名称2</summary>
        public const string MITUMOTISYO_TANKA_TATE_NAME = "単価見積書(縦)";
        /// <summary>見積書名称3</summary>
        public const string MITUMOTISYO_KINGAKU_YOKO_NAME = "見積書(横)";
        /// <summary>見積書名称4</summary>
        public const string MITUMOTISYO_KINGAKU_TATE_NAME = "見積書(縦)";
        /// <summary>単価見積書(横)</summary>
        public const int MITUMOTISYO_TANKA_YOKO = 11;
        /// <summary>単価見積書(縦)</summary>
        public const int MITUMOTISYO_TANKA_TATE = 12;
        /// <summary>見積書(横)</summary>
        public const int MITUMOTISYO_KINGAKU_YOKO = 21;
        /// <summary>見積書(縦)</summary>
        public const int MITUMOTISYO_KINGAKU_TATE = 22;
        /// <summary>単価見積書(横)Index</summary>
        public const int MITUMOTISYO_TANKA_YOKO_INDEX = 1;
        /// <summary>単価見積書(縦)Index</summary>
        public const int MITUMOTISYO_TANKA_TATE_INDEX = 2;
        /// <summary>見積書(横)Index</summary>
        public const int MITUMOTISYO_KINGAKU_YOKO_INDEX = 3;
        /// <summary>見積書(縦)Index</summary>
        public const int MITUMOTISYO_KINGAKU_TATE_INDEX = 4;

        /// <summary> 見積明細カラム名  明細システムID </summary>
        public const string MITSUMORI_COLUMN_SYS_ID = "clm_DetailSystemID1";
        /// <summary> 見積明細カラム名  No. </summary>
        public const string MITSUMORI_COLUMN_NAME_NO = "clm_DenpyoNumber1";
        /// <summary> 見積明細カラム名  品名CD </summary>
        public const string MITSUMORI_COLUMN_NAME_HINMEI_CD = "clm_HinmeiCD1";
        /// <summary> 見積明細カラム名  品名 </summary>
        public const string MITSUMORI_COLUMN_NAME_HINMEI_NAME = "clm_HinmeiName1";
        /// <summary> 見積明細カラム名  伝票区分CD </summary>
        public const string MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD = "clm_DenpyouKbnCD1";
        /// <summary> 見積明細カラム名  伝票区分 </summary>
        public const string MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME = "clm_DenpyouKbnMei1";
        /// <summary> 見積明細カラム名  数量 </summary>
        public const string MITSUMORI_COLUMN_NAME_SUURYOU = "clm_Suuryou1";
        /// <summary> 見積明細カラム名  単位 </summary>
        public const string MITSUMORI_COLUMN_NAME_UNIT_NAME = "clm_UnitName1";
        /// <summary> 見積明細カラム名  単位CD </summary>
        public const string MITSUMORI_COLUMN_NAME_UNIT_CD = "clm_UnitCD1";
        /// <summary> 見積明細カラム名  単価 </summary>
        public const string MITSUMORI_COLUMN_NAME_TANKA = "clm_Tanka1";
        /// <summary> 見積明細カラム名  金額 </summary>
        public const string MITSUMORI_COLUMN_NAME_KINGAKU = "clm_Kingaku1";
        /// <summary> 見積明細カラム名  消費税 </summary>
        public const string MITSUMORI_COLUMN_NAME_TAX_SOTO = "clm_TaxSoto";
        /// <summary> 見積明細カラム名  税区分 </summary>
        public const string MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD = "clm_HinmeiZeiKbnCD1";
        /// <summary> 見積明細カラム名  備考 </summary>
        public const string MITSUMORI_COLUMN_NAME_MEISAI_BIKOU = "clm_MeisaiBikou1";
        /// <summary> 見積明細カラム名  摘要 </summary>
        public const string MITSUMORI_COLUMN_NAME_MEISAI_TEKIYOU = "clm_MeisaiTekiyo1";
        /// <summary> 見積明細カラム名  消費税内税 </summary>
        public const string MITSUMORI_COLUMN_NAME_TAX_UCHI = "clm_TaxUchi";
        /// <summary> 見積明細カラム名  行番号 </summary>
        public const string MITSUMORI_COLUMN_NAME_ROW_NO = "clm_RowNo1";
        /// <summary> 見積明細カラム名  小計フラグ </summary>
        public const string MITSUMORI_COLUMN_NAME_SHOUKEI_FLG = "clm_ShoukeiFlg1";

        /// <summary>小計</summary>
        public const String SUB_TOTAL = "小　　　計";

        /// <summary>見積書の帳票ID（10:見積書）</summary>
        public const int MITSUMORISHO = 10;

        /// <summary>
        /// 伝票区分CD(共通)文字列
        /// </summary>
        public const string DENPYOU_KBN_CD_KYOTU = "9";

        //20250416
        public const string BIKO_COLUMN_NAME_BIKO_CD = "BIKO_CD";

        public const string BIKO_COLUMN_NAME_BIKO_NOTE = "BIKO_NOTE";

        public const string BIKO_COLUMN_SYS_ID = "DETAIL_SYSTEM_ID";
    }
}
