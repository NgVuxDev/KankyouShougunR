using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;

namespace Shougun.Core.Billing.SeikyuuShimeError.APP
{
    /// <summary>
    /// ヘッダー
    /// </summary>
    public partial class HeaderSeikyuuShimeError : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HeaderSeikyuuShimeError()
        {
            InitializeComponent();

            base.windowTypeLabel.Visible = false;

        }

        /// <summary>
        /// ページ読み込み時処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = WINDOW_ID.T_SHIHARAI_SHIME_ERROR.ToString();
        }

    }
}
