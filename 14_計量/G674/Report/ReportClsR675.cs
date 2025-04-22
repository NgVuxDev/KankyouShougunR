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
    /// ○○表作成クラス（伝票日付順）
    /// </summary>
    internal class ReportClsR675
    {
        /// <summary>
        ///
        /// </summary>
        private UIForm form;

        private string strFormat;
        private string jyuryouFormat;
        private string suuryoFormat;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal ReportClsR675(UIForm targetForm)
        {
            LogUtility.DebugMethodStart();
            this.form = targetForm;
            this.strFormat = "#,##0";
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

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouTable);
            reportInfo.Create(ConstCls.R675OutputFormFullPathName, ConstCls.LAYOUT_1, chouhyouTable);
            reportInfo.Title = ConstCls.KEIRYOU_MEISAIHYOU_TITLE;

            // 帳票の表示制御
            this.UpdateReportFields(reportInfo, dto);

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = String.Empty;
            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        private void UpdateReportFields(ReportInfoBase reportInfo, DTOCls dto)
        {
            // 帳票タイトル
            reportInfo.SetFieldName("TITLE", ConstCls.KEIRYOU_MEISAIHYOU_TITLE);

            // 出力日付の書式設定
            reportInfo.SetFieldName("PRINT_DATE", ReportUtil.GetSysDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行");

            // 自社略称
            reportInfo.SetFieldName("CORP_NAME_RYAKU", this.form.CommInfo.CorpInfo.CORP_RYAKU_NAME);

            // 拠点名称
            reportInfo.SetFieldName("KYOTEN_NAME_RYAKU", dto.KyotenName);

            // 伝票日付
            if (dto.DateShuruiCd == 1)
            {
                reportInfo.SetFieldName("SEARCH_DATE_LABEL", ConstCls.DATE_SHURUI_DENPYOU);
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_DATE_LABEL", ConstCls.DATE_SHURUI_INPUT);
            }
            // 伝票範囲
            if (dto.DateHani == 1)
            {
                reportInfo.SetFieldName("SEARCH_DATE", "当日");
            }
            else if (dto.DateHani == 2)
            {
                reportInfo.SetFieldName("SEARCH_DATE", "当月");
            }
            else
            {
                string strDate = dto.DateFrom + "　～　" + dto.DateTo;
                reportInfo.SetFieldName("SEARCH_DATE", strDate);
            }

            // 計量区分
            if (dto.KeiryouKbn == 1)
            {
                reportInfo.SetFieldName("SEARCH_KEIRYOU_KBN", ConstCls.KEIRYOU_KBN_1);
            }
            else if (dto.KeiryouKbn == 2)
            {
                reportInfo.SetFieldName("SEARCH_KEIRYOU_KBN", ConstCls.KEIRYOU_KBN_2);
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_KEIRYOU_KBN", ConstCls.ALL);
            }

            // 運搬業者
            if (string.IsNullOrEmpty(dto.UpnGyoushaCdFrom) && string.IsNullOrEmpty(dto.UpnGyoushaCdTo))
            {
                reportInfo.SetFieldName("SEARCH_UNPAN_GYOUSHA_FROM", ConstCls.ALL);
                reportInfo.SetFieldName("SEARCH_UNPAN_GYOUSHA_TO", "");
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_UNPAN_GYOUSHA_FROM", dto.UpnGyoushaCdFrom + "　" + dto.UpnGyoushaFrom + "　～");
                reportInfo.SetFieldName("SEARCH_UNPAN_GYOUSHA_TO", dto.UpnGyoushaCdTo + "　" + dto.UpnGyoushaTo);
            }

            // 形態区分
            if (string.IsNullOrEmpty(dto.KeitaiKbnCdFrom) && string.IsNullOrEmpty(dto.KeitaiKbnCdTo))
            {
                reportInfo.SetFieldName("SEARCH_KEITAI_KBN_FROM", ConstCls.ALL);
                reportInfo.SetFieldName("SEARCH_KEITAI_KBN_TO", "");
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_KEITAI_KBN_FROM", dto.KeitaiKbnCdFrom + "　" + dto.KeitaiKbnFrom + "　～");
                reportInfo.SetFieldName("SEARCH_KEITAI_KBN_TO", dto.KeitaiKbnCdTo + "　" + dto.KeitaiKbnTo);
            }

            // 取引先
            if (string.IsNullOrEmpty(dto.TorihikisakiCdFrom) && string.IsNullOrEmpty(dto.TorihikisakiCdTo))
            {
                reportInfo.SetFieldName("SEARCH_TORIHIKISAKI_FROM", ConstCls.ALL);
                reportInfo.SetFieldName("SEARCH_TORIHIKISAKI_TO", "");
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_TORIHIKISAKI_FROM", dto.TorihikisakiCdFrom + "　" + dto.TorihikisakiFrom + "　～");
                reportInfo.SetFieldName("SEARCH_TORIHIKISAKI_TO", dto.TorihikisakiCdTo + "　" + dto.TorihikisakiTo);
            }

            // 業者
            if (string.IsNullOrEmpty(dto.GyoushaCdFrom) && string.IsNullOrEmpty(dto.GyoushaCdTo))
            {
                reportInfo.SetFieldName("SEARCH_GYOUSHA_FROM", ConstCls.ALL);
                reportInfo.SetFieldName("SEARCH_GYOUSHA_TO", "");
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_GYOUSHA_FROM", dto.GyoushaCdFrom + "　" + dto.GyoushaFrom + "　～");
                reportInfo.SetFieldName("SEARCH_GYOUSHA_TO", dto.GyoushaCdTo + "　" + dto.GyoushaTo);
            }

            // 現場
            if (string.IsNullOrEmpty(dto.GenbaCdFrom) && string.IsNullOrEmpty(dto.GenbaCdTo))
            {
                reportInfo.SetFieldName("SEARCH_GENBA_FROM", ConstCls.ALL);
                reportInfo.SetFieldName("SEARCH_GENBA_TO", "");
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_GENBA_FROM", dto.GenbaCdFrom + "　" + dto.GenbaFrom + "　～");
                reportInfo.SetFieldName("SEARCH_GENBA_TO", dto.GenbaCdTo + "　" + dto.GenbaTo);
            }

            // 品名
            if (string.IsNullOrEmpty(dto.HinmeiCdFrom) && string.IsNullOrEmpty(dto.HinmeiCdTo))
            {
                reportInfo.SetFieldName("SEARCH_HINMEI_FROM", ConstCls.ALL);
                reportInfo.SetFieldName("SEARCH_HINMEI_TO", "");
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_HINMEI_FROM", dto.HinmeiCdFrom + "　" + dto.HinmeiFrom + "　～");
                reportInfo.SetFieldName("SEARCH_HINMEI_TO", dto.HinmeiCdTo + "　" + dto.HinmeiTo);
            }

            // 種類
            if (string.IsNullOrEmpty(dto.ShuruiCdFrom) && string.IsNullOrEmpty(dto.ShuruiCdTo))
            {
                reportInfo.SetFieldName("SEARCH_SHURUI_FROM", ConstCls.ALL);
                reportInfo.SetFieldName("SEARCH_SHURUI_TO", "");
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_SHURUI_FROM", dto.ShuruiCdFrom + "　" + dto.ShuruiFrom + "　～");
                reportInfo.SetFieldName("SEARCH_SHURUI_TO", dto.ShuruiCdTo + "　" + dto.ShuruiTo);
            }

            // 分類
            if (string.IsNullOrEmpty(dto.BunruiCdFrom) && string.IsNullOrEmpty(dto.BunruiCdTo))
            {
                reportInfo.SetFieldName("SEARCH_BUNRUI_FROM", ConstCls.ALL);
                reportInfo.SetFieldName("SEARCH_BUNRUI_TO", "");
            }
            else
            {
                reportInfo.SetFieldName("SEARCH_BUNRUI_FROM", dto.BunruiCdFrom + "　" + dto.BunruiFrom + "　～");
                reportInfo.SetFieldName("SEARCH_BUNRUI_TO", dto.BunruiCdTo + "　" + dto.BunruiTo);
            }
            // グループの表示制御
            // 伝票グループヘッダの表示、非表示制御
            reportInfo.SetGroupVisible("GROUP1", true, dto.IsGroupDenpyouNumber);

            // 現場グループヘッダの表示、非表示制御
            reportInfo.SetGroupVisible("GROUP2", false, dto.IsGroupGenba);

            // 業者グループヘッダの表示、非表示制御
            reportInfo.SetGroupVisible("GROUP3", false, dto.IsGroupGyousha);

            // 取引先グループヘッダの表示、非表示制御
            reportInfo.SetGroupVisible("GROUP4", false, dto.IsGroupTorihikisaki);

            reportInfo.SetFieldFormat("NET_JYUURYOU", jyuryouFormat);
            reportInfo.SetFieldFormat("NET_JYUURYOU_DENPYOU", jyuryouFormat);
            reportInfo.SetFieldFormat("NET_JYUURYOU_GENBA", jyuryouFormat);
            reportInfo.SetFieldFormat("NET_JYUURYOU_GYOUSHA", jyuryouFormat);
            reportInfo.SetFieldFormat("NET_JYUURYOU_TORIHIKISAKI", jyuryouFormat);
            reportInfo.SetFieldFormat("NET_JYUURYOU_TOTAL", jyuryouFormat);
            reportInfo.SetFieldFormat("KINGAKU", strFormat);
            reportInfo.SetFieldFormat("KINGAKU_DENPYOU", strFormat);
            reportInfo.SetFieldFormat("KINGAKU_GENBA", strFormat);
            reportInfo.SetFieldFormat("KINGAKU_GYOUSHA", strFormat);
            reportInfo.SetFieldFormat("KINGAKU_TORIHIKISAKI", strFormat);
            reportInfo.SetFieldFormat("KINGAKU_TOTAL", strFormat);
            reportInfo.SetFieldFormat("TAX", strFormat);
            reportInfo.SetFieldFormat("TAX_DENPYOU", strFormat);
            reportInfo.SetFieldFormat("TAX_GENBA", strFormat);
            reportInfo.SetFieldFormat("TAX_GYOUSHA", strFormat);
            reportInfo.SetFieldFormat("TAX_TORIHIKISAKI", strFormat);
            reportInfo.SetFieldFormat("TAX_TOTAL", strFormat);
            reportInfo.SetFieldFormat("SUM_KINGAKU", strFormat);
            reportInfo.SetFieldFormat("SUM_KINGAKU_DENPYOU", strFormat);
            reportInfo.SetFieldFormat("SUM_KINGAKU_GENBA", strFormat);
            reportInfo.SetFieldFormat("SUM_KINGAKU_GYOUSHA", strFormat);
            reportInfo.SetFieldFormat("SUM_KINGAKU_TORIHIKISAKI", strFormat);
            reportInfo.SetFieldFormat("SUM_KINGAKU_TOTAL", strFormat);
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

            // 正味
            dt.Columns.Add("NET_JYUURYOU_FORMATED");
            // 数量
            dt.Columns.Add("SUURYOU_FORMATED");
            // 金額
            dt.Columns.Add("KINGAKU_FORMATED");
            // 消費税
            dt.Columns.Add("TAX_FORMATED");
            // 合計金額
            dt.Columns.Add("SUM_KINGAKU_FORMATED");

            // データ加工
            foreach (DataRow row in ret.Rows)
            {
                if (!string.IsNullOrEmpty(row["NET_JYUURYOU"].ToString()))
                {
                    row["NET_JYUURYOU_FORMATED"] = Convert.ToDecimal(row["NET_JYUURYOU"]).ToString(jyuryouFormat);
                }
                if (!string.IsNullOrEmpty(row["SUURYOU"].ToString()))
                {
                    row["SUURYOU_FORMATED"] = Convert.ToDecimal(row["SUURYOU"]).ToString(suuryoFormat);
                }
                if (!string.IsNullOrEmpty(row["KINGAKU"].ToString()))
                {
                    row["KINGAKU_FORMATED"] = Convert.ToDecimal(row["KINGAKU"]).ToString(strFormat);
                }
                if (!string.IsNullOrEmpty(row["TAX"].ToString()))
                {
                    row["TAX_FORMATED"] = Convert.ToDecimal(row["TAX"]).ToString(strFormat);
                }
                if (!string.IsNullOrEmpty(row["SUM_KINGAKU"].ToString()))
                {
                    row["SUM_KINGAKU_FORMATED"] = Convert.ToDecimal(row["SUM_KINGAKU"]).ToString(strFormat);
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
    }
}