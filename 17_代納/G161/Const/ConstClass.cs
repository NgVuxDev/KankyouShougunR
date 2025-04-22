using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PayByProxy.DainoNyuryuku
{
    public class ConstClass
    {
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        public const string ButtonInfoXmlPath = "Shougun.Core.PayByProxy.DainoNyuryuku.Setting.ButtonSetting.xml";

        /// <summary>
        /// エラーメッセージ
        ///</summary>
        public class ExceptionErrMsg
        {
            public const string Err = "排他エラーが発生しました。";
        }

        /// <summary>拠点CD（全社：99）</summary>
        public const string KYOTEN_ZENSHA = "99";

        public const string UKEIRE_DENPYOU_KBN_NAME = "支払";
        public const string SHUKKA_DENPYOU_KBN_NAME = "売上";

        /// <summary>締処理状況表示文字列(締済)</summary>
        public const string SHIMEZUMI = "締済";

        /// <summary>締処理状況表示文字列(未締)</summary>
        public const string MISHIME = "未締";

        /// <summary>
        /// 売上/支払情報確定利用区分(する)
        /// </summary>
        public const int UR_SH_KAKUTEI_USE_KBN_YES = 1;

        /// <summary>
        /// 売上/支払情報確定利用区分(しない)
        /// </summary>
        public const int UR_SH_KAKUTEI_USE_KBN_NO = 2;

        /// <summary>
        /// システム確定登録単位区分(伝票)
        /// </summary>
        public const int SYS_KAKUTEI_TANNI_KBN_DENPYOU = 1;
        
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

        #region コントロール名称
        /// <summary>
        /// 入力担当者
        /// </summary>
        public const string CONTROL_NYUURYOKU_TANTOUSHA_CD = "NYUURYOKU_TANTOUSHA_CD";

        /// <summary>
        /// 運搬業者
        /// </summary>
        public const string CONTROL_UNPAN_GYOUSHA_CD = "UNPAN_GYOUSHA_CD";

        /// <summary>
        /// 車種
        /// </summary>
        public const string CONTROL_SHASHU_CD = "SHASHU_CD";

        /// <summary>
        /// 車輌
        /// </summary>
        public const string CONTROL_SHARYOU_CD = "SHARYOU_CD";

        /// <summary>
        /// 運転者
        /// </summary>
        public const string CONTROL_UNTENSHA_CD = "UNTENSHA_CD";

        /// <summary>
        /// ROW_NO
        /// </summary>
        public const string CONTROL_ROW_NO = "ROW_NO";

        #region 受入場合の項目名

        /// <summary>
        /// (受入)取引先
        /// </summary>
        public const string CONTROL_UKEIRE_TORIHIKISAKI_CD = "UKEIRE_TORIHIKISAKI_CD";

        /// <summary>
        /// (受入)業者
        /// </summary>
        public const string CONTROL_UKEIRE_GYOUSHA_CD = "UKEIRE_GYOUSHA_CD";

        /// <summary>
        /// (受入)現場
        /// </summary>
        public const string CONTROL_UKEIRE_GENBA_CD = "UKEIRE_GENBA_CD";

        /// <summary>
        /// (受入)営業担当者
        /// </summary>
        public const string CONTROL_UKEIRE_EIGYOU_TANTOUSHA_CD = "UKEIRE_EIGYOU_TANTOUSHA_CD";

        /// <summary>
        /// (受入)品名CD
        /// </summary>
        public const string CONTROL_UKEIRE_HINMEI_CD = "UKEIRE_HINMEI_CD";

        /// <summary>
        /// (受入)品名名
        /// </summary>
        public const string CONTROL_UKEIRE_HINMEI_NAME = "UKEIRE_HINMEI_NAME";

        /// <summary>
        /// (受入)伝票区分CD
        /// </summary>
        public const string CONTROL_UKEIRE_DENPYOU_KBN_CD = "UKEIRE_DENPYOU_KBN_CD";

        /// <summary>
        /// (受入)伝票区分
        /// </summary>
        public const string CONTROL_UKEIRE_DENPYOU_KBN_NAME = "UKEIRE_DENPYOU_KBN_NAME";

        /// <summary>
        /// (受入)正味
        /// </summary>
        public const string CONTROL_UKEIRE_STACK_JYUURYOU = "UKEIRE_STACK_JYUURYOU";

        /// <summary>
        /// (受入)調整
        /// </summary>
        public const string CONTROL_UKEIRE_CHOUSEI_JYUURYOU = "UKEIRE_CHOUSEI_JYUURYOU";

        /// <summary>
        /// (受入)実正味
        /// </summary>
        public const string CONTROL_UKEIRE_NET_JYUURYOU = "UKEIRE_NET_JYUURYOU";

        /// <summary>
        /// (受入)数量
        /// </summary>
        public const string CONTROL_UKEIRE_SUURYOU = "UKEIRE_SUURYOU";

        /// <summary>
        /// (受入)単位CD
        /// </summary>
        public const string CONTROL_UKEIRE_UNIT_CD = "UKEIRE_UNIT_CD";

        /// <summary>
        /// (受入)単位
        /// </summary>
        public const string CONTROL_UKEIRE_UNIT_NAME = "UKEIRE_UNIT_NAME";

        /// <summary>
        /// (受入)単価
        /// </summary>
        public const string CONTROL_UKEIRE_TANKA = "UKEIRE_TANKA";

        /// <summary>
        /// (受入)金額
        /// </summary>
        public const string CONTROL_UKEIRE_KINGAKU = "UKEIRE_KINGAKU";

        /// <summary>
        /// (受入)明細備考
        /// </summary>
        public const string CONTROL_UKEIRE_MEISAI_BIKOU = "UKEIRE_MEISAI_BIKOU";

        /// <summary>
        /// (受入)税区分
        /// </summary>
        public const string CONTROL_UKEIRE_ZEI_KBN_CD = "UKEIRE_ZEI_KBN_CD";

        /// <summary>
        /// (受入)明細SYSTEM_ID
        /// </summary>
        public const string CONTROL_UKEIRE_DETAIL_SYSTEM_ID = "UKEIRE_DETAIL_SYSTEM_ID";

        #endregion

        #region 出荷場合の項目名

        /// <summary>
        /// (出荷)取引先
        /// </summary>
        public const string CONTROL_SHUKKA_TORIHIKISAKI_CD = "SHUKKA_TORIHIKISAKI_CD";

        /// <summary>
        /// (出荷)業者
        /// </summary>
        public const string CONTROL_SHUKKA_GYOUSHA_CD = "SHUKKA_GYOUSHA_CD";

        /// <summary>
        /// (出荷)現場
        /// </summary>
        public const string CONTROL_SHUKKA_GENBA_CD = "SHUKKA_GENBA_CD";

        /// <summary>
        /// (出荷)営業担当者
        /// </summary>
        public const string CONTROL_SHUKKA_EIGYOU_TANTOUSHA_CD = "SHUKKA_EIGYOU_TANTOUSHA_CD";

        /// <summary>
        /// (出荷)品名CD
        /// </summary>
        public const string CONTROL_SHUKKA_HINMEI_CD = "SHUKKA_HINMEI_CD";

        /// <summary>
        /// (出荷)品名名
        /// </summary>
        public const string CONTROL_SHUKKA_HINMEI_NAME = "SHUKKA_HINMEI_NAME";

        /// <summary>
        /// (出荷)伝票区分CD
        /// </summary>
        public const string CONTROL_SHUKKA_DENPYOU_KBN_CD = "SHUKKA_DENPYOU_KBN_CD";

        /// <summary>
        /// (出荷)伝票区分
        /// </summary>
        public const string CONTROL_SHUKKA_DENPYOU_KBN_NAME = "SHUKKA_DENPYOU_KBN_NAME";

        /// <summary>
        /// (出荷)正味
        /// </summary>
        public const string CONTROL_SHUKKA_STACK_JYUURYOU = "SHUKKA_STACK_JYUURYOU";

        /// <summary>
        /// (出荷)調整
        /// </summary>
        public const string CONTROL_SHUKKA_CHOUSEI_JYUURYOU = "SHUKKA_CHOUSEI_JYUURYOU";

        /// <summary>
        /// (出荷)実正味
        /// </summary>
        public const string CONTROL_SHUKKA_NET_JYUURYOU = "SHUKKA_NET_JYUURYOU";

        /// <summary>
        /// (出荷)数量
        /// </summary>
        public const string CONTROL_SHUKKA_SUURYOU = "SHUKKA_SUURYOU";

        /// <summary>
        /// (出荷)単位CD
        /// </summary>
        public const string CONTROL_SHUKKA_UNIT_CD = "SHUKKA_UNIT_CD";

        /// <summary>
        /// (出荷)単位
        /// </summary>
        public const string CONTROL_SHUKKA_UNIT_NAME = "SHUKKA_UNIT_NAME";

        /// <summary>
        /// (出荷)単価
        /// </summary>
        public const string CONTROL_SHUKKA_TANKA = "SHUKKA_TANKA";

        /// <summary>
        /// (出荷)金額
        /// </summary>
        public const string CONTROL_SHUKKA_KINGAKU = "SHUKKA_KINGAKU";

        /// <summary>
        /// (出荷)明細備考
        /// </summary>
        public const string CONTROL_SHUKKA_MEISAI_BIKOU = "SHUKKA_MEISAI_BIKOU";

        /// <summary>
        /// (出荷)税区分
        /// </summary>
        public const string CONTROL_SHUKKA_ZEI_KBN_CD = "SHUKKA_ZEI_KBN_CD";

        /// <summary>
        /// (出荷)明細SYSTEM_ID
        /// </summary>
        public const string CONTROL_SHUKKA_DETAIL_SYSTEM_ID = "SHUKKA_DETAIL_SYSTEM_ID";

        #endregion

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

        #endregion
    }
}
