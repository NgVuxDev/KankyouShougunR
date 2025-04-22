using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon.Const;

namespace Shougun.Core.Allocation.ContenaRirekiIchiranHyou.Report
{
    public class ReportInfoR596 : ReportInfoBase
    {
        #region - Fields -

        /// <summary>レイアウト名</summary>
        const string HidukeBetsuLayout = "LAYOUT1";
        const string ContenaShuruiBetsuLayout = "LAYOUT2";
        const string GyoushaBetsuLayout = "LAYOUT3";
        const string GenbaBetsuLayout = "LAYOUT4";
        const string ContenaNameBetsuLayout = "LAYOUT5";

        /// <summary>Header出力用DataTable</summary>
        DataTable headerData = new DataTable();

        /// <summary>Detail出力用DataTable</summary>
        DataTable detailData = new DataTable();

        /// <summary>レイアウト名</summary>
        string LayoutName;

        /// <summary>対象レポートのフルパス</summary>
        string OutputFileFullPathName;

        /// <summary>コンテナ個体管理対応(1：数量管理、2：個体管理)</summary>
        short ContenaKanriHouhou;
        #endregion

        #region - Methods -

        #region constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerData">Header表示用DataTable</param>
        /// <param name="detailData">Detail表示用DataTable</param>
        /// <param name="layout">レイアウト名</param>
        /// <param name="outputFileFullPathName">レポートのフルパス</param>
        /// <param name="contenaKanriHouhou">コンテナ管理方法</param>
        public ReportInfoR596(DataTable headerData, DataTable detailData, string layout, string outputFileFullPathName, short contenaKanriHouhou)
        {
            this.headerData = headerData;
            this.detailData = detailData;
            this.LayoutName = layout;
            this.OutputFileFullPathName = outputFileFullPathName;
            this.ContenaKanriHouhou = contenaKanriHouhou;
        }
        #endregion

        /// <summary>
        /// C1Reportの帳票データの作成を実行します
        /// </summary>
        public void R594_Reprt()
        {
            // 明細データTABLEをセット
            this.SetRecord(this.detailData);

            string layout = HidukeBetsuLayout;

            if (this.ContenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                // 個体管理
                switch (LayoutName)
                {
                    case "1":
                        layout = HidukeBetsuLayout;
                        break;

                    case "2":
                        layout = ContenaShuruiBetsuLayout;
                        break;

                    case "3":
                        layout = ContenaNameBetsuLayout;
                        break;

                    case "4":
                        layout = GyoushaBetsuLayout;
                        break;

                    case "5":
                        layout = GenbaBetsuLayout;
                        break;
                }
            }
            else
            {
                // 数量管理
                switch (LayoutName)
                {
                    case "1":
                        layout = HidukeBetsuLayout;
                        break;

                    case "2":
                        layout = ContenaShuruiBetsuLayout;
                        break;

                    case "3":
                        layout = GyoushaBetsuLayout;
                        break;

                    case "4":
                        layout = GenbaBetsuLayout;
                        break;
                }
            }

            this.Create(this.OutputFileFullPathName, layout, detailData);
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            /* Header情報設定 */
            if (this.headerData.Rows.Count > 0)
            {
                DataRow row = this.headerData.Rows[0];
                // 自社名
                this.SetFieldName("FH_CORP_NAME_RYAKU_VLB", row["FH_CORP_NAME_RYAKU_VLB"].ToString());
                // 自社名
                this.SetFieldName("FH_KYOTEN_NAME_RYAKU_VLB", row["FH_KYOTEN_NAME_RYAKU_VLB"].ToString());
                // 発行日
                this.SetFieldName("FH_PRINT_DATE_VLB", row["FH_PRINT_DATE_VLB"].ToString() + "　発行");

                // 日付範囲
                this.SetFieldName("FH_DATE_FROM_VLB", row["FH_DATE_FROM_VLB"].ToString());
                this.SetFieldName("FH_DATE_TO_VLB", row["FH_DATE_TO_VLB"].ToString());

                // コンテナ種類範囲
                this.SetFieldName("FH_CONTENA_SHURUI_CD_FROM_VLB", row["FH_CONTENA_SHURUI_CD_FROM_VLB"].ToString());
                this.SetFieldName("FH_CONTENA_SHURUI_NAME_FROM_VLB", row["FH_CONTENA_SHURUI_NAME_FROM_VLB"].ToString());
                this.SetFieldName("FH_CONTENA_SHURUI_CD_FROM_TO_VLB", row["FH_CONTENA_SHURUI_CD_FROM_TO_VLB"].ToString());
                this.SetFieldName("FH_CONTENA_SHURUI_NAME_TO_VLB", row["FH_CONTENA_SHURUI_NAME_TO_VLB"].ToString());

                // 業者範囲
                this.SetFieldName("FH_GYOUSHA_CD_FROM_VLB", row["FH_GYOUSHA_CD_FROM_VLB"].ToString());
                this.SetFieldName("FH_GYOUSHA_NAME_FROM_VLB", row["FH_GYOUSHA_NAME_FROM_VLB"].ToString());
                this.SetFieldName("FH_GYOUSHA_CD_TO_VLB", row["FH_GYOUSHA_CD_TO_VLB"].ToString());
                this.SetFieldName("FH_GYOUSHA_NAME_TO_VLB", row["FH_GYOUSHA_NAME_TO_VLB"].ToString());

                // 現場範囲
                this.SetFieldName("FH_GENBA_CD_FROM_VLB", row["FH_GENBA_CD_FROM_VLB"].ToString());
                this.SetFieldName("FH_GENBA_NAME_FROM_VLB", row["FH_GENBA_NAME_FROM_VLB"].ToString());
                this.SetFieldName("FH_GENBA_CD_TO_VLB", row["FH_GENBA_CD_TO_VLB"].ToString());
                this.SetFieldName("FH_GENBA_NAME_TO_VLB", row["FH_GENBA_NAME_TO_VLB"].ToString());

                // コンテナ種類範囲
                this.SetFieldName("FH_CONTENA_CD_FROM_VLB", row["FH_CONTENA_CD_FROM_VLB"].ToString());
                this.SetFieldName("FH_CONTENA_NAME_FROM_VLB", row["FH_CONTENA_NAME_FROM_VLB"].ToString());
                this.SetFieldName("FH_CONTENA_CD_FROM_TO_VLB", row["FH_CONTENA_CD_FROM_TO_VLB"].ToString());
                this.SetFieldName("FH_CONTENA_NAME_TO_VLB", row["FH_CONTENA_NAME_TO_VLB"].ToString());

                // 日付の文言
                if (this.LayoutName.Equals("1"))
                {
                    this.SetFieldName("PH_SECCHI_DATE_FLB", row["PH_SECCHI_DATE_FLB"].ToString());
                }
            }
        }
        #endregion
    }
}