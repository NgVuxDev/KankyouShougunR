using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup
{
    #region - Class -

    /// <summary>レポート情報を表すクラス・コントロール</summary>
    internal class ReportInfoR369_R376 : ReportInfoBase
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoBase" /> class.</summary>
        /// <param name="dataTable">データーテーブル</param>
        public ReportInfoR369_R376(DataTable dataTable)
            : base(dataTable)
        {
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            ////FieldStatus sts = new FieldStatus();

            ////this.GetFieldStatus("BANK_SHITEN_CD_LBL", ref sts);
            ////sts.Visible = false;
            ////this.SetFieldStatus("BANK_SHITEN_CD_LBL", sts);

            ////this.GetFieldStatus("BANK_SHITEN_CD_CTL", ref sts);
            ////sts.Visible = false;
            ////this.SetFieldStatus("BANK_SHITEN_CD_CTL", sts);
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
