using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using System.Data.SqlTypes;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Carriage.UnchinDaichou
{
    #region - Class -

    /// <summary> 運賃台帳帳票を表すクラス・コントロール </summary>
    public class ReportInfoUnchinDaichou : ReportInfoBase
    {
        #region - Fields -
        #endregion

        /// <summary> C1Reportの帳票データの作成を実行する </summary>
        /// <param name="headerTable">chouhyouData</param>
        /// <param name="detailTable">nyuukinData</param>
        public void UnchinDaichou_Reprt(DataTable table)
        {

            this.SetRecord(table);
            // データテーブル情報から帳票情報作成処理を実行する
            this.Create(Const.UIConstans.OutputFormFullPathName, "UnchinDaichouReport", table);
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            string format = "#,##0.###";
            this.SetFieldFormat("KINGAKU_CTL2", format);
        }
    }
    #endregion
}