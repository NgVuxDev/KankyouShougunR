using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.Logic;

namespace Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.APP
{
    public partial class LocalCustomNumTextControl : CustomNumericTextBox
    {

        #region デフォルト作成
        public LocalCustomNumTextControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        #endregion

        /// <summary>
        /// 整数部分の最大入力可能数
        /// </summary>
        [Category("M423_ローカル画面設定")]
        [Description("整数部分の数値数の最大値を設定してください。")]
        public int NumInputMaxLength { set; get; }

        /// <summary>
        /// キー押下
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // 整数部分の文字数で入力制限有りの場合
            if (this.NumInputMaxLength > 0)
            {
                // 入力可能文字の場合
                if (this.InputLimitEnable(e) && this.InputSelectionStartEnable())
                {
                    // 現在の設定値が数値であることを確認
                    decimal tmp;
                    if (decimal.TryParse(this.Text, out tmp))
                    {
                        // 不要文字の削除
                        string checkString = this.GetSeisuuString(this.Text);

                        // 最大文字数オーバー
                        if (checkString.Length - this.SelectionLength >= this.NumInputMaxLength)
                        {
                            // 入力制限として扱う
                            e.Handled = true;
                            return;
                        }
                    }
                }
            }
            base.OnKeyPress(e);
        }

        /// <summary>
        /// 有効な入力値かを判断する
        /// </summary>
        /// <param name="e"></param>
        /// <returns>true=有効、false=無効</returns>
        private bool InputLimitEnable(KeyPressEventArgs e)
        {
            bool res = false;

            // 有効な文字
            if (('0' <= e.KeyChar && e.KeyChar <= '9') )
            {
                // 有効なデータ
                res = true;
            }
            return res;
        }

        /// <summary>
        /// チェックに必要な入力開始位置かを判断する
        /// </summary>
        /// <param name="e"></param>
        /// <returns>true=判定対象、false=対象外</returns>
        private bool InputSelectionStartEnable()
        {
            // 基本的に判定対象データ
            bool res = true;

            // 小数点よりも入力開始位置が大きい場合（小数点以下の数値入力の場合）
            int idx = this.Text.IndexOf('.');
            if (idx > 0 && idx < this.SelectionStart)
            {
                res = false;
            }
            
            return res;
        }

        /// <summary>
        /// 整数部分の文字列を取得
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private string GetSeisuuString(string target)
        {
            string res = string.Empty;

            if (!string.IsNullOrWhiteSpace(target))
            {
                // 整数文字以外の削除
                string checkString = this.Text.Replace("-", string.Empty)
                                              .Replace(",", string.Empty);

                // 小数点以下を削除
                int idx = checkString.IndexOf('.');
                if (idx > 0)
                {
                    checkString = checkString.Remove(idx);
                }

                res = checkString;
            }

            return res;
        }

        /// <summary>
        /// フォーカスアウト時に発生
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            decimal value;
            if (e.Cancel == false && 
                !string.IsNullOrEmpty(this.Text) && Decimal.TryParse(this.Text, out value))
            {
                // 整数部分の文字数が最大値を超えている場合
                if (this.GetSeisuuString(this.Text).Length > this.NumInputMaxLength)
                {
                    e.Cancel = true;
                    this.IsInputErrorOccured = true;

                    this.Focus();
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E084", this.Text);
                    return;
                }
            }

        }

    }
}
