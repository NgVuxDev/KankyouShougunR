// $Id: MenuKengenPtIchiranLogic.cs 36342 2014-12-02 07:51:20Z sanbongi $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using GrapeCity.Win.MultiRow;
using MenuKengenHoshu.APP;
using MenuKengenHoshu.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Menu;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace MenuKengenHoshu.Logic
{
    /// <summary>
    /// メニュー権限パターン一覧画面のロジック
    /// </summary>
    [Implementation]
    public partial class MenuKengenPtIchiranLogic : IBuisinessLogic
    {
        // Shougun.Core.Common.BusinessCommon.Base.BaseForm.BasePopFormで画面を呼び出すため
        // FWは「r_framework」を使用する(r_frameworkは使用しない)

        #region - Fields -

        /// <summary>
        /// ボタン設定XMLパス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "MenuKengenHoshu.Setting.ButtonSetting_PtIchiran.xml";

        /// <summary>
        /// メニュー権限パターン一覧画面Form
        /// </summary>
        private MenuKengenPtIchiranForm form;

        /// <summary>
        /// メニュー権限パターンのDao
        /// </summary>
        private IM_MENU_AUTH_PT_ENTRYDao daoPtEntry;

        /// <summary>
        /// メニュー権限パターン詳細のDao
        /// </summary>
        private IM_MENU_AUTH_PT_DETAILDao daoPtDetail;

        /// <summary>
        /// メニューテーブル
        /// </summary>
        private DataTable MenuTable;

        /// <summary>
        /// 行番号
        /// </summary>
        private int rowNo;

        /// <summary>メニュー権限パターン　パターンID</summary>
        private static readonly string PATTERN_ID = "PATTERN_ID";

        #endregion

        #region - Properties -

        /// <summary>
        /// メニューアイテムリスト
        /// 呼出元画面のリボンフォームより取得
        /// </summary>
        internal List<MenuItemComm> MenuItems;

        /// <summary>
        /// 検索結果 メニュー権限パターン
        /// </summary>
        internal DataTable SearchResultEntry { get; set; }

        /// <summary>
        /// 検索結果 メニュー権限パターン詳細
        /// </summary>
        internal DataTable SearchResultDetail { get; set; }

        #endregion

        #region - Constructor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public MenuKengenPtIchiranLogic(MenuKengenPtIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フォーム
            this.form = targetForm;

            // DAO
            this.daoPtEntry = DaoInitUtility.GetComponent<IM_MENU_AUTH_PT_ENTRYDao>();
            this.daoPtDetail = DaoInitUtility.GetComponent<IM_MENU_AUTH_PT_DETAILDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region - Method -

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                // ボタンテキストの初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // メニューテーブル作成
                this.CreateMenuTable();

                // パターン一覧作成
                var count = this.CreateIchiranPtEntry();

                // ファンクションボタン制御
                this.SetEnabledFunctionButton(count);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #region FunctionButton初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BasePopForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region イベント初期化

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BasePopForm)this.form.Parent;

            // F3 適用
            parentForm.bt_func3.Click -= new EventHandler(this.form.Tekiyo);
            parentForm.bt_func3.Click += new EventHandler(this.form.Tekiyo);

            // F4 削除
            parentForm.bt_func4.Click -= new EventHandler(this.form.Sakujo);
            parentForm.bt_func4.Click += new EventHandler(this.form.Sakujo);

            // F7 条件クリア
            parentForm.bt_func7.Click -= new EventHandler(this.form.Clear);
            parentForm.bt_func7.Click += new EventHandler(this.form.Clear);

            // F8 検索
            parentForm.bt_func8.Click -= new EventHandler(this.form.Search);
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            // F12 閉じる
            parentForm.bt_func12.Click -= new EventHandler(this.form.FormClose);
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            this.form.Ichiran_PtEntry.SelectionChanged -= new EventHandler(this.form.Ichiran_PtEntry_SelectionChanged);
            this.form.Ichiran_PtEntry.SelectionChanged += new EventHandler(this.form.Ichiran_PtEntry_SelectionChanged);

            this.form.Ichiran_PtEntry.CellDoubleClick -= new EventHandler<CellEventArgs>(this.form.Ichiran_PtEntry_CellDoubleClick);
            this.form.Ichiran_PtEntry.CellDoubleClick += new EventHandler<CellEventArgs>(this.form.Ichiran_PtEntry_CellDoubleClick);
        }

        #endregion

        #region メニューテーブル作成

        /// <summary>
        /// メニューテーブル作成
        /// </summary>
        private void CreateMenuTable()
        {
            this.MenuTable = new DataTable();

            AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.KUBUN_NAME, typeof(string));
            AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.KINOU_NAME, typeof(string));
            AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.FORM_ID, typeof(string));
            AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.MENU_NAME, typeof(string));
            AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.WINDOW_ID, typeof(SqlInt32));
            AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.USE_AUTH_ADD, typeof(bool));
            AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.USE_AUTH_EDIT, typeof(bool));
            AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.USE_AUTH_DELETE, typeof(bool));

            // メニューテーブル設定
            this.SetMenuTable(this.MenuTable);
        }

        #region メニューテーブル設定

        /// <summary>
        /// メニューテーブル設定
        /// </summary>
        /// <param name="table"></param>
        private void SetMenuTable(DataTable table)
        {
            // データテーブル設定
            foreach (var menuItem in this.MenuItems)
            {
                var groupItem = menuItem as r_framework.Menu.GroupItem;
                if (groupItem != null && !groupItem.Disabled)
                {
                    this.SetMenuTable(table, groupItem);
                }
            }
        }

        /// <summary>
        /// メニューテーブル設定
        /// </summary>
        /// <param name="table"></param>
        /// <param name="groupItem"></param>
        private void SetMenuTable(DataTable table, GroupItem groupItem)
        {
            foreach (var item in groupItem.SubItems)
            {
                var subItem = item as SubItem;
                if (subItem != null && !subItem.Disabled)
                {
                    this.SetMenuTable(
                        table,
                        groupItem,
                        subItem);
                }
            }
        }

        /// <summary>
        /// メニューテーブル設定
        /// </summary>
        /// <param name="table"></param>
        /// <param name="groupItem"></param>
        /// <param name="subItem"></param>
        private void SetMenuTable(DataTable table, GroupItem groupItem, SubItem subItem)
        {
            foreach (var item in subItem.AssemblyItems)
            {
                var assemblyItem = item as AssemblyItem;
                if (assemblyItem != null)
                {
                    // 画面IDがNULLの場合、飛ばす
                    // ※ログアウト等
                    if (string.IsNullOrEmpty(assemblyItem.FormID))
                    {
                        continue;
                    }

                    // 無効の場合は飛ばす
                    if (assemblyItem.Disabled)
                    {
                        continue;
                    }

                    // 表示させないメニューの場合は飛ばす
                    if (MenuKengenHoshuConstans.NotDispFormIdList.Contains(assemblyItem.FormID))
                    {
                        continue;
                    }

                    this.SetMenuTable(
                        table,
                        groupItem,
                        subItem,
                        assemblyItem);
                }
            }
        }

        /// <summary>
        /// メニューテーブル設定
        /// </summary>
        /// <param name="table"></param>
        /// <param name="groupItem"></param>
        /// <param name="subItem"></param>
        /// <param name="assemblyItem"></param>
        private void SetMenuTable(DataTable table, GroupItem groupItem, SubItem subItem, AssemblyItem assemblyItem)
        {
            var row = table.NewRow();

            row[MenuKengenHoshuConstans.KUBUN_NAME] = groupItem.Name;
            row[MenuKengenHoshuConstans.KINOU_NAME] = subItem.Name;
            row[MenuKengenHoshuConstans.FORM_ID] = assemblyItem.FormID;
            row[MenuKengenHoshuConstans.WINDOW_ID] = assemblyItem.WindowID;
            row[MenuKengenHoshuConstans.MENU_NAME] = assemblyItem.Name;
            row[MenuKengenHoshuConstans.USE_AUTH_ADD] = assemblyItem.UseAuthAdd;
            row[MenuKengenHoshuConstans.USE_AUTH_EDIT] = assemblyItem.UseAuthEdit;
            row[MenuKengenHoshuConstans.USE_AUTH_DELETE] = assemblyItem.UseAuthDelete;

            table.Rows.Add(row);
        }

        #endregion

        #endregion

        #region パターン一覧作成

        /// <summary>
        /// メニュー権限パターン一覧の作成
        /// </summary>
        /// <returns></returns>
        private int CreateIchiranPtEntry()
        {
            var sql = CreateSqlForIchiranPtEntry();

            var result = daoPtEntry.GetDateForStringSql(sql);

            this.form.Ichiran_PtEntry.IsBrowsePurpose = false;
            this.form.Ichiran_PtEntry.DataSource = result;
            this.form.Ichiran_PtEntry.IsBrowsePurpose = true;
            if (result.Rows.Count == 0)
            {
                this.form.Ichiran_PtDetail.DataSource = null;
            }

            this.SearchResultEntry = result;

            return result.Rows.Count;
        }

        /// <summary>
        /// メニュー権限パターンTBLのデータ抽出SQLを作成
        /// </summary>
        /// <returns></returns>
        private string CreateSqlForIchiranPtEntry()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM M_MENU_AUTH_PT_ENTRY")
              .Append(" WHERE DELETE_FLG = 0");

            // パターン名
            if (!string.IsNullOrEmpty(this.form.PATTERN_NAME.Text))
            {
                string sqlPatternName = SqlCreateUtility.CounterplanEscapeSequence(this.form.PATTERN_NAME.Text);
                sb.AppendFormat(" AND PATTERN_NAME LIKE '%{0}%'", sqlPatternName);
            }

            // フリガナ
            if (!string.IsNullOrEmpty(this.form.PATTERN_FURIGANA.Text))
            {
                string sqlPatternFurigana = SqlCreateUtility.CounterplanEscapeSequence(this.form.PATTERN_FURIGANA.Text);
                sb.AppendFormat(" AND PATTERN_FURIGANA LIKE '%{0}%'", sqlPatternFurigana);
            }

            sb.Append(" ORDER BY PATTERN_NAME, PATTERN_FURIGANA");

            return sb.ToString();
        }

        #endregion

        #region ファンクションボタン制御

        /// <summary>
        /// ファンクションボタン制御
        /// </summary>
        /// <param name="count">検索件数</param>
        private void SetEnabledFunctionButton(int count)
        {
            var enabled = (0 < count);
            var parentForm = (BasePopForm)this.form.Parent;

            parentForm.bt_func3.Enabled = enabled;
            parentForm.bt_func4.Enabled = enabled;
        }

        #endregion

        #endregion

        #region 検索条件クリア

        /// <summary>
        /// 検索条件クリア
        /// </summary>
        internal bool Clear()
        {
            try
            {
                this.form.PATTERN_NAME.Text = string.Empty;
                this.form.PATTERN_FURIGANA.Text = string.Empty;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Clear", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region 削除処理

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <returns></returns>
        internal bool Delete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var patternID = GetPatternID();
                if (patternID == 0)
                {
                    return false;
                }

                // M_MENU_AUTH_PT_ENTRYの生成
                M_MENU_AUTH_PT_ENTRY entity = null;
                {
                    // 選択中のM_MENU_AUTH_PT_ENTRYを取得(0 or 1件)
                    var rows = this.SearchResultEntry.AsEnumerable()
                                                     .Where(c => c[MenuKengenPtIchiranLogic.PATTERN_ID].Equals(patternID))
                                                     .ToList();

                    if (0 < rows.Count())
                    {
                        DataTable dt = rows.CopyToDataTable();
                        var entityList = EntityUtility.DataTableToEntity<M_MENU_AUTH_PT_ENTRY>(dt);

                        // 1件固定なので、先頭を取得
                        entity = entityList.First();

                        // TIME_STAMPだけ上手く設定出来ないため個別対応
                        if (!DBNull.Value.Equals(rows.First()["TIME_STAMP"]))
                        {
                            entity.TIME_STAMP = (byte[])rows.First()["TIME_STAMP"];
                        }
                    }
                }

                if (entity == null)
                {
                    return false;
                }

                // トランザクション開始
                using (var tran = new Transaction())
                {
                    var logic = new DataBinderLogic<M_MENU_AUTH_PT_ENTRY>(entity);
                    logic.SetSystemProperty(entity, false);

                    entity.DELETE_FLG = SqlBoolean.True;

                    daoPtEntry.Update(entity);

                    // トランザクション終了
                    tran.Commit();
                }

                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 一覧で選択しているパターンIDを取得
        /// </summary>
        /// <returns></returns>
        internal long GetPatternID()
        {
            long patternID = 0;

            var rowIndex = this.form.Ichiran_PtEntry.SelectedCells[0].RowIndex;
            var value = this.form.Ichiran_PtEntry.Rows[rowIndex].Cells[MenuKengenPtIchiranLogic.PATTERN_ID].Value;

            long id;

            if (value != null && long.TryParse(value.ToString(), out id))
            {
                patternID = id;
            }

            return patternID;
        }

        #endregion

        #region 検索処理(メニュー権限パターン)

        /// <summary>
        /// 検索処理(メニュー権限パターン)
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                var count = CreateIchiranPtEntry();

                // ファンクションボタン制御
                this.SetEnabledFunctionButton(count);

                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        #endregion

        #region パターン一覧詳細表示

        /// <summary>
        /// 検索処理(メニュー権限パターン詳細)
        /// </summary>
        internal bool SearchPtDetail()
        {
            try
            {
                if (this.form.Ichiran_PtEntry.SelectedCells.Count == 0)
                {
                    return false;
                }

                // パターンID取得
                var patternID = GetPatternID();

                // メニュー権限詳細データ取得
                var tblMenuAuthPtDetail = GetMenuAuthPtDetail(patternID);
                if (tblMenuAuthPtDetail == null || tblMenuAuthPtDetail.Rows.Count == 0)
                {
                    return false;
                }

                // メニュー権限データテーブル用初期化処理
                this.Init_ForAuthPtDetailDataTable(tblMenuAuthPtDetail);

                // メニュー権限データテーブル設定
                this.SetAuthPtDetailDataTable(patternID, tblMenuAuthPtDetail);

                this.SearchResultDetail = tblMenuAuthPtDetail;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchPtDetail", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// メニュー権限パターン詳細データ取得
        /// </summary>
        /// <param name="patternID">パターンID</param>
        /// <returns></returns>
        private DataTable GetMenuAuthPtDetail(long patternID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM M_MENU_AUTH_PT_DETAIL")
              .AppendFormat(" WHERE PATTERN_ID = {0}", patternID);

            var result = daoPtDetail.GetDateForStringSql(sb.ToString());

            return result;
        }

        /// <summary>
        /// メニュー権限パターン詳細データテーブル用初期化処理
        /// </summary>
        /// <param name="table"></param>
        private void Init_ForAuthPtDetailDataTable(DataTable table)
        {
            // 表示用カラムを追加
            AddColumn_ForDataTable(table, MenuKengenHoshuConstans.KUBUN_NAME, typeof(string));
            AddColumn_ForDataTable(table, MenuKengenHoshuConstans.KINOU_NAME, typeof(string));
            AddColumn_ForDataTable(table, MenuKengenHoshuConstans.MENU_NAME, typeof(string));
            AddColumn_ForDataTable(table, MenuKengenHoshuConstans.AUTH_ALL, typeof(bool));
            AddColumn_ForDataTable(table, MenuKengenHoshuConstans.ROW_NO, typeof(int));

            // DBNull許容設定
            SetAllowDBNull_ForDataTable(table, MenuKengenHoshuConstans.TIME_STAMP, true);

            // Unique設定
            SetUnique_ForDataTable(table, MenuKengenHoshuConstans.TIME_STAMP, false);
        }

        #endregion

        #region メニュー権限パターン詳細データテーブル設定

        /// <summary>
        /// メニュー権限データテーブル設定
        /// </summary>
        /// <param name="patternID"></param>
        /// <param name="table"></param>
        private void SetAuthPtDetailDataTable(long patternID, DataTable table)
        {
            // 新規追加行の為、クローンテーブル作成
            var tblNewData = table.Clone();

            // 行番号初期化
            this.rowNo = 0;

            // データテーブル設定
            foreach (DataRow menuRow in this.MenuTable.Rows)
            {
                this.SetAuthPtDetailDataTable(patternID, table, tblNewData, menuRow);
            }

            //// 新規/編集フラグを初期化
            //// ※GetChangesの変更分取得の為
            //table.AcceptChanges();

            // 新規追加行保存用テーブルから新規追加行を加える
            // ※GetChangesで新規行と見なされるように
            table.Clear();
            foreach (DataRow row in tblNewData.Rows)
            {
                table.ImportRow(row);
            }

            // 新規/編集フラグを初期化
            // ※GetChangesの変更分取得の為
            // ※Insertに時間がかかる為、変更分のみ追加する場合
            table.AcceptChanges();
        }

        /// <summary>
        /// メニュー権限データテーブル設定
        /// </summary>
        /// <param name="patternID"></param>
        /// <param name="table"></param>
        /// <param name="tblNewData">新規追加行保存用テーブル</param>
        /// <param name="menuRow"></param>
        private void SetAuthPtDetailDataTable(
            long patternID,
            DataTable table,
            DataTable tblNewData,
            DataRow menuRow)
        {
            var formID = GetString_ByObject(menuRow[MenuKengenHoshuConstans.FORM_ID]);
            var windowID = GetNullableInt_ByObject(menuRow[MenuKengenHoshuConstans.WINDOW_ID]);
            var kubunName = GetString_ByObject(menuRow[MenuKengenHoshuConstans.KUBUN_NAME]);
            var kinouName = GetString_ByObject(menuRow[MenuKengenHoshuConstans.KINOU_NAME]);
            var menuName = GetString_ByObject(menuRow[MenuKengenHoshuConstans.MENU_NAME]);

            // 対象の画面ID(&WindowID)データが存在するかをチェック
            DataRow[] rows;
            if (FindDataRows_ByFormID(table, formID, windowID, out rows))
            {
                // 対象の画面ID(&WindowID)データが存在する
                foreach (var row in rows)
                {
                    row[MenuKengenHoshuConstans.KUBUN_NAME] = kubunName;
                    row[MenuKengenHoshuConstans.KINOU_NAME] = kinouName;
                    row[MenuKengenHoshuConstans.MENU_NAME] = menuName;
                    SetToAllCheck_ForDataRow(row);
                    this.rowNo++;
                    row[MenuKengenHoshuConstans.ROW_NO] = this.rowNo;

                    var newRow = tblNewData.NewRow();
                    newRow[MenuKengenPtIchiranLogic.PATTERN_ID] = patternID;
                    newRow[MenuKengenHoshuConstans.FORM_ID] = formID;
                    newRow[MenuKengenHoshuConstans.WINDOW_ID] = windowID.HasValue ? windowID.Value : -1;
                    newRow[MenuKengenHoshuConstans.KUBUN_NAME] = row[MenuKengenHoshuConstans.KUBUN_NAME];
                    newRow[MenuKengenHoshuConstans.KINOU_NAME] = row[MenuKengenHoshuConstans.KINOU_NAME];
                    newRow[MenuKengenHoshuConstans.MENU_NAME] = row[MenuKengenHoshuConstans.MENU_NAME];
                    newRow[MenuKengenHoshuConstans.AUTH_ADD] = row[MenuKengenHoshuConstans.AUTH_ADD];
                    newRow[MenuKengenHoshuConstans.AUTH_READ] = row[MenuKengenHoshuConstans.AUTH_READ];
                    newRow[MenuKengenHoshuConstans.AUTH_EDIT] = row[MenuKengenHoshuConstans.AUTH_EDIT];
                    newRow[MenuKengenHoshuConstans.AUTH_DELETE] = row[MenuKengenHoshuConstans.AUTH_DELETE];
                    newRow[MenuKengenHoshuConstans.BIKOU] = row[MenuKengenHoshuConstans.BIKOU];
                    SetToAllCheck_ForDataRow(newRow);
                    newRow[MenuKengenHoshuConstans.ROW_NO] = this.rowNo;
                    tblNewData.Rows.Add(newRow);
                }
            }
            else
            {
                // 対象の画面ID(&WindowID)データが存在しない
                var newRow = tblNewData.NewRow();
                newRow[MenuKengenPtIchiranLogic.PATTERN_ID] = patternID;
                newRow[MenuKengenHoshuConstans.FORM_ID] = formID;
                newRow[MenuKengenHoshuConstans.WINDOW_ID] = windowID.HasValue ? windowID.Value : -1;
                newRow[MenuKengenHoshuConstans.KUBUN_NAME] = kubunName;
                newRow[MenuKengenHoshuConstans.KINOU_NAME] = kinouName;
                newRow[MenuKengenHoshuConstans.MENU_NAME] = menuName;
                newRow[MenuKengenHoshuConstans.AUTH_ADD] = true;
                newRow[MenuKengenHoshuConstans.AUTH_READ] = true;
                newRow[MenuKengenHoshuConstans.AUTH_EDIT] = true;
                newRow[MenuKengenHoshuConstans.AUTH_DELETE] = true;
                SetToAllCheck_ForDataRow(newRow);
                this.rowNo++;
                newRow[MenuKengenHoshuConstans.ROW_NO] = this.rowNo;
                tblNewData.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// データテーブルに対象の画面IDが存在するか
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="formID">画面ID</param>
        /// <param name="windowID">ウィンドウID</param>
        /// <param name="rows">データ行</param>
        /// <returns>true:存在する／false:存在しない</returns>
        private bool FindDataRows_ByFormID(DataTable table, string formID, int? windowID, out DataRow[] rows)
        {
            var sbFilter = new StringBuilder(256);
            sbFilter.AppendFormat("{0} = '{1}'", MenuKengenHoshuConstans.FORM_ID, formID);
            if (windowID.HasValue)
            {
                // ウィンドウIDが存在する場合、検索条件に加える
                sbFilter.Append(" AND ");
                sbFilter.AppendFormat("{0} = {1}", MenuKengenHoshuConstans.WINDOW_ID, windowID.Value);
            }
            rows = table.Select(sbFilter.ToString());
            return 0 < rows.Length;
        }

        /// <summary>
        /// データ行用一括チェック設定
        /// </summary>
        /// <param name="eRow">データ行</param>
        private void SetToAllCheck_ForDataRow(DataRow row)
        {
            if (true.Equals(row[MenuKengenHoshuConstans.AUTH_ADD]) &&
                true.Equals(row[MenuKengenHoshuConstans.AUTH_READ]) &&
                true.Equals(row[MenuKengenHoshuConstans.AUTH_EDIT]) &&
                true.Equals(row[MenuKengenHoshuConstans.AUTH_DELETE]))
            {
                row[MenuKengenHoshuConstans.AUTH_ALL] = true;
            }
            else
            {
                row[MenuKengenHoshuConstans.AUTH_ALL] = false;
            }
        }

        #endregion

        #region 検索結果設定

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiranDetail()
        {
            try
            {
                var table = this.SearchResultDetail;
                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }
                // DataViewとソート設定
                DataView dv = new DataView(table);
                dv.Sort = MenuKengenHoshuConstans.ROW_NO;
                this.form.Ichiran_PtDetail.DataSource = dv;

                // チェックボックス押下不可項目設定処理
                this.SetIchiranCheckBoxEnabled();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranDetail", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region メニュー一覧 - チェックボックス操作制御

        /// <summary>
        /// メニュー項目によって権限設定チェックボックス操作可能/不可能かの設定を行います
        /// </summary>
        internal void SetIchiranCheckBoxEnabled()
        {
            if (this.form.Ichiran_PtDetail == null || this.form.Ichiran_PtDetail.Rows.Count == 0)
            {
                return;
            }

            int i = 0;
            DataView dv = this.GetCloneDataView((DataView)this.form.Ichiran_PtDetail.DataSource);
            foreach (DataRowView row in dv)
            {
                // メニューテーブルより該当データを検索
                string formId = row[MenuKengenHoshuConstans.FORM_ID].ToString();
                string windowId = row[MenuKengenHoshuConstans.WINDOW_ID].ToString();
                string selectString = "FORM_ID = '" + formId + "' AND WINDOW_ID = " + windowId;
                DataRow[] dataRowList = MenuTable.Select(selectString);

                // 複数取得されるが、同画面を別大分類に配置しているだけなので先頭行を使用
                // ※同メニューが同じ設定の前提(例：G055 - 伝票一覧は2個定義されているがどちらも使用出来るのは参照のみ)
                // もし同メニューでも使用可能項目の定義が違うのであれば、大・中分類を使用して判定すれば一意になるはず
                bool isUseAdd = dataRowList[0][MenuKengenHoshuConstans.USE_AUTH_ADD].ToString() == bool.TrueString;
                bool isUseEdit = dataRowList[0][MenuKengenHoshuConstans.USE_AUTH_EDIT].ToString() == bool.TrueString;
                bool isUseDelete = dataRowList[0][MenuKengenHoshuConstans.USE_AUTH_DELETE].ToString() == bool.TrueString;

                // GcCustomCheckBoxCell.AutoChangeBackColorEnabledプロパティはデザインで設定しても反映されないので、ここで設定
                // 一括
                {
                    var cell = this.form.Ichiran_PtDetail.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ALL];
                    var checxBoxCell = cell as GcCustomCheckBoxCell;
                    if (checxBoxCell != null)
                    {
                        checxBoxCell.AutoChangeBackColorEnabled = false;
                        checxBoxCell.ReadOnly = true;
                    }
                }

                // 参照
                {
                    var cell = this.form.Ichiran_PtDetail.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_READ];
                    var checxBoxCell = cell as GcCustomCheckBoxCell;
                    if (checxBoxCell != null)
                    {
                        checxBoxCell.AutoChangeBackColorEnabled = false;
                        checxBoxCell.ReadOnly = true;
                    }
                }

                // 新規,修正,削除
                CtrlCheckBoxCell(i, MenuKengenHoshuConstans.AUTH_ADD, isUseAdd);
                CtrlCheckBoxCell(i, MenuKengenHoshuConstans.AUTH_EDIT, isUseEdit);
                CtrlCheckBoxCell(i, MenuKengenHoshuConstans.AUTH_DELETE, isUseDelete);

                // モバイルの場合、一括チェックの背景色を変更する。
                if (formId.StartsWith("MOBILE"))
                {
                    CtrlCheckBoxCell(i, MenuKengenHoshuConstans.AUTH_ALL, false);
                }

                // 列一括チェックを再設定
                SetToAllCheck_Row(this.form.Ichiran_PtDetail.Rows[i], formId);

                i++;
            }
        }

        /// <summary>
        /// チェックボックスセルのコントロール設定
        /// </summary>
        /// <param name="index">対象の行数</param>
        /// <param name="cellName">対象セル名</param>
        /// <param name="isUsed">権限の設定可否</param>
        private void CtrlCheckBoxCell(int index, string cellName, bool isUsed)
        {
            var cell = this.form.Ichiran_PtDetail.Rows[index].Cells[cellName];

            cell.Enabled = isUsed;
            cell.ReadOnly = isUsed;

            var checxBoxCell = cell as GcCustomCheckBoxCell;
            if (checxBoxCell != null)
            {
                checxBoxCell.AutoChangeBackColorEnabled = !isUsed;
            }

            if (!isUsed)
            {
                cell.Value = false;
            }
        }

        /// <summary>
        /// 一括チェック処理（行）
        /// [参照/新規/修正/削除] ⇒ [一括]
        /// </summary>
        /// <param name="eRow">行</param>
        private void SetToAllCheck_Row(Row row, string formId)
        {
            if (formId.StartsWith("MOBILE"))
            {
                row[MenuKengenHoshuConstans.AUTH_ALL].Value = false;
                return;
            }

            if ((true.Equals(row[MenuKengenHoshuConstans.AUTH_ADD].Value) || !row[MenuKengenHoshuConstans.AUTH_ADD].Enabled) &&
                (true.Equals(row[MenuKengenHoshuConstans.AUTH_READ].Value) || !row[MenuKengenHoshuConstans.AUTH_READ].Enabled) &&
                (true.Equals(row[MenuKengenHoshuConstans.AUTH_EDIT].Value) || !row[MenuKengenHoshuConstans.AUTH_EDIT].Enabled) &&
                (true.Equals(row[MenuKengenHoshuConstans.AUTH_DELETE].Value) || !row[MenuKengenHoshuConstans.AUTH_DELETE].Enabled))
            {
                row[MenuKengenHoshuConstans.AUTH_ALL].Value = true;
            }
            else
            {
                row[MenuKengenHoshuConstans.AUTH_ALL].Value = false;
            }
        }

        #endregion

        #region データテーブルへカラム追加、Null許容、Unique操作処理

        /// <summary>
        /// データテーブルへカラム追加
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="Type">データタイプ</param>
        private void AddColumn_ForDataTable(DataTable table, string columnName, Type type)
        {
            if (!table.Columns.Contains(columnName))
            {
                table.Columns.Add(columnName, type);
            }
        }

        /// <summary>
        /// データテーブルへDBNull許容設定を行う
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="isAllowDBNull">DBNull許容</param>
        private void SetAllowDBNull_ForDataTable(DataTable table, string columnName, bool isAllowDBNull)
        {
            if (table.Columns.Contains(columnName))
            {
                table.Columns[columnName].AllowDBNull = isAllowDBNull;
            }
        }

        /// <summary>
        /// データテーブルへUnique設定を行う
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="isUnique">Unique</param>
        private void SetUnique_ForDataTable(DataTable table, string columnName, bool isUnique)
        {
            if (table.Columns.Contains(columnName))
            {
                table.Columns[columnName].Unique = isUnique;
            }
        }

        #endregion

        #region Utility

        /// <summary>
        /// 文字列取得
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetString_ByObject(object value)
        {
            var result = value as string;
            if (result == null)
            {
                result = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// NULL許容INT取得
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int? GetNullableInt_ByObject(object value)
        {
            int? result = null;
            int iTemp;
            if (value != null && int.TryParse(value.ToString(), out iTemp))
            {
                result = iTemp;
            }
            return result;
        }

        #endregion

        #region - Get Clone -

        /// <summary>
        /// DataViewのクローン処理
        /// </summary>
        /// <param name="dv"></param>
        /// <returns></returns>
        private DataView GetCloneDataView(DataView dv)
        {
            // テーブル情報をコピー
            DataTable table = this.GetCloneDataTable(dv.Table);
            DataView view = new DataView(table);
            view.Sort = dv.Sort;    // ソート情報

            return view;
        }

        /// <summary>
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable(DataTable dt)
        {
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            return table;
        }

        #endregion

        #region 未使用

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}