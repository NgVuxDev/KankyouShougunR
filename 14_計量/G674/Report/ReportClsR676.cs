using System;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Scale.KeiryouHoukoku.APP;
using Shougun.Core.Scale.KeiryouHoukoku.Const;
using Shougun.Core.Scale.KeiryouHoukoku.DTO;

namespace Shougun.Core.Scale.KeiryouHoukoku.Report
{
    /// <summary>
    /// ○○表作成クラス（伝票番号順）
    /// </summary>
    internal class ReportClsR676
    {
        /// <summary>
        ///
        /// </summary>
        private UIForm form;

        private string jyuryouFormat;
        private string suuryoFormat;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal ReportClsR676(UIForm targetForm)
        {
            LogUtility.DebugMethodStart();
            this.form = targetForm;
            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
            this.jyuryouFormat = mSysInfo.SYS_JYURYOU_FORMAT.ToString();
            this.suuryoFormat = mSysInfo.SYS_SUURYOU_FORMAT.ToString();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <param name="dt">出力データ</param>
        /// <param name="dto">画面入力データ</param>
        internal void CreateReport(DataTable dt, DTOCls dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            var chouhyouTable = this.EditChouhyouTable(dt, dto);

            string LAYOUT = string.Empty;
            if (1 == dto.GroupTani)
            {
                LAYOUT = ConstCls.LAYOUT_1;
            }
            else if (2 == dto.GroupTani)
            {
                LAYOUT = ConstCls.LAYOUT_2;
            }
            else if (3 == dto.GroupTani)
            {
                LAYOUT = ConstCls.LAYOUT_3;
            }

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouTable);
            reportInfo.Create(ConstCls.R676OutputFormFullPathName, LAYOUT, chouhyouTable);
            reportInfo.Title = ConstCls.KEIRYOU_MOTOCHO_TITLE;

            // 帳票の表示制御
            this.UpdateReportFields(reportInfo, dto);

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = String.Empty;
            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票に渡すデータを加工します
        /// </summary>
        /// <param name="dt">元となるデータ</param>
        /// <param name="dto">画面入力データ</param>
        /// <returns>帳票用に作成したデータ</returns>
        private DataTable EditChouhyouTable(DataTable dt, DTOCls dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            DataTable ret = dt;

            // DBで取得できない項目のカラムを追加

            // 帳票タイトル
            dt.Columns.Add("FH_TITLE_VLB");
            // 自社名称
            dt.Columns.Add("FH_CORP_NAME_RYAKU_VLB");
            // 拠点名称
            dt.Columns.Add("FH_KYOTEN_NAME_RYAKU_VLB");
            // 出力日時
            dt.Columns.Add("FH_PRINT_DATE_VLB");
            // 伝票日付名
            dt.Columns.Add("FH_DATE_NAME");
            // 伝票日付
            dt.Columns.Add("FH_DENPYOU_DATE");
            // 総重量　－　空車重量
            dt.Columns.Add("SE_JYUURYOU_FORMATED");
            // 調整
            dt.Columns.Add("CHOUSEI_JYUURYOU_FORMATED");
            // 正味重量
            dt.Columns.Add("NET_JYUURYOU_FORMATED");
            // 数量
            dt.Columns.Add("SUURYOU_FORMATED");

            // データ加工
            foreach (DataRow row in ret.Rows)
            {
                // タイトルと項目名の設定
                if (1 == dto.GroupTani)
                {
                    row["FH_TITLE_VLB"] = ConstCls.KEIRYOU_MOTOCHO_TITLE + ConstCls.SORT_TORIHIKISAKI_SUB_TITLE;
                }
                else if (2 == dto.GroupTani)
                {
                    row["FH_TITLE_VLB"] = ConstCls.KEIRYOU_MOTOCHO_TITLE + ConstCls.SORT_GYOUSHA_SUB_TITLE;
                }
                else if (3 == dto.GroupTani)
                {
                    row["FH_TITLE_VLB"] = ConstCls.KEIRYOU_MOTOCHO_TITLE + ConstCls.SORT_GENBA_SUB_TITLE;
                }

                // 出力日付の書式設定
                row["FH_PRINT_DATE_VLB"] = ReportUtil.GetSysDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行";

                // 自社略称
                row["FH_CORP_NAME_RYAKU_VLB"] = this.form.CommInfo.CorpInfo.CORP_RYAKU_NAME;

                // 拠点名称
                row["FH_KYOTEN_NAME_RYAKU_VLB"] = dto.KyotenName;

                // 伝票日付
                if (dto.DateShuruiCd == 1)
                {
                    row["FH_DATE_NAME"] = ConstCls.DATE_SHURUI_DENPYOU;
                }
                else
                {
                    row["FH_DATE_NAME"] = ConstCls.DATE_SHURUI_INPUT;
                }
                // 伝票範囲
                if (dto.DateHani == 1)
                {
                    row["FH_DENPYOU_DATE"] = "当日";
                }
                else if (dto.DateHani == 2)
                {
                    row["FH_DENPYOU_DATE"] = "当月";
                }
                else
                {
                    string strDate = dto.DateFrom + "　～　" + dto.DateTo;
                    row["FH_DENPYOU_DATE"] = strDate;
                }
                if (1 != dto.GroupTani)
                {
                    if (!string.IsNullOrEmpty(row["SE_JYUURYOU"].ToString()))
                    {
                        row["SE_JYUURYOU_FORMATED"] = Convert.ToDecimal(row["SE_JYUURYOU"]).ToString(jyuryouFormat);
                    }
                    if (!string.IsNullOrEmpty(row["CHOUSEI_JYUURYOU"].ToString()))
                    {
                        row["CHOUSEI_JYUURYOU_FORMATED"] = Convert.ToDecimal(row["CHOUSEI_JYUURYOU"]).ToString(jyuryouFormat);
                    }
                }
                if (!string.IsNullOrEmpty(row["NET_JYUURYOU"].ToString()))
                {
                    row["NET_JYUURYOU_FORMATED"] = Convert.ToDecimal(row["NET_JYUURYOU"]).ToString(jyuryouFormat);
                }
                if (!string.IsNullOrEmpty(row["SUURYOU"].ToString()))
                {
                    row["SUURYOU_FORMATED"] = Convert.ToDecimal(row["SUURYOU"]).ToString(suuryoFormat);
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        private void UpdateReportFields(ReportInfoBase reportInfo, DTOCls dto)
        {
            if (1 == dto.GroupTani)
            {
                reportInfo.SetFieldFormat("NET_JYUURYOU_TORIHIKISAKI_CTL", jyuryouFormat);
            }
            else if (2 == dto.GroupTani )
            {
                reportInfo.SetFieldFormat("SE_JYUURYOU_TOTAL_CTL", jyuryouFormat);
                reportInfo.SetFieldFormat("CHOUSEI_JYUURYOU_TOTAL_CTL", jyuryouFormat);
                reportInfo.SetFieldFormat("NET_JYUURYOU_TOTOL_CTL", jyuryouFormat);
            }
            else if (3 == dto.GroupTani)
            {
                reportInfo.SetFieldFormat("SE_JYUURYOU_TOTAL_CTL", jyuryouFormat);
                reportInfo.SetFieldFormat("CHOUSEI_JYUURYOU_TOTAL_CTL", jyuryouFormat);
                reportInfo.SetFieldFormat("NET_JYUURYOU_TOTOL_CTL", jyuryouFormat);
            }
        }
    }
}