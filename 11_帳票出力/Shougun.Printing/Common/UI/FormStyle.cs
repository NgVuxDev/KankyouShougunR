using System;
using System.Drawing;

namespace Shougun.Printing.Common.UI
{
    /// <summary>
    /// Formの将軍ルックスタイル
    /// </summary>
    public static class FormStyle
    {
        public static System.Drawing.Color FormBackColor { set; get; }
        public static System.Drawing.Color ReadOnlyBackColor { set; get; }
        public static System.Drawing.Color FocusedBackColor { set; get; }
        public static System.Drawing.Color LabelBackColor { set; get; }
        public static System.Drawing.Color LabelForeColor {set; get;}

        /// <summary>
        /// Styleクラスの各種プロパティに固定値のデフォルト値を設定する
        /// </summary>
        public static void SetDefaultStyle()
        {
            FormStyle.FormBackColor = Color.FromArgb(232, 247, 240);
            FormStyle.ReadOnlyBackColor = Color.FromArgb(240, 250, 230);
            FormStyle.FocusedBackColor = Color.Aqua;
            FormStyle.LabelBackColor = Color.FromArgb(0, 105, 51);
            FormStyle.LabelForeColor = Color.White;
        }
    }
}
