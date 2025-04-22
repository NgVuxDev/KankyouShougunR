using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Utility;
using logic = r_framework.Logic.CustomNumericTextBox2Logic;

namespace r_framework.CustomControl
{
    public partial class CustomNumericTextBox2 : CustomTextBox, ICustomControl, ICustomNumericTextBox2
    {
        #region プロパティ

        /// <summary>
        ///
        /// </summary>
        [Category("動作")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(0)]
        public new int MaxLength
        {
            get { return base.MaxLength; }
            set { base.MaxLength = value; }
        }

        /// <summary>
        ///
        /// </summary>
        [Category("動作")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(ImeMode.Disable)]
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
            return this.CharactersNumber != decimal.Zero;
        }
        internal void ResetCharactersNumber()
        {
            this.CharactersNumber = decimal.Zero;
        }

        /// <summary>
        ///
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(false)]
        public new bool ZeroPaddengFlag
        {
            get { return base.ZeroPaddengFlag; }
            set { base.ZeroPaddengFlag = value; }
        }

        /// <summary>
        ///
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("入力可能な範囲を指定してください。負数を許可しない場合は0以上を指定してください。")]
        public RangeSettingDto RangeSetting { get; set; }
        private bool ShouldSerializeRangeSetting()
        {
            return this.RangeSetting != null;
        }
        internal void ResetRangeSetting()
        {
            this.RangeSetting = this.RangeSetting ?? new RangeSettingDto();
            this.RangeSetting.ResetMax();
            this.RangeSetting.ResetMin();
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description(
            "制限を行う場合は入力可能となる文字を設定してください。" +
            "但し、【0～9】の1桁整数のみ有効になります。" +
            "LinkedRadioButtonArrayと伴に動作します。")]
        public char[] CharacterLimitList { get; set; }
        private bool ShouldSerializeCharacterLimitList()
        {
            return this.CharacterLimitList != null && this.CharacterLimitList.Length > 0;
        }
        internal void ResetCharacterLimitList()
        {
            this.CharacterLimitList = null;
        }

        /// <summary>
        ///
        /// </summary>
        [Category("EDISONプロパティ_チェック設定")]
        [Description(
            "このテキストボックスとリンクするラジオボタンを登録してください。" +
            "CharacterLimitListと伴に動作します。")]
        public string[] LinkedRadioButtonArray { get; set; }
        private bool ShouldSerializeLinkedRadioButtonArray()
        {
            return this.LinkedRadioButtonArray != null && this.LinkedRadioButtonArray.Length > 0;
        }
        internal void ResetLinkedRadioButtonArray()
        {
            this.LinkedRadioButtonArray = null;
        }

        /// <summary>
        ///
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description(
            "FormatSettingでカスタムを指定した場合にフォーマットを設定してください。" +
            "（数値の場合は、整数部「#」、「0」、「#,###」又は「#,##0」と" +
            "桁数に合せての小数部「.0」、「.0#」、「.#」を設定してください、" +
            "コードの場合は、桁数に合わせての「0」を設定してください。")]
        [RefreshProperties(RefreshProperties.All)]
        public new string CustomFormatSetting
        {
            get
            {
                return logic.CheckNumericCustomFormatSetting(base.CustomFormatSetting) ? base.CustomFormatSetting : null;
            }
            set
            {
                base.CustomFormatSetting = logic.CheckNumericCustomFormatSetting(value) ? value : null;
            }
        }
        private bool ShouldSerializeCustomFormatSetting()
        {
            return !string.IsNullOrWhiteSpace(base.CustomFormatSetting);
        }
        internal void ResetCustomFormatSetting()
        {
            base.CustomFormatSetting = string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("フォーカスアウト時に行うフォーマットを選んでください。")]
        [TypeConverter(typeof(FormatConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public new string FormatSetting
        {
            get
            {
                return logic.CheckNumericFormatSetting(base.FormatSetting) ? base.FormatSetting : null;
            }
            set
            {
                base.FormatSetting = logic.CheckNumericFormatSetting(value) ? value : null;
            }
        }
        private bool ShouldSerializeFormatSetting()
        {
            return !string.IsNullOrWhiteSpace(base.FormatSetting);
        }
        internal void ResetFormatSetting()
        {
            base.FormatSetting = null;
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

        #endregion プロパティ

        /// <summary>
        ///
        /// </summary>
        public CustomNumericTextBox2()
        {
            this.InitializeComponent();

            this.ImeMode = ImeMode.Disable;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.MaxLength = 0;
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.ZeroPaddengFlag = false;
            this.RangeSetting = this.RangeSetting ?? new RangeSettingDto();
            this.CharacterLimitList = this.CharacterLimitList ?? new char[0];
            this.LinkedRadioButtonArray = this.LinkedRadioButtonArray ?? new string[0];
        }

        /// <summary>
        ///
        /// </summary>
        protected override void OnCreateControl()
        {
            if (!this.DesignMode)
            {
                if (this.RangeSetting.Max == decimal.MaxValue)
                {
                    // 最大値がデフォルト値の場合、ヒントテキストを正確に設定するため、事前でMaxLengthなどを0にする。
                    this.MaxLength = 0;
                    this.CharactersNumber = 0M;
                }
                else
                {
                    // ヒントテキストを正確に設定するため、事前で入力状態のMaxLengthなどを計算する。
                    this.MaxLength = logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, true);
                    this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
                }
                this.ZeroPaddengFlag = logic.GetZeroPaddingFlag(this.FormatSetting, this.CustomFormatSetting);
            }

            base.OnCreateControl();

            if (!this.DesignMode)
            {
                // 非入力状態のMaxLengthなどを戻す。
                this.MaxLength = logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, false);
                this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            // 勝手にIMEモードが有効になってしまう現象の対策
            this.ImeMode = ImeMode.Disable;

            this.MaxLength = logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, true);
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.ZeroPaddengFlag = logic.GetZeroPaddingFlag(this.FormatSetting, this.CustomFormatSetting);

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
            this.MaxLength = logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, false);
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.ZeroPaddengFlag = logic.GetZeroPaddingFlag(this.FormatSetting, this.CustomFormatSetting);

            if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
            {
                if (this.Enabled && !this.ReadOnly &&
                    !logic.Validating(this.Text, this.RangeSetting.Min, this.RangeSetting.Max, this.CharacterLimitList))
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
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            // ラジオボタンリンク処理
            if (this.LinkedRadioButtonArray.Length > 0)
            {
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.FindForm().Controls;
                foreach (var radioButtonName in LinkedRadioButtonArray)
                {
                    var radioButton = controlUtil.GetSettingField(radioButtonName) as CustomRadioButton;
                    if (radioButton != null &&
                        // ラジオ自体が押下できる、又は、数値自体が入力できないプログラムで設定する場合
                        (radioButton.Enabled || !this.Enabled || this.ReadOnly))
                    {
                        radioButton.Checked = radioButton.Value == this.Text;
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            string text = ControlUtility.GetUnselectedText(this);
            bool accept = logic.CanAcceptOnKeyPress(e.KeyChar, text,
                this.SelectionStart, this.FormatSetting, this.CustomFormatSetting,
                this.RangeSetting.Min, this.RangeSetting.Max, this.CharacterLimitList);

            // 入力(単)又はコピペの場合
            if (char.IsNumber(e.KeyChar) || (char.IsControl(e.KeyChar) && e.KeyChar == 22))
            {
                // ラジオボタン連動処理
                // NOTE: logic.CanAcceptOnKeyPressが先に実行するので、CharacterLimitListが優先されます。
                if (this.LinkedRadioButtonArray.Length > 0)
                {
                    // 入力後で構成予定なTextで判断する
                    var linkedAccept = false;
                    var linkedValue = text;
                    if (char.IsNumber(e.KeyChar))
                    {
                        // 文字入力の場合、入力後の文字列で判定。
                        linkedValue = linkedValue.Insert(this.SelectionStart, e.KeyChar.ToString());
                    }
                    else if ((char.IsControl(e.KeyChar) && e.KeyChar == 22))
                    {
                        // 貼り付けの場合
                        var iData = Clipboard.GetDataObject();
                        if (iData != null && iData.GetDataPresent(DataFormats.Text))
                        {
                            linkedValue = linkedValue.Insert(this.SelectionStart, (string)iData.GetData(DataFormats.Text)).
                                // 貼り付け文字列を判定する時、改行を除外する。
                                Replace("\r", "").Replace("\n", "");
                        }
                    }

                    ControlUtility controlUtil = new ControlUtility();
                    controlUtil.ControlCollection = this.FindForm().Controls;
                    foreach (var radioButtonName in LinkedRadioButtonArray)
                    {
                        // ラジオボタン連動以外の値を入力不可にする
                        var radioButton = controlUtil.GetSettingField(radioButtonName) as CustomRadioButton;
                        if (radioButton != null && radioButton.Value == linkedValue)
                        {
                            linkedAccept = radioButton.Enabled;
                            break;
                        }
                    }
                    accept &= linkedAccept;
                }
            }

            if (!accept)
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// base内部にGetResultTextを利用してるので、オーバーライドではなく、
        /// 影響を与えないように、ICustomControlから継承して明示的な再実装する。
        /// </remarks>
        public new string GetResultText()
        {
            // SuperEntity取得する時、「,」削除対応。
            return base.GetResultText().Replace(",", "");
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
            decimal val = 0M;
            if (logic.Parsing(this.FormatSetting, this.CustomFormatSetting, value, out val))
            {
                if (!this.Enabled || this.ReadOnly || !this.Focused || this.ZeroPaddengFlag)
                {
                    base.Text = logic.Formatting(this.FormatSetting, this.CustomFormatSetting, val);
                }
                else
                {
                    base.Text = val.ToString();
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