using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Logic;

namespace Shougun.Core.PaperManifest.HensoSakiAnnaisho
{
    public partial class UIHeader : HeaderBaseForm
    {
        public string beforeKyotenCd = string.Empty;

        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        /// <summary>
        /// アラート処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alertNumber_Validated(object sender, EventArgs e)
        {
            // 1.「1」以上の数値のみ入力可。
            //「0」を入力された場合、フォーカス移動しない。

            if (string.IsNullOrEmpty(this.alertNumber.Text.ToString()) || !string.IsNullOrEmpty(this.alertNumber.Text.ToString()) && int.Parse(this.alertNumber.Text.ToString()) <= 0)
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E002", "アラート件数", "1～99999");
                this.alertNumber.Focus();
            }
        }

        private void KYOTEN_CD_Validated(object sender, EventArgs e)
        {
            if (this.KYOTEN_CD.Text != this.beforeKyotenCd)
            {
                if (string.IsNullOrEmpty(this.KYOTEN_CD.Text))
                {
                    this.HAKKOU_KYOTEN_CD.Text = string.Empty;
                    this.HAKKOU_KYOTEN_NAME.Text = string.Empty;
                }
                else if (!this.KYOTEN_CD.Text.Equals("99"))
                {
                    this.HAKKOU_KYOTEN_CD.Text = this.KYOTEN_CD.Text;
                    this.HAKKOU_KYOTEN_NAME.Text = this.KYOTEN_NAME.Text;
                }

                this.beforeKyotenCd = this.HAKKOU_KYOTEN_CD.Text;
            }
        }
    }
}
