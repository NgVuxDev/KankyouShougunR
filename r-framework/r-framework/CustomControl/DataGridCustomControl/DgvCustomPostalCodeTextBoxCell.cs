using System;
using System.ComponentModel;
using System.Windows.Forms;
using logic = r_framework.Logic.CustomPostalCodeTextBoxLogic;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public partial class DgvCustomPostalCodeTextBoxCell : DgvCustomTextBoxCell, ICustomCell
    {
        #region プロパティ

        /// <summary>
        ///
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(8)]
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
        ///
        /// </summary>
        public override Type EditType
        {
            get { return typeof(DgvCustomPostalCodeTextBoxEditingControl); }
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        public DgvCustomPostalCodeTextBoxCell()
        {
            this.MaxInputLength = 8;
            this.CharactersNumber = Convert.ToDecimal(MaxInputLength);
            this.DisplayItemName = string.IsNullOrWhiteSpace(this.DisplayItemName) ? "郵便番号" : this.DisplayItemName;
            this.FormatSetting = "郵便番号フォーマット";
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

            var editingControl = this.DataGridView.EditingControl as DgvCustomPostalCodeTextBoxEditingControl;
            if (editingControl != null)
            {
                editingControl.ImeMode = ImeMode.Disable;
                editingControl.MaxLength = this.MaxInputLength;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formattedValue"></param>
        /// <returns></returns>
        public object CellParsing(object formattedValue)
        {
            string val = string.Empty;
            if (logic.Parsing(formattedValue, out val))
            {
                return val;
            }
            else
            {
                return this.OwningColumn.IsDataBound ? DBNull.Value : null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object CellFormatting(object value)
        {
            string val = string.Empty;
            if (logic.Parsing(value, out val))
            {
                return logic.Formatting(val);
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
            bool isValid = true;
            if (this.DataGridView.IsHandleCreated && !this.DataGridView.Disposing && !this.DataGridView.IsDisposed)
            {
                string txt = formattedValue as string;
                if (!string.IsNullOrWhiteSpace(txt))
                {
                    isValid = logic.Validating(txt);
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