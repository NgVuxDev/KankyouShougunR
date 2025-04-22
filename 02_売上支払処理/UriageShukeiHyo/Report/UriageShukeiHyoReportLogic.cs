using CommonChouhyouPopup.App;
using r_framework.Utility;
using System;
using System.Data;

namespace Shougun.Core.SalesPayment.UriageShukeiHyo
{
    /// <summary>
    /// 売上集計表帳票出力ロジッククラス
    /// </summary>
    internal class UriageShukeiHyoReportLogic
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal UriageShukeiHyoReportLogic()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <param name="dt">出力するデータ</param>
        /// <param name="dto">売上集計表DTOクラス</param>
        internal void CreateReport(DataTable dt, UriageShukeiHyoDto dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            ReportInfoBase reportInfo = new ReportInfoBase(dt);
            reportInfo.Create(UriageShukeiHyoConst.FORM_FILE, UriageShukeiHyoConst.LAYOUT, dt);
            reportInfo.Title = "売上集計表(" + dto.Pattern.PATTERN_NAME + ")";

            // グループの表示制御
            reportInfo.SetGroupVisible("GROUP1", false, dto.Pattern.GetPatternColumn(1).DETAIL_KBN.Value);
            reportInfo.SetGroupVisible("GROUP2", false, dto.Pattern.GetPatternColumn(2).DETAIL_KBN.Value);
            reportInfo.SetGroupVisible("GROUP3", false, dto.Pattern.GetPatternColumn(3).DETAIL_KBN.Value);
            reportInfo.SetGroupVisible("GROUP4", false, dto.Pattern.GetPatternColumn(4).DETAIL_KBN.Value);

            //VAN 20200330 #134975 S
            //明細項目の表示制御
            //ラベルの表示制御
            reportInfo.SetFieldName("COLUMN_6", string.Empty);
            reportInfo.SetFieldName("COLUMN_7", string.Empty);
            if (dto.Pattern.Pattern.NET_JYUURYOU_DISP_KBN.IsTrue && dto.Pattern.Pattern.SUURYOU_UNIT_DISP_KBN.IsTrue)
            {
                reportInfo.SetFieldName("COLUMN_6", "正味重量");
                reportInfo.SetFieldName("COLUMN_7", "数量/単位");
            }
            else
            {
                if (dto.Pattern.Pattern.NET_JYUURYOU_DISP_KBN.IsTrue)
                {
                    reportInfo.SetFieldName("COLUMN_7", "正味重量");
                }
                else if (dto.Pattern.Pattern.SUURYOU_UNIT_DISP_KBN.IsTrue)
                {
                    reportInfo.SetFieldName("COLUMN_7", "数量/単位");
                }
            }

            //正味重量の項目
            string[] NetFields = new string[] { "FORMAT_NET_JYUURYOU", "FORMAT_GROUP4_NET_JYUURYOU_SUM", "FORMAT_GROUP3_NET_JYUURYOU_SUM", "FORMAT_GROUP2_NET_JYUURYOU_SUM", "FORMAT_GROUP1_NET_JYUURYOU_SUM", "FORMAT_ALL_NET_JYUURYOU_SUM" };
            if (!dto.Pattern.Pattern.NET_JYUURYOU_DISP_KBN.IsTrue)
            {
                //非表示
                foreach (string fieldName in NetFields)
                {
                    reportInfo.SetFieldVisible(fieldName, false);
                }
            }
            else
            {
                //数量/単位を表示しない場合
                if (!dto.Pattern.Pattern.SUURYOU_UNIT_DISP_KBN.IsTrue)
                {
                    //位置を変更
                    foreach (string fieldName in NetFields)
                    {
                        reportInfo.SetFieldLeft(fieldName, 12160);
                    }
                }
            }

            //数量/単位の項目
            string[] SuuUnitFields = new string[] { "FORMAT_SUURYOU", "UNIT_NAME" };
            if (!dto.Pattern.Pattern.SUURYOU_UNIT_DISP_KBN.IsTrue)
            {
                //非表示
                foreach (string fieldName in SuuUnitFields)
                {
                    reportInfo.SetFieldVisible(fieldName, false);
                }
            }
            //VAN 20200330 #134975 E

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = dto.Pattern.PATTERN_NAME;
            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }
    }
}
