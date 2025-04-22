using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.Const
{
    /// <summary>
    /// WINDOW_IDのカスタム属性
    /// </summary>
    class WindowTitle : Attribute
    {
        /// <summary>
        /// 左上に表示される画面タイトル
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 属性値の設定
        /// </summary>
        /// <param name="title">画面タイトル</param>
        public WindowTitle(string title)
        {
            this.Title = title;
        }
    }

    /// <summary>
    /// パターン作成時の出力区分を設定するカスタム属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    class PatternOutputKbn : Attribute
    {
        /// <summary>
        /// 出力区分
        /// </summary>
        public int OutputKbn { get; private set; }

        /// <summary>
        /// 属性値の設定
        /// </summary>
        /// <param name="outputKbn">0:指定なし, 1:伝票固定, 2:明細固定</param>
        public PatternOutputKbn(int outputKbn)
        {
            this.OutputKbn = outputKbn;
        }
    }
}
