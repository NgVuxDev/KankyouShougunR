using r_framework.Utility;
using CommonChouhyouPopup.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Reception.UketsukeMeisaihyo
{
    /// <summary> R659(受付明細表)帳票を表すクラス・コントロール </summary>
    public class ReportInfoR659 : ReportInfoBase
    {
        #region 定数

        /// <summary>テンプレートパス</summary>
        private const string OUTPUT_FORM_PATH = "./Template/R659-Form.xml";

        /// <summary>取引先CD順レイアウト</summary>
        private const string LAYOUT_TORIHKISAKI = "LAYOUT_TORIHIKISAKI";
        /// <summary>業者CD順レイアウト</summary>
        private const string LAYOUT_GYOUSHA = "LAYOUT_GYOUSHA";
        /// <summary>運搬業者CD順レイアウト</summary>
        private const string LAYOUT_UNPAN_GYOUSHA = "LAYOUT_UNPAN_GYOUSHA";
        /// <summary>運転者CD順レイアウト</summary>
        private const string LAYOUT_UNTENSHA = "LAYOUT_UNTENSHA";
        /// <summary>作業日順レイアウト</summary>
        private const string LAYOUT_SAGYOU_DATE = "LAYOUT_SAGYOU_DATE";
        /// <summary>受付番号順レイアウト</summary>
        private const string LAYOUT_UKETSUKE_NUMBER = "LAYOUT_UKETSUKE_NUMBER";

        #endregion

        #region フィールド

        /// <summary>帳票出力用DTO</summary>
        PrintDtoClass dto;

        #endregion

        #region メソッド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dto">帳票出力用DTO</param>
        public ReportInfoR659(PrintDtoClass dto)
        {
            LogUtility.DebugMethodStart(dto);

            this.dto = dto;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// 受付明細表の帳票データ作成を実行する
        /// </summary>
        public void R659_Reprt()
        {
            LogUtility.DebugMethodStart();

            if (this.dto != null)
            {
                this.SetRecord(this.dto.DETAIL_DATA_TABLE);

                switch (this.dto.ORDER)
                {
                    case "1":
                        // 取引先CD順
                        this.Create(OUTPUT_FORM_PATH, LAYOUT_TORIHKISAKI, dto.DETAIL_DATA_TABLE);
                        break;
                    case "2":
                        // 業者CD順
                        this.Create(OUTPUT_FORM_PATH, LAYOUT_GYOUSHA, dto.DETAIL_DATA_TABLE);
                        break;
                    case "3":
                        // 運搬業者CD順
                        this.Create(OUTPUT_FORM_PATH, LAYOUT_UNPAN_GYOUSHA, dto.DETAIL_DATA_TABLE);
                        break;
                    case "4":
                        // 運転者CD順
                        this.Create(OUTPUT_FORM_PATH, LAYOUT_UNTENSHA, dto.DETAIL_DATA_TABLE);
                        break;
                    case "5":
                        // 作業日順
                        this.Create(OUTPUT_FORM_PATH, LAYOUT_SAGYOU_DATE, dto.DETAIL_DATA_TABLE);
                        break;
                    case "6":
                        // 受付番号順
                        this.Create(OUTPUT_FORM_PATH, LAYOUT_UKETSUKE_NUMBER, dto.DETAIL_DATA_TABLE);
                        break;
                    default:
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #region UpdateFieldsStatus : ヘッダ部の出力

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            // 会社名
            this.SetFieldName("CORP_NAME_RYAKU", this.dto.CORP_NAME_RYAKU);
            // 拠点名
            this.SetFieldName("KYOTEN_NAME_RYAKU", this.dto.KYOTEN_NAME_RYAKU);
            // 印刷日
            this.SetFieldName("PRINT_DATE", this.dto.PRINT_DATE);
            // 抽出条件 伝票種類
            this.SetFieldName("SEARCH_DENPYOU_KBN", this.dto.SEARCH_DENPYOU_KBN);
            // 抽出条件 日付
            this.SetFieldName("SEARCH_DATE_LABEL", this.dto.SEARCH_DATE_LABEL);
            // 抽出条件 日付範囲
            this.SetFieldName("SEARCH_DATE", this.dto.SEARCH_DATE);
            // 抽出条件 配車状況
            this.SetFieldName("SEARCH_HAISHA_JOUKYOU", this.dto.SEARCH_HAISHA_JOUKYOU);
            // 抽出条件 取引先
            this.SetFieldName("SEARCH_TORIHIKISAKI", this.dto.SEARCH_TORIHIKISAKI);
            // 抽出条件 業者
            this.SetFieldName("SEARCH_GYOUSHA", this.dto.SEARCH_GYOUSHA);
            // 抽出条件 現場
            this.SetFieldName("SEARCH_GENBA", this.dto.SEARCH_GENBA);
            // 抽出条件 運搬業者
            this.SetFieldName("SEARCH_UNPAN_GYOUSHA", this.dto.SEARCH_UNPAN_GYOUSHA);
            // 抽出条件 車種
            this.SetFieldName("SEARCH_SHASHU", this.dto.SEARCH_SHASHU);
            // 抽出条件 車輌
            this.SetFieldName("SEARCH_SHARYO", this.dto.SEARCH_SHARYO);
            // 抽出条件 運転者
            this.SetFieldName("SEARCH_UNTENSHA", this.dto.SEARCH_UNTENSHA);
            // 売上金額合計
            this.SetFieldName("URIAGE_TOTAL_KINGAKU", this.dto.URIAGE_TOTAL_KINGAKU);
            // 支払金額合計
            this.SetFieldName("SHIHARAI_TOTAL_KINGAKU", this.dto.SHIHARAI_TOTAL_KINGAKU);
        }

        #endregion

        #endregion
    }
}
