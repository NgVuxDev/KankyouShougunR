using System;
using System.Data;
using r_framework.Dto;
using CommonChouhyouPopup.App;

namespace Shougun.Core.PaperManifest.ManifestShukeihyo
{
    /// <summary>
    /// マニフェスト明細表帳票作成クラス
    /// </summary>
    public class ReportInfoR479 : ReportInfoBase
    {
        /// <summary>
        /// 帳票テンプレートのパス
        /// </summary>
        private static readonly string FORM_FULL_PATH_NAME = "./Template/R479-Form.xml";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        private static readonly string LAYOUT_NAME = "LAYOUT1";

        /// <summary>Header出力用DataTable</summary>
        private DataTable headerTable;

        /// <summary>Detail出力用DataTable</summary>
        private DataTable detailTable;

        /// <summary>マニフェスト集計表Dto</summary>
        private ManifestShukeihyoDto manifestShukeihyoDto;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerTable">Header出力用DataTable</param>
        /// <param name="detailTable">Detail出力用DataTable</param>
        /// <param name="manifestShukeihyoDto">マニフェスト集計表Dto</param>
        public ReportInfoR479(DataTable headerTable, DataTable detailTable, ManifestShukeihyoDto manifestShukeihyoDto)
        {
            this.headerTable = headerTable;
            this.detailTable = detailTable;
            this.manifestShukeihyoDto = manifestShukeihyoDto;
        }

        /// <summary>
        /// レポート出力を実行します
        /// </summary>
        public void CreateR479()
        {
            // 実行
            this.Create(FORM_FULL_PATH_NAME, LAYOUT_NAME, this.detailTable);
        }

