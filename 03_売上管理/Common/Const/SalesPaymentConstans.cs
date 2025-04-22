using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Function.ShougunCSCommon.Const
{
    /// <summary>
    /// 売上／支払用定数クラス
    /// </summary>
    public class SalesPaymentConstans
    {
        /// <summary>
        /// 確定伝票の表示文字列
        /// </summary>
        public static readonly string[] KAKUTEI_KBN_NAME_STRINGS = new string[] { "確定伝票", "未確定伝票" };

        /// <summary>
        /// 台貫区分　自社
        /// </summary>
        public const string DAIKAN_KBN_JISHA = "1";

        /// <summary>
        /// 台貫区分　他社
        /// </summary>
        public const string DAIKAN_KBN_TASHA = "2";

        /// <summary>
        /// 台貫区分
        /// </summary>
        public enum DAIKAN_KBN
        {
            JISHA = 1,
            TASHA = 2
        }

        /// <summary>
        /// 台貫区分表示用文字列
        /// </summary>
        public static class DAIKAN_KBNExt
        {

            /// <summary>
            /// 台貫区分表示用文字列変換メソッド
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            public static string ToTypeString(DAIKAN_KBN e)
            {
                switch (e)
                {
                    case DAIKAN_KBN.JISHA:
                        return "自社";

                    case DAIKAN_KBN.TASHA:
                        return "他社";
                }
                return String.Empty;
            }

            /// <summary>
            /// 数値からSalesPaymentConstansの値を返却します
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            public static DAIKAN_KBN ToDaikanKbn(string e)
            {
                switch (e)
                {
                    case SalesPaymentConstans.DAIKAN_KBN_JISHA:
                        return DAIKAN_KBN.JISHA;

                    case SalesPaymentConstans.DAIKAN_KBN_TASHA:
                        return DAIKAN_KBN.TASHA;
                }
                // 一致しない場合に返す値がこれでいいかは確認していない
                return DAIKAN_KBN.JISHA;
            }
        }

        /// <summary>
        /// 受入情報確定利用区分(する)
        /// </summary>
        public const int UKEIRE_KAKUTEI_USE_KBN_YES = 1;

        /// <summary>
        /// 受入情報確定利用区分(しない)
        /// </summary>
        public const int UKEIRE_KAKUTEI_USE_KBN_NO = 2;

        /// <summary>
        /// 受入情報差引基準(売上)
        /// </summary>
        public const int UKEIRE_CALC_BASE_KBN_URIAGE = 1;


        /// <summary>
        /// 受入情報差引基準(支払)
        /// </summary>
        public const int UKEIRE_CALC_BASE_KBN_SHIHARAI = 2;

        /// <summary>
        /// システム確定登録単位区分(伝票)
        /// </summary>
        public const int SYS_KAKUTEI_TANNI_KBN_DENPYOU = 1;

        /// <summary>
        /// システム確定登録単位区分(明細)
        /// </summary>
        public const int SYS_KAKUTEI_TANNI_KBN_MEISAI = 2;

        /// <summary>
        /// システム連番方法区分(日連番)
        /// </summary>
        public const int SYS_RENBAN_HOUHOU_KBN_HIRENBAN = 1;

        /// <summary>
        /// システム連番方法区分(年連番)
        /// </summary>
        public const int SYS_RENBAN_HOUHOU_KBN_NENRENBAN = 2;

        /// <summary>
        /// SYS_RENBAN_HOUHOU_KBNのEnum
        /// </summary>
        public enum SYS_RENBAN_HOUHOU_KBN
        {
            HIRENBAN = SYS_RENBAN_HOUHOU_KBN_HIRENBAN,
            NENRENBAN = SYS_RENBAN_HOUHOU_KBN_NENRENBAN
        }

        /// <summary>
        /// SYS_RENBAN_HOUHOU_KBN表示用文字列
        /// </summary>
        public static class SYS_RENBAN_HOUHOU_KBNExt
        {
            /// <summary>
            /// SYS_RENBAN_HOUHOU_KBNから表示用文字列を返す
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            public static string ToTypeString(SYS_RENBAN_HOUHOU_KBN e)
            {
                switch (e)
                {
                    case SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN.HIRENBAN:
                        return "日連番";

                    case SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN.NENRENBAN:
                        return "年連番";
                }

                return string.Empty;
 
            }
        }

        /// <summary>
        /// システムマニ登録形態区分(伝票1:マニN)
        /// </summary>
        public static SqlInt16 SYS_MANI_KEITAI_KBN_DENPYOU = 1;

        /// <summary>
        /// システムマニ登録形態区分(明細1:マニN)
        /// </summary>
        public static int SYS_MANI_KEITAI_KBN_MEISAI = 2;

        /// <summary>
        /// 伝種区分CD(受入)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_UKEIRE = 1;

        /// <summary>
        /// 伝種区分CD(出荷)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_SHUKKA = 2;

        /// <summary>
        /// 伝種区分CD(売上支払)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_URIAGESHIHARAI = 3;

        /// <summary>
        /// ローカル用登録区分
        /// DBには登録しない値。あくまでローカルで使うための変数
        /// </summary>
        public enum REGIST_KBN
        {
            NONE = 0,
            INSERT = 1,
            UPDATE = 2
        }

    }
}
