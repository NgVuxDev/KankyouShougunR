using System;
using r_framework.APP.Base;

namespace Shougun.Core.ExternalConnection.NaviTimeMasterRenkei
{
    public partial class HeaderForm : HeaderBaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HeaderForm()
        {
            InitializeComponent();

            base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //読込データ件数の初期値設定
            this.ReadDataNumber.Text = "0";
        }
    }
}
