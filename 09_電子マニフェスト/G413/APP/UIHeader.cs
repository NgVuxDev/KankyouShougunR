using System;
using r_framework.APP.Base;
using Shougun.Core.ElectronicManifest.RealInfoSearch.Logic;

namespace Shougun.Core.ElectronicManifest.RealInfoSearch
{
    public partial class UIHeader : HeaderBaseForm
    {

        

        //アラート件数
        public Int32 NumberAlert = 0;

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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //アラート件数
            //this.AlertNumber.LostFocus += new EventHandler(NUMBER_ALERT_LostFocus);
        }

        /// <summary>
        /// アラート件数
        /// </summary>
        private void NUMBER_ALERT_LostFocus(object sender, EventArgs e)
        {
            //this.AlertNumber.Text = this.NumberAlert.ToString();
        }

        private void NUMBER_ALERT_TextChanged(object sender, EventArgs e)
        {
            //if (this.AlertNumber.Text == "")
            //{
            //    this.NumberAlert = this.InitialNumberAlert;
            //}
            //else
            //{
            //    this.NumberAlert = Int32.Parse(this.AlertNumber.Text.ToString());
            //}
        }

    }
}
