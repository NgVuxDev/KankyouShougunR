using System;

namespace Shougun.Core.Scale.KeiryouIchiran
{
    internal partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal HeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;

        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = "計量一覧";

            //読込データ件数の初期値設定
            this.ReadDataNumber.Text = "0";
        }

    }
}
