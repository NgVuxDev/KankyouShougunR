using System;
using r_framework.APP.Base;

namespace Shougun.Core.ElectronicManifest.DenshiCSVTorikomu
{
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        
        //アラート件数（初期値）
        public Int32 InitialNumberAlert = 0;

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
            this.AlertNumber.LostFocus += new EventHandler(NUMBER_ALERT_LostFocus);
        }

        /// <summary>
        /// アラート件数
        /// </summary>
        private void NUMBER_ALERT_LostFocus(object sender, EventArgs e)
        {
            if (this.AlertNumber.Text == "")
            {
                this.AlertNumber.Text = this.NumberAlert.ToString();
            }
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
