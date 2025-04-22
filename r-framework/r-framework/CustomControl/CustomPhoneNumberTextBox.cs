using System;
using System.ComponentModel;
using System.Windows.Forms;
using logic = r_framework.Logic.CustomPhoneNumberTextBoxLogic;

namespace r_framework.CustomControl
{
    public partial class CustomPhoneNumberTextBox : CustomTextBox
    {
        #region プロパティ
        
        /// <summary>
        ///
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(13)]
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
            return this.CharactersNumber != 13M;
        }
        internal void ResetCharactersNumber()
        {
            this.CharactersNumber = 13M;
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue("電話番号フォーマット")]
        public new string FormatSetting
        {
            get { return base.FormatSetting; }
            set { base.FormatSetting = "電話番号フォーマット"; }
        }

        /// <summary>
        /// 括弧を含めた電話電話番号表示を行うかのプロパティ
        /// 括弧使用時は15桁表示にする
        /// </summary>
        private bool useParentheses;
        [Category("EDISONプロパティ_画面設定")]
        [Description("括弧を許容し15桁表示にする場合はtrueに変更してください")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool UseParentheses
        {
            get { return useParentheses; }
            set
            {
                useParentheses = value;
                if (useParentheses)
                {
                    this.MaxLength = 15;
                    this.CharactersNumber = 15M;
                }
                else
                {
                    this.MaxLength = 13;
                    this.CharactersNumber = 13M;
                }
            }
        }
        private bool ShouldSerializeUseParentheses()
        {
            return this.UseParentheses != false;
        }
        internal void ResetUseParentheses()
        {
            this.UseParentheses = false;
        }
        
        #endregion

        /// <summary>
        ///
        /// </summary>
        public CustomPhoneNumberTextBox()
        {
            this.InitializeComponent();

            this.ImeMode = ImeMode.Disable;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.MaxLength = 13;
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.DisplayItemName = string.IsNullOrWhiteSpace(this.DisplayItemName) ? "電話番号" : this.DisplayItemName;
            this.FormatSetting = "電話番号フォーマット";
            this.useParentheses = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            this.ImeMode = ImeMode.Disable;
            base.OnEnter(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
            {
                if (this.Enabled && !this.ReadOnly && !logic.Validating(this.Text, UseParentheses))
                {
                    e.Cancel = true;
                    this.IsInputErrorOccured = true;
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
            bool accept = logic.CanAcceptOnKeyPress(e.KeyChar, this.UseParentheses);
            if (!accept)
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
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
                    if (!logic.CanAcceptClipboardText(UseParentheses))
                        return;
                    break;

                default:
                    break;
            }

            base.WndProc(ref m);
        }
    }
}