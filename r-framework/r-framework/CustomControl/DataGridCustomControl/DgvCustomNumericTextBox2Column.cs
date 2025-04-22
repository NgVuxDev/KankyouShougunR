using System;
using System.ComponentModel;
using r_framework.Converter;
using r_framework.Dto;
using logic = r_framework.Logic.CustomNumericTextBox2Logic;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public partial class DgvCustomNumericTextBox2Column : DgvCustomTextBoxColumn, ICustomNumericTextBox2
    {
        #region プロパティ

        /// <summary>
        ///
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue("")]
        public string PrevText { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Category("動作")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(0)]
        public new int MaxInputLength
        {
            get { return base.MaxInputLength; }
            set { base.MaxInputLength = value; }
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
            "但し、【0～9】の1桁整数のみ有効になります。")]
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

        #endregion プロパティ

        /// <summary>
        ///
        /// </summary>
        public DgvCustomNumericTextBox2Column()
        {
            this.MaxInputLength = 0;
            this.CharactersNumber = Convert.ToDecimal(this.MaxInputLength);
            this.RangeSetting = this.RangeSetting ?? new RangeSettingDto();
            this.CharacterLimitList = this.CharacterLimitList ?? new char[0];
            this.CellTemplate = new DgvCustomNumericTextBox2Cell();
        }

        public override object Clone()
        {
            var column = base.Clone() as DgvCustomNumericTextBox2Column;

            column.RangeSetting = this.RangeSetting;
            column.CharacterLimitList = this.CharacterLimitList;

            return column;
        }
    }
}