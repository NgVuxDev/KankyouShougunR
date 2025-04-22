using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
{
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// タイトルラベルの最大横幅
        /// </summary>
        private int TitleMaxWidth = 290;

        /// <summary>
        /// UIFrom
        /// </summary>
        public UIForm form;

        public UIHeader()
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

            // 読込データ件数の初期値設定
            this.readDataNumber.Text = "0";
        }

        /// <summary>
        /// ヘッダテキスト変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_title_TextChanged(object sender, EventArgs e)
        {
            ControlUtility.AdjustTitleSize(lb_title, this.TitleMaxWidth);
        }

        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
