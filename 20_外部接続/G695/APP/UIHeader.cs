using System;
using r_framework.APP.Base;
using r_framework.Logic;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku
{
    public partial class UIHeader : HeaderBaseForm
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 読込データ件数の初期値設定
            this.ReadDataNumber.Text = "0";
        }
    }
}
