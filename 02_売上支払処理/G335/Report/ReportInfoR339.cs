using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using r_framework.Utility;

namespace Shougun.Core.SalesPayment.DenpyouHakou.Report
{
    /// <summary>
    /// R339 領収書
    /// </summary>
    public class ReportInfoR339 : ReportInfoBase
    {
        #region - Fields -
        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable chouhyouDataTable = new DataTable();
        /// <summary>
        /// テンプレートファイル 
        /// </summary>
        private const string Template = @".\Template\RyoushuushoReport_Meisai.xml";
        /// <summary>
        /// レイアウト
        /// </summary>
        private const string Layout = "RyoushuushoReport_Meisai";
        #endregion - Fields -

        #region - Constructors -        
        /// <summary>
        /// Initializes a new instance of the ReportInfoR339
        /// </summary>
        /// <param name="windowID">画面ＩＤを保持するフィールド</param>
        /// <param name="reportData">フォーム入力からのデータ </param>
        public ReportInfoR339(WINDOW_ID windowID, DataTable reportData)
        {
            this.windowID = windowID;
            this.chouhyouDataTable = reportData;
        }
        #endregion - Constructors -

        #region - Methods -
        /// <summary>
        /// データテーブル情報から帳票情報作成処理を実行する
        /// </summary>
        public void CreateReport()
        {
            this.SetRecord(this.chouhyouDataTable);
            this.Create(Template, Layout, this.chouhyouDataTable);
        }
        /// <summary>
        /// フィールド状態の更新処理を実行する
        /// </summary>
        protected override void UpdateFieldsStatus()
        {
            base.UpdateFieldsStatus();
        }
        #endregion - Methods -
    }
}
