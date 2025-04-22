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

    #region - ReportInfoR359_R363_R356 -

    /// <summary>売上集計表(R359)のレポート情報を表すクラス・コントロール</summary>
    internal class ReportInfoR359_R363_R356 : ReportInfoCommon
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR359_R363_R356" /> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <param name="dataTable">データーテーブル</param>
        /// <param name="chusyutsuJoken">抽出条件</param>
        public ReportInfoR359_R363_R356(WINDOW_ID windowID, DataTable dataTable, CommonChouhyouBase commonChouhyouBase)
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

                int itemColumnIndex = 0;

                switch (this.WindowID)
                {
                    case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:            // R359(売上集計表)
                    case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:          // R363(支払集計表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:   // R356(売上／支払集計表)

                        #region - 全てのタイトルカラムテキスト初期化 -

                        // 集計項目領域初期化
                        this.SetFieldName("PHY_KAHEN1_1_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN1_2_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN1_3_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN1_4_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN1_5_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN1_6_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN1_7_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN1_8_VLB", string.Empty);

                        // 帳票出力項目領域（明細部）初期化
                        this.SetFieldName("PHY_KAHEN3_1_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_1_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_2_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_2_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_3_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_3_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_4_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_4_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_5_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_5_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_6_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_6_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_7_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_7_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_8_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_8_VLB", string.Empty);

                        #endregion - 全てのタイトルカラムテキスト初期化 -

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
                            string syuukeiKoumokuTotalLabel = string.Empty;   // No.3781
                            if (syuukeiKoumoku.Name != string.Empty)
                            {
                                string syuukeiKoumokuName = syuukeiKoumoku.Name.Replace("別", string.Empty);
                                syuukeiKoumokuCD = syuukeiKoumokuName + "CD";
                                syuukeiKoumokuCDName = syuukeiKoumokuName + "名";
                                syuukeiKoumokuTotalLabel = syuukeiKoumokuName + "合計";   // No.3781
                            }

                            switch (itemColumnIndex + 1)
                            {
                                case 1: // 集計項目１
                                    this.SetFieldName("PHY_KAHEN1_1_VLB", syuukeiKoumokuCD);
                                    this.SetFieldName("PHY_KAHEN1_2_VLB", syuukeiKoumokuCDName);
                                    // No.3781-->
                                    if (string.IsNullOrEmpty(syuukeiKoumoku.Name))
                                    {
                                        this.SetGroupVisible("GROUP2", true, false);
                                    }
                                    else{
                                        this.SetGroupVisible("GROUP2", true, true);
                                    }
                                    this.SetFieldName("G2F_FILL_COND_ID_1_TOTAL_LBL_CTL", syuukeiKoumokuTotalLabel);
                                    // No.3781<--
                                    break;
                                case 2: // 集計項目２
                                    this.SetFieldName("PHY_KAHEN1_3_VLB", syuukeiKoumokuCD);
                                    this.SetFieldName("PHY_KAHEN1_4_VLB", syuukeiKoumokuCDName);
                                    // No.3781-->
                                    if (string.IsNullOrEmpty(syuukeiKoumoku.Name))
                                    {
                                        this.SetGroupVisible("GROUP3", false, false);
                                    }
                                    else{
                                        this.SetGroupVisible("GROUP3", false, true);
                                    }
                                    this.SetFieldName("G3F_FILL_COND_ID_2_TOTAL_LBL_CTL", syuukeiKoumokuTotalLabel);
                                    // No.3781<--
                                    break;
                                case 3: // 集計項目３
                                    this.SetFieldName("PHY_KAHEN1_5_VLB", syuukeiKoumokuCD);
                                    this.SetFieldName("PHY_KAHEN1_6_VLB", syuukeiKoumokuCDName);
                                    // No.3781-->
                                    if (string.IsNullOrEmpty(syuukeiKoumoku.Name))
                                    {
                                        this.SetGroupVisible("GROUP4", false, false);
                                    }
                                    else{
                                        this.SetGroupVisible("GROUP4", false, true);
                                    }
                                    this.SetFieldName("G4F_FILL_COND_ID_3_TOTAL_LBL_CTL", syuukeiKoumokuTotalLabel);
                                    // No.3781<--
                                    break;
                                case 4: // 集計項目４
                                    this.SetFieldName("PHY_KAHEN1_7_VLB", syuukeiKoumokuCD);
                                    this.SetFieldName("PHY_KAHEN1_8_VLB", syuukeiKoumokuCDName);
                                    // No.3781-->
                                    if (string.IsNullOrEmpty(syuukeiKoumoku.Name))
                                    {
                                        this.SetGroupVisible("GROUP5", true, false);
                                    }
                                    else{
                                        this.SetGroupVisible("GROUP5", true, true);
                                    }
                                    this.SetFieldName("G5F_FILL_COND_ID_4_TOTAL_LBL_CTL", syuukeiKoumokuTotalLabel);
                                    // No.3781<--
                                    break;
                            }
                        }

                        // No.3781-->
                        // 未使用グループ非表示
                        for (; itemColumnIndex < 4; itemColumnIndex++)
                        {
                            switch (itemColumnIndex + 1)
                            {
                                case 1: // 集計項目１
                                    this.SetGroupVisible("GROUP2", false, false);
                                    break;
                                case 2: // 集計項目２
                                    this.SetGroupVisible("GROUP3", false, false);
                                    break;
                                case 3: // 集計項目３
                                    this.SetGroupVisible("GROUP4", false, false);
                                    break;
                                case 4: // 集計項目４
                                    this.SetGroupVisible("GROUP5", true, false);
                                    break;
                            }
                        }
                        // No.3781<--

                        #endregion - 集計項目用タイトルカラムテキスト -

                        this.SetFieldName("PHY_BLANK9_VLB", string.Empty);
                        this.SetFieldName("PHY_BLANK1_VLB", string.Empty);

                        // No.3781-->
                        this.SetFieldName("PHY_KAHEN3_7_VLB", "正味重量");
                        this.SetFieldName("PHY_KAHEN3_8_VLB", "金額");
                        // No.3781<--

                        #region - 帳票出力項目（明細）用タイトルカラムテキスト -

                        itemColumnIndex = 0;
                        for (int i = 0; i < this.ComChouhyouBase.SelectChouhyouOutKoumokuMeisaiList.Count; i++, itemColumnIndex++)
                        {
                            ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.ComChouhyouBase.SelectChouhyouOutKoumokuMeisaiList[i];
                            ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[0];

                            ALIGN_TYPE alignment = (ALIGN_TYPE)chouhyouOutKoumoku.OutputAlignment;

                            switch (itemColumnIndex)
                            {
                                case 0: // 帳票出力可能項目１番目
                                    this.SetFieldName("PHY_KAHEN2_1_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN2_1_CTL", alignment);

                                    break;
                                case 1: // 帳票出力可能項目２番目
                                    this.SetFieldName("PHY_KAHEN2_2_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN2_2_CTL", alignment);

                                    break;
                                case 2: // 帳票出力可能項目３番目
                                    this.SetFieldName("PHY_KAHEN2_3_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN2_3_CTL", alignment);

                                    break;
                                case 3: // 帳票出力可能項目４番目
                                    this.SetFieldName("PHY_KAHEN2_4_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN2_4_CTL", alignment);

                                    break;
                                case 4: // 帳票出力可能項目５番目
                                    this.SetFieldName("PHY_KAHEN2_5_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN2_5_CTL", alignment);

                                    break;
                                case 5: // 帳票出力可能項目６番目
                                    this.SetFieldName("PHY_KAHEN2_6_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN2_6_CTL", alignment);

                                    break;
                                case 6: // 帳票出力可能項目７番目
                                    this.SetFieldName("PHY_KAHEN2_7_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN2_7_CTL", alignment);

                                    break;
                                case 7: // 帳票出力可能項目８番目
                                    this.SetFieldName("PHY_KAHEN2_8_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN2_8_CTL", alignment);

                                    break;
                            }
                        }

                        #endregion - 帳票出力項目（明細）用タイトルカラムテキスト -

                        break;
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - ReportInfoR359_R363_R356 -

    #endregion - Classes -
}
