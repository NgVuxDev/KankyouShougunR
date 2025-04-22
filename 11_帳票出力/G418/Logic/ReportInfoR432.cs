using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Classes -

    #region - ReportInfoR432 -

    /// <summary>推移表(R432)のレポート情報を表すクラス・コントロール</summary>
    internal class ReportInfoR432 : ReportInfoCommon
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR370_R377" /> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <param name="dataTable">データーテーブル</param>
        /// <param name="chusyutsuJoken">抽出条件</param>
        public ReportInfoR432(WINDOW_ID windowID, DataTable dataTable, CommonChouhyouBase commonChouhyouBase)
            : base(windowID, dataTable, commonChouhyouBase)
        {
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            try
            {
                // フィールド状態の更新処理
                base.UpdateFieldsStatus();

                int index;
                int itemColumnIndex = 0;
                DateTime dateTimeStartTmp;
                DateTime dateTimeEndTmp;
                DateTime dateTimeTmp;
                string tmp;

                #region - 全てのタイトルカラムテキスト初期化 -

                // 集計項目領域初期化
                this.SetFieldName("PHY_KAHEN1_1_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_2_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_3_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_4_VLB", string.Empty);

                // 帳票出力項目(明細伝票月)領域初期化
                this.SetFieldName("PHY_KAHEN2_1_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_2_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_3_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_4_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_5_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_6_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_7_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_8_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_9_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_10_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_11_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_12_VLB", string.Empty);

                // 帳票出力項目(明細伝票合計)領域初期化
                this.SetFieldName("PHY_TOTAL_FLB", string.Empty);

                #endregion - 全てのタイトルカラムテキスト初期化 -

                if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                {   // 売上推移表・支払推移表

                    if (this.ComChouhyouBase.DenpyouSyurui == CommonChouhyouBase.DENPYOU_SYURUI.Subete)
                    {   // 全て
                        if (this.ComChouhyouBase.IsDenpyouSyuruiGroupKubun)
                        {   // グループ区分有
                            this.SetGroupVisible("GROUP2", true, false);
                        }
                        else
                        {   // グループ区分無
                            this.SetGroupVisible("GROUP2", false, false);
                        }
                    }
                    else
                    {   // 受入・出荷・売上／支払
                        this.SetGroupVisible("GROUP2", false, false);
                    }
                }
                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU)
                {   // 売上／支払推移表
                    if (this.ComChouhyouBase.IsDenpyouSyuruiGroupKubun)
                    {   // グループ区分有
                        this.SetGroupVisible("GROUP2", true, false);
                    }
                    else
                    {   // グループ区分無
                        this.SetGroupVisible("GROUP2", false, false);
                    }
                }
                else
                {   // 計量推移表
                    if (this.ComChouhyouBase.DenpyouSyurui == CommonChouhyouBase.DENPYOU_SYURUI.Subete && this.ComChouhyouBase.IsDenpyouSyuruiGroupKubun)
                    {   // 伝票種類が全てかつグループ区分有
                        this.SetGroupVisible("GROUP2", true, false);
                    }
                    else
                    {   // グループ区分無
                        this.SetGroupVisible("GROUP2", false, false);
                    }
                }

                #region - 集計項目用タイトルカラムテキスト -

                // 有効な集計項目グループ数
                int syuukeiKoumokuEnableGroup = this.ComChouhyouBase.SelectEnableSyuukeiKoumokuGroupCount;
                for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroup; itemColumnIndex++)
                {
                    int itemIndex = this.ComChouhyouBase.SelectSyuukeiKoumokuList[itemColumnIndex];
                    SyuukeiKoumoku syuukeiKoumoku = this.ComChouhyouBase.SyuukeiKomokuList[itemIndex];

                    // 別という文字を削除
                    string syuukeiKoumokuCD = string.Empty;
                    string syuukeiKoumokuCDName = string.Empty;
                    if (syuukeiKoumoku.Name != string.Empty)
                    {
                        string syuukeiKoumokuName = syuukeiKoumoku.Name.Replace("別", string.Empty);
                        syuukeiKoumokuCD = syuukeiKoumokuName + "CD";
                        syuukeiKoumokuCDName = syuukeiKoumokuName + "名";
                    }

                    switch (itemColumnIndex + 1)
                    {
                        case 1: // 集計項目１
                            this.SetFieldName("PHY_KAHEN1_1_VLB", syuukeiKoumokuCDName);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_1_NAME_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCDName;

                            break;
                        case 2: // 集計項目２
                            this.SetFieldName("PHY_KAHEN1_2_VLB", syuukeiKoumokuCDName);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_2_NAME_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCDName;

                            break;
                        case 3: // 集計項目３
                            this.SetFieldName("PHY_KAHEN1_3_VLB", syuukeiKoumokuCDName);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_3_NAME_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCDName;

                            break;
                        case 4: // 集計項目４
                            this.SetFieldName("PHY_KAHEN1_4_VLB", syuukeiKoumokuCDName);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_4_NAME_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCDName;

                            break;
                    }
                }

                #endregion - 集計項目用タイトルカラムテキスト -

                #region - 集計月用タイトルカラムテキスト -

                dateTimeStartTmp = this.ComChouhyouBase.DateTimeStart;
                dateTimeEndTmp = this.ComChouhyouBase.DateTimeEnd;

                int year = dateTimeStartTmp.Year;

                for (int i = 0; i < 12; i++)
                {
                    dateTimeTmp = dateTimeStartTmp.AddMonths(i);

                    tmp = string.Format("PHY_KAHEN2_{0}_VLB", i + 1);
                    if (dateTimeTmp > dateTimeEndTmp)
                    {
                        this.SetFieldName(tmp, string.Empty);

                        tmp = string.Format("OUTPUT_YEAR_MONTH_{0}_LABEL", i + 1);
                        index = this.DataTableList["Header"].Columns.IndexOf(tmp);
                        this.DataTableList["Header"].Rows[0][index] = string.Empty;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            this.SetFieldName(tmp, dateTimeTmp.ToString("yyyy年           MM月"));
                        }
                        else if (year != dateTimeTmp.Year)
                        {
                            year = dateTimeTmp.Year;
                            this.SetFieldName(tmp, dateTimeTmp.ToString("yyyy年           MM月"));
                        }
                        else
                        {
                            this.SetFieldName(tmp, dateTimeTmp.ToString("                 MM月"));
                        }

                        tmp = string.Format("OUTPUT_YEAR_MONTH_{0}_LABEL", i + 1);
                        index = this.DataTableList["Header"].Columns.IndexOf(tmp);
                        this.DataTableList["Header"].Rows[0][index] = dateTimeTmp.ToString("yyyy/MM");
                    }
                }

                #endregion - 集計月用タイトルカラムテキスト -

                this.SetFieldName("PHY_TOTAL_FLB", "            合計");

                index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_YEAR_MONTH_TOTAL_LABEL");
                this.DataTableList["Header"].Rows[0][index] = "合計";

                // データテーブルリストに明細を追加
                this.DataTableList.Add("Detail", this.ComChouhyouBase.DataTableUkewatashi);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - ReportInfoR432 -

    #endregion - Classes -
}
