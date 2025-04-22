using System;
using r_framework.APP.Base;
using r_framework.Logic;

namespace Shougun.Core.PaperManifest.ManifestoJissekiIchiran
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

        private void HaikiKbn_CheckedChanged(object sender, EventArgs e)
        {
            var HaikiKbn1 = this.HaikiKbn_1.CheckState;
            var HaikiKbn2 = this.HaikiKbn_2.CheckState;
            var HaikiKbn3 = this.HaikiKbn_3.CheckState;
            var HaikiKbn4 = this.HaikiKbn_4.CheckState;

            if (HaikiKbn1 == System.Windows.Forms.CheckState.Unchecked
                && HaikiKbn2 == System.Windows.Forms.CheckState.Unchecked
                && HaikiKbn3 == System.Windows.Forms.CheckState.Unchecked
                && HaikiKbn4 == System.Windows.Forms.CheckState.Unchecked)
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E001","廃棄物区分");

                if (this.HaikiKbn_1.Focused)
                    this.HaikiKbn_1.Checked = true;
                else if (this.HaikiKbn_2.Focused)
                    this.HaikiKbn_2.Checked = true;
                else if (this.HaikiKbn_3.Focused)
                    this.HaikiKbn_3.Checked = true;
                else
                    this.HaikiKbn_4.Checked = true;

                return;
            }
        }
    }
}
