using System;
using System.ComponentModel;
using System.Windows.Forms;
using logic = r_framework.Logic.CustomPhoneNumberTextBoxLogic;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public partial class DgvCustomPhoneNumberTextBoxCell : DgvCustomTextBoxCell, ICustomCell
    {
        #region プロパティ

        /// <summary>
        ///
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(13)]
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
        ///
        /// </summary>
        public override Type EditType
        {
            get { return typeof(DgvCustomPhoneNumberTextBoxEditingControl); }
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
                    this.MaxInputLength = 15;
                    this.CharactersNumber = 15M;
                }
                else
                {
                    this.MaxInputLength = 13;
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
        public DgvCustomPhoneNumberTextBoxCell()
        {
            this.MaxInputLength = 13;
            this.CharactersNumber = Convert.ToDecimal(this.MaxInputLength);
            this.DisplayItemName = string.IsNullOrWhiteSpace(this.DisplayItemName) ? "電話番号" : this.DisplayItemName;
            this.FormatSetting = "電話番号フォーマット";
            this.useParentheses = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="initialFormattedValue"></param>
        /// <param name="dataGridViewCellStyle"></param>
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            var editingControl = this.DataGridView.EditingControl as DgvCustomPhoneNumberTextBoxEditingControl;
            if (editingControl != null)
            {
                editingControl.ImeMode = ImeMode.Disable;
                editingControl.MaxLength = this.MaxInputLength;
            }
        }

        public override void CloneChiled()
        {
            base.CloneChiled();

            var column = this.OwningColumn as DgvCustomPhoneNumberTextBoxColumn;
            if (column != null)
            {
                this.MaxInputLength = column.MaxInputLength;
                this.CharactersNumber = column.CharactersNumber;
                this.FormatSetting = column.FormatSetting;
                this.UseParentheses = column.UseParentheses;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formattedValue"></param>
        /// <returns></returns>
        public object CellParsing(object formattedValue)
        {
            return formattedValue ?? (this.OwningColumn.IsDataBound ? DBNull.Value : null);
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
            if (this.DataGridView.IsHandleCreated && !this.DataGridView.Disposing && !this.DataGridView.IsDisposed)
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