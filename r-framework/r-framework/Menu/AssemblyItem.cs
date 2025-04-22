using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Const;
using r_framework.Dto;

namespace r_framework.Menu
{
    /// <summary>アセンブリアイテムを表すクラス・コントロール</summary>
    public class AssemblyItem : MenuItemComm
    {
        #region - Constructors -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AssemblyItem()
        {
            this.Name = string.Empty;
            this.IndexNo = 0;
            this.IconName = string.Empty;
            this.IconSize = string.Empty;
            this.ToolTip = string.Empty;
            this.FormID = string.Empty;
            this.WindowID = -1;
            this.NameSpace = string.Empty;
            this.AssemblyName = string.Empty;
            this.ClassName = string.Empty;
            this.GroupName = string.Empty;
            this.GroupIconName = string.Empty;
            this.GroupIconSize = string.Empty;
            this.GroupToolTip = string.Empty;
            this.UserAuth = AuthMethodFlag.All; // デフォルトは全操作可能
            this.WindowType = WINDOW_TYPE.NONE; // デフォルトはモード無とする
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>フォームIDを保持するプロパティ</summary>
        public string FormID { get; set; }

        /// <summary>ウィンドウIDを保持するプロパティ</summary>
        public int WindowID { get; set; }

        /// <summary>画面の名前空間を保持するプロパティ</summary>
        public string NameSpace { get; set; }

        /// <summary>アセンブリ名を保持するプロパティ</summary>
        public string AssemblyName { get; set; }

        /// <summary>呼び出すフォームのクラス名を保持するプロパティ</summary>
        public string ClassName { get; set; }

        /// <summary>グループボタンの名前を保持するプロパティ</summary>
        public string GroupName { get; set; }

        /// <summary>グループボタンのアイコン名を保持するプロパティ</summary>
        public string GroupIconName { get; set; }

        /// <summary>グループボタンのアイコンサイズを保持するプロパティ</summary>
        public string GroupIconSize { get; set; }

        /// <summary>グループボタンのツールチップを保持するプロパティ</summary>
        public string GroupToolTip { get; set; }

        /// <summary>画面の種類を保持するプロパティ（Modal,Dialog等）</summary>
        public string FormType { get; set; }

        /// <summary>ログインユーザーの権限情報を保持するプロパティ。デフォルトはAll</summary>
        public AuthMethodFlag UserAuth { get; set; }

        /// <summary>画面の起動モードを保持するプロパティ。デフォルトはNONE</summary>
        public WINDOW_TYPE WindowType { get; set; }

        /// <summary>新規権限が有効かどうかを保持するプロパティ</summary>
        public bool UseAuthAdd { get; set; }

        /// <summary>修正権限が有効かどうかを保持するプロパティ</summary>
        public bool UseAuthEdit { get; set; }

        /// <summary>削除権限が有効かどうかを保持するプロパティ</summary>
        public bool UseAuthDelete { get; set; }

        #endregion - Properties -
    }
}
