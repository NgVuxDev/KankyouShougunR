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
            TASHA = 2,
            SONOTA = 3
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
                return DAIKAN_KBN.SONOTA;
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
        /// 受入情報確定利用区分(する)
        /// </summary>
        public const int SHUKKA_KAKUTEI_USE_KBN_YES = 1;

        /// <summary>
        /// 受入情報確定利用区分(しない)
        /// </summary>
        public const int SHUKKA_KAKUTEI_USE_KBN_NO = 2;

        /// <summary>
        /// 受入情報差引基準(売上)
        /// </summary>
        public const int UKEIRE_CALC_BASE_KBN_URIAGE = 1;

        /// <summary>
        /// 受入情報差引基準(支払)
        /// </summary>
        public const int UKEIRE_CALC_BASE_KBN_SHIHARAI = 2;

        /// <summary>
        /// 出荷情報差引基準(売上)
        /// </summary>
        public const int SHUKKA_CALC_BASE_KBN_URIAGE = 1;

        /// <summary>
        /// 出荷情報差引基準(支払)
        /// </summary>
        public const int SHUKKA_CALC_BASE_KBN_SHIHARAI = 2;

        /// <summary>
        /// 売上/支払情報確定利用区分(する)
        /// </summary>
        public const int UR_SH_KAKUTEI_USE_KBN_YES = 1;

        /// <summary>
        /// 売上/支払情報確定利用区分(しない)
        /// </summary>
        public const int UR_SH_KAKUTEI_USE_KBN_NO = 2;

        /// <summary>
        /// 売上/支払情報差引基準(売上)
        /// </summary>
        public const int UR_SH_CALC_BASE_KBN_URIAGE = 1;

        /// <summary>
        /// 売上/支払情報差引基準(支払)
        /// </summary>
        public const int UR_SH_CALC_BASE_KBN_SHIHARAI = 2;


        /// <summary>
        /// システム税計算区分利用形態(伝票毎)
        /// </summary>
        public const int SYS_ZEI_KEISAN_KBN_USE_KBN_DENPYOU = 1;

        /// <summary>
        /// システム税計算区分利用形態(明細毎)
        /// </summary>
        public const int SYS_ZEI_KEISAN_KBN_USE_KBN_MEISAI = 2;

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
        /// 伝種区分CD(共通)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_KYOTU = 9;

        /// <summary>
        /// 伝種区分CD(売上支払)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_URIAGESHIHARAI = 3;

        /// <summary>
        /// 伝種区分CD(受入)(文字列)
        /// </summary>
        public static string DENSHU_KBN_CD_UKEIRE_STR = "1";

        /// <summary>
        /// 伝種区分CD(出荷)(文字列)
        /// </summary>
        public static string DENSHU_KBN_CD_SHUKKA_STR = "2";

        /// <summary>
        /// 伝種区分CD(売上支払)(文字列)
        /// </summary>
        public static string DENSHU_KBN_CD_URIAGESHIHARAI_STR = "3";

        /// <summary>
        /// 伝種区分CD(入金)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_NYUUKIN = 10;

        /// <summary>
        /// 伝種区分CD(出金)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_SHUKKIN = 20;

        /// <summary>
        /// 伝種区分CD(計量)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_KEIRYOU = 140;

        /// <summary>
        /// 伝種区分CD(受入実績)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_UKEIRE_JISSEKI = 370;

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

        /// <summary>
        /// 伝票区分CD(売上/支払)
        /// </summary>
        public const short DENPYOU_KBN_CD_UR_SH = 3;

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

        /// <summary>
        /// 単位区分名(kg)
        /// </summary>
        public const string UNIT_NAME_KG = "Kg";

        // No.2275
        /// <summary>
        /// 単位区分名(t)
        /// </summary>
        public const string UNIT_NAME_TON = "ｔ";

        /// <summary>
        /// 単位CD（t）
        /// </summary>
        public const string UNIT_CD_TON = "1";

        /// <summary>
        /// 単位CD（kg）
        /// </summary>
        public const string UNIT_CD_KG = "3";

        /// <summary>
        /// 確定区分(確定)
        /// </summary>
        public const short KAKUTEI_KBN_KAKUTEI = 1;

        /// <summary>
        /// 確定区分(未確定)
        /// </summary>
        public const short KAKUTEI_KBN_MIKAKUTEI = 2;

        /// <summary>
        /// 確定区分名取得
        /// 想定外の数値が渡されたら空を返す
        /// </summary>
        /// <param name="kbn">確定区分</param>
        /// <returns>確定区分名</returns>
        public static string GetKakuteiKbnName(short kbn)
        {
            string returnVal = string.Empty;
            switch (kbn)
            {
                case 1:
                    returnVal = "確定伝票";
                    break;

                case 2:
                    returnVal = "未確定伝票";
                    break;

                default:
                    returnVal = string.Empty;
                    break;
            }

            return returnVal;
        }

        /// <summary>締処理状況表示文字列(締済)</summary>
        public const string SHIMEZUMI = "締済";

        /// <summary>締処理状況表示文字列(未締)</summary>
        public const string MISHIME = "未締";

        /// <summary>締処理状況表示文字列(詳細用)(済)</summary>
        public const string SHIMEZUMI_DETAIL = "済";

        /// <summary>締処理状況表示文字列(詳細用)(ブランク)</summary>
        public const string MISHIME_DETAIL = " ";

        /// <summary>入出金区分(現金)</summary>
        public const short NYUUSHUKKIN_KBN_CD_GENKIN = 1;

        /// <summary>入出金区分(相殺)</summary>
        public const short NYUUSHUKKIN_KBN_CD_SOUSAI = 22;

        /// <summary>拠点CD（全社：99）</summary>
        public const string KYOTEN_ZENSHA = "99";

        /// <summary>帳票ID（計量票：7）</summary>
        public const int KEIRYOUHYOU = 7;

        /// <summary>帳票ID（請求書：8）</summary>
        public const int SEIKYUSHO = 8;

        /// <summary>帳票ID（領収書：12）</summary>
        public const int RYOUSYUUSHO = 12;

        /// <summary>帳票ID（仕切書：13）</summary>
        public const int SHIKIRISHO = 13;

        /// <summary>
        /// 配車状況CD「1:受注」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_JUCHU = "1";

        /// <summary>
        /// 配車状況CD「2:配車」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_HAISHA = "2";

        /// <summary>
        /// 配車状況CD「3:計上」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_KEIJO = "3";

        /// <summary>
        /// 配車状況CD「4:キャンセル」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_CANCEL = "4";

        /// <summary>
        /// 配車状況CD「5:回収なし」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_NASHI = "5";

        /// <summary>
        /// 配車状況「1:受注」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_JUCHU = "受注";

        /// <summary>
        /// 配車状況「2:配車」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_HAISHA = "配車";

        /// <summary>
        /// 配車状況「3:計上」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_KEIJO = "計上";

        /// <summary>
        /// 配車状況「4:キャンセル」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_CANCEL = "キャンセル";

        /// <summary>
        /// 配車状況「5:回収なし」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_NASHI = "回収なし";

        /// <summary>
        /// 継続入力：する
        /// 受入、出荷、売上/支払入力の画面上のみで使用する値
        /// </summary>
        public static readonly string KEIZOKU_NYUURYOKU_ON = "1";

        /// <summary>
        /// 継続入力：しない
        /// 受入、出荷、売上/支払入力の画面上のみで使用する値
        /// </summary>
        public static readonly string KEIZOKU_NYUURYOKU_OFF = "2";

        /// <summary>
        /// 取引区分：現金(文字列)
        /// </summary>
        public static readonly string STR_TORIHIKI_KBN_1 = "現金";

        /// <summary>
        /// 取引区分：掛け(文字列)
        /// </summary>
        public static readonly string STR_TORIHIKI_KBN_2 = "掛け";

        /// <summary>
        /// 取引区分CD：現金
        /// </summary>
        public static readonly short TORIHIKI_KBN_CD_1 = 1;

        /// <summary>
        /// 取引区分CD：掛け
        /// </summary>
        public static readonly short TORIHIKI_KBN_CD_2 = 2;

        /// <summary>
        /// トラックスケール通信 取込ファイル名
        /// </summary>
        public static readonly string TORIKOMI_FILE_NAME = "Torikomi.txt";

        /// <summary>
        /// 予約状況CD「1:予約完了」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_CD_YOYAKU_KANRYOU = "1";

        /// <summary>
        /// 予約状況CD「3:計上」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_CD_KEIJOU = "3";

        /// <summary>
        /// 予約状況「1:予約完了」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_YOYAKU_KANRYOU_NAME = "予約完了";

        /// <summary>
        /// 予約状況「3:計上」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_KEIJOU_NAME = "計上";
    }
}
