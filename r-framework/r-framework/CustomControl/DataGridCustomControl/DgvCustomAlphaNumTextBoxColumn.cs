using System.ComponentModel;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// データグリッドビューの英数字入力可能カラム
    /// </summary>
    public partial class DgvCustomAlphaNumTextBoxColumn : DgvCustomTextBoxColumn
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomAlphaNumTextBoxColumn()
        {
            this.AlphabetLimitFlag = true;
            this.NumberLimitFlag = true;
            this.CharacterLimitList = null;
            this.CellTemplate = new DgvCustomAlphaNumTextBoxCell();
        }

        /// <summary>
        /// コピー処理
        /// </summary>
        /// <returns>コピー済みの対象カラム</returns>
        public override object Clone()
        {
            DgvCustomAlphaNumTextBoxColumn myTextBoxCell = base.Clone() as DgvCustomAlphaNumTextBoxColumn;
            myTextBoxCell.Name = this.Name;
            myTextBoxCell.AlphabetLimitFlag = this.AlphabetLimitFlag;
            myTextBoxCell.NumberLimitFlag = this.NumberLimitFlag;
            myTextBoxCell.CharacterLimitList = this.CharacterLimitList;

            return myTextBoxCell;
        }

        #region Property

        [Category("EDISONプロパティ_画面設定")]
        [Description("英語入力の可能有無を指定してください。")]
        public bool AlphabetLimitFlag { get; set; }
        private bool ShouldSerializeAlphabetLimitFlag()
        {
            return this.AlphabetLimitFlag != true;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("数字入力の可能有無を指定してください。")]
        public bool NumberLimitFlag { get; set; }
        private bool ShouldSerializeNumberLimitFlag()
        {
            return this.NumberLimitFlag != true;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("制限を行う場合は入力可能となる文字を設定してください。")]
        public string CharacterLimitList { get; set; }
        private bool ShouldSerializeCharacterLimitList()
        {
            return this.CharacterLimitList != null;
        }

        #endregion

    }
}
