using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Shougun.Printing.Viewer
{
    public class Initializer
    {
        public static void Initialize()
        {
            // WPFの印刷ダイアログはWPFのメインウインドウがいないと常にスクリーン左上に表示されてしまう。
            // ここでは非表示のダミーメインウインドウを作成する。
            // Shougun.Printing.Viewer.XpsFile.Print()メソッド参照
            if (Application.Current == null)
            {
                new Application();
                var window = new Window();
                window.Left = -100;
                window.Top = -100;
                window.Width = 0;
                window.Height = 0;
                window.Show();
                window.Hide();
            }
        }
    }
}
