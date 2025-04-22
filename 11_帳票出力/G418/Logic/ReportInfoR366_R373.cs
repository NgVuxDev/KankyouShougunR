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

    #region - ReportInfoR366_R373 -

    /// <summary>入金明細表(R366)のレポート情報を表すクラス・コントロール</summary>
    internal class ReportInfoR366_R373 : ReportInfoCommon
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR366_R373" /> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <param name="dataTable">データーテーブル</param>
        /// <param name="chusyutsuJoken">抽出条件</param>
        public ReportInfoR366_R373(WINDOW_ID windowID, DataTable dataTable, CommonChouhyouBase commonChouhyouBase)
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
                    case WINDOW_ID.R_NYUUKIN_MEISAIHYOU:    // R366(入金明細表)
                    case WINDOW_ID.R_SYUKKINN_MEISAIHYOU:   // R373(出金明細表)
                        this.SetFieldName("PHY_DENPYOU_NUMBER_FLB", "伝票番号");
                        this.SetFieldName("PHY_DENPYOU_DATE_FLB", "伝票日付");
                        this.SetFieldName("PHY_ROW_NO_FLB", "明細行番");

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

                        // 帳票出力項目領域（伝票部）初期化
                        this.SetFieldName("PHY_KAHEN2_1_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_2_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_3_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_4_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_5_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_6_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_7_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN2_8_VLB", string.Empty);

                        // 帳票出力項目領域（明細部）初期化
                        this.SetFieldName("PHY_KAHEN3_1_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_2_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_3_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_4_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_5_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_6_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_7_VLB", string.Empty);
                        this.SetFieldName("PHY_KAHEN3_8_VLB", string.Empty);

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
                            if (syuukeiKoumoku.Name != string.Empty)
                            {
                                string syuukeiKoumokuName = syuukeiKoumoku.Name.Replace("別", string.Empty);
                                syuukeiKoumokuCD = syuukeiKoumokuName + "CD";
                                syuukeiKoumokuCDName = syuukeiKoumokuName + "名";
                            }

                            switch (itemColumnIndex + 1)
                            {
                                case 1: // 集計項目１
                                    this.SetFieldName("PHY_KAHEN1_1_VLB", syuukeiKoumokuCD);
                                    this.SetFieldName("PHY_KAHEN1_2_VLB", syuukeiKoumokuCDName);

                                    break;
                                case 2: // 集計項目２
                                    this.SetFieldName("PHY_KAHEN1_3_VLB", syuukeiKoumokuCD);
                                    this.SetFieldName("PHY_KAHEN1_4_VLB", syuukeiKoumokuCDName);

                                    break;
                                case 3: // 集計項目３
                                    this.SetFieldName("PHY_KAHEN1_5_VLB", syuukeiKoumokuCD);
                                    this.SetFieldName("PHY_KAHEN1_6_VLB", syuukeiKoumokuCDName);

                                    break;
                                case 4: // 集計項目４
                                    this.SetFieldName("PHY_KAHEN1_7_VLB", syuukeiKoumokuCD);
                                    this.SetFieldName("PHY_KAHEN1_8_VLB", syuukeiKoumokuCDName);

                                    break;
                            }
                        }

                        #endregion - 集計項目用タイトルカラムテキスト -

                        #region - 帳票出力項目（伝票）用タイトルカラムテキスト -

                        itemColumnIndex = 0;
                        for (int i = 0; i < this.ComChouhyouBase.SelectChouhyouOutKoumokuDepyouList.Count; i++, itemColumnIndex++)
                        {
                            ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.ComChouhyouBase.SelectChouhyouOutKoumokuDepyouList[i];
                            ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[0];

                            ALIGN_TYPE alignment = (ALIGN_TYPE)chouhyouOutKoumoku.OutputAlignment;

                            switch (itemColumnIndex)
                            {
                                case 0: // 帳票出力可能項目１番目
                                    this.SetFieldName("PHY_KAHEN2_1_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("G5H_KAHEN2_1_CTL", alignment);

                                    break;
                                case 1: // 帳票出力可能項目２番目
                                    this.SetFieldName("PHY_KAHEN2_2_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("G5H_KAHEN2_2_CTL", alignment);

                                    break;
                                case 2: // 帳票出力可能項目３番目
                                    this.SetFieldName("PHY_KAHEN2_3_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("G5H_KAHEN2_3_CTL", alignment);

                                    break;
                                case 3: // 帳票出力可能項目４番目
                                    this.SetFieldName("PHY_KAHEN2_4_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("G5H_KAHEN2_4_CTL", alignment);

                                    break;
                                case 4: // 帳票出力可能項目５番目
                                    this.SetFieldName("PHY_KAHEN2_5_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("G5H_KAHEN2_5_CTL", alignment);

                                    break;
                                case 5: // 帳票出力可能項目６番目
                                    this.SetFieldName("PHY_KAHEN2_6_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("G5H_KAHEN2_6_CTL", alignment);

                                    break;
                                case 6: // 帳票出力可能項目７番目
                                    this.SetFieldName("PHY_KAHEN2_7_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("G5H_KAHEN2_7_CTL", alignment);

                                    break;
                                case 7: // 帳票出力可能項目８番目
                                    this.SetFieldName("PHY_KAHEN2_8_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("G5H_KAHEN2_8_CTL", alignment);

                                    break;
                            }
                        }

                        #endregion - 帳票出力項目（伝票）用タイトルカラムテキスト -

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
                                    this.SetFieldName("PHY_KAHEN3_1_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN3_1_CTL", alignment);

                                    break;
                                case 1: // 帳票出力可能項目２番目
                                    this.SetFieldName("PHY_KAHEN3_2_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN3_2_CTL", alignment);

                                    break;
                                case 2: // 帳票出力可能項目３番目
                                    this.SetFieldName("PHY_KAHEN3_3_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN3_3_CTL", alignment);

                                    break;
                                case 3: // 帳票出力可能項目４番目
                                    this.SetFieldName("PHY_KAHEN3_4_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN3_4_CTL", alignment);

                                    break;
                                case 4: // 帳票出力可能項目５番目
                                    this.SetFieldName("PHY_KAHEN3_5_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN3_5_CTL", alignment);

                                    break;
                                case 5: // 帳票出力可能項目６番目
                                    this.SetFieldName("PHY_KAHEN3_6_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN3_6_CTL", alignment);

                                    break;
                                case 6: // 帳票出力可能項目７番目
                                    this.SetFieldName("PHY_KAHEN3_7_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN3_7_CTL", alignment);

                                    break;
                                case 7: // 帳票出力可能項目８番目
                                    this.SetFieldName("PHY_KAHEN3_8_VLB", chouhyouOutKoumoku.Name);
                                    this.SetFieldAlign("DTL_KAHEN3_8_CTL", alignment);

                                    break;
                            }
                        }

                        #endregion - 帳票出力項目（明細）用タイトルカラムテキスト -

                        for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroup; itemColumnIndex++)
                        {
                            int itemIndex = this.ComChouhyouBase.SelectSyuukeiKoumokuList[itemColumnIndex];
                            SyuukeiKoumoku syuukeiKoumoku = this.ComChouhyouBase.SyuukeiKomokuList[itemIndex];

                            if (syuukeiKoumoku.Name == string.Empty)
                            {
                                continue;
                            }

                            // 別文字を削除
                            string name = syuukeiKoumoku.Name.Replace("別", string.Empty);

                            switch (itemColumnIndex)
                            {
                                case 0: // 集計項目１が有効
                                    this.SetGroupVisible("GROUP1", false, true);
                                    this.SetFieldName("G1F_FILL_COND_ID_1_TOTAL_LBL_CTL", name + "合計");

                                    break;
                                case 1: // 集計項目２が有効
                                    this.SetGroupVisible("GROUP2", false, true);
                                    this.SetFieldName("G2F_FILL_COND_ID_2_TOTAL_LBL_CTL", name + "合計");

                                    break;
                                case 2: // 集計項目３が有効
                                    this.SetGroupVisible("GROUP3", false, true);
                                    this.SetFieldName("G3F_FILL_COND_ID_3_TOTAL_LBL_CTL", name + "合計");

                                    break;
                                case 3: // 集計項目４が有効
                                    //    this.SetGroupVisible("GROUP4", false, true);
                                    //    this.SetFieldName("G3F_FILL_COND_ID_3_TOTAL_LBL_CTL", syuukeiKoumoku.Name);

                                    break;
                            }
                        }

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

    #endregion - ReportInfoR366_R373 -

    #endregion - Classes -
}
