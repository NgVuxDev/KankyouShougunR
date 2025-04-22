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

    #region - ReportInfoR434 -

    /// <summary>前年対比表(R434)のレポート情報を表すクラス・コントロール</summary>
    internal class ReportInfoR434 : ReportInfoCommon
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR434" /> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <param name="dataTable">データーテーブル</param>
        /// <param name="chusyutsuJoken">抽出条件</param>
        public ReportInfoR434(WINDOW_ID windowID, DataTable dataTable, CommonChouhyouBase commonChouhyouBase)
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

                #region - 全てのタイトルカラムテキスト初期化 -

                // 集計項目領域初期化
                this.SetFieldName("PHY_KAHEN1_1_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_2_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_3_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_4_VLB", string.Empty);

                // 帳票出力項目(明細換算)領域初期化
                this.SetFieldName("PHY_KAHEN1_5_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_6_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_7_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_8_VLB", string.Empty);

                #endregion - 全てのタイトルカラムテキスト初期化 -

                if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                {   // 売上前年対比表・支払前年対比表

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
                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU)
                {   // 売上／支払前年対比表
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
                {   // 計量前年対比表
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

                #region - 明細換算用タイトルカラムテキスト -

                switch (this.WindowID)
                {
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:           // 売上前年対比表
                        this.SetFieldName("PHY_KAHEN1_5_VLB", "今年売上額（円）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_1_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "今年売上額（円）";

                        this.SetFieldName("PHY_KAHEN1_6_VLB", "昨年売上額（円）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_2_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "昨年売上額（円）";

                        this.SetFieldName("PHY_KAHEN1_7_VLB", "差額（円）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_3_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "差額（円）";

                        this.SetFieldName("PHY_KAHEN1_8_VLB", "対比率（％）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_4_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "対比率（％）";

                        break;
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:         // 支払前年対比表
                        this.SetFieldName("PHY_KAHEN1_5_VLB", "今年支払額（円）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_1_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "今年支払額（円）";

                        this.SetFieldName("PHY_KAHEN1_6_VLB", "昨年支払額（円）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_2_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "昨年支払額（円）";

                        this.SetFieldName("PHY_KAHEN1_7_VLB", "差額（円）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_3_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "差額（円）";

                        this.SetFieldName("PHY_KAHEN1_8_VLB", "対比率（％）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_4_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "対比率（％）";

                        break;
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:  // 売上／支払前年対比表
                        this.SetFieldName("PHY_KAHEN1_5_VLB", "今年換算数量（ｔ）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_1_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "今年換算数量（ｔ）";

                        this.SetFieldName("PHY_KAHEN1_6_VLB", "昨年換算数量（ｔ）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_2_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "昨年換算数量（ｔ）";

                        this.SetFieldName("PHY_KAHEN1_7_VLB", "換算数量差（ｔ）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_3_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "換算数量差（ｔ）";

                        this.SetFieldName("PHY_KAHEN1_8_VLB", "対比率（％）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_4_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "対比率（％）";

                        break;
                    case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:          // 計量前年対比表
                        this.SetFieldName("PHY_KAHEN1_5_VLB", "今年換算数量（ｔ）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_1_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "今年換算数量（ｔ）";

                        this.SetFieldName("PHY_KAHEN1_6_VLB", "昨年換算数量（ｔ）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_2_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "昨年換算数量（ｔ）";

                        this.SetFieldName("PHY_KAHEN1_7_VLB", "換算数量差（ｔ）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_3_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "換算数量差（ｔ）";

                        this.SetFieldName("PHY_KAHEN1_8_VLB", "対比率（％）");
                        index = this.DataTableList["Header"].Columns.IndexOf("OUTPUT_ITEM_4_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = "対比率（％）";

                        break;
                }

                #endregion - 明細換算用タイトルカラムテキスト -

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

    #endregion - ReportInfoR434 -

    #endregion - Classes -
}
