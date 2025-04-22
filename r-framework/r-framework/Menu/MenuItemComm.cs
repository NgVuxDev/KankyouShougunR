namespace r_framework.Menu
{
    /// <summary>メニューアイテム共通を表すクラス・コントロール</summary>
    public class MenuItemComm
    {
        /// <summary>インデックス番号(表示順)を保持するプロパティ</summary>
        public int IndexNo { get; set; }

        /// <summary>表示項目名を保持するプロパティ</summary>
        public string Name { get; set; }

        /// <summary>アイコン名を保持するプロパティ</summary>
        public string IconName { get; set; }

        /// <summary>アイコンサイズを保持するプロパティ</summary>
        public string IconSize { get; set; }

        /// <summary>ツールチップを保持するプロパティ</summary>
        public string ToolTip { get; set; }

        /// <summary>アイテムが無効かどうかを保持するプロパティ</summary>
        public bool Disabled { get; set; }
    }
}
