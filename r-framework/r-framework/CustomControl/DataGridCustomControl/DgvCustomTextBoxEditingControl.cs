using System;
using System.Windows.Forms;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 入力用コントロール。DataGridViewTextBoxEditingControlからの派生。
    /// </summary>
    public class DvgCustomTextBoxEditingControl : DataGridViewTextBoxEditingControl
    {
        /// <summary>
        /// フォーカス取得時
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (!this.ReadOnly)
            {
                // アクティブなテキストボックスのIME変換モードを設定する。
                // 勝手に変換モードが無変換になってしまうことがある現象の対策。
                r_framework.Utility.ImeUtility.AdjustControlImeSentenceMode(this);

                // フォーカス取得で既存テキストを全選択状態にする
                this.SelectAll();
            }
        }

        /// <summary>
        /// キープレビュー
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            // アクティブなテキストボックスのIME変換モードを設定する。
            // 勝手に変換モードが無変換になってしまうことがある現象の対策。
            r_framework.Utility.ImeUtility.AdjustControlImeSentenceMode(this);
        }
    }
}