        /// <summary>
        /// フィールド状態の更新処理を実行する
        /// </summary>
        protected override void UpdateFieldsStatus()
        {
            if (0 < this.headerTable.Rows.Count)
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

                // 集計項目1名
                if (this.headerTable.Rows[0]["COLUMN_1"] != null)
                {
                    this.SetFieldName("COLUMN_1", this.headerTable.Rows[0]["COLUMN_1"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_1", string.Empty);
                }

                // 集計項目2名
                if (this.headerTable.Rows[0]["COLUMN_2"] != null)
                {
                    this.SetFieldName("COLUMN_2", this.headerTable.Rows[0]["COLUMN_2"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_2", string.Empty);
                }

                // 集計項目3名
                if (this.headerTable.Rows[0]["COLUMN_3"] != null)
                {
                    this.SetFieldName("COLUMN_3", this.headerTable.Rows[0]["COLUMN_3"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_3", string.Empty);
                }

                // 集計項目4名
                if (this.headerTable.Rows[0]["COLUMN_4"] != null)
                {
                    this.SetFieldName("COLUMN_4", this.headerTable.Rows[0]["COLUMN_4"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_4", string.Empty);
                }

                // 集計項目5名
                if (this.headerTable.Rows[0]["COLUMN_5"] != null)
                {
                    this.SetFieldName("COLUMN_5", this.headerTable.Rows[0]["COLUMN_5"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_5", string.Empty);
                }

                // 集計項目6名
                if (this.headerTable.Rows[0]["COLUMN_6"] != null)
                {
                    this.SetFieldName("COLUMN_6", this.headerTable.Rows[0]["COLUMN_6"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_6", string.Empty);
                }

                // 集計項目7名
                if (this.headerTable.Rows[0]["COLUMN_7"] != null)
                {
                    this.SetFieldName("COLUMN_7", this.headerTable.Rows[0]["COLUMN_7"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_7", string.Empty);
                }

                // 換算後数量
                if (this.headerTable.Rows[0]["COLUMN_KANSANGO_SURYO"] != null)
                {
                    this.SetFieldName("COLUMN_KANSANGO_SURYO", this.headerTable.Rows[0]["COLUMN_KANSANGO_SURYO"].ToString());
                }
                else
                {
                    this.SetFieldName("COLUMN_KANSANGO_SURYO", string.Empty);
                }

                // 換算後数量合計
                this.SetFieldFormat("KANSANGO_SURYO", SystemProperty.Format.ManifestSuuryou);
                //this.SetFieldFormat("KANSANGO_SURYO_SUM", SystemProperty.Format.ManifestSuuryou);
                if (this.headerTable.Rows[0]["KANSANGO_SURYO_SUM"] != null)
                {
                    this.SetFieldName("KANSANGO_SURYO_SUM", Convert.ToDecimal(this.headerTable.Rows[0]["KANSANGO_SURYO_SUM"]).ToString(SystemProperty.Format.ManifestSuuryou));
                }
                else
                {
                    this.SetFieldName("KANSANGO_SURYO_SUM", string.Empty);
                }
               
                #endregion
            }

            if (this.manifestShukeihyoDto != null && this.manifestShukeihyoDto.Pattern != null)
            {
                #region Group Visible

                var gr1Visible = this.manifestShukeihyoDto.Pattern.GetShuukeiFlag(1);
                var gr2Visible = this.manifestShukeihyoDto.Pattern.GetShuukeiFlag(2);
                var gr3Visible = this.manifestShukeihyoDto.Pattern.GetShuukeiFlag(3);
                var gr4Visible = this.manifestShukeihyoDto.Pattern.GetShuukeiFlag(4);
                var gr5Visible = this.manifestShukeihyoDto.Pattern.GetShuukeiFlag(5);
                var gr6Visible = this.manifestShukeihyoDto.Pattern.GetShuukeiFlag(6);
                var gr7Visible = this.manifestShukeihyoDto.Pattern.GetShuukeiFlag(7);

                this.SetGroupVisible("GROUP1", gr1Visible, gr1Visible);
                this.SetGroupVisible("GROUP2", gr2Visible, gr2Visible);
                this.SetGroupVisible("GROUP3", gr3Visible, gr3Visible);
                this.SetGroupVisible("GROUP4", gr4Visible, gr4Visible);
                this.SetGroupVisible("GROUP5", gr5Visible, gr5Visible);
                this.SetGroupVisible("GROUP6", gr6Visible, gr6Visible);
                this.SetGroupVisible("GROUP7", gr7Visible, gr7Visible);

                // 換算後数量のフォーマットを設定
                if (gr1Visible)
                {
                    this.SetFieldFormat("FORMAT_GROUP1_KANSANGO_SURYO_SUM", SystemProperty.Format.ManifestSuuryou);
                }
                if (gr2Visible)
                {
                    this.SetFieldFormat("FORMAT_GROUP2_KANSANGO_SURYO_SUM", SystemProperty.Format.ManifestSuuryou);
                }
                if (gr3Visible)
                {
                    this.SetFieldFormat("FORMAT_GROUP3_KANSANGO_SURYO_SUM", SystemProperty.Format.ManifestSuuryou);
                }
                if (gr4Visible)
                {
                    this.SetFieldFormat("FORMAT_GROUP4_KANSANGO_SURYO_SUM", SystemProperty.Format.ManifestSuuryou);
                }
                if (gr5Visible)
                {
                    this.SetFieldFormat("FORMAT_GROUP5_KANSANGO_SURYO_SUM", SystemProperty.Format.ManifestSuuryou);
                }
                if (gr6Visible)
                {
                    this.SetFieldFormat("FORMAT_GROUP6_KANSANGO_SURYO_SUM", SystemProperty.Format.ManifestSuuryou);
                }
                if (gr7Visible)
                {
                    this.SetFieldFormat("FORMAT_GROUP7_KANSANGO_SURYO_SUM", SystemProperty.Format.ManifestSuuryou);
                }

                //this.SetFieldFormat("FORMAT_ALL_KANSANGO_SURYO_SUM", SystemProperty.Format.ManifestSuuryou);
                #endregion
            }
        }
    }
}
