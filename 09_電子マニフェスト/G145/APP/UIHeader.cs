using System;
using r_framework.APP.Base;

namespace Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku
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
            //base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
