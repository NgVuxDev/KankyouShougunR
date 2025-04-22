using System;
using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using logic = r_framework.Logic.CustomPhoneNumberTextBoxLogic;

namespace r_framework.CustomControl
{
    public partial class GcCustomPhoneNumberTextBoxCell : GcCustomTextBoxCell, ICustomCell
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

        public override Type EditType
        {
            get { return typeof(GcCustomPhoneNumberTextBoxEditingControl); }
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
        public GcCustomPhoneNumberTextBoxCell()
        {
            this.InitializeComponent();

            base.Style.ImeMode = ImeMode.Disable;
            this.MaxLength = 13;
            this.CharactersNumber = Convert.ToDecimal(this.MaxLength);
            this.DisplayItemName = string.IsNullOrWhiteSpace(this.DisplayItemName) ? "電話番号" : this.DisplayItemName;
            this.FormatSetting = "電話番号フォーマット";
            this.useParentheses = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var cell = base.Clone() as GcCustomPhoneNumberTextBoxCell;

            cell.MaxLength = this.MaxLength;
            cell.CharactersNumber = this.CharactersNumber;
            cell.FormatSetting = this.FormatSetting;
            cell.UseParentheses = this.UseParentheses;

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

            var editingControl = this.GcMultiRow.EditingControl as GcCustomPhoneNumberTextBoxEditingControl;
            if (editingControl != null)
            {
                editingControl.ImeMode = ImeMode.Disable;
                editingControl.MaxLength = this.MaxLength;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formattedValue"></param>
        /// <returns></returns>
        public object CellParsing(object formattedValue)
        {
            return formattedValue ?? (this.IsDataBound ? DBNull.Value : null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object CellFormatting(object value)
        {
            return value ?? string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formattedValue"></param>
        /// <returns></returns>
        public bool CellValidating(object formattedValue)
        {
            bool isValid = true;
            if (this.GcMultiRow.IsHandleCreated && !this.GcMultiRow.Disposing && !this.GcMultiRow.IsDisposed)
            {
                string txt = formattedValue as string;
                if (!string.IsNullOrWhiteSpace(txt))
                {
                    isValid = logic.Validating(txt, this.UseParentheses);
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
            // 処理なし
        }
    }
}