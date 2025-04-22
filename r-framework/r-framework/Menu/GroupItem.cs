using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.Menu
{
    /// <summary>グループアイテムを表すクラス・コントロール</summary>
    public class GroupItem : MenuItemComm
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="GroupItem"/> class.</summary>
        public GroupItem()
        {
            this.SubItems = new List<SubItem>();
            this.Name = string.Empty;
            this.IndexNo = 0;
            this.IconName = string.Empty;
            this.IconSize = "large";
            this.ToolTip = string.Empty;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>マスターアイテムか否かの状態を保持するプロパティ</summary>
        /// <remarks>真の場合：マスターアイテム、偽の場合：その他</remarks>
        public bool IsMasterItem { get; set; }

        /// <summary>サブアイテムを保持するプロパティ</summary>
        public List<SubItem> SubItems { get; set; }

        #endregion - Properties -
    }
}
