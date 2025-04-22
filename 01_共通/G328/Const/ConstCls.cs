// $Id: ConstCls.cs 17314 2014-03-12 06:42:02Z ogawa@takumi-sys.co.jp $

namespace Shougun.Core.Common.KaisyuuHinmeShousai
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>削除</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>品名CD</summary>
        public static readonly string HINMEI_CD = "HINMEI_CD";

        /// <summary>品名</summary>
        public static readonly string HINMEI_NAME = "HINMEI_NAME";

        /// <summary>単位CD</summary>
        public static readonly string UNIT_CD = "UNIT_CD";

        /// <summary>単位</summary>
        public static readonly string UNIT_NAME = "UNIT_NAME";

        /// <summary>換算値</summary>
        public static readonly string KANSANCHI = "KANSANCHI";

        /// <summary>換算後単位CD</summary>
        public static readonly string KANSAN_UNIT_CD = "KANSAN_UNIT_CD";

        /// <summary>換算後単位名</summary>
        public static readonly string KANSAN_UNIT_NAME = "KANSAN_UNIT_NAME";


        public static readonly string DENPYOU_KBN_CD = "DENPYOU_KBN_CD";
        public static readonly string DENPYOU_KBN_CD_NM = "DENPYOU_KBN_CD_NM";
        public static readonly string KEIYAKU_KBN = "KEIYAKU_KBN";
        public static readonly string KEIYAKU_KBN_NM = "KEIYAKU_KBN_NM";
        public static readonly string KEIJYOU_KBN = "KEIJYOU_KBN";
        public static readonly string KEIJYOU_KBN_NM = "KEIJYOU_KBN_NM";
        public static readonly string YOU_KINYU = "YOU_KINYU";
        public static readonly string INPUT_KBN = "INPUT_KBN";
        public static readonly string INPUT_KBN_NAME = "INPUT_KBN_NAME";
        public static readonly string NIOROSHI_NUMBER = "NIOROSHI_NUMBER";
        public static readonly string ANBUN_FLG = "ANBUN_FLG";
        public static readonly string TEKIYOU_BEGIN = "TEKIYOU_BEGIN";
        public static readonly string TEKIYOU_END = "TEKIYOU_END";
        public static readonly string GENBA_TEKIYOU_BEGIN = "GENBA_TEKIYOU_BEGIN";
        public static readonly string GENBA_TEKIYOU_END = "GENBA_TEKIYOU_END";

        public static readonly string INPUT_KBN_1 = "直接入力";
        public static readonly string INPUT_KBN_2 = "組込";
        /// <summary>
        /// 伝票区分CD 1:売上
        /// </summary>
        public static readonly string DENPYOU_KBN_CD_URIAGE = "1";

        /// <summary>
        /// 伝票区分CD 2:支払
        /// </summary>
        public static readonly string DENPYOU_KBN_CD_SHIHARAI = "2";

        /// <summary>
        /// 伝票区分CD 9:共通
        /// </summary>
        public static readonly string DENPYOU_KBN_CD_KYOUTU = "9";

        /// <summary>
        /// 取引区分（現金）
        /// </summary>
        internal static readonly System.Data.SqlTypes.SqlInt16 TORIHIKI_KBN_GENKIN = 1;

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public class ExceptionErrMsg
        {
            public const string HAITA = "排他エラーが発生しました。";
            public const string REIGAI = "例外エラーが発生しました。";
        }
    }
}
