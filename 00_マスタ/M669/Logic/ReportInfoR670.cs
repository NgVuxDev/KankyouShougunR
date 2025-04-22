using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.Master.ContenaQrHakkou
{
    #region - Class -

    /// <summary>(R670)QRコード印刷を表すクラス・コントロール</summary>
    public class ReportInfoR670 : ReportInfoBase
    {

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <param name="dt">出力するデータ</param>
        internal void CreateReport(DataTable dt, string LAYOUT)
        {
            LogUtility.DebugMethodStart(dt, LAYOUT);

            ReportInfoBase reportInfo = new ReportInfoBase(dt);
            reportInfo.Create(Const.Constans.FORM_FILE, LAYOUT, dt);
            reportInfo.Title = Const.Constans.ReportInfoTitle;

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = Const.Constans.ReportInfoTitle;
            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }
    }

    #endregion - Class -
}
