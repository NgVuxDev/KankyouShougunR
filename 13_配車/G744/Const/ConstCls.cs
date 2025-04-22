using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.CarTransferTeiki
{
    class ConstCls
    {
        //拠点CD（全社）
        public const string KyouTenZenSya = "99";

        /// <summary>明細部の非表示列（システムID）</summary>
        public const string HIDDEN_COLUMN_SYSTEM_ID = "SYSTEM_ID_HIDDEN";
        /// <summary>明細部の非表示列（枝番）</summary>
        public const string HIDDEN_COLUMN_SEQ = "SEQ_HIDDEN";
        /// <summary>明細部の非表示列（明細システムID）</summary>
        public const string HIDDEN_COLUMN_DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID_HIDDEN";
        /// <summary>明細部の非表示列（定期配車番号）</summary>
        public const string HIDDEN_COLUMN_HAISHA_NUMBER = "TEIKI_HAISHA_NUMBER";
        ///// <summary>明細部の非表示列（作業日）</summary>
        //public const string HIDDEN_COLUMN_SAGYOU_DATE = "SAGYOU_DATE_HIDDEN";
        /// <summary>明細部の品名情報列</summary>
        public const string COLUMN_HINMEI_INFO_NAME = "品名情報";
        /// <summary>
        /// 定期配車詳細（品名情報）のカラム名
        /// </summary>
        public class ShousaiColName
        {
            /// <summary>削除フラグ</summary>
            public const string DELETE_FLG = "DELETE_FLG";
            /// <summary>レコードID/明細システムID</summary>
            public const string REC_ID = "REC_ID";
            /// <summary>レコードSEQ/行番号</summary>
            public const string REC_SEQ = "REC_SEQ";
            /// <summary>品名CD</summary>
            public const string HINMEI_CD = "HINMEI_CD";
            /// <summary>品名名</summary>
            public const string HINMEI_NAME_RYAKU = "HINMEI_NAME_RYAKU";
            /// <summary>単位CD</summary>
            public const string UNIT_CD = "UNIT_CD";
            /// <summary>単位名</summary>
            public const string UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";
            /// <summary>換算値</summary>
            public const string KANSANCHI = "KANSANCHI";
            /// <summary>換算後単位CD</summary>
            public const string KANSAN_UNIT_CD = "KANSAN_UNIT_CD";

            //2014/01/30 追加 仕様変更 喬 start
            /// <summary>契約区分</summary>
            public const string KEIYAKU_KBN = "KEIYAKU_KBN";
            /// <summary>計上区分</summary>
            public const string KEIJYOU_KBN = "KEIJYOU_KBN";
            /// <summary>伝票区分CD</summary>
            public const string DENPYOU_KBN_CD = "DENPYOU_KBN_CD";
            /// <summary>換算後単位モバイル出力フラグ</summary>
            public const string KANSAN_UNIT_MOBILE_OUTPUT_FLG = "KANSAN_UNIT_MOBILE_OUTPUT_FLG";
            //2014/01/30 追加 仕様変更 喬 end

            /// <summary>入力区分</summary>
            public const string INPUT_KBN = "INPUT_KBN";
            /// <summary>荷降No</summary>
            public const string NIOROSHI_NUMBER = "NIOROSHI_NUMBER";
            /// <summary>実数</summary>
            public const string ANBUN_FLG = "ANBUN_FLG";

        }
    }



}
