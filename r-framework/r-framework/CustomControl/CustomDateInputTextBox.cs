using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Utility;
using r_framework.Logic;

namespace r_framework.CustomControl
{
    /// <summary>
    /// テキストボックスの日付用カスタムコントロール
    /// </summary>
    public partial class CustomDateInputTextBox : CustomTextBox
    {
        private string Before { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomDateInputTextBox()
        {
            this.Before = string.Empty;
            InitializeComponent();
        }

        /// <summary>
        /// テキストボックスの日付用カスタムコントロール
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        /// <summary>
        /// テキスト変更後処理
        /// 入力文字列が正しいかチェック
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            var validator = new Validator();
            int temp = 0;

            // 数値以外が入力される
            if (!string.IsNullOrEmpty(this.Text) && !int.TryParse(this.Text, out temp))
            {
                this.SetBeforeData();
                return;
            }

            switch (DateFormat)
            {
                case DATE_FORMAT.YYYY:
                    if (4 < this.Text.Length)
                    {
                        this.SetBeforeData();
                        return;
                    }
                    break;

                case DATE_FORMAT.MM:
                    if (2 < this.Text.Length)
                    {
                        this.SetBeforeData();
                        return;
                    }

                    if (Array.IndexOf(Constans.MONTH_LIST, this.Text) < 0)
                    {
                        this.SetBeforeData();
                        return;
                    }
                    break;

                case DATE_FORMAT.DD:
                    if (2 < this.Text.Length)
                    {
                        this.SetBeforeData();
                        return;
                    }

                    if (Array.IndexOf(Constans.DAY_LIST, this.Text) < 0)
                    {
                        this.SetBeforeData();
                        return;
                    }
                    break;
            }

            // 前回設定値として保持
            this.Before = this.Text;
            base.OnTextChanged(e);
        }

        /// <summary>
        /// 前回入力値をセットする処理
        /// </summary>
        private void SetBeforeData()
        {
            this.Text = this.Before;
            this.Select(this.Text.Length, 0);
        }

        #region Property

        /// <summary>
        /// 入力が行えるフォーマットを指定
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("入力が行えるフォーマットを指定してください。")]
        public DATE_FORMAT DateFormat { get; set; }

        #endregion

    }
}
