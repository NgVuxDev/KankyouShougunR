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

    #region - ReportInfoR369_R376 -

    /// <summary>未入金一覧表(R369)・未出金一覧表(R376)のレポート情報を表すクラス・コントロール</summary>
    internal class ReportInfoR369_R376 : ReportInfoCommon
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR369_R376" /> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <param name="dataTable">データーテーブル</param>
        /// <param name="chusyutsuJoken">抽出条件</param>
        public ReportInfoR369_R376(WINDOW_ID windowID, DataTable dataTable, CommonChouhyouBase commonChouhyouBase)
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

                int item = this.ComChouhyouBase.SelectSyuukeiKoumokuList[0];
                SyuukeiKoumoku syuukeiKoumoku = this.ComChouhyouBase.SyuukeiKomokuList[item];

                switch (this.WindowID)
                {
                    case WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU:     // R369(未入金一覧表)
                        this.SetFieldName("PHY_NYUUKIN_YOTEI_BI_VLB", "入金予定日");

                        if (syuukeiKoumoku.Type != SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                        {
                            this.SetGroupGroupBy("GROUP1", string.Empty);
                            this.SetGroupVisible("GROUP2", false, false);

                            //this.SetFieldName("PHY_EIGYOU_TANTOUSHA_CD_VLB", "請求先CD");
                            //this.SetFieldName("PHY_SHAIN_NAME_VLB", "請求先名");
                            this.SetFieldName("PHY_EIGYOU_TANTOUSHA_CD_VLB", "取引先CD");
                            this.SetFieldName("PHY_SHAIN_NAME_VLB", "取引先名");
                        }
                        else
                        {
                            // No.1452
                            this.SetGroupVisible("GROUP2", false, false);

                            this.SetFieldName("PHY_EIGYOU_TANTOUSHA_CD_VLB", "営業担当者CD");
                            this.SetFieldName("PHY_SHAIN_NAME_VLB", "営業担当者名");
                        }

                        this.SetFieldName("PHY_KONKAI_SEIKYU_GAKU_VLB", "請求額");
                        this.SetFieldName("PHY_KONKAI_NYUUKIN_GAKU_VLB", "入金額");
                        this.SetFieldName("PHY_MI_NYUUKIN_GAKU_VLB", "未入金額");
                        this.SetFieldName("PHY_SHIMEBI_VLB", "締");
                        this.SetFieldName("PHY_SEIKYUU_DATE_VLB", "請求日");
                        this.SetFieldName("PHY_NYUUSHUKKIN_KBN_NAME_VLB", "回収方法");

                        if (syuukeiKoumoku.Type != SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                        {
                            this.SetFieldVisible("PHN_TORIHIKISAKI_CD_VLB", false);
                            this.SetFieldVisible("PHN_TORIHIKISAKI_NAME_VLB", false);
                            this.SetFieldVisible("PHN_TORIHIKISAKI_CD_VLB", false);
                            this.SetFieldVisible("PHN_TORIHIKISAKI_CD_VLB", false);
                        }

                        // No.1452
                        if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.TorihikisakiBetsu)
                        {
                            this.SetFieldName("PHN_SEIKYUSAKI_BETSU_VLB", "取　　　引　　　先　　　計");
                        }
                        else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                        {
                            this.SetFieldName("PHN_SEIKYUSAKI_BETSU_VLB", "営　 業　 担　 当　 者　 計");
                        }
                        else
                        {
                            this.SetFieldName("PHN_SEIKYUSAKI_BETSU_VLB", "請　　　求　　　先　　　計");
                        }
                        break;

                    case WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU:     // R376(未出金一覧表)
                        this.SetFieldName("PHY_NYUUKIN_YOTEI_BI_VLB", "出金予定日");

                        if (syuukeiKoumoku.Type != SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                        {
                            this.SetGroupGroupBy("GROUP1", string.Empty);
                            this.SetGroupVisible("GROUP2", false, false);

                            //this.SetFieldName("PHY_EIGYOU_TANTOUSHA_CD_VLB", "支払先CD");
                            //this.SetFieldName("PHY_SHAIN_NAME_VLB", "支払先名");
                            this.SetFieldName("PHY_EIGYOU_TANTOUSHA_CD_VLB", "取引先CD");
                            this.SetFieldName("PHY_SHAIN_NAME_VLB", "取引先名");
                        }
                        else
                        {
                            // No.1452
                            this.SetGroupVisible("GROUP2", false, false);

                            this.SetFieldName("PHY_EIGYOU_TANTOUSHA_CD_VLB", "営業担当者CD");
                            this.SetFieldName("PHY_SHAIN_NAME_VLB", "営業担当者名");
                        }

                        this.SetFieldName("PHY_KONKAI_SEIKYU_GAKU_VLB", "支払額");
                        this.SetFieldName("PHY_KONKAI_NYUUKIN_GAKU_VLB", "前回支払額");
                        this.SetFieldName("PHY_MI_NYUUKIN_GAKU_VLB", "未出金額");
                        this.SetFieldName("PHY_SHIMEBI_VLB", "締");
                        this.SetFieldName("PHY_SEIKYUU_DATE_VLB", "支払日");
                        this.SetFieldName("PHY_NYUUSHUKKIN_KBN_NAME_VLB", "支払方法");

                        if (syuukeiKoumoku.Type != SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                        {
                            this.SetFieldVisible("PHN_TORIHIKISAKI_CD_VLB", false);
                            this.SetFieldVisible("PHN_TORIHIKISAKI_NAME_VLB", false);
                            this.SetFieldVisible("PHN_TORIHIKISAKI_CD_VLB", false);
                            this.SetFieldVisible("PHN_TORIHIKISAKI_CD_VLB", false);
                        }

                        // No.1452
                        if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.TorihikisakiBetsu)
                        {
                            this.SetFieldName("PHN_SEIKYUSAKI_BETSU_VLB", "取　　　引　　　先　　　計");
                        }
                        else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                        {
                            this.SetFieldName("PHN_SEIKYUSAKI_BETSU_VLB", "営　 業　 担　 当　 者　 計");
                        }
                        else
                        {
                            this.SetFieldName("PHN_SEIKYUSAKI_BETSU_VLB", "支　　　払　　　先　　　計");
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

    #endregion - ReportInfoR369_R376 -

    #endregion - Classes -
}
