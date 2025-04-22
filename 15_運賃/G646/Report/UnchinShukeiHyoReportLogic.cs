using CommonChouhyouPopup.App;
using r_framework.Utility;
using System;
using System.Data;

namespace Shougun.Core.Carriage.UnchinShukeiHyo
{
    /// <summary>
    /// 運賃集計表帳票出力ロジッククラス
    /// </summary>
    internal class UnchinShukeiHyoReportLogic
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal UnchinShukeiHyoReportLogic()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <param name="dt">出力するデータ</param>
        /// <param name="dto">運賃集計表DTOクラス</param>
        internal void CreateReport(DataTable dt, UnchinShukeiHyoDto dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            ReportInfoBase reportInfo = new ReportInfoBase(dt);
            reportInfo.Create(UnchinShukeiHyoConst.FORM_FILE, UnchinShukeiHyoConst.LAYOUT, dt);
//            reportInfo.Title = dto.Pattern.PATTERN_NAME;
            reportInfo.Title = "運賃集計表（" + dto.Pattern.PATTERN_NAME + "）";
            
            // グループの表示制御
            reportInfo.SetGroupVisible("GROUP1", false, dto.Pattern.GetPatternColumn(1).DETAIL_KBN.Value);
            reportInfo.SetGroupVisible("GROUP2", false, dto.Pattern.GetPatternColumn(2).DETAIL_KBN.Value);
            reportInfo.SetGroupVisible("GROUP3", false, dto.Pattern.GetPatternColumn(3).DETAIL_KBN.Value);
            reportInfo.SetGroupVisible("GROUP4", false, dto.Pattern.GetPatternColumn(4).DETAIL_KBN.Value);

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = dto.Pattern.PATTERN_NAME;

            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }
    }
}
