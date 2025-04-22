using System;
using System.ComponentModel;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public partial class DgvCustomPostalCodeTextBoxColumn : DgvCustomTextBoxColumn
    {
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
        public DgvCustomPostalCodeTextBoxColumn()
        {
            this.MaxInputLength = 8;
            this.CharactersNumber = Convert.ToDecimal(this.MaxInputLength);
            this.DisplayItemName = string.IsNullOrWhiteSpace(this.DisplayItemName) ? "郵便番号" : this.DisplayItemName;
            this.FormatSetting = "郵便番号フォーマット";
            this.CellTemplate = new DgvCustomPostalCodeTextBoxCell();
        }
    }
}