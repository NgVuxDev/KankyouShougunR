using System;
using System.ComponentModel;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public partial class DgvCustomPhoneNumberTextBoxColumn : DgvCustomTextBoxColumn
    {
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

        /// <summary>
        ///
        /// </summary>
        public DgvCustomPhoneNumberTextBoxColumn()
        {
            this.MaxInputLength = 13;
            this.CharactersNumber = Convert.ToDecimal(this.MaxInputLength);
            this.DisplayItemName = string.IsNullOrWhiteSpace(this.DisplayItemName) ? "電話番号" : this.DisplayItemName;
            this.FormatSetting = "電話番号フォーマット";
            this.CellTemplate = new DgvCustomPhoneNumberTextBoxCell();
            this.useParentheses = false;
        }

        public override object Clone()
        {
            var column = base.Clone() as DgvCustomPhoneNumberTextBoxColumn;

            column.MaxInputLength = this.MaxInputLength;
            column.CharactersNumber = this.CharactersNumber;
            column.FormatSetting = this.FormatSetting;
            column.UseParentheses = this.UseParentheses;

            return column;
        }
    }
}