using CommonChouhyouPopup.App;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Shougun.Core.ReceiptPayManagement.ShukkinShuukeiChouhyou
{
    /// <summary>
    /// ReportInfoBase(出金集計表)
    /// </summary>
    public class ReportInfoR630 : ReportInfoBase
    {
        #region Const

        /// <summary>帳票テンプレート - ファイルパス</summary>
        const string FORM_FULL_PATH_NAME = "./Template/R630-Form.xml";

        /// <summary>帳票テンプレート - レイアウト名</summary>
        const string LAYOUT_NAME = "LAYOUT1";

        #endregion

        #region Field

        /// <summary>Header出力用DataTable</summary>
        private DataTable headerTable;

        /// <summary>Detail出力用DataTable</summary>
        private DataTable detailTable;

        #endregion

        #region Constructer

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerTable">Header出力用DataTable</param>
        /// <param name="detailTable">Detail出力用DataTable</param>
        public ReportInfoR630(DataTable headerTable, DataTable detailTable)
        {
            this.headerTable = headerTable;
            this.detailTable = detailTable;
        }

        #endregion

        #region Method

        /// <summary>
        /// レポートデータ作成を実行します
        /// </summary>
        public void CreateR630()
        {
            // 実行
            this.SetRecord(this.detailTable);
            this.Create(FORM_FULL_PATH_NAME, LAYOUT_NAME, this.detailTable);
        }

        /// <summary> フィールド状態の更新処理を実行します </summary>
        protected override void UpdateFieldsStatus()
        {
            #region Header

            // タイトル
            if (this.headerTable.Rows[0]["TITLE"] != null)
            {
                this.SetFieldName("TITLE", this.headerTable.Rows[0]["TITLE"].ToString());
            }
            else
            {
                this.SetFieldName("TITLE", string.Empty);
            }

            // 自社名
            if (this.headerTable.Rows[0]["JISHA"] != null)
            {
                this.SetFieldName("JISHA", this.headerTable.Rows[0]["JISHA"].ToString());
            }
            else
            {
                this.SetFieldName("JISHA", string.Empty);
            }

            // 拠点
            if (this.headerTable.Rows[0]["KYOTEN"] != null)
            {
                this.SetFieldName("KYOTEN", this.headerTable.Rows[0]["KYOTEN"].ToString());
            }
            else
            {
                this.SetFieldName("KYOTEN", string.Empty);
            }

            // 発行日付
            if (this.headerTable.Rows[0]["HAKKOU_DATE"] != null)
            {
                this.SetFieldName("HAKKOU_DATE", this.headerTable.Rows[0]["HAKKOU_DATE"].ToString());
            }
            else
            {
                this.SetFieldName("HAKKOU_DATE", string.Empty);
            }

            // 条件1
            if (this.headerTable.Rows[0]["JOUKEN_1"] != null)
            {
                this.SetFieldName("JOUKEN_1", this.headerTable.Rows[0]["JOUKEN_1"].ToString());
            }
            else
            {
                this.SetFieldName("JOUKEN_1", string.Empty);
            }

            // 条件2
            if (this.headerTable.Rows[0]["JOUKEN_2"] != null)
            {
                this.SetFieldName("JOUKEN_2", this.headerTable.Rows[0]["JOUKEN_2"].ToString());
            }
            else
            {
                this.SetFieldName("JOUKEN_2", string.Empty);
            }

            #endregion

            #region PageHeader

            // 項目1名
            if (this.headerTable.Rows[0]["COL_1"] != null)
            {
                this.SetFieldName("COL_1", this.headerTable.Rows[0]["COL_1"].ToString());
            }
            else
            {
                this.SetFieldName("COL_1", string.Empty);
            }

            // 項目2名
            if (this.headerTable.Rows[0]["COL_2"] != null)
            {
                this.SetFieldName("COL_2", this.headerTable.Rows[0]["COL_2"].ToString());
            }
            else
            {
                this.SetFieldName("COL_2", string.Empty);
            }

            #endregion

            #region Group Visible

            if (this.detailTable.Rows[0]["GROUP1_NAME"] == null || string.IsNullOrEmpty(this.detailTable.Rows[0]["GROUP1_NAME"].ToString()))
            {
                this.SetGroupVisible("GROUP1", false, false);
            }

            if (this.detailTable.Rows[0]["GROUP2_NAME"] == null || string.IsNullOrEmpty(this.detailTable.Rows[0]["GROUP2_NAME"].ToString()))
            {
                this.SetGroupVisible("GROUP2", false, false);
            }

            #endregion
        }

        #endregion
    }
}
