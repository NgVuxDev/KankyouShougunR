using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.PaperManifest.ManifestSyuuryoubiIchiran

{
    public class ReportInfoR600 : ReportInfoBase
    {
        #region - Fields -
        /// <summary>帳票フルパス名</summary>
        const string OutputFormFullPathName = "./Template/R600-Form.xml";

        /// <summary>レイアウト名</summary>
        const string LayoutName = "LAYOUT1";

        /// <summary>Header出力用DataTable</summary>
        DataTable headerData = new DataTable();

        /// <summary>Detail出力用DataTable</summary>
        DataTable detailData = new DataTable();
        #endregion

        #region - Methods -

        #region constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerData">Header表示用DataTable</param>
        /// <param name="detailData">Detail表示用DataTable</param>
        public ReportInfoR600(DataTable headerData, DataTable detailData)
        {
            this.headerData = headerData;
            this.detailData = detailData;
        }
        #endregion

        /// <summary>
        /// C1Reportの帳票データの作成を実行します
        /// </summary>
        public void R600_Reprt()
        {
            // 明細データTABLEをセット
            this.SetRecord(this.detailData);

            this.Create(OutputFormFullPathName, LayoutName, detailData);
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
            }
            //数値フォーマット情報取得
            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
            string ManifestSuuryoFormat = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();

            //数量のフォーマット制御
            //受入量
            this.SetFieldFormat("D_SUURYO_VLB", ManifestSuuryoFormat);

        }
        #endregion
    }
}