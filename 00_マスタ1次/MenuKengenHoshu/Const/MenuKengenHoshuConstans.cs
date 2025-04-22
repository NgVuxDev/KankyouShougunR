// $Id: MenuKengenHoshuConstans.cs 36822 2014-12-09 05:27:53Z nagata $
using System.Collections.Generic;

namespace MenuKengenHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class MenuKengenHoshuConstans
    {

        #region - Boolean String -

        /// <summary>TRUE</summary>
        public static readonly string TEXT_BOOL_TRUE = "True";

        /// <summary>FALSE</summary>
        public static readonly string TEXT_BOOL_FALSE = "False";

        #endregion

        #region - Search Condition -

        /// <summary>検索条件　社員CD</summary>
        public static readonly string SHAIN_CD = "SHAIN_CD";

        /// <summary>検索条件　社員略称</summary>
        public static readonly string SHAIN_NAME_RYAKU = "SHAIN_NAME_RYAKU";

        /// <summary>検索条件　部署CD</summary>
        public static readonly string BUSHO_CD = "BUSHO_CD";

        /// <summary>検索条件　部署略称</summary>
        public static readonly string BUSHO_NAME_RYAKU = "BUSHO_NAME_RYAKU";

        #endregion

        #region - Column Header -

        /// <summary>カラムヘッダー　新規権限チェック</summary>
        public static readonly string HD_AUTH_ADD = "HD_AUTH_ADD";

        /// <summary>カラムヘッダー　参照権限チェック</summary>
        public static readonly string HD_AUTH_READ = "HD_AUTH_READ";

        /// <summary>カラムヘッダー　修正権限チェック</summary>
        public static readonly string HD_AUTH_EDIT = "HD_AUTH_EDIT";

        /// <summary>カラムヘッダー　削除権限チェック</summary>
        public static readonly string HD_AUTH_DELETE = "HD_AUTH_DELETE";

        /// <summary>カラムヘッダー　備考</summary>
        public static readonly string HD_BIKOU = "HD_BIKOU";

        /// <summary>カラムヘッダー　社員一括チェック</summary>
        public static readonly string HD_SHAIN_ALL_CHECK = "HD_SHAIN_ALL_CHECK";

        #endregion

        #region - Display Column -

        /// <summary>表示用カラム　区分名</summary>
        public static readonly string KUBUN_NAME = "KUBUN_NAME";

        /// <summary>表示用カラム　機能名</summary>
        public static readonly string KINOU_NAME = "KINOU_NAME";

        /// <summary>表示用カラム　メニュー名</summary>
        public static readonly string MENU_NAME = "MENU_NAME";

        /// <summary>表示用カラム　一括チェック</summary>
        public static readonly string AUTH_ALL = "AUTH_ALL";

        /// <summary>表示用カラム　社員一括チェック</summary>
        public static readonly string SHAIN_CHECK = "SHAIN_CHECK";

        /// <summary>表示用カラム　社員CD</summary>
        public static readonly string CELL_SHAIN_CD = "SHAIN_CD";

        /// <summary>表示用カラム　社員名</summary>
        public static readonly string CELL_SHAIN_NAME = "SHAIN_NAME";

        #endregion

        #region - Hiding Column -

        /// <summary>隠し項目　行番号</summary>
        public static readonly string ROW_NO = "ROW_NO";

        /// <summary>隠し項目　部署CD</summary>
        public static readonly string CELL_BUSHO_CD = "BUSHO_CD";

        #endregion

        #region - Menu Auth Column -

        /// <summary>メニュー権限　画面ID</summary>
        public static readonly string FORM_ID = "FORM_ID";

        /// <summary>メニュー権限　ウィンドウID</summary>
        public static readonly string WINDOW_ID = "WINDOW_ID";

        /// <summary>メニュー権限　新規権限</summary>
        public static readonly string AUTH_ADD = "AUTH_ADD";

        /// <summary>メニュー権限　参照権限</summary>
        public static readonly string AUTH_READ = "AUTH_READ";

        /// <summary>メニュー権限　修正権限</summary>
        public static readonly string AUTH_EDIT = "AUTH_EDIT";

        /// <summary>メニュー権限　削除権限</summary>
        public static readonly string AUTH_DELETE = "AUTH_DELETE";

        /// <summary>メニュー権限　備考</summary>
        public static readonly string BIKOU = "BIKOU";

        /// <summary>メニュー権限　作成者</summary>
        public static readonly string CREATE_USER = "CREATE_USER";

        /// <summary>メニュー権限　作成日</summary>
        public static readonly string CREATE_DATE = "CREATE_DATE";

        /// <summary>メニュー権限　作成PC</summary>
        public static readonly string CREATE_PC = "CREATE_PC";

        /// <summary>メニュー権限　最終更新者</summary>
        public static readonly string UPDATE_USER = "UPDATE_USER";

        /// <summary>メニュー権限　最終更新日時</summary>
        public static readonly string UPDATE_DATE = "UPDATE_DATE";

        /// <summary>メニュー権限　最終更新PC</summary>
        public static readonly string UPDATE_PC = "UPDATE_PC";

        /// <summary>メニュー権限　TIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>メニュー権限　DELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>メニュー権限　新規権限設定可否</summary>
        public static readonly string USE_AUTH_ADD = "USE_AUTH_ADD";

        /// <summary>メニュー権限　修正権限設定可否</summary>
        public static readonly string USE_AUTH_EDIT = "USE_AUTH_EDIT";

        /// <summary>メニュー権限　削除権限設定可否</summary>
        public static readonly string USE_AUTH_DELETE = "USE_AUTH_DELETE";

        #endregion

        #region - Tekiyou Column -
        
        /// <summary>SQL取得項目　適用外（項目名）</summary>
        public static readonly string TEKIYOU_OUT = "TEKIYOU_OUT";

        /// <summary>SQL取得項目　適用区分（適用外）</summary>
        public static readonly int TEKIYOU_KBN_OUT = 1;

        /// <summary>SQL取得項目　適用区分（適用内）</summary>
        public static readonly int TEKIYOU_KBN_IN = 0;
        
        #endregion

        #region - Menu Kbn -

        /// <summary>メニュー区分　社員毎</summary>
        //public static readonly int MENU_KBN_SHAIN = 2;

        /// <summary>メニュー区分　個人</summary>
        public static readonly int MENU_KBN_SINGLE = 1;
        /// <summary>メニュー区分　複数</summary>
        public static readonly int MENU_KBN_MULTIPLE = 2;

        /// <summary>メニュー区分　メニュー毎</summary>
        //public static readonly int MENU_KBN_MENU = 1;

        #endregion

        #region - Not Display Form ID -

        /// <summary>メニューへ表示させないフォームIDリスト</summary>
        /// <remarks>対象：印刷設定、ＴＳ通信設定、モバイル通信設定、CTI連携設定、システム個別設定入力</remarks>
        public static readonly List<string> NotDispFormIdList = new List<string>() { "G487", "G477", "G601", "G666", "M688" };


        #endregion

    }
}
