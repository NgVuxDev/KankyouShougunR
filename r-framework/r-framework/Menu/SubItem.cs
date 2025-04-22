using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.Menu
{
    /// <summary>サブアイテムを表すクラス・コントロール</summary>
    public class SubItem : MenuItemComm
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="SubItem"/> class.</summary>
        public SubItem()
        {
            this.AssemblyItems = new List<AssemblyItem>();
            this.Name = string.Empty;
            this.IndexNo = 0;
            this.IconName = string.Empty;
            this.IconSize = "large";
            this.ToolTip = string.Empty;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>アセンブリアイテムを保持するプロパティ</summary>
        public List<AssemblyItem> AssemblyItems { get; set; }

        #endregion - Properties -
    }
}
