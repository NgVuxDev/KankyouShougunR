// $Id: ConstCls.cs 25365 2014-07-11 07:49:27Z j-kikuchi $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>
        /// 回収明細部（定期配車明細）のカラム名
        /// </summary>
        public class DetailColName
        {
            /// <summary>削除フラグ</summary>
            public const string DELETE_FLG = "DELETE_FLG";
            /// <summary>No</summary>
            public const string NO = "ROW_NUMBER";
            /// <summary>順番</summary>
            public const string JUNNBANN = "JUNNBANN";
            /// <summary>回数</summary>
            public const string ROUND_NO = "ROUND_NO";
            /// <summary>業者CD</summary>
            public const string GYOUSHA_CD = "GYOUSHA_CD";
            /// <summary>業者名</summary>
            public const string GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
            /// <summary>現場CD</summary>
            public const string GENBA_CD = "GENBA_CD";
            /// <summary>現場名</summary>
            public const string GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
            /// <summary>詳細ボタン</summary>
            public const string SHOUSAI = "SHOUSAI";
            /// <summary>品名情報</summary>
            public const string HINMEI_INFO = "HINMEI_INFO";
            /// <summary>希望時間</summary>
            public const string KIBOU_TIME = "KIBOU_TIME";
            /// <summary>作業時間(分)</summary>
            public const string SAGYOU_TIME_MINUTE = "SAGYOU_TIME_MINUTE";
            /// <summary>明細備考</summary>
            public const string MEISAI_BIKOU = "MEISAI_BIKOU";
            /// <summary>受付番号</summary>
            public const string UKETSUKE_NUMBER = "UKETSUKE_NUMBER";
            /// <summary>明細システムID</summary>
            public const string DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";
            /// <summary>レコードID</summary>
            public const string REC_ID = "REC_ID";
            /// <summary>詳細ポップアップ用テーブル名</summary>
            public const string SHOUSAI_TABLE_NAME = "SHOUSAI_TABLE_NAME";
            /// <summary>モバイル将軍連携</summary>
            public const string MOBILE_RENKEI = "MOBILE_RENKEI";
        }

        /// <summary>
        /// 荷降明細部（定期配車荷降）のカラム名
        /// </summary>
        public class NioroshiColName
        {
            /// <summary>荷降No</summary>
            public const string NIOROSHI_NUMBER = "NIOROSHI_NUMBER";
            /// <summary>荷降業者CD</summary>
            public const string NIOROSHI_GYOUSHA_CD = "NIOROSHI_GYOUSHA_CD";
            /// <summary>荷降業者名</summary>
            public const string NIOROSHI_GYOUSHA_NAME_RYAKU = "NIOROSHI_GYOUSHA_NAME_RYAKU";
            /// <summary>荷降現場CD</summary>
            public const string NIOROSHI_GENBA_CD = "NIOROSHI_GENBA_CD";
            /// <summary>荷降現場名</summary>
            public const string NIOROSHI_GENBA_NAME_RYAKU = "NIOROSHI_GENBA_NAME_RYAKU";
        }

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

        /// <summary>詳細ポップアップ用テーブル名</summary>
        public static readonly string preTableName = "SHOUSAI_TABLE_";

        /// <summary>
        /// 配車状況「1:受注」
        /// </summary>
        internal static readonly string HAISHA_JOUKYOU_1 = "受注";

        /// <summary>
        /// 配車種類「1:通常」
        /// </summary>
        internal static readonly string HAISHA_SHURUI_1 = "通常";

        /// <summary>mapbox連携で使用する項目</summary>
        public static readonly string ROW_NO = "ROW_NO";
        public static readonly string ROUND_NO = "ROUND_NO";
        public static readonly string COURSE_NAME = "COURSE_NAME";
        public static readonly string GYOUSHA_CD = "GYOUSHA_CD";
        public static readonly string GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
        public static readonly string GENBA_CD = "GENBA_CD";
        public static readonly string GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
        public static readonly string GENBA_ADDRESS1 = "GENBA_ADDRESS1";
        public static readonly string GENBA_ADDRESS2 = "GENBA_ADDRESS2";
        public static readonly string GENBA_POST = "GENBA_POST";
        public static readonly string GENBA_TEL = "GENBA_TEL";
        public static readonly string BIKOU1 = "BIKOU1";
        public static readonly string BIKOU2 = "BIKOU2";
        public static readonly string GENBA_LATITUDE = "GENBA_LATITUDE";
        public static readonly string GENBA_LONGITUDE = "GENBA_LONGITUDE";
        public static readonly string TODOUFUKEN_NAME = "TODOUFUKEN_NAME";
        public static readonly string HINMEI_NAME = "HINMEI_NAME";

        public static readonly string GYOUSHA_ADDRESS1 = "GYOUSHA_ADDRESS1";
        public static readonly string GYOUSHA_ADDRESS2 = "GYOUSHA_ADDRESS2";
        public static readonly string GYOUSHA_POST = "GYOUSHA_POST";
        public static readonly string GYOUSHA_TEL = "GYOUSHA_TEL";
        public static readonly string GYOUSHA_LATITUDE = "GYOUSHA_LATITUDE";
        public static readonly string GYOUSHA_LONGITUDE = "GYOUSHA_LONGITUDE";
    }
}
