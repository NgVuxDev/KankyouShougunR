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

    #region - ReportInfoR379_R384 -

    /// <summary>請求明細表(R379)・支払明細明細表(R384)のレポート情報を表すクラス・コントロール</summary>
    internal class ReportInfoR379_R384 : ReportInfoCommon
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR379_R384" /> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <param name="dataTable">データーテーブル</param>
        /// <param name="chusyutsuJoken">抽出条件</param>
        public ReportInfoR379_R384(WINDOW_ID windowID, DataTable dataTable, CommonChouhyouBase commonChouhyouBase)
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

                switch (this.WindowID)
                {
                    case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:        // R379(請求明細表)
                        this.SetFieldName("PHY_TORIHIKISAKI_CD_VLB", "請求先CD");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME_RYAKU_VLB", "請求先名");
                        this.SetFieldName("PHY_SHIMEBI_VLB", "締");
                        this.SetFieldName("PHY_ZENKAI_KURIKOSI_GAKU_VLB", "前回請求額");
                        this.SetFieldName("PHY_KONKAI_NYUUKIN_GAKU_VLB", "入金額");
                        this.SetFieldName("PHY_KONKAI_CHOUSEI_GAKU_VLB", "調整額");
                        this.SetFieldName("PHY_KURIKOSI_GAKU_VLB", "繰越額");
                        this.SetFieldName("PHY_KONKAI_URIAGE_GAKU_VLB", "今回取引額(税抜)");
                        this.SetFieldName("PHY_SHOUHIZEI_VLB", "消費税");
                        this.SetFieldName("PHY_KONKAI_TORIHIKI_GAKU_VLB", "今回取引額");
                        this.SetFieldName("PHY_KONKAI_KURIKOSI_GAKU_VLB", "今回御請求額");
                        this.SetFieldName("PHY_NYUUKIN_YOTEI_BI_VLB", "入金予定日");
                        this.SetFieldName("PHY_SEIKYUU_DATE_VLB", "請求年月日");

                        break;
                    case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU: // R384(支払明細明細表)
                        this.SetFieldName("PHY_TORIHIKISAKI_CD_VLB", "支払先CD");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME_RYAKU_VLB", "支払先名");
                        this.SetFieldName("PHY_SHIMEBI_VLB", "締");
                        this.SetFieldName("PHY_ZENKAI_KURIKOSI_GAKU_VLB", "前回精算額");
                        this.SetFieldName("PHY_KONKAI_NYUUKIN_GAKU_VLB", "出金額");
                        this.SetFieldName("PHY_KONKAI_CHOUSEI_GAKU_VLB", "調整額");
                        this.SetFieldName("PHY_KURIKOSI_GAKU_VLB", "繰越額");
                        this.SetFieldName("PHY_KONKAI_URIAGE_GAKU_VLB", "今回取引額(税抜)");
                        this.SetFieldName("PHY_SHOUHIZEI_VLB", "消費税");
                        this.SetFieldName("PHY_KONKAI_TORIHIKI_GAKU_VLB", "今回取引額");
                        this.SetFieldName("PHY_KONKAI_KURIKOSI_GAKU_VLB", "今回御精算額");
                        this.SetFieldName("PHY_NYUUKIN_YOTEI_BI_VLB", "出金予定日");
                        this.SetFieldName("PHY_SEIKYUU_DATE_VLB", "支払年月日");

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

    #endregion - ReportInfoR379_R384 -

    #endregion - Classes -
}
