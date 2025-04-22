using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using r_framework.Utility;
using logic = r_framework.Logic.CustomNumericTextBox2Logic;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public class DgvCustomNumericTextBox2EditingControl : DataGridViewTextBoxEditingControl
    {
        public override int MaxLength
        {
            get { return base.MaxLength; }
            set { base.MaxLength = value; }
        }

        public new ImeMode ImeMode
        {
            get { return base.ImeMode; }
            set { base.ImeMode = ImeMode.Disable; }
        }
        protected override ImeMode ImeModeBase
        {
            get { return base.ImeModeBase; }
            set { base.ImeModeBase = ImeMode.Disable; }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            // 勝手にIMEモードが有効になってしまう現象の対策
            this.ImeMode = ImeMode.Disable;

            // 
            var cell = this.EditingControlDataGridView.CurrentCell as DgvCustomNumericTextBox2Cell;
            this.MaxLength = cell.MaxInputLength;

            if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
            {
                if (!this.ReadOnly)
                {
                    // フォーカス取得でテキスト全選択させたいが、なぜかここでは選択されないので
                    // Enterイベント終わった後に実行させる。
                    this.BeginInvoke((Action)this.SelectAll);
                }
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            var cell = this.EditingControlDataGridView.CurrentCell as DgvCustomNumericTextBox2Cell;
            string text = ControlUtility.GetUnselectedText(this as TextBox);
            bool accept = logic.CanAcceptOnKeyPress(e.KeyChar, text,
                this.SelectionStart, cell.FormatSetting, cell.CustomFormatSetting,
                cell.RangeSetting.Min, cell.RangeSetting.Max, cell.CharacterLimitList);
            if (!accept)
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x302: /*WM_PASTE*/
                    if (!logic.CanParseClipboardText())
                        return;
                    break;

                default:
                    break;
            }

            base.WndProc(ref m);
        }
    }
}