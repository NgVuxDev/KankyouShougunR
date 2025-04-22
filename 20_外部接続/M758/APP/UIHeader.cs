using System;
using r_framework.APP.Base;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukaiWanSign
{
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// タイトルラベルの最大横幅
        /// </summary>
        private int TitleMaxWidth = 450;

        //アラート件数（初期値）
        public int InitialNumberAlert = 0;

        //アラート件数
        public int NumberAlert = 0;

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
        ///  アラート件数 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AlertNumber_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.AlertNumber.Text))
            {
                this.NumberAlert = this.InitialNumberAlert;
                return;
            }

            decimal d = 0;
            string count = (this.AlertNumber.Text.Replace(",", ""));

            //decimalに変換できるか確かめる
            if (decimal.TryParse(Convert.ToString(count), out d))
            {
                this.AlertNumber.Text = d.ToString("#,0");
            }
            else
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E084", this.AlertNumber.Text);

                return;
            }
            return;
        }
    }
}
