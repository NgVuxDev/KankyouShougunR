using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.Scale.Keiryou.Const
{
    /// <summary>
    /// 売上／支払用定数クラス
    /// </summary>
    public class KeiryouConstans
    {
        // (手)
        public const string MANUAL = "(手)";

        //領収書1（あり）
        public const string RYOSYUSYO_KBN_1 = "1";
        //領収書1（なし）
        public const string RYOSYUSYO_KBN_2 = "2";
 
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
        /// 単位区分名(kg)
        /// </summary>
        public const string UNIT_NAME_KG = "Kg";

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
                    case KeiryouConstans.SYS_RENBAN_HOUHOU_KBN.HIRENBAN:
                        return "日連番";

                    case KeiryouConstans.SYS_RENBAN_HOUHOU_KBN.NENRENBAN:
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
        /// 伝種区分CD(計量)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_UKEIRE = 1;

        /// <summary>
        /// 伝種区分CD(出荷)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_SHUKKA = 2;

        /// <summary>
        /// 伝種区分CD(共通)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_KYOTU = 9;

        /// <summary>
        /// 伝種区分CD(売上支払)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_URIAGESHIHARAI = 3;

        /// <summary>
        /// 伝種区分CD(入金)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_NYUUKIN = 10;

        /// <summary>
        /// 伝種区分CD(出金)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_SHUKKIN = 20;

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

        /// <summary>
        /// 伝票区分CD(売上)
        /// </summary>
        public const short DENPYOU_KBN_CD_URIAGE = 1;

        /// <summary>
        /// 伝票区分CD(支払)
        /// </summary>
        public const short DENPYOU_KBN_CD_SHIHARAI = 2;

        // No.4001-->
        /// <summary>
        /// 伝票区分CD(売上)文字列
        /// </summary>
        public const string KIHON_KEIRYOU_CD_UKEIRE_STR = "1";

        /// <summary>
        /// 伝票区分CD(支払)文字列
        /// </summary>
        public const string KIHON_KEIRYOU_CD_SHUKKA_STR = "2";
        // No.4001<--

        /// <summary>
        /// 伝票区分CD(売上)文字列
        /// </summary>
        public const string DENPYOU_KBN_CD_URIAGE_STR = "1";

        /// <summary>
        /// 伝票区分CD(支払)文字列
        /// </summary>
        public const string DENPYOU_KBN_CD_SHIHARAI_STR = "2";

        /// <summary>
        /// 伝票区分略称名を取得する
        /// </summary>
        /// <param name="denpyouKbnCd"></param>
        /// <returns></returns>
        public static string GetDenpyouKbnNameRyaku(short denpyouKbnCd)
        {
            // TODO: これはDBから取得するほうがいい
            switch (denpyouKbnCd)
            {
                case DENPYOU_KBN_CD_URIAGE:
                    return "売上";

                case DENPYOU_KBN_CD_SHIHARAI:
                    return "支払";
            }
            return string.Empty;
        }

        /// <summary>名細部のコントロール名</summary>
        public static readonly string CTR_DETAIL = "gcMultiRow1";

        /// <summary>T_KEIRYOU_DETAILの品名CD</summary>
        public static readonly string HINMEI_CD = "HINMEI_CD";

        /// <summary>総重量</summary>
        public static readonly string STACK_JYUURYOU = "STACK_JYUURYOU";

        /// <summary>空重量</summary>
        public static readonly string EMPTY_JYUURYOU = "EMPTY_JYUURYOU";
    }
}
