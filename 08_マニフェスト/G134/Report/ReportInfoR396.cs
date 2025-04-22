using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using System.Data.SqlTypes;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using r_framework.Dao;
using C1.C1Report;

namespace Shougun.Core.PaperManifest.JissekiHokoku
{
    #region - Class -

    /// <summary> R396(実績報告書（処分実績）)帳票を表すクラス・コントロール </summary>
    public class ReportInfoR396 : ReportInfoBase
    {
        #region - Fields -
        // Detail部データテーブル
        private DataTable detailTable = new DataTable();
        // Header部データテーブル
        private DataTable headerTable = new DataTable();
        private IM_UNITDao unitdao;
        #endregion

        /// <summary> C1Reportの帳票データの作成を実行する </summary>
        /// <param name="headerTable">chouhyouData</param>
        /// <param name="detailTable">nyuukinData</param>
        public void R396_Reprt(DataTable headerTable, DataTable detailTable)
        {
            unitdao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.headerTable = headerTable;
            this.detailTable = detailTable;

            this.SetRecord(this.detailTable);
            // データテーブル情報から帳票情報作成処理を実行する
            this.Create("./Template/R396-Form.xml", "LAYOUT1", this.detailTable);
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            // Header
            // 和暦年を取得する
            System.Globalization.CultureInfo ci =
            new System.Globalization.CultureInfo("ja-JP", false);
            ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
            this.SetFieldName("HOKOKU_TITLE", "（" + Convert.ToDateTime(this.headerTable.Rows[0]["HOUKOKU_YEAR"]).ToString("gy年", ci) + "度）");
            this.SetFieldName("TEISHUTSU_NAME", this.headerTable.Rows[0]["TEISHUTSU_NAME"].ToString());
            this.SetFieldName("GYOUSHA_ADDRESS", this.headerTable.Rows[0]["GYOUSHA_ADDRESS"].ToString());
            this.SetFieldName("GYOUSHA_NAME", this.headerTable.Rows[0]["GYOUSHA_NAME"].ToString());
            this.SetFieldName("GYOUSHA_DAIHYOU", this.headerTable.Rows[0]["GYOUSHA_DAIHYOU"].ToString());
            this.SetFieldName("GYOUSHA_TEL", this.headerTable.Rows[0]["GYOUSHA_TEL"].ToString());
            this.SetFieldName("HOUKOKU_TANTO_NAME", this.headerTable.Rows[0]["HOUKOKU_TANTO_NAME"].ToString());
            this.SetFieldName("HOUKOKU_TITLE1", this.headerTable.Rows[0]["HOUKOKU_TITLE1"].ToString());
            this.SetFieldName("HOUKOKU_TITLE2", this.headerTable.Rows[0]["HOUKOKU_TITLE2"].ToString());

            if (!string.IsNullOrEmpty(this.headerTable.Rows[0]["KYOKA_DATA"].ToString()))
            {
                // 元号○○年○○月○○日
                DateTime temp = DateTime.Parse(this.headerTable.Rows[0]["KYOKA_DATA"].ToString());
                this.SetFieldName("KYOKA_DATA", temp.ToString("gy年MM月dd日", ci));
            }

            // 特管区分
            if (this.headerTable.Rows[0]["TOKUBETSU_KANRI_KBN"] != null)
            {
                if (this.headerTable.Rows[0]["TOKUBETSU_KANRI_KBN"].ToString().Equals("1"))
                {
                    // title
                    this.SetFieldVisible("HOKOKU_TITLE4", false);
                    this.SetFieldVisible("HOKOKU_TITLE5", false);

                    this.SetFieldVisible("HOKOKU_TITLE2", true);
                    this.SetFieldVisible("HOKOKU_TITLE3", true);

                    // 明細title
                    this.SetFieldVisible("HOKOKU_MEISAITITLE1", true);
                    this.SetFieldVisible("HOKOKU_MEISAITITLE2", true);

                    this.SetFieldVisible("HOKOKU_MEISAITITLE3", false);
                    this.SetFieldVisible("HOKOKU_MEISAITITLE4", false);
                }
                else
                {
                    // title
                    this.SetFieldVisible("HOKOKU_TITLE4", true);
                    this.SetFieldVisible("HOKOKU_TITLE5", true);

                    this.SetFieldVisible("HOKOKU_TITLE2", false);
                    this.SetFieldVisible("HOKOKU_TITLE3", false);

                    // 明細title
                    this.SetFieldVisible("HOKOKU_MEISAITITLE1", false);
                    this.SetFieldVisible("HOKOKU_MEISAITITLE2", false);

                    this.SetFieldVisible("HOKOKU_MEISAITITLE3", true);
                    this.SetFieldVisible("HOKOKU_MEISAITITLE4", true);
                }
            }

            this.SetFieldName("KYOKA_NO", this.headerTable.Rows[0]["KYOKA_NO"].ToString());

            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();

            string format = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();

            this.SetFieldFormat("JYUTAKU_RYOU", format);
            this.SetFieldFormat("SBN_RYOU", format);
            this.SetFieldFormat("SBN_AFTER_RYOU", format);
            this.SetFieldFormat("ITAKU_RYOU", format);

            this.SetFieldFormat("JYUTAKU_RYOU_SUM", format);
            this.SetFieldFormat("SBN_RYOU_SUM", format);
            this.SetFieldFormat("SBN_AFTER_RYOU_SUM", format);
            this.SetFieldFormat("ITAKU_RYOU_SUM", format);

            this.SetFieldFormat("JYUTAKU_RYOU_TOTAL_SUM", format);
            this.SetFieldFormat("SBN_RYOU_TOTAL_SUM", format);
            this.SetFieldFormat("SBN_AFTER_RYOU_TOTAL_SUM", format);
            this.SetFieldFormat("ITAKU_RYOU_TOTAL_SUM", format);

            // 書式が1-3,1-4(HOKOKU_SYOSHIKI=3or4)の時は非表示にする
            if (headerTable.Rows[0]["HOUKOKU_SHOSHIKI"].ToString() == "3" || headerTable.Rows[0]["HOUKOKU_SHOSHIKI"].ToString() == "4")
            {
                this.SetFieldName("HST_GYOUSHA_NAME", string.Empty);
                this.SetFieldName("HST_GENBA_ADDRESS", string.Empty);
                this.SetFieldName("JYUTAKU_KBN", string.Empty);
                this.SetFieldName("SHOBUN_HOUHOU_NAME", string.Empty);
                this.SetFieldName("SBN_RYOU", string.Empty);
                this.SetFieldName("SBN_AFTER_RYOU", string.Empty);
                this.SetFieldName("SBN_GENBA_ADDRESS", string.Empty);
                this.SetFieldName("ITAKUSAKI_KYOKA_NO", string.Empty);
                this.SetFieldName("ITAKUSAKI_NAME", string.Empty);
                this.SetFieldName("ITAKUSAKI_ADDRESS", string.Empty);

                this.SetFieldName("JYUTAKU_RYOU", string.Empty);
                this.SetFieldName("ITAKU_RYOU", string.Empty);
                this.SetFieldName("SBN_RYOU_SUM", string.Empty);
                this.SetFieldName("SBN_AFTER_RYOU_SUM", string.Empty);

                this.SetFieldName("SBN_RYOU_TOTAL_SUM", string.Empty);
                this.SetFieldName("SBN_AFTER_RYOU_TOTAL_SUM", string.Empty);

                this.SetFieldName("SBN_RYOU_TOTAL_SUM", string.Empty);
                this.SetFieldName("SBN_AFTER_RYOU_TOTAL_SUM", string.Empty);

                this.SetFieldName("HAIKI_SHURUI_CD", string.Empty);

                this.SetFieldFormat("JYUTAKU_RYOU", string.Empty);
                this.SetFieldFormat("SBN_RYOU", string.Empty);
                this.SetFieldFormat("SBN_AFTER_RYOU", string.Empty);
                this.SetFieldFormat("ITAKU_RYOU", string.Empty);


                this.SetFieldFormat("SBN_RYOU_SUM", string.Empty);
                this.SetFieldFormat("SBN_AFTER_RYOU_SUM", string.Empty);

                this.SetFieldFormat("SBN_RYOU_TOTAL_SUM", string.Empty);
                this.SetFieldFormat("SBN_AFTER_RYOU_TOTAL_SUM", string.Empty);


            }
            else
            {
                this.SetFieldName("HTS_HAIKI_SHURUI_CD", string.Empty);
            }

            //this.SetFieldVisible("HST_GENBA_CHIIKI_NAME", false);
            //this.SetFieldVisible("GROUP_KEY", false);
            short unit_cd = mSysInfo.MANI_KANSAN_KIHON_UNIT_CD.Value;
            M_UNIT unit = new M_UNIT();
            unit = unitdao.GetDataByCd(unit_cd);
            this.SetFieldName("FH_SEIKYUU_SOUFU_POST_CTL32", "受託量(" + unit.UNIT_NAME + ")");
            this.SetFieldName("FH_SEIKYUU_SOUFU_POST_CTL43", "委託量(" + unit.UNIT_NAME + ")");
        }
    }
    #endregion
}