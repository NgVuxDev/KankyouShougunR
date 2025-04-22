using System;
using r_framework.APP.Base;

namespace Shougun.Core.Allocation.TeikiJissekiHoukoku
{
    /// <summary>
    /// 定期実績CSV出力ヘッダ
    /// </summary>
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIHeader()
        {
            InitializeComponent();

            base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// オンロード処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = "定期実績CSV出力";
        }
    }
}
