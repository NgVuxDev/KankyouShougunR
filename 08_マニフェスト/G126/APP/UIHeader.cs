using System;
using r_framework.APP.Base;
using r_framework.Logic;

namespace Shougun.Core.PaperManifest.ManifestIchiran
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
        /// 拠点 名称 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        /// <summary>
        /// アラート件数　ロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NUMBER_ALERT_LostFocus(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// アラート件数 TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NUMBER_ALERT_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// アラート件数 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlertNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        /// <summary>
        /// アラート件数 Validatedイベント
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
            String Suryo = (this.AlertNumber.Text.Replace(",", ""));
            //decimalに変換できるか確かめる
            if (decimal.TryParse(Convert.ToString(Suryo), out d))
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
