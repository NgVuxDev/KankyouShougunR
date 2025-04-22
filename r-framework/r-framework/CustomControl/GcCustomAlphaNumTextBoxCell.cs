using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using System.ComponentModel;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 英数入力テキストボックス
    /// </summary>
    public partial class GcCustomAlphaNumTextBoxCell : GcCustomTextBoxCell
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GcCustomAlphaNumTextBoxCell()
        {
            InitializeComponent();

            //IMEを無効にする
            base.Style.ImeMode = ImeMode.Disable;
            this.AlphabetLimitFlag = true;
            this.NumberLimitFlag = true;
        }

        /// <summary>
        /// クローン処理
        /// </summary>
        /// <returns>クローン済み要素</returns>
        public override object Clone()
        {
            GcCustomAlphaNumTextBoxCell myAlphaNumTextBoxCell = base.Clone() as GcCustomAlphaNumTextBoxCell;
            myAlphaNumTextBoxCell.AlphabetLimitFlag = this.AlphabetLimitFlag;
            myAlphaNumTextBoxCell.NumberLimitFlag = this.NumberLimitFlag;
            myAlphaNumTextBoxCell.CharacterLimitList = this.CharacterLimitList;


            return myAlphaNumTextBoxCell;
        }



        #region Property

        [Category("EDISONプロパティ_画面設定")]
        [Description("英語入力の可能有無を指定してください。")]
        [DefaultValue(true)]
        public bool AlphabetLimitFlag
        {
            get;
            set;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("数字入力の可能有無を指定してください。")]
        [DefaultValue(true)]
        public bool NumberLimitFlag
        {
            get;
            set;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("制限を行う場合は入力可能となる文字を設定してください。")]
        public string CharacterLimitList { get; set; }

        #endregion

        public override Type EditType
        {
            get
            {
                return typeof(GcCustomAlphaNumTextBoxEditingControl);
            }
        }

        public static GcCustomAlphaNumTextBoxCell alphaNumTextBoxCell { get; set; }
        public static GcCustomAlphaNumTextBoxCell GetalphaNumTextBoxCell
        {
            get
            {
                return alphaNumTextBoxCell;
            }
        }
    }
}
