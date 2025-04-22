using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Utility;
using r_framework.CustomControl;
using System.Drawing;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.Const
{
    /// <summary>
    /// 定義クラス
    /// </summary>
    public class ConstClass
    {
        #region 定数（UIで使用）

        /// <summary>
        /// ToStringの数値フォーマット（カンマ付与）
        /// </summary>
        public const string DEF_TOSTRING_FORMAT_COMMA_VALIE = "#,0";

        /// <summary>
        /// エラーメッセージ 税計算区分
        /// </summary>
        public const string DEF_ERROR_MSG_ZEI_KEISAN_KBN = "税計算区分";

        /// <summary>
        /// エラーメッセージ 税区分
        /// </summary>
        public const string DEF_ERROR_MSG_ZEI_KBN = "税区分";

        /// <summary>
        /// エラーメッセージ 伝票発行区分
        /// </summary>
        public const string DEF_ERROR_MSG_DENPYOU_HAKKOU_KBN = "伝票発行区分";

        /// <summary>
        /// エラーメッセージ 発行区分
        /// </summary>
        public const string DEF_ERROR_MSG_HAKKOU_KBN = "発行区分";

        /// <summary>
        /// 差引基準 請求
        /// </summary>
        public const string SASIHIKI_KIJUN_SEIKYUU = "請求";

        /// <summary>
        /// 差引基準 支払
        /// </summary>
        public const string SASIHIKI_KIJUN_SHIHARAI = "支払";

        /// <summary>
        /// 仕切書 御請求額
        /// </summary>
        public const string GOSEIKYUUGAKU = "御請求額";

        /// <summary>
        /// 仕切書 御精算額
        /// </summary>
        public const string GOSEISANGAKU = "御精算額";

        #endregion

        #region ボタン設定ファイル定数
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        public const string ButtonInfoXmlPath = "Shougun.Core.PayByProxy.DainoDenpyoHakkou.Setting.ButtonSetting.xml";
        #endregion

        #region ツールチップ文言定数
        /// <summary>
        /// 【1、2】のいずれかで入力してください
        /// </summary>
        public const string TOOL_TIP_TXT_1 = "【1、2】のいずれかで入力してください";
        /// <summary>
        /// 【2、3】のいずれかで入力してください
        /// </summary>
        public const string TOOL_TIP_TXT_2 = "【2、3】のいずれかで入力してください";
        /// <summary>
        /// 【1～3】のいずれかで入力してください
        /// </summary>
        public const string TOOL_TIP_TXT_3 = "【1～3】のいずれかで入力してください";

        /// <summary>
        /// 【1、3】のいずれかで入力してください
        /// </summary>
        public const string TOOL_TIP_TXT_4 = "【1、3】のいずれかで入力してください";
        #endregion

        #region ENUM（DBデータ値）

        /// <summary>
        /// 税区分CD 設定値
        /// </summary>
        public enum ZeiKbnCd
        {
            /// <summary>
            /// なし
            /// </summary>
            None = 0,
            /// <summary>
            /// 外税(1)
            /// </summary>
            SotoZei = 1,
            /// <summary>
            /// 内税(2)
            /// </summary>
            UchiZei = 2,
            /// <summary>
            /// 非課税(3)
            /// </summary>
            HikaZei = 3,
        }

        /// <summary>
        /// 税計算区分 設定値
        /// </summary>
        public enum ZeiKeisanKbn
        {
            /// <summary>
            /// なし(0)
            /// </summary>
            None = 0,
            /// <summary>
            /// 伝票(1)
            /// </summary>
            Denpyo = 1,
            /// <summary>
            /// 請求(2)
            /// </summary>
            Seikyu = 2,
            /// <summary>
            /// 明細(3)
            /// </summary>
            Meisai = 3,
        }

        /// <summary>
        /// 発行区分 設定値
        /// </summary>
        public enum HakkouKbnCd
        {
            /// <summary>
            /// なし
            /// </summary>
            None = 0,
            /// <summary>
            /// 個別(1)
            /// </summary>
            Kobetu = 1,
            /// <summary>
            /// 相殺(2)
            /// </summary>
            Sousai = 2,
            /// <summary>
            /// 全て(3)
            /// </summary>
            Subete = 3,
        }

        /// <summary>
        /// 伝票発行区分 設定値
        /// </summary>
        public enum DenpyoHakkouKbnCd
        {
            /// <summary>
            /// なし
            /// </summary>
            None = 0,
            /// <summary>
            /// あり(1)
            /// </summary>
            Ari = 1,
            /// <summary>
            /// なし(2)
            /// </summary>
            Nashi = 2,
        }

        /// <summary>
        /// システム税計算区分利用形態
        /// </summary>
        public enum SysZeiKeisanKbnUseKbn
        {
            /// <summary>
            /// なし(0)
            /// </summary>
            None = 0,
            /// <summary>
            /// 締毎税・伝票毎税(1)
            /// </summary>
            Denpyo = 1,
            /// <summary>
            /// 締毎税・明細毎税(2)
            /// </summary>
            Meisai = 2,
        }

        /// <summary>
        /// システム情報テーブル 有り無し定義
        /// </summary>
        public enum SysInfoUmuValu
        {
            /// <summary>
            /// なし(0)
            /// </summary>
            None = 0,
            /// <summary>
            /// 有り(1)
            /// </summary>
            Ari = 1,
            /// <summary>
            /// 無し(2)
            /// </summary>
            Nashi = 2,
        }

        /// <summary>
        /// システム情報テーブル 伝票出力区分 定義
        /// </summary>
        public enum DenpyoPrintKbn
        {
            /// <summary>
            /// なし(0)
            /// </summary>
            None = 0,
            /// <summary>
            /// 個別(1)
            /// </summary>
            Kobetu = 1,
            /// <summary>
            /// 相殺(2)
            /// </summary>
            Sousatu = 2,
            /// <summary>
            /// 全て(3)
            /// </summary>
            Subete = 3,
        }

        /// <summary>
        /// 自SQL用 レコード種類
        /// </summary>
        public enum SelectRecordType
        {
            /// <summary>
            /// なし(0)
            /// </summary>
            None = 0,

            /// <summary>
            /// 受入請求(1)
            /// </summary>
            UkeireSeikyu = 1,

            /// <summary>
            /// 受入支払(2)
            /// </summary>
            UkeireShiharai = 2,

            /// <summary>
            /// 出荷請求(3)
            /// </summary>
            ShukkaSeikyu = 3,

            /// <summary>
            /// 出荷支払(4)
            /// </summary>
            ShukkaShiharai = 4,

        }

        #endregion

        #region Utility

        /// <summary>
        /// 文字列から税区分を判断する
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ZeiKbnCd GetZeiKbnCd(string text)
        {
            LogUtility.DebugMethodStart(text);

            int selected;
            ZeiKbnCd res = ZeiKbnCd.None;

            if (text.Trim().Length > 0 && int.TryParse(text, out selected))
            {
                res = (ZeiKbnCd)selected;
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 文字列から発行区分を判断する
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static HakkouKbnCd GetHakkouKbnCd(string text)
        {
            LogUtility.DebugMethodStart(text);

            int selected;
            HakkouKbnCd res = HakkouKbnCd.None;

            if (text.Trim().Length > 0 && int.TryParse(text, out selected))
            {
                res = (HakkouKbnCd)selected;
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 文字列から税計算区分を判断する
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ZeiKeisanKbn GetZeiKeisanKbnCd(string text)
        {
            LogUtility.DebugMethodStart(text);

            int selected;
            ZeiKeisanKbn res = ZeiKeisanKbn.None;

            if (text.Trim().Length > 0 && int.TryParse(text, out selected))
            {
                res = (ZeiKeisanKbn)selected;
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 文字列から伝票発行区分を判断する
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DenpyoHakkouKbnCd GetDenpyoHakkouKbnCd(string text)
        {
            LogUtility.DebugMethodStart(text);

            int selected;
            DenpyoHakkouKbnCd res = DenpyoHakkouKbnCd.None;

            if (text.Trim().Length > 0 && int.TryParse(text, out selected))
            {
                res = (DenpyoHakkouKbnCd)selected;
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 文字列を数値に変換する
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static decimal GetDoubleValue(string text)
        {
            LogUtility.DebugMethodStart(text);

            decimal tmp;
            if (decimal.TryParse(text, out tmp))
            {
                LogUtility.DebugMethodEnd(tmp);
                return tmp;
            }

            LogUtility.DebugMethodEnd(0);
            return 0;
        }

        /// <summary>
        /// コントロール文字色変更
        /// </summary>
        /// <param name="con"></param>
        /// <param name="value"></param>
        public static void SetForeColor(CustomNumericTextBox2 con, decimal value)
        {
            LogUtility.DebugMethodStart(con, value);

            if (con != null)
            {
                // プラスの場合文字色変更
                if (value >= 0)
                {
                    // 黒文字
                    con.ForeColor = Color.Black;
                }
                else
                {
                    // 赤文字
                    con.ForeColor = Color.Red;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

    }
}
