using System;
using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Converter;
using r_framework.Dto;
using logic = r_framework.Logic.CustomNumericTextBox2Logic;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 数値入力コントロール(MultiRow用セル)
    /// </summary>
    public partial class GcCustomNumericTextBox2Cell : GcCustomTextBoxCell, ICustomCell, ICustomNumericTextBox2
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

        /// <summary>
        ///
        /// </summary>
        public override Type EditType
        {
            get { return typeof(GcCustomNumericTextBox2EditingControl); }
        }

        #endregion プロパティ

        /// <summary>
        ///
        /// </summary>
        public GcCustomNumericTextBox2Cell()
        {
            this.InitializeComponent();

            this.Style.ImeMode = ImeMode.Disable;
            this.MaxLength = 0;
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.RangeSetting = this.RangeSetting ?? new RangeSettingDto();
            this.CharacterLimitList = this.CharacterLimitList ?? new char[0];
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var cell = base.Clone() as GcCustomNumericTextBox2Cell;

            cell.RangeSetting = this.RangeSetting;
            cell.CharacterLimitList = this.CharacterLimitList;

            return cell;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="formattedValue"></param>
        /// <param name="style"></param>
        protected override void InitializeEditingControl(int rowIndex, object formattedValue, CellStyle style)
        {
            base.InitializeEditingControl(rowIndex, formattedValue, style);

            this.MaxLength = logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, true);
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.ZeroPaddengFlag = logic.GetZeroPaddingFlag(this.FormatSetting, this.CustomFormatSetting);

            var editingControl = this.GcMultiRow.EditingControl as GcCustomNumericTextBox2EditingControl;
            if (editingControl != null)
            {
                editingControl.ImeMode = ImeMode.Disable;
                editingControl.MaxLength = this.MaxLength;

                // Valueから取得し、変換する、変換できなかった場合、表示文字そのまま。
                decimal val = 0M;
                if (logic.Parsing(this.FormatSetting, this.CustomFormatSetting, this.Value, out val))
                {
                    if (!base.Enabled || !base.Focused || base.ReadOnly || base.ZeroPaddengFlag)
                    {
                        // 初期編集モードに入る時、非編集モードでフォーマットする。(0埋め保持するため)
                        editingControl.Text = logic.Formatting(this.FormatSetting, this.CustomFormatSetting, val);
                    }
                    else
                    {
                        editingControl.Text = val.ToString();
                    }
                }
                else
                {
                    editingControl.Text = this.GetResultText();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rowIndex"></param>
        protected override void TerminateEditingControl(int rowIndex)
        {
            // 削除前、値を保持する。
            var editingControl = this.GcMultiRow.EditingControl as GcCustomNumericTextBox2EditingControl;
            if (editingControl != null)
            {
                decimal val = 0M;
                if (logic.Parsing(this.FormatSetting, this.CustomFormatSetting, editingControl.FormattedValue, out val))
                {
                    this.Value = val;
                }
            }

            base.TerminateEditingControl(rowIndex);

            this.MaxLength = logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, false);
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.ZeroPaddengFlag = logic.GetZeroPaddingFlag(this.FormatSetting, this.CustomFormatSetting);
        }

        ///// <summary>
        ///// ヒントテキスト設定処理
        ///// </summary>
        //public override void CreateHintText()
        //{
        //    if (this.RangeSetting.Max == decimal.MaxValue)
        //    {
        //        // 最大値がデフォルト値の場合、ヒントテキストを正確に設定するため、事前でCharactersNumberを0にする。
        //        this.CharactersNumber = 0M;
        //    }
        //    else
        //    {
        //        // ヒントテキストを正確に設定するため、事前で入力状態のCharactersNumberを計算する。
        //        this.CharactersNumber = Convert.ToDecimal(
        //            logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, true)
        //            );
        //    }
        //    base.CreateHintText();
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="formattedValue"></param>
        /// <returns></returns>
        public object CellParsing(object formattedValue)
        {
            this.CharactersNumber = Convert.ToDecimal(
                logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, true)
                );
            this.ZeroPaddengFlag = logic.GetZeroPaddingFlag(this.FormatSetting, this.CustomFormatSetting);

            decimal val = 0M;
            if (logic.Parsing(this.FormatSetting, this.CustomFormatSetting, formattedValue, out val))
            {
                return val;
            }
            else
            {
                return this.IsDataBound ? DBNull.Value : null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object CellFormatting(object value)
        {
            this.CharactersNumber = Convert.ToDecimal(
                logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, false)
                );
            this.ZeroPaddengFlag = logic.GetZeroPaddingFlag(this.FormatSetting, this.CustomFormatSetting);

            decimal val = 0M;
            if (value != null && logic.Parsing(this.FormatSetting, this.CustomFormatSetting, value, out val))
            {
                return logic.Formatting(this.FormatSetting, this.CustomFormatSetting, val);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formattedValue"></param>
        /// <returns></returns>
        public bool CellValidating(object formattedValue)
        {
            this.MaxLength = logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, false);
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.ZeroPaddengFlag = logic.GetZeroPaddingFlag(this.FormatSetting, this.CustomFormatSetting);

            bool isValid = true;
            if (this.GcMultiRow.IsHandleCreated && !this.GcMultiRow.Disposing && !this.GcMultiRow.IsDisposed)
            {
                string txt = formattedValue as string;
                if (!string.IsNullOrWhiteSpace(txt))
                {
                    isValid = logic.Validating(txt, this.RangeSetting.Min, this.RangeSetting.Max, this.CharacterLimitList);
                    if (!isValid)
                    {
                        this.IsInputErrorOccured = true;
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        ///
        /// </summary>
        public void PreCellValidating()
        {
            // 処理なし
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancel"></param>
        public void PostCellValidating(bool cancel)
        {
            if (cancel)
            {
                // 失敗したら、入力状態に戻す。
                this.MaxLength = logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, true);
                this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
                this.ZeroPaddengFlag = logic.GetZeroPaddingFlag(this.FormatSetting, this.CustomFormatSetting);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(CellPaintingEventArgs e)
        {
            base.OnPaint(e);
            if (this.RangeSetting.Max == decimal.MaxValue)
            {
                // 最大値がデフォルト値の場合、ヒントテキストを正確に設定するため、事前でCharactersNumberを0にする。
                this.CharactersNumber = 0M;
            }
            else
            {
                // ヒントテキストを正確に設定するため、事前で入力状態のCharactersNumberを計算する。
                this.CharactersNumber = Convert.ToDecimal(
                    logic.GetMaxLength(this.FormatSetting, this.CustomFormatSetting, this.RangeSetting.Min, this.RangeSetting.Max, true)
                    );
            }
        }
    }
}