using System;
using System.ComponentModel;
using System.Windows.Forms;
using logic = r_framework.Logic.CustomPostalCodeTextBoxLogic;

namespace r_framework.CustomControl
{
    public partial class CustomPostalCodeTextBox : CustomTextBox
    {
        #region プロパティ

        /// <summary>
        ///
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(8)]
        public new int MaxLength
        {
            get { return base.MaxLength; }
            set { base.MaxLength = value; }
        }

        /// <summary>
        ///
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new decimal CharactersNumber
        {
            get { return base.CharactersNumber; }
            set { base.CharactersNumber = value; }
        }
        private bool ShouldSerializeCharactersNumber()
        {
            return this.CharactersNumber != 8M;
        }
        internal void ResetCharactersNumber()
        {
            this.CharactersNumber = 8M;
        }

        /// <summary>
        ///
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue("郵便番号フォーマット")]
        public new string FormatSetting
        {
            get { return base.FormatSetting; }
            set { base.FormatSetting = "郵便番号フォーマット"; }
        }

        /// <summary>
        /// テキスト
        /// </summary>
        /// <remarks>
        /// 取得：base.Textそのまま取得；
        /// 設定：base.Textをフォーマットを通じ再設定する。
        /// </remarks>
        public override string Text
        {
            get { return base.Text; }
            set { this.SetResultText(value); }
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        public CustomPostalCodeTextBox()
        {
            this.InitializeComponent();

            this.ImeMode = ImeMode.Disable;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.MaxLength = 8;
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.DisplayItemName = string.IsNullOrWhiteSpace(this.DisplayItemName) ? "郵便番号" : this.DisplayItemName;
            this.FormatSetting = "郵便番号フォーマット";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            this.ImeMode = ImeMode.Disable;

            if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
            {
                this.BeginInvoke((Action)delegate { this.SetResultText(this.Text); });
            }

            base.OnEnter(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
            {
                this.BeginInvoke((Action)delegate { this.SetResultText(this.Text); });
            }

            base.OnLeave(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
            {
                if (this.Enabled && !this.ReadOnly && !logic.Validating(this.Text))
                {
                    this.IsInputErrorOccured = true;
                    e.Cancel = true;
                    return;
                }
            }

            base.OnValidating(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            bool accept = logic.CanAcceptOnKeyPress(e.KeyChar);
            if (!accept)
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// base内部のSetResultTextを使わずので、オーバーライドではなく、
        /// 影響を与えないように、ICustomControlから継承して明示的な再実装する。
        /// </remarks>
        public new void SetResultText(string value)
        {
            string txt = string.Empty;
            if (logic.Parsing(value, out txt))
            {
                // フォーマットした文字を表示する
                if (!this.Enabled || this.ReadOnly || !this.Focused)
                {
                    base.Text = logic.Formatting(txt);
                }
                else
                {
                    base.Text = txt;
                }
            }
            else
            {
                base.Text = value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x302: /*WM_PASTE*/
                    if (!logic.CanAcceptClipboardText())
                        return;
                    break;

                default:
                    break;
            }

            base.WndProc(ref m);
        }
    }
}