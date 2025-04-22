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

    #region - ReportInfoR370_R377 -

    /// <summary>入金予定一覧表(R370)・出金予定一覧表(R377)のレポート情報を表すクラス・コントロール</summary>
    internal class ReportInfoR370_R377 : ReportInfoCommon
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR370_R377" /> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <param name="dataTable">データーテーブル</param>
        /// <param name="chusyutsuJoken">抽出条件</param>
        public ReportInfoR370_R377(WINDOW_ID windowID, DataTable dataTable, CommonChouhyouBase commonChouhyouBase)
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
                    case WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU:     // R370(入金予定一覧表)
                        this.SetFieldName("PHY_NYUUKIN_YOTEI_BI_VLB", "入金予定日");
                        this.SetFieldName("PHY_TORIHIKISAKI_CD_VLB", "請求先CD");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME_VLB", "請求先名");
                        this.SetFieldName("PHY_KONKAI_NYUUKIN_GAKU_VLB", "入金額");
                        this.SetFieldName("PHY_SHIMEBI_VLB", "締");
                        this.SetFieldName("PHY_SEIKYUU_DATE_VLB", "締実行日");

                        break;
                    case WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU:     // R377(出金予定一覧表)
                        this.SetFieldName("PHY_NYUUKIN_YOTEI_BI_VLB", "出金予定日");
                        this.SetFieldName("PHY_TORIHIKISAKI_CD_VLB", "支払先CD");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME_VLB", "支払先名");
                        this.SetFieldName("PHY_KONKAI_NYUUKIN_GAKU_VLB", "支払額");
                        this.SetFieldName("PHY_SHIMEBI_VLB", "締");
                        this.SetFieldName("PHY_SEIKYUU_DATE_VLB", "締実行日");

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

    #endregion - ReportInfoR370_R377 -

    #endregion - Classes -
}
