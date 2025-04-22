using System;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.PaperManifest.Himodukeichiran
{
    public partial class UIHeader : HeaderBaseForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        //アラート件数（初期値）
        public Int32 InitialNumberAlert = 0;

        //アラート件数
        public Int32 NumberAlert = 0;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// アラート件数 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlertNumber_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.alertNumber.Text))
            {
                this.NumberAlert = this.InitialNumberAlert;
                return;
            }

            decimal d = 0;
            String Suryo = (this.alertNumber.Text.Replace(",", ""));
            //decimalに変換できるか確かめる
            if (decimal.TryParse(Convert.ToString(Suryo), out d))
            {
                this.alertNumber.Text = d.ToString("#,0");
            }
            else
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E084", this.alertNumber.Text);

                return;
            }
            return;
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_MANIFEST_HIMODUKEICHIRAN);
            //読込データ件数の初期値設定
            this.ReadDataNumber.Text = "0";
            //アラート件数
            this.alertNumber.LostFocus += new EventHandler(NUMBER_ALERT_LostFocus);
        }
        #endregion
    }
}
