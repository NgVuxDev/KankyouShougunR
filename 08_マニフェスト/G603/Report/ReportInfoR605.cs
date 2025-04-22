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

namespace Shougun.Core.PaperManifest.JissekiHokokuSisetsu
{
    #region - Class -

    /// <summary> R605(実績報告書（処理施設）)帳票を表すクラス・コントロール </summary>
    public class ReportInfoR605 : ReportInfoBase
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
        public void R605_Reprt(DataTable headerTable, DataTable detailTable)
        {
            unitdao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.headerTable = headerTable;
            this.detailTable = detailTable;
            this.SetRecord(this.detailTable);
            // データテーブル情報から帳票情報作成処理を実行する
            this.Create("./Template/R605-Form.xml", "LAYOUT1", this.detailTable);
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            // Header
            // 和暦年を取得する
            this.SetFieldName("HOKOKU_TITLE", "("+this.headerTable.Rows[0]["HOUKOKU_YEAR"].ToString()+"度)");
            this.SetFieldName("HAIKI_SHURUI_CD1", this.headerTable.Rows[0]["HAIKI_SHURUI_CD1"].ToString());
            this.SetFieldName("HAIKI_SHURUI_NAME1", this.headerTable.Rows[0]["HAIKI_SHURUI_NAME1"].ToString());
            this.SetFieldName("HAIKI_SHURUI_CD2", this.headerTable.Rows[0]["HAIKI_SHURUI_CD2"].ToString());
            this.SetFieldName("HAIKI_SHURUI_NAME2", this.headerTable.Rows[0]["HAIKI_SHURUI_NAME2"].ToString());
            this.SetFieldName("HAIKI_SHURUI_CD3", this.headerTable.Rows[0]["HAIKI_SHURUI_CD3"].ToString());
            this.SetFieldName("HAIKI_SHURUI_NAME3", this.headerTable.Rows[0]["HAIKI_SHURUI_NAME3"].ToString());
            this.SetFieldName("HAIKI_SHURUI_CD4", this.headerTable.Rows[0]["HAIKI_SHURUI_CD4"].ToString());
            this.SetFieldName("HAIKI_SHURUI_NAME4", this.headerTable.Rows[0]["HAIKI_SHURUI_NAME4"].ToString());

            string cd_format = "0###";
            int outValue = 0;
            if (!string.IsNullOrEmpty(this.headerTable.Rows[0]["HAIKI_SHURUI_CD1"].ToString()) && int.TryParse(this.headerTable.Rows[0]["HAIKI_SHURUI_CD1"].ToString(), out outValue))
            {
                this.SetFieldFormat("HAIKI_SHURUI_CD1", cd_format);
            }
            outValue = 0;
            if (!string.IsNullOrEmpty(this.headerTable.Rows[0]["HAIKI_SHURUI_CD2"].ToString()) && int.TryParse(this.headerTable.Rows[0]["HAIKI_SHURUI_CD2"].ToString(), out outValue))
            {
                this.SetFieldFormat("HAIKI_SHURUI_CD2", cd_format);
            }
            outValue = 0;
            if (!string.IsNullOrEmpty(this.headerTable.Rows[0]["HAIKI_SHURUI_CD3"].ToString()) && int.TryParse(this.headerTable.Rows[0]["HAIKI_SHURUI_CD3"].ToString(), out outValue))
            {
                this.SetFieldFormat("HAIKI_SHURUI_CD3", cd_format);
            }
            outValue = 0;
            if (!string.IsNullOrEmpty(this.headerTable.Rows[0]["HAIKI_SHURUI_CD4"].ToString()) && int.TryParse(this.headerTable.Rows[0]["HAIKI_SHURUI_CD4"].ToString(), out outValue))
            {
                this.SetFieldFormat("HAIKI_SHURUI_CD4", cd_format);
            }

            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();

            //string format = mSysInfo.SYS_SUURYOU_FORMAT.ToString();
            string format = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();

            this.SetFieldFormat("SBN_RYOU1", format);
            this.SetFieldFormat("SBN_RYOU2", format);
            this.SetFieldFormat("SBN_RYOU3", format);
            this.SetFieldFormat("SBN_RYOU4", format);
            this.SetFieldFormat("HST_RYOU", format);
            this.SetFieldFormat("SBN_RYOU", format);

            this.SetFieldFormat("SBN_RYOU1_SUM", format);
            this.SetFieldFormat("SBN_RYOU2_SUM", format);
            this.SetFieldFormat("SBN_RYOU3_SUM", format);
            this.SetFieldFormat("SBN_RYOU4_SUM", format);
            this.SetFieldFormat("HST_RYOU_SUM", format);
            this.SetFieldFormat("SBN_RYOU_SUM", format);

            this.SetFieldFormat("SBN_RYOU1_TOTAL_SUM", format);
            this.SetFieldFormat("SBN_RYOU2_TOTAL_SUM", format);
            this.SetFieldFormat("SBN_RYOU3_TOTAL_SUM", format);
            this.SetFieldFormat("SBN_RYOU4_TOTAL_SUM", format);
            this.SetFieldFormat("HST_RYOU_TOTAL_SUM", format);
            this.SetFieldFormat("SBN_RYOU_TOTAL_SUM", format);

            short unit_cd = mSysInfo.MANI_KANSAN_KIHON_UNIT_CD.Value;
            M_UNIT unit = new M_UNIT();
            unit = unitdao.GetDataByCd(unit_cd);
            this.SetFieldName("FH_SEIKYUU_SOUFU_POST_CTL18", "処分した産業廃棄物の種類と年間処理量（単位：" + unit.UNIT_NAME + "）");
            this.SetFieldName("FH_SEIKYUU_SOUFU_POST_CTL5", "処分後の産業廃棄物の処分量（単位：" + unit.UNIT_NAME + "）");
        }
    }
    #endregion
}