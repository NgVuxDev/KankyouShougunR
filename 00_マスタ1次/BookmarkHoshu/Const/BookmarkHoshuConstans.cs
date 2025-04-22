// $Id:BookmarkHoshuConstans.cs 190 $

namespace BookmarkHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class BookmarkHoshuConstans
    {
        #region - Search Condition -

        /// <summary>検索条件 社員CD</summary>
        public static readonly string SHAIN_CD = "SHAIN_CD";

        /// <summary>検索条件 社員略称</summary>
        public static readonly string SHAIN_NAME_RYAKU = "SHAIN_NAME_RYAKU";

        /// <summary>検索条件 部署CD</summary>
        public static readonly string BUSHO_CD = "BUSHO_CD";

        /// <summary>検索条件 部署略称</summary>
        public static readonly string BUSHO_NAME_RYAKU = "BUSHO_NAME_RYAKU";

        #endregion

        #region - Display Column -

        /// <summary>表示用カラム 区分名</summary>
        public static readonly string KUBUN_NAME = "KUBUN_NAME";

        /// <summary>表示用カラム 機能名</summary>
        public static readonly string KINOU_NAME = "KINOU_NAME";

        /// <summary>表示用カラム メニュー名</summary>
        public static readonly string MENU_NAME = "MENU_NAME";

        /// <summary>表示用カラム マイメニュー</summary>
        public static readonly string MY_FAVORITE = "MY_FAVORITE";

        #endregion

        #region - Hiding Column -

        /// <summary>隠し項目 行番号</summary>
        public static readonly string ROW_NO = "ROW_NO";

        /// <summary>隠し項目 INDEX_NO</summary>
        public static readonly string INDEX_NO = "INDEX_NO";

        #endregion

        #region - MY FAVORITE Column -

        /// <summary>マイメニュー選択 画面ID</summary>
        public static readonly string FORM_ID = "FORM_ID";

        /// <summary>マイメニュー選択 ウィンドウID</summary>
        public static readonly string WINDOW_ID = "WINDOW_ID";

        /// <summary>マイメニュー選択 作成者</summary>
        public static readonly string CREATE_USER = "CREATE_USER";

        /// <summary>マイメニュー選択 作成日</summary>
        public static readonly string CREATE_DATE = "CREATE_DATE";

        /// <summary>マイメニュー選択 作成PC</summary>
        public static readonly string CREATE_PC = "CREATE_PC";

        /// <summary>マイメニュー選択 最終更新者</summary>
        public static readonly string UPDATE_USER = "UPDATE_USER";

        /// <summary>マイメニュー選択 最終更新日時</summary>
        public static readonly string UPDATE_DATE = "UPDATE_DATE";

        /// <summary>マイメニュー選択 最終更新PC</summary>
        public static readonly string UPDATE_PC = "UPDATE_PC";

        /// <summary>マイメニュー選択 TIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>マイメニュー選択 DELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        #endregion
    }
}
