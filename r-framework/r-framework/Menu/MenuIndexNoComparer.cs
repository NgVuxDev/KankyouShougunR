using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.Menu
{
    /// <summary>メニューインデクスの並べ替えルールを表すクラス・コントロール</summary>
    public class MenuIndexNoComparer : IComparer<MenuItemComm>
    {
        #region - Methods -

        /// <summary>2 つのオブジェクトを比較し、一方が他方より小さいか、等しいか、大きいかを示す値を返します。</summary>
        /// <param name="x">比較する最初のオブジェクトです。</param>
        /// <param name="y">比較する 2 番目のオブジェクト。</param>
        /// <returns>x と y の相対的な値を示す符号付き整数</returns>
        public int Compare(MenuItemComm x, MenuItemComm y)
        {
            return x.IndexNo - y.IndexNo;
        }

        #endregion - Methods -
    }
}
