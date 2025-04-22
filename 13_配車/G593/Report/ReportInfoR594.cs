using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;

namespace Shougun.Core.Allocation.KaraContenaIchiranHyou.Report
{
    public class ReportInfoR594 : ReportInfoBase
    {
        #region - Fields -
        /// <summary>帳票フルパス名</summary>
        const string OutputFormFullPathName = "./Template/R594-Form.xml";

        /// <summary>レイアウト名</summary>
        const string LayoutName = "LAYOUT1";

        /// <summary>Header出力用DataTable</summary>
        DataTable headerData = new DataTable();

        /// <summary>Detail出力用DataTable</summary>
        DataTable detailData = new DataTable();

        /// <summary>レイアウト名</summary>
        string layoutName;
        #endregion

        #region - Methods -

        #region constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerData">Header表示用DataTable</param>
        /// <param name="detailData">Detail表示用DataTable</param>
        public ReportInfoR594(DataTable headerData, DataTable detailData, string layoutName)
        {
            this.headerData = headerData;
            this.detailData = detailData;
            this.layoutName = layoutName;
        }
        #endregion

        /// <summary>
        /// C1Reportの帳票データの作成を実行します
        /// </summary>
        public void R594_Reprt()
        {
            // 明細データTABLEをセット
            this.SetRecord(this.detailData);

            this.Create(OutputFormFullPathName, this.layoutName, detailData);
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            /* Header情報設定 */
            if (this.headerData.Rows.Count > 0)
            {
                DataRow row = this.headerData.Rows[0];
                // 自社名
                this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", row["FH_CORP_RYAKU_NAME_VLB"].ToString());
                // 発行日
                this.SetFieldName("FH_PRINT_DATE_VLB", row["FH_PRINT_DATE_VLB"].ToString() + "　発行");

                // 検索コンテナ種類CD From
                this.SetFieldName("FH_CONTENA_SHURUI_CD_START_CTL", row["FH_CONTENA_SHURUI_CD_START_CTL"].ToString());
                // 検索コンテナ種類名 From
                this.SetFieldName("FH_CONTENA_SHURUI_NAME_START_CTL", row["FH_CONTENA_SHURUI_NAME_START_CTL"].ToString());
                // 検索コンテナ種類CD To
                this.SetFieldName("FH_CONTENA_SHURUI_CD_END_CTL", row["FH_CONTENA_SHURUI_CD_END_CTL"].ToString());
                // 検索コンテナ種類名 From
                this.SetFieldName("FH_CONTENA_SHURUI_NAME_END_CTL", row["FH_CONTENA_SHURUI_NAME_END_CTL"].ToString());
            }
        }
        #endregion
    }
}