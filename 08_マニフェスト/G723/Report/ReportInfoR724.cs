using CommonChouhyouPopup.App;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Shougun.Core.PaperManifest.ManifestSuiihyoData
{
    /// <summary>
    /// ReportInfoBase(マニフェスト推移表)
    /// </summary>
    public class ReportInfoR724 : ReportInfoBase
    {
        #region Const

        /// <summary>帳票テンプレート - ファイルパス</summary>
        const string FORM_FULL_PATH_NAME = "./Template/R724-Form.xml";

        /// <summary>帳票テンプレート - レイアウト名1(表示項目数2以下)</summary>
        const string LAYOUT_NAME1 = "LAYOUT1";
        /// <summary>帳票テンプレート - レイアウト名2((表示項目数3以上)</summary>
        const string LAYOUT_NAME2 = "LAYOUT2";


        #endregion

        #region Field

        /// <summary>Header出力用DataTable</summary>
        private DataTable headerTable;

        /// <summary>Detail出力用DataTable</summary>
        private DataTable detailTable;

        /// <summary>レイアウト</summary>
        private string layOut;

        /// <summary>マニフェスト数量書式</summary>
        private string format;

        #endregion

        #region Constructer

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerTable">Header出力用DataTable</param>
        /// <param name="detailTable">Detail出力用DataTable</param>
        /// <param name="format">フォーマット</param>
        public ReportInfoR724(DataTable headerTable, DataTable detailTable, string format)
        {
            this.headerTable = headerTable;
            this.detailTable = detailTable;
            this.format = format;
        }

        #endregion

        #region Method

        /// <summary>
        /// レポートデータ作成を実行します
        /// <param name="LayOut">レイアウト</param>
        /// </summary>
        public void CreateR724(string LayOut)
        {
            this.layOut = LayOut;
            // 実行
            this.SetRecord(this.detailTable);

            this.Create(FORM_FULL_PATH_NAME, LayOut, this.detailTable);
        }

        /// <summary>
        /// フィールド状態の更新処理を実行します
        /// </summary>
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
            if (this.headerTable.Rows[0]["COLUMN_1"] != null)
            {
                this.SetFieldName("COLUMN_1", this.headerTable.Rows[0]["COLUMN_1"].ToString());
            }
            else
            {
                this.SetFieldName("COLUMN_1", string.Empty);
            }

            // 項目2名
            if (this.headerTable.Rows[0]["COLUMN_2"] != null)
            {
                this.SetFieldName("COLUMN_2", this.headerTable.Rows[0]["COLUMN_2"].ToString());
            }
            else
            {
                this.SetFieldName("COLUMN_2", string.Empty);
            }

            if (this.layOut == LAYOUT_NAME2)
            {
                // 項目3名
                if (this.headerTable.Rows[0]["COLUMN_3"] != null)
                {
                    this.SetFieldName("COLUMN_3", this.headerTable.Rows[0]["COLUMN_3"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_3", string.Empty);
                }

                // 項目4名
                if (this.headerTable.Rows[0]["COLUMN_4"] != null)
                {
                    this.SetFieldName("COLUMN_4", this.headerTable.Rows[0]["COLUMN_4"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_4", string.Empty);
                }
            }

            // 年月1名
            if (this.headerTable.Rows[0]["COL_1"] != null)
            {
                this.SetFieldName("COL_1", this.headerTable.Rows[0]["COL_1"].ToString());
            }
            else
            {
                this.SetFieldName("COL_1", string.Empty);
            }

            // 年月2名
            if (this.headerTable.Rows[0]["COL_2"] != null)
            {
                this.SetFieldName("COL_2", this.headerTable.Rows[0]["COL_2"].ToString());
            }
            else
            {
                this.SetFieldName("COL_2", string.Empty);
            }

            // 年月3名
            if (this.headerTable.Rows[0]["COL_3"] != null)
            {
                this.SetFieldName("COL_3", this.headerTable.Rows[0]["COL_3"].ToString());
            }
            else
            {
                this.SetFieldName("COL_3", string.Empty);
            }

            // 年月4名
            if (this.headerTable.Rows[0]["COL_4"] != null)
            {
                this.SetFieldName("COL_4", this.headerTable.Rows[0]["COL_4"].ToString());
            }
            else
            {
                this.SetFieldName("COL_4", string.Empty);
            }

            // 年月5名
            if (this.headerTable.Rows[0]["COL_5"] != null)
            {
                this.SetFieldName("COL_5", this.headerTable.Rows[0]["COL_5"].ToString());
            }
            else
            {
                this.SetFieldName("COL_5", string.Empty);
            }

            // 年月6名
            if (this.headerTable.Rows[0]["COL_6"] != null)
            {
                this.SetFieldName("COL_6", this.headerTable.Rows[0]["COL_6"].ToString());
            }
            else
            {
                this.SetFieldName("COL_6", string.Empty);
            }

            // 年月7名
            if (this.headerTable.Rows[0]["COL_7"] != null)
            {
                this.SetFieldName("COL_7", this.headerTable.Rows[0]["COL_7"].ToString());
            }
            else
            {
                this.SetFieldName("COL_7", string.Empty);
            }

            // 年月8名
            if (this.headerTable.Rows[0]["COL_8"] != null)
            {
                this.SetFieldName("COL_8", this.headerTable.Rows[0]["COL_8"].ToString());
            }
            else
            {
                this.SetFieldName("COL_8", string.Empty);
            }

            // 年月9名
            if (this.headerTable.Rows[0]["COL_9"] != null)
            {
                this.SetFieldName("COL_9", this.headerTable.Rows[0]["COL_9"].ToString());
            }
            else
            {
                this.SetFieldName("COL_9", string.Empty);
            }

            // 年月10名
            if (this.headerTable.Rows[0]["COL_10"] != null)
            {
                this.SetFieldName("COL_10", this.headerTable.Rows[0]["COL_10"].ToString());
            }
            else
            {
                this.SetFieldName("COL_10", string.Empty);
            }

            // 年月11名
            if (this.headerTable.Rows[0]["COL_11"] != null)
            {
                this.SetFieldName("COL_11", this.headerTable.Rows[0]["COL_11"].ToString());
            }
            else
            {
                this.SetFieldName("COL_11", string.Empty);
            }

            // 年月12名
            if (this.headerTable.Rows[0]["COL_12"] != null)
            {
                this.SetFieldName("COL_12", this.headerTable.Rows[0]["COL_12"].ToString());
            }
            else
            {
                this.SetFieldName("COL_12", string.Empty);
            }

            // 合計
            if (this.headerTable.Rows[0]["COL_TOTAL"] != null)
            {
                this.SetFieldName("COL_TOTAL", this.headerTable.Rows[0]["COL_TOTAL"].ToString());
            }
            else
            {
                this.SetFieldName("COL_TOTAL", string.Empty);
            }

            // 全合計
            if (this.headerTable.Rows[0]["TOTAL"] != null)
            {
                this.SetFieldName("TOTAL", this.headerTable.Rows[0]["TOTAL"].ToString());
            }
            else
            {
                this.SetFieldName("TOTAL", string.Empty);
            }

            // 各月の合計
            for (int i = 0; i < 12; i++)
            {
                string colName = "COL_" + (i + 1);
                string totalColName = "TOTAL_" + (i + 1);
                if (!string.IsNullOrEmpty(this.headerTable.Rows[0][colName].ToString()))
                {
                    this.SetFieldFormat(totalColName, this.format);
                }
                else
                {
                    this.SetFieldFormat(totalColName, string.Empty);
                }
            }

            // 全ての月の合計
            this.SetFieldFormat("ALL_VAL_TOTAL", this.format);

            this.SetFieldName("ALL_VAL_AVG", string.Empty);
            #endregion
        }

        #endregion
    }
}
