// $Id: MenuKengenHoshuLogic.cs 53174 2015-06-23 04:07:43Z wuq@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using MenuKengenHoshu.APP;
using MenuKengenHoshu.Const;
using MenuKengenHoshu.Dto;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Menu;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;
using Seasar.Dao;
using r_framework.Dto;
using r_framework.Configuration;

namespace MenuKengenHoshu.Logic
{
    /// <summary>
    /// 社員保守画面のビジネスロジック
    /// </summary>
    public class MenuKengenHoshuLogic : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>
        /// ボタン設定XMLパス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "MenuKengenHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// 一覧取得用SQL（社員毎）
        /// </summary>
        private readonly string GET_ICHIRAN_SHAIN_AUTH_DATA_SQL = "MenuKengenHoshu.Sql.GetIchiranDataShainSql.sql";

        /// <summary>
        /// メニュー権限データ存在確認用SQL
        /// </summary>
        private readonly string GET_ICHIRAN_AUTH_DATACHECK_SQL = "MenuKengenHoshu.Sql.GetIchiranDataCheckSql.sql";

        /// <summary>
        /// メニュー権限登録前チェックSQL
        /// </summary>
        private readonly string CHECK_REGIST_DATA_SQL = "MenuKengenHoshu.Sql.CheckRegistDataSql.sql";

        /// <summary>
        /// メニュー権限保守画面Form
        /// </summary>
        private MenuKengenHoshuForm form;

        /// <summary>
        /// メニュー権限のDao
        /// </summary>
        private IM_MENU_AUTHDao daoAuth;

        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao daoShain;

        /// <summary>
        /// 部署Dao
        /// </summary>
        private IM_BUSHODao daoBusho;

        /// <summary>
        /// 検索条件（メニュー権限）
        /// </summary>
        private M_MENU_AUTH entMenuAuth_ForSeach;

        /// <summary>
        /// 検索条件（社員）
        /// </summary>
        private M_SHAIN entShain_ForSeach;

        /// <summary>
        /// メニュー権限保守画面のDTO
        /// </summary>
        private MenuKengenHoshuDto dto = new MenuKengenHoshuDto();

        /// <summary>
        /// メニューアイテムリスト
        /// リボンフォームより取得
        /// </summary>
        private List<r_framework.Menu.MenuItemComm> menuItems;

        /// <summary>
        /// メニューテーブル
        /// </summary>
        private DataTable MenuTable;

        /// <summary>
        /// 行番号
        /// </summary>
        private int rowNo;

        /// <summary>
        /// パターン番号
        /// </summary>
        private long patternID;

        /// <summary>画面表示位置記憶用 - パターン名ラベル</summary>
        private System.Drawing.Point lblPatternPoint;

        /// <summary>画面表示位置記憶用 - パターン名</summary>
        private System.Drawing.Point patternNamePoint;

        /// <summary>画面表示位置記憶用 - 一覧(メニュー)Location</summary>
        private System.Drawing.Point ichiranPoint;

        /// <summary>メニュー権限設定差分用 検索後の一覧データ</summary>
        private DataView prevIchiranDataView = new DataView();

        #endregion - Fields -

        #region - Properties -

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        #endregion - Properties -

        #region - Constructor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public MenuKengenHoshuLogic(MenuKengenHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フォーム
            this.form = targetForm;

            // DAO
            this.daoAuth = DaoInitUtility.GetComponent<IM_MENU_AUTHDao>();
            this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.daoBusho = DaoInitUtility.GetComponent<IM_BUSHODao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructor -

        #region - Method -

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // メニューアイテムクラス取得
                this.GetMenuItems();

                // メニューテーブル作成
                this.CreateMenuTable();

                // 前回設定値取得
                this.GetPrevData();

                // 画面切替用項目情報記憶
                this.lblPatternPoint = this.form.LBL_PATTERN.Location;
                this.patternNamePoint = this.form.PATTERN_NAME.Location;
                this.ichiranPoint = this.form.Ichiran.Location;

                // 社員一覧作成
                this.CreateIchiranShain();

                // 検索条件初期化
                this.entMenuAuth_ForSeach = new M_MENU_AUTH();
                this.entShain_ForSeach = new M_SHAIN();

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M188", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }

                /* 一覧が見えなくなるほどWindowを小さくした場合、F1ボタンで一覧の描画を変更した際に画面のリサイズが */
                /* 上手くいかないので暫定として一覧が見える程度のMinimumSizeを設定する。                            */
                var parentForm = (MasterBaseForm)this.form.Parent;
                parentForm.MinimumSize = new System.Drawing.Size(558, 385);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInit", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
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
            var parentForm = (MasterBaseForm)this.form.Parent;
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

        #endregion FunctionButton初期化

        #region イベント初期化

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            // メニューボタン(F1)イベント生成
            parentForm.bt_func1.Click -= new EventHandler(this.form.ModeChange);
            parentForm.bt_func1.Click += new EventHandler(this.form.ModeChange);

            // CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click -= new EventHandler(this.form.CSV);
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            // 検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click -= new EventHandler(this.form.Search);
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            // 登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click -= new EventHandler(this.form.Regist);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            // 取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click -= new EventHandler(this.form.Cancel);
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click -= new EventHandler(this.form.FormClose);
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // パターン登録
            parentForm.bt_process1.Click -= new EventHandler(this.form.PatternRegist);
            parentForm.bt_process1.Click += new EventHandler(this.form.PatternRegist);

            // パターン呼出
            parentForm.bt_process2.Click -= new EventHandler(this.form.PatternCall);
            parentForm.bt_process2.Click += new EventHandler(this.form.PatternCall);
        }

        #endregion イベント初期化

        #region メニューアイテムクラス取得

        /// <summary>
        /// メニューアイテムクラス取得
        /// </summary>
        private void GetMenuItems()
        {
            MasterBaseForm baseForm;
            r_framework.APP.Base.RibbonMainMenu ribbonForm;

            baseForm = this.form.Parent as MasterBaseForm;
            if (baseForm != null)
            {
                ribbonForm = baseForm.ribbonForm as r_framework.APP.Base.RibbonMainMenu;
                if (ribbonForm != null)
                {
                    this.menuItems = ribbonForm.menuItems;

                    // モバイル将軍連携オプションがONの場合のみ、メニューに追加する。
                    if (AppConfig.AppOptions.IsMobile())
                    {
                        this.menuItems.Add(this.SetMobileItems());
                    }
                }
            }
        }

        /// <summary>
        /// モバイル将軍のメニューを設定する。
        /// </summary>
        /// <returns></returns>
        private GroupItem SetMobileItems()
        {
            // 区分（固定）
            GroupItem group = new GroupItem();
            group.IndexNo = 99;
            group.Name = "モバイル将軍";
            group.Disabled = false;
            group.SubItems = new List<SubItem>();

            // 機能（固定）
            SubItem sub = new SubItem();
            sub.IndexNo = 99;
            sub.Name = "ﾒｲﾝﾒﾆｭｰ表示設定";
            sub.Disabled = false;
            sub.AssemblyItems = new List<AssemblyItem>();

            // メニュー(11要素固定)
            for (int i = 1; i < 12; i++)
            {
                AssemblyItem item = new AssemblyItem();

                switch (i)
                {
                    case 1:
                        item.FormID = "MOBILE001";
                        item.IndexNo = i;
                        item.Name = "回収実績入力";
                        item.Disabled = false;
                        break;

                    case 2:
                        item.FormID = "MOBILE002";
                        item.IndexNo = i;
                        item.Name = "搬入実績登録";
                        item.Disabled = false;
                        break;

                    case 3:
                        item.FormID = "MOBILE003";
                        item.IndexNo = i;
                        item.Name = "現場メモ";
                        item.Disabled = false;
                        break;

                    case 4:
                        item.FormID = "MOBILE004";
                        item.IndexNo = i;
                        item.Name = "作業代行設定";
                        item.Disabled = false;
                        break;

                    case 5:
                        item.FormID = "MOBILE005";
                        item.IndexNo = i;
                        item.Name = "受入実績登録";
                        item.Disabled = false;
                        break;

                    case 6:
                        item.FormID = "MOBILE006";
                        item.IndexNo = i;
                        item.Name = "受入実績一覧";
                        item.Disabled = false;
                        break;

                    case 7:
                        item.FormID = "MOBILE007";
                        item.IndexNo = i;
                        item.Name = "配車状況";
                        item.Disabled = false;
                        break;

                    case 8:
                        item.FormID = "MOBILE008";
                        item.IndexNo = i;
                        item.Name = "稼働状況";
                        item.Disabled = false;
                        break;

                    case 9:
                        item.FormID = "MOBILE009";
                        item.IndexNo = i;
                        item.Name = "委託契約";
                        item.Disabled = false;
                        break;

                    case 10:
                        item.FormID = "MOBILE010";
                        item.IndexNo = i;
                        item.Name = "新着メッセージ";
                        item.Disabled = false;
                        break;

                    case 11:
                        item.FormID = "MOBILE011";
                        item.IndexNo = i;
                        item.Name = "システム設定";
                        item.Disabled = false;
                        break;

                    default:
                        break;
                }

                sub.AssemblyItems.Add(item);
            }
            
            group.SubItems.Add(sub);

            return group;
        }

        #endregion メニューアイテムクラス取得

        #region メニューテーブル作成

        /// <summary>
        /// メニューテーブル作成
        /// </summary>
        private void CreateMenuTable()
        {
            this.MenuTable = new DataTable();

            MenuKengenHoshuLogic.AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.KUBUN_NAME, typeof(string));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.KINOU_NAME, typeof(string));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.FORM_ID, typeof(string));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.MENU_NAME, typeof(string));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.WINDOW_ID, typeof(SqlInt32));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.USE_AUTH_ADD, typeof(bool));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.USE_AUTH_EDIT, typeof(bool));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(this.MenuTable, MenuKengenHoshuConstans.USE_AUTH_DELETE, typeof(bool));

            // メニューテーブル作成
            this.SetMenuTable(this.MenuTable);
        }

        #region メニューテーブル作成

        /// <summary>
        /// メニューテーブル作成
        /// </summary>
        /// <param name="table"></param>
        private void SetMenuTable(DataTable table)
        {
            // データテーブル設定
            foreach (var menuItem in this.menuItems)
            {
                var groupItem = menuItem as r_framework.Menu.GroupItem;
                if (groupItem != null && !groupItem.Disabled)
                {
                    this.SetMenuTable(table, groupItem);
                }
            }
        }

        /// <summary>
        /// メニューテーブル作成
        /// </summary>
        /// <param name="table"></param>
        /// <param name="groupItem"></param>
        private void SetMenuTable(DataTable table, GroupItem groupItem)
        {
            foreach (var item in groupItem.SubItems)
            {
                var subItem = item as r_framework.Menu.SubItem;
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
        /// メニューテーブル作成
        /// </summary>
        /// <param name="table"></param>
        /// <param name="groupItem"></param>
        /// <param name="subItem"></param>
        private void SetMenuTable(DataTable table, GroupItem groupItem, SubItem subItem)
        {
            // マイメニューの場合は飛ばす
            if (subItem.Name.Equals("マイメニュー"))
            {
                return;
            }

            foreach (var item in subItem.AssemblyItems)
            {
                var assemblyItem = item as r_framework.Menu.AssemblyItem;
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
        /// メニューテーブル作成
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

        #endregion メニューテーブル作成

        #endregion メニューテーブル作成

        #region 前回設定値取得

        /// <summary>
        /// 前回設定値取得
        /// </summary>
        public void GetPrevData()
        {
            bool catchErr = false;
            // 部署
            this.form.BUSHO_CD.Text = Properties.Settings.Default.BUSHO_CD_Text;
            if (!string.IsNullOrEmpty(this.form.BUSHO_CD.Text))
            {
                var strBushoName = string.Empty;
                if (this.GetBushoName(this.form.BUSHO_CD.Text, out strBushoName, out catchErr) && !catchErr)
                {
                    this.form.BUSHO_NAME_RYAKU.Text = strBushoName;
                }
                if (catchErr)
                {
                    throw new Exception("");
                }
                if (this.form.BUSHO_CD.Text.Equals("999"))
                {
                    this.form.BUSHO_CD_POPUP.Text = string.Empty;
                }
                else
                {
                    this.form.BUSHO_CD_POPUP.Text = this.form.BUSHO_CD.Text;
                }
            }
            else
            {
                this.form.BUSHO_CD_POPUP.Text = string.Empty;
            }

            // 社員
            this.form.SHAIN_CD.Text = Properties.Settings.Default.SHAIN_CD_Text;
            if (!string.IsNullOrEmpty(this.form.SHAIN_CD.Text))
            {
                var strShainName = string.Empty;
                var strBushoCD = this.form.BUSHO_CD.Text;

                if (this.form.BUSHO_CD.Text.Equals("999"))
                {
                    strBushoCD = null;
                }

                if (this.GetShainName(this.form.SHAIN_CD.Text, ref strBushoCD, out strShainName, out catchErr) && !catchErr)
                {
                    this.form.BUSHO_CD_POPUP.Text = strBushoCD;
                    this.form.SHAIN_NAME_RYAKU.Text = strShainName;
                }
                else
                {
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    this.form.SHAIN_CD.Text = string.Empty;
                }
            }
        }

        #endregion 前回設定値取得

        #region 社員一覧作成

        /// <summary>
        /// 社員一覧を作成します
        /// </summary>
        private void CreateIchiranShain()
        {
            // 検索
            M_SHAIN entity = new M_SHAIN();
            M_SHAIN[] shainData = daoShain.GetAllValidData(entity);

            if (shainData == null || shainData.Length == 0)
            {
                return;
            }

            this.form.Ichiran_Shain.IsBrowsePurpose = false;
            this.form.Ichiran_Shain.CausesValidation = false;
            // 一覧作成
            Array.Sort(shainData, delegate(M_SHAIN x, M_SHAIN y) { return x.SHAIN_CD.CompareTo(y.SHAIN_CD); });
            this.form.Ichiran_Shain.Rows.Clear();
            for (int i = 0; i < shainData.Length; i++)
            {
                this.form.Ichiran_Shain.Rows.Add();
                this.form.Ichiran_Shain.Rows[i].Cells[MenuKengenHoshuConstans.CELL_SHAIN_CD].Value = shainData[i].SHAIN_CD;
                this.form.Ichiran_Shain.Rows[i].Cells[MenuKengenHoshuConstans.CELL_SHAIN_NAME].Value = shainData[i].SHAIN_NAME_RYAKU;
                this.form.Ichiran_Shain.Rows[i].Cells[MenuKengenHoshuConstans.CELL_BUSHO_CD].Value = shainData[i].BUSHO_CD;
            }
            this.form.Ichiran_Shain.CausesValidation = true;
            this.form.Ichiran_Shain.IsBrowsePurpose = true;
        }

        #endregion 社員一覧作成

        #endregion 画面初期化処理

        #region 検索条件初期化

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        internal bool ClearCondition()
        {
            try
            {
                if (this.form.MenuKbn == MenuKengenHoshuConstans.MENU_KBN_SINGLE)
                {
                    // 部署の初期化
                    this.form.BUSHO_CD.Text = string.Empty;
                    this.form.BUSHO_NAME_RYAKU.Text = string.Empty;
                    this.form.BUSHO_CD_POPUP.Text = string.Empty;

                    // 社員の初期化
                    this.form.SHAIN_CD.Text = string.Empty;
                    this.form.SHAIN_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    // 検索用社員の初期化
                    this.form.SEARCH_SHAIN_CD.Text = string.Empty;
                    this.form.SEARCH_SHAIN_NAME.Text = string.Empty;

                    // 社員一覧の初期化
                    this.form.Ichiran_Shain.ColumnHeaders[0][MenuKengenHoshuConstans.HD_SHAIN_ALL_CHECK].Value = false;
                    foreach (var row in this.form.Ichiran_Shain.Rows)
                    {
                        row.Cells[MenuKengenHoshuConstans.SHAIN_CHECK].Value = false;
                    }
                }

                // パターンの初期化
                this.form.PATTERN_NAME.Text = string.Empty;

                // 一覧の初期化
                bool catchErr = this.ClearIchiran();
                return catchErr;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion 検索条件初期化

        #region メニュー一覧初期化

        /// <summary>
        /// メニュー一覧の初期化
        /// </summary>
        internal bool ClearIchiran()
        {
            try
            {
                this.SearchResult = null;
                this.form.Ichiran.DataSource = this.SearchResult;

                this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_READ].Value = false;
                this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_ADD].Value = false;
                this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_EDIT].Value = false;
                this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_DELETE].Value = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion メニュー一覧初期化

        #region 前回値保存

        /// <summary>
        /// 前回設定値保存
        /// </summary>
        public bool SetPrevData()
        {
            try
            {
                // 部署
                Properties.Settings.Default.BUSHO_CD_Text = this.form.BUSHO_CD.Text;

                // 社員
                Properties.Settings.Default.SHAIN_CD_Text = this.form.SHAIN_CD.Text;

                // 保存
                Properties.Settings.Default.Save();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetPrevData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion 前回値保存

        #region 参照モード表示

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.Ichiran.ReadOnly = true;

            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func1.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func9.Enabled = false;
            parentForm.bt_process1.Enabled = false;
            parentForm.bt_process2.Enabled = false;
            parentForm.txb_process.Enabled = false;
        }

        #endregion 参照モード表示

        #region メニュー区分(個人/複数)変更処理

        /// <summary>
        /// メニュー区分変更処理
        /// </summary>
        public bool ChangeMenuKbn()
        {
            try
            {
                // 現在の設定が個人の場合
                if (this.form.MenuKbn == MenuKengenHoshuConstans.MENU_KBN_SINGLE)
                {
                    // 区分を切り替え設定
                    this.form.MenuKbn = MenuKengenHoshuConstans.MENU_KBN_MULTIPLE;
                }
                // 現在の設定がメニュー毎の場合
                else
                {
                    // 区分を切り替え設定
                    this.form.MenuKbn = MenuKengenHoshuConstans.MENU_KBN_SINGLE;
                }

                // 一覧初期化
                bool catchErr = this.ClearIchiran();
                if (catchErr)
                {
                    return true;
                }

                // 画面表示変更処理
                catchErr = this.ChangeDisplay();
                return catchErr;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeMenuKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion メニュー区分(個人/複数)変更処理

        #region メニュー区分変更による画面表示変更処理

        /// <summary>
        /// 画面表示変更処理
        /// </summary>
        public bool ChangeDisplay()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.form.Parent;

                if (this.form.MenuKbn == MenuKengenHoshuConstans.MENU_KBN_SINGLE)
                {
                    /* 検索条件の表示切替（個人）*/

                    // 画面表示項目切替
                    this.form.LBL_BUSHO.Visible = true;
                    this.form.BUSHO_CD.Visible = true;
                    this.form.BUSHO_NAME_RYAKU.Visible = true;
                    this.form.LBL_SHAIN.Visible = true;
                    this.form.SHAIN_CD.Visible = true;
                    this.form.SHAIN_NAME_RYAKU.Visible = true;
                    this.form.LBL_SEARCH_SHAIN.Visible = false;
                    this.form.SEARCH_SHAIN_CD.Visible = false;
                    this.form.SEARCH_SHAIN_NAME.Visible = false;
                    this.form.Ichiran_Shain.Visible = false;

                    // 項目表示位置の変更
                    // パターン名ラベル
                    System.Drawing.Point lblSearchShainPoint = this.form.LBL_SEARCH_SHAIN.Location;
                    this.form.LBL_PATTERN.Location = lblSearchShainPoint;
                    // パターン名
                    System.Drawing.Point searchShainCdPoint = this.form.SEARCH_SHAIN_CD.Location;
                    this.form.PATTERN_NAME.Location = searchShainCdPoint;
                    // 一覧(メニュー)
                    System.Drawing.Point ichiranShainPoint = this.form.Ichiran_Shain.Location;
                    System.Drawing.Size ichiranShainSize = this.form.Ichiran_Shain.Size;
                    System.Drawing.Point ichiranPoint = this.form.Ichiran.Location;
                    System.Drawing.Size newIchiranSize = new System.Drawing.Size(this.form.Ichiran.Size.Width + (ichiranPoint.X - ichiranShainPoint.X), this.form.Ichiran.Size.Height);
                    this.form.Ichiran.Location = ichiranShainPoint;
                    this.form.Ichiran.Size = newIchiranSize;

                    // FunctionButton
                    parentForm.bt_func1.Text = "[F1]" + Environment.NewLine + "複数";
                    parentForm.bt_func6.Text = "[F6]" + Environment.NewLine + "CSV";
                    parentForm.bt_func6.Enabled = true;
                    parentForm.bt_func8.Text = "[F8]" + Environment.NewLine + "検索";
                    parentForm.bt_func8.Enabled = true;

                    // メニュー一覧 - 複数時のテンプレート操作を戻すために個人の場合はWidthを変えず再読
                    MultiRowTemplate.MenuKengenHoshuDetail template = new MultiRowTemplate.MenuKengenHoshuDetail();
                    this.form.Ichiran.Template = template;

                    if (!string.IsNullOrEmpty(this.form.BUSHO_CD.Text) &&
                        !string.IsNullOrEmpty(this.form.SHAIN_CD.Text))
                    {
                        this.form.Search(null, null);
                    }
                }
                else
                {
                    /* 検索条件の表示切替（複数）*/

                    // 画面表示項目切替
                    this.form.LBL_BUSHO.Visible = false;
                    this.form.BUSHO_CD.Visible = false;
                    this.form.BUSHO_NAME_RYAKU.Visible = false;
                    this.form.LBL_SHAIN.Visible = false;
                    this.form.SHAIN_CD.Visible = false;
                    this.form.SHAIN_NAME_RYAKU.Visible = false;
                    this.form.LBL_SEARCH_SHAIN.Visible = true;
                    this.form.SEARCH_SHAIN_CD.Visible = true;
                    this.form.SEARCH_SHAIN_NAME.Visible = true;
                    this.form.Ichiran_Shain.Visible = true;

                    // 項目表示位置の変更
                    // パターン名ラベル
                    this.form.LBL_PATTERN.Location = this.lblPatternPoint;
                    // パターン名
                    this.form.PATTERN_NAME.Location = this.patternNamePoint;
                    // 一覧(メニュー)
                    this.form.Ichiran.Location = this.ichiranPoint;
                    System.Drawing.Point ichiranShainPoint = this.form.Ichiran_Shain.Location;
                    System.Drawing.Size ichiranShainSize = this.form.Ichiran_Shain.Size;
                    System.Drawing.Point ichiranPoint = this.form.Ichiran.Location;
                    int tmpWidh = this.form.Ichiran.Size.Width - (ichiranPoint.X - ichiranShainPoint.X);
                    if (tmpWidh < 0) tmpWidh = 0;
                    System.Drawing.Size newIchiranSize = new System.Drawing.Size(tmpWidh, this.form.Ichiran.Size.Height);
                    this.form.Ichiran.Size = newIchiranSize;

                    // FunctionButton
                    parentForm.bt_func1.Text = "[F1]" + Environment.NewLine + "個人";
                    parentForm.bt_func6.Text = "";
                    parentForm.bt_func6.Enabled = false;
                    parentForm.bt_func8.Text = "";
                    parentForm.bt_func8.Enabled = false;

                    // メニュー一覧 - 更新者情報の列を非表示にするため、テンプレートのWidth変更で実現させる。
                    // ※MultiRowだと列の非表示がVisibleでは出来ない(?)
                    MultiRowTemplate.MenuKengenHoshuDetail template = new MultiRowTemplate.MenuKengenHoshuDetail();
                    int width = template.HD_KUBUN_NAME.Width + template.HD_KINOU_NAME.Width + template.HD_MENU_NAME.Width
                                + template.HD_AUTH_ALL.Width + template.HD_AUTH_READ.Width + template.HD_AUTH_ADD.Width
                                + template.HD_AUTH_EDIT.Width + template.HD_AUTH_DELETE.Width + template.HD_BIKOU.Width;
                    template.Width = width;
                    this.form.Ichiran.Template = template;
                }

                /* 共通：項目値クリア */
                this.form.SEARCH_SHAIN_CD.Text = string.Empty;
                this.form.SEARCH_SHAIN_NAME.Text = string.Empty;
                this.form.PATTERN_NAME.Text = string.Empty;

                this.patternID = 0;

                this.form.Ichiran_Shain.ColumnHeaders[0][MenuKengenHoshuConstans.HD_SHAIN_ALL_CHECK].Value = false;
                foreach (var row in this.form.Ichiran_Shain.Rows)
                {
                    row.Cells[MenuKengenHoshuConstans.SHAIN_CHECK].Value = false;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeDisplay", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion メニュー区分変更による画面表示変更処理

        #region メニュー一覧 - チェックボックス操作制御

        /// <summary>
        /// メニュー項目によって権限設定チェックボックス操作可能/不可能かの設定を行います
        /// </summary>
        internal bool SetIchiranCheckBoxEnabled()
        {
            try
            {
                if (this.form.Ichiran == null || this.form.Ichiran.Rows.Count == 0)
                {
                    return false;
                }

                int i = 0;
                DataView dv = this.GetCloneDataView((DataView)this.form.Ichiran.DataSource);

                //SONNT #142901 メニュー権限 INXS連携 2020/10 START
                M_SHAIN shainEntity = daoShain.GetDataByCd(form.SHAIN_CD.Text);
                //SONNT #142901 メニュー権限 INXS連携 2020/10 END

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

                    //MOD SONNT #142901 メニュー権限 INXS連携 2020/10 START
                    if (formId.StartsWith("S")) //INXS
                    {
                        //一括
                        this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ALL].Enabled = false;

                        //参照
                        this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_READ].Enabled = false;

                        // 新規
                        this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ADD].Enabled = false;

                        // 修正
                        this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_EDIT].Enabled = false;

                        // 削除
                        this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_DELETE].Enabled = false;

                        if (shainEntity.INXS_TANTOU_FLG.IsTrue)
                        {
                            // 新規
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ADD].Value = isUseAdd;

                            // 修正
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_EDIT].Value = isUseEdit;

                            // 削除
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_DELETE].Value = isUseDelete;
                        }
                        else
                        {
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ADD].Value = false;

                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_EDIT].Value = false;

                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_DELETE].Value = false;

                            if (!isUseAdd && !isUseEdit && !isUseDelete)
                            {
                                this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ALL].Value = true;
                            }
                            else
                            {
                                this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ALL].Value = false;
                            }
                        }
                    }
                    else
                    {
                        //MOD SONNT #142901 メニュー権限 INXS連携 2020/10 END

                        // 新規
                        if (!isUseAdd)
                        {
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ADD].Enabled = false;
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ADD].Value = false;
                        }

                        // 修正
                        if (!isUseEdit)
                        {
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_EDIT].Enabled = false;
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_EDIT].Value = false;
                        }

                        // 削除
                        if (!isUseDelete)
                        {
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_DELETE].Enabled = false;
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_DELETE].Value = false;
                        }

                        // 列一括チェックを再設定
                        MenuKengenHoshuLogic.SetToAllCheck_Row(this.form.Ichiran.Rows[i]);

                        // モバイル項目
                        if (formId.StartsWith("MOBILE"))
                        {
                            // 一括チェックをOFF、ReadOnlyにする。
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ALL].Value = false;
                            this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ALL].Enabled = false;
                        }

                    } //MOD SONNT #142901

                    i++;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranCheckBoxEnabled", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion メニュー一覧 - チェックボックス操作制御

        #region 検索処理

        #region 検索前チェック

        /// <summary>
        /// 検索チェック
        /// </summary>
        /// <param name="bDispMessage"></param>
        /// <returns></returns>
        public bool SearchCheck(bool bDispMessage)
        {
            try
            {
                var result = false;
                var msgItem = new StringBuilder();
                Control ctrlFocus = null;

                // 部署と社員が入力されていれば検索OK
                if (string.IsNullOrWhiteSpace(this.form.BUSHO_CD.Text) &&
                    string.IsNullOrWhiteSpace(this.form.SHAIN_CD.Text))
                {
                    msgItem.Append("部署、社員");
                    ctrlFocus = this.form.BUSHO_CD;
                }
                else if (string.IsNullOrWhiteSpace(this.form.BUSHO_CD.Text))
                {
                    msgItem.Append("部署");
                    ctrlFocus = this.form.BUSHO_CD;
                }
                else if (string.IsNullOrWhiteSpace(this.form.SHAIN_CD.Text))
                {
                    msgItem.Append("社員");
                    ctrlFocus = this.form.SHAIN_CD;
                }
                else
                {
                    result = true;
                }

                // エラーメッセージ表示
                if (bDispMessage && !result)
                {
                    if (ctrlFocus != null)
                    {
                        ctrlFocus.Focus();
                    }
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E001", msgItem.ToString());
                }

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        #endregion 検索前チェック

        #region 検索

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件の設定
                this.SetSearchString();

                // メニュー権限データ検索
                var tblMenuAuth = this.SearchMenuAuth();

                // メニュー権限データテーブル用初期化処理
                MenuKengenHoshuLogic.Init_ForAuthDataTable_Shain(tblMenuAuth);

                // メニュー権限データテーブル設定
                this.SetAuthDataTable_ForMenuKbnShain(tblMenuAuth);

                // 検索結果設定
                this.SearchResult = tblMenuAuth;

                // 検索条件を保存
                this.SetPrevData();

                int count = this.SearchResult.Rows == null ? 0 : 1;

                // パターン名をクリア
                this.form.PATTERN_NAME.Text = string.Empty;

                LogUtility.DebugMethodEnd(count);
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

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            var strTemp = string.Empty;

            // 部署CD
            strTemp = this.form.BUSHO_CD.Text;
            if (!string.IsNullOrEmpty(strTemp))
            {
                this.entMenuAuth_ForSeach.BUSHO_CD = strTemp;
                this.entShain_ForSeach.BUSHO_CD = strTemp;
            }

            // 社員CD
            strTemp = this.form.SHAIN_CD.Text;
            if (!string.IsNullOrEmpty(strTemp))
            {
                this.entMenuAuth_ForSeach.SHAIN_CD = strTemp;
                this.entShain_ForSeach.SHAIN_CD = strTemp;
            }
            else
            {
                this.entMenuAuth_ForSeach.SHAIN_CD = string.Empty;
            }
        }

        /// <summary>
        /// メニュー権限データ検索
        /// </summary>
        /// <returns></returns>
        private DataTable SearchMenuAuth()
        {
            // 社員権限
            DataTable shainAuth = this.daoAuth.GetDataBySqlFile(this.GET_ICHIRAN_SHAIN_AUTH_DATA_SQL, this.entMenuAuth_ForSeach);
            return shainAuth;
        }

        /// <summary>
        /// メニュー権限データテーブル用初期化処理
        /// </summary>
        /// <param name="table"></param>
        private static void Init_ForAuthDataTable_Shain(DataTable table)
        {
            // 表示用カラムを追加
            MenuKengenHoshuLogic.AddColumn_ForDataTable(table, MenuKengenHoshuConstans.KUBUN_NAME, typeof(string));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(table, MenuKengenHoshuConstans.KINOU_NAME, typeof(string));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(table, MenuKengenHoshuConstans.MENU_NAME, typeof(string));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(table, MenuKengenHoshuConstans.AUTH_ALL, typeof(bool));
            MenuKengenHoshuLogic.AddColumn_ForDataTable(table, MenuKengenHoshuConstans.ROW_NO, typeof(int));

            // DBNull許容設定
            MenuKengenHoshuLogic.SetAllowDBNull_ForDataTable(table, MenuKengenHoshuConstans.CREATE_USER, true);
            MenuKengenHoshuLogic.SetAllowDBNull_ForDataTable(table, MenuKengenHoshuConstans.CREATE_DATE, true);
            MenuKengenHoshuLogic.SetAllowDBNull_ForDataTable(table, MenuKengenHoshuConstans.CREATE_PC, true);
            MenuKengenHoshuLogic.SetAllowDBNull_ForDataTable(table, MenuKengenHoshuConstans.UPDATE_USER, true);
            MenuKengenHoshuLogic.SetAllowDBNull_ForDataTable(table, MenuKengenHoshuConstans.UPDATE_DATE, true);
            MenuKengenHoshuLogic.SetAllowDBNull_ForDataTable(table, MenuKengenHoshuConstans.UPDATE_PC, true);
            MenuKengenHoshuLogic.SetAllowDBNull_ForDataTable(table, MenuKengenHoshuConstans.TIME_STAMP, true);

            // Unique設定
            MenuKengenHoshuLogic.SetUnique_ForDataTable(table, MenuKengenHoshuConstans.TIME_STAMP, false);
        }

        #endregion 検索

        #region メニュー権限データテーブル設定

        /// <summary>
        /// メニュー権限データテーブル設定
        /// </summary>
        /// <param name="table"></param>
        private void SetAuthDataTable_ForMenuKbnShain(DataTable table)
        {
            // 新規追加行の為、クローンテーブル作成
            var tblNewData = table.Clone();

            // 行番号初期化
            this.rowNo = 0;

            // データテーブル設定
            foreach (DataRow menuRow in this.MenuTable.Rows)
            {
                this.SetAuthDataTable_ForMenuKbnShain(table, tblNewData, menuRow);
            }

            // 新規追加行保存用テーブルから新規追加行を加える
            // ※GetChangesで新規行と見なされるように
            table.Clear();
            foreach (DataRow row in tblNewData.Rows)
            {
                table.ImportRow(row);
            }
        }

        /// <summary>
        /// メニュー権限データテーブル設定
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tblNewData">新規追加行保存用テーブル</param>
        private void SetAuthDataTable_ForMenuKbnShain(
            DataTable table,
            DataTable tblNewData,
            DataRow menuRow)
        {
            var formID = MenuKengenHoshuLogic.GetString_ByObject(menuRow[MenuKengenHoshuConstans.FORM_ID]);
            var windowID = MenuKengenHoshuLogic.GetNullableInt_ByObject(menuRow[MenuKengenHoshuConstans.WINDOW_ID]);
            var kubunName = MenuKengenHoshuLogic.GetString_ByObject(menuRow[MenuKengenHoshuConstans.KUBUN_NAME]);
            var kinouName = MenuKengenHoshuLogic.GetString_ByObject(menuRow[MenuKengenHoshuConstans.KINOU_NAME]);
            var menuName = MenuKengenHoshuLogic.GetString_ByObject(menuRow[MenuKengenHoshuConstans.MENU_NAME]);

            // 対象の画面ID(&WindowID)データが存在するかをチェック
            DataRow[] rows;
            if (MenuKengenHoshuLogic.FindDataRows_ByFormID(table, formID, windowID, out rows))
            {
                // 対象の画面ID(&WindowID)データが存在する
                foreach (var row in rows)
                {
                    row[MenuKengenHoshuConstans.KUBUN_NAME] = kubunName;
                    row[MenuKengenHoshuConstans.KINOU_NAME] = kinouName;
                    row[MenuKengenHoshuConstans.MENU_NAME] = menuName;
                    MenuKengenHoshuLogic.SetToAllCheck_ForDataRow(row);
                    this.rowNo++;
                    row[MenuKengenHoshuConstans.ROW_NO] = this.rowNo;

                    var newRow = tblNewData.NewRow();
                    newRow[MenuKengenHoshuConstans.FORM_ID] = formID;
                    newRow[MenuKengenHoshuConstans.WINDOW_ID] = windowID.HasValue ? windowID.Value : -1;
                    newRow[MenuKengenHoshuConstans.BUSHO_CD] = this.entMenuAuth_ForSeach.BUSHO_CD;
                    newRow[MenuKengenHoshuConstans.SHAIN_CD] = this.entMenuAuth_ForSeach.SHAIN_CD;
                    newRow[MenuKengenHoshuConstans.DELETE_FLG] = false;
                    newRow[MenuKengenHoshuConstans.KUBUN_NAME] = row[MenuKengenHoshuConstans.KUBUN_NAME];
                    newRow[MenuKengenHoshuConstans.KINOU_NAME] = row[MenuKengenHoshuConstans.KINOU_NAME];
                    newRow[MenuKengenHoshuConstans.MENU_NAME] = row[MenuKengenHoshuConstans.MENU_NAME];
                    newRow[MenuKengenHoshuConstans.AUTH_ADD] = row[MenuKengenHoshuConstans.AUTH_ADD];
                    newRow[MenuKengenHoshuConstans.AUTH_READ] = row[MenuKengenHoshuConstans.AUTH_READ];
                    newRow[MenuKengenHoshuConstans.AUTH_EDIT] = row[MenuKengenHoshuConstans.AUTH_EDIT];
                    newRow[MenuKengenHoshuConstans.AUTH_DELETE] = row[MenuKengenHoshuConstans.AUTH_DELETE];
                    newRow[MenuKengenHoshuConstans.BIKOU] = row[MenuKengenHoshuConstans.BIKOU];
                    newRow[MenuKengenHoshuConstans.CREATE_USER] = row[MenuKengenHoshuConstans.CREATE_USER];
                    newRow[MenuKengenHoshuConstans.CREATE_DATE] = row[MenuKengenHoshuConstans.CREATE_DATE];
                    newRow[MenuKengenHoshuConstans.UPDATE_USER] = row[MenuKengenHoshuConstans.UPDATE_USER];
                    newRow[MenuKengenHoshuConstans.UPDATE_DATE] = row[MenuKengenHoshuConstans.UPDATE_DATE];
                    newRow[MenuKengenHoshuConstans.TIME_STAMP] = row[MenuKengenHoshuConstans.TIME_STAMP];
                    MenuKengenHoshuLogic.SetToAllCheck_ForDataRow(newRow);
                    newRow[MenuKengenHoshuConstans.ROW_NO] = this.rowNo;
                    tblNewData.Rows.Add(newRow);
                }
            }
            else
            {
                // 対象の画面ID(&WindowID)データが存在しない
                var newRow = tblNewData.NewRow();
                newRow[MenuKengenHoshuConstans.FORM_ID] = formID;
                newRow[MenuKengenHoshuConstans.WINDOW_ID] = windowID.HasValue ? windowID.Value : -1;
                newRow[MenuKengenHoshuConstans.BUSHO_CD] = this.entMenuAuth_ForSeach.BUSHO_CD;
                newRow[MenuKengenHoshuConstans.SHAIN_CD] = this.entMenuAuth_ForSeach.SHAIN_CD;
                newRow[MenuKengenHoshuConstans.DELETE_FLG] = false;
                newRow[MenuKengenHoshuConstans.KUBUN_NAME] = kubunName;
                newRow[MenuKengenHoshuConstans.KINOU_NAME] = kinouName;
                newRow[MenuKengenHoshuConstans.MENU_NAME] = menuName;
                newRow[MenuKengenHoshuConstans.AUTH_ADD] = true;
                newRow[MenuKengenHoshuConstans.AUTH_READ] = true;
                newRow[MenuKengenHoshuConstans.AUTH_EDIT] = true;
                newRow[MenuKengenHoshuConstans.AUTH_DELETE] = true;
                MenuKengenHoshuLogic.SetToAllCheck_ForDataRow(newRow);
                this.rowNo++;
                newRow[MenuKengenHoshuConstans.ROW_NO] = this.rowNo;
                tblNewData.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// データテーブルに対象の画面IDが存在するか
        /// [2014.03.03 杉岡]WindowIDも考慮するように修正
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="formID">画面ID</param>
        /// <param name="windowID">ウィンドウID</param>
        /// <param name="rows">データ行</param>
        /// <returns>true:存在する／false:存在しない</returns>
        private static bool FindDataRows_ByFormID(DataTable table, string formID, int? windowID, out DataRow[] rows)
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
        private static void SetToAllCheck_ForDataRow(DataRow row)
        {
            //SONNT #142901 メニュー権限 INXS連携 2020/10 START
            string formId = row[MenuKengenHoshuConstans.FORM_ID].ToString();
            if (formId.StartsWith("S")) //INXS
            {
                return;
            }
            //SONNT #142901 メニュー権限 INXS連携 2020/10 END

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

        #endregion メニュー権限データテーブル設定

        #region 検索結果設定

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            try
            {
                var table = this.SearchResult;
                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }
                // DataViewとソート設定
                DataView dv = new DataView(table);
                dv.Sort = MenuKengenHoshuConstans.ROW_NO;
                this.form.Ichiran.DataSource = dv;

                // チェックボックス押下不可項目設定処理
                bool catchErr = this.SetIchiranCheckBoxEnabled();
                if (catchErr)
                {
                    return true;
                }

                // 列ヘッダー一括チェックボックス状態を合わせる
                this.SetToAllCheck_Column();

                // 差分取得用に検索後の一覧データを保持
                this.prevIchiranDataView = this.GetCloneDataView((DataView)this.form.Ichiran.DataSource);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M188", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // ファンクションボタンを活性
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion 検索結果設定

        #endregion 検索処理

        #region 取消処理

        /// <summary>
        /// 取消処理
        /// </summary>
        public void Cancel()
        {
            LogUtility.DebugMethodStart();

            // 前回設定値取得
            this.GetPrevData();

            // 画面表示変更処理
            this.ChangeDisplay();

            LogUtility.DebugMethodEnd();
        }

        #endregion 取消処理

        #region CSV出力

        /// <summary>
        /// CSV
        /// </summary>
        public bool CSV()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var msgLogic = new MessageBoxShowLogic();
                if (this.form.Ichiran == null || this.form.Ichiran.Rows.Count == 0)
                {
                    msgLogic.MessageBoxShow("E061");
                    return false;
                }

                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    // 出力用MultiRow作成
                    GcCustomMultiRow dgv = this.GetCSVMultiRow();
                    dgv.Visible = false;
                    this.form.Controls.Add(dgv);
                    dgv.Refresh();

                    // 位置情報の作成
                    var multirowLocationLogic = new MultiRowIndexCreateLogic();
                    multirowLocationLogic.multiRow = dgv;
                    multirowLocationLogic.CreateLocations();

                    // CSVロジック作成
                    var csvLogic = new CSVFileLogic();
                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;
                    csvLogic.Detail = dgv;
                    var id = this.form.WindowId;
                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    // CSV出力
                    csvLogic.CreateCSVFile(this.form);

                    #region 新しいCSV出力利用するように、余計なメッセージを削除

                    //msgLogic.MessageBoxShow("I000");

                    #endregion 新しいCSV出力利用するように、余計なメッセージを削除

                    this.form.Controls.Remove(dgv);
                    dgv.Dispose();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// CSV出力用MultiRowを取得します
        /// </summary>
        /// <returns>CSV出力用MultiRow</returns>
        private GcCustomMultiRow GetCSVMultiRow()
        {
            MultiRowTemplate.MenuKengenHoshuDetail_ByShain template = new MultiRowTemplate.MenuKengenHoshuDetail_ByShain();
            GcCustomMultiRow dgv = new GcCustomMultiRow();
            dgv.Template = template;

            for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
            {
                dgv.Rows.Add();
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.KUBUN_NAME].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.KUBUN_NAME].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.KINOU_NAME].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.KINOU_NAME].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.MENU_NAME].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.MENU_NAME].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ALL].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ALL].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_READ].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_READ].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ADD].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_ADD].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_EDIT].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_EDIT].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_DELETE].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.AUTH_DELETE].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.BIKOU].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.BIKOU].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.UPDATE_USER].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.UPDATE_USER].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.UPDATE_DATE].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.UPDATE_DATE].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.CREATE_USER].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.CREATE_USER].Value;
                dgv.Rows[i].Cells[MenuKengenHoshuConstans.CREATE_DATE].Value = this.form.Ichiran.Rows[i].Cells[MenuKengenHoshuConstans.CREATE_DATE].Value;
            }

            return dgv;
        }

        #endregion CSV出力

        #region 一覧セル編集関連処理

        /// <summary>
        /// 一覧セル編集開始時イベント処理
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranCellEnter(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (e.Scope != CellScope.Row)
                {
                    return false;
                }

                if (this.SearchResult == null)
                {
                    this.form.Ichiran.CurrentRow.Selectable = false;
                }
                else
                {
                    this.form.Ichiran.CurrentRow.Selectable = true;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// メニュー一覧セル値変更時処理
        /// </summary>
        /// <param name="e"></param>
        public bool Ichiran_CellValueChanged(CellEventArgs e)
        {
            try
            {
                // 行領域以外の場合、何もしない
                if (e.Scope != CellScope.Row ||
                    e.RowIndex < 0)
                {
                    return false;
                }

                // 行取得
                var row = this.form.Ichiran.Rows[e.RowIndex];

                // 一括チェックセル
                if (MenuKengenHoshuConstans.AUTH_ALL.Equals(e.CellName))
                {
                    // 一括チェック処理（行）
                    MenuKengenHoshuLogic.SetAllCheck_Row(row);
                }
                // 参照/新規/修正/削除チェックセル
                else if (
                    MenuKengenHoshuConstans.AUTH_READ.Equals(e.CellName) ||
                    MenuKengenHoshuConstans.AUTH_ADD.Equals(e.CellName) ||
                    MenuKengenHoshuConstans.AUTH_EDIT.Equals(e.CellName) ||
                    MenuKengenHoshuConstans.AUTH_DELETE.Equals(e.CellName))
                {
                    if (MenuKengenHoshuConstans.AUTH_READ.Equals(e.CellName) &&
                        !(bool)this.form.Ichiran.Rows[e.RowIndex].Cells[e.CellName].EditedFormattedValue)
                    {
                        // 参照がOFFに変更された場合は参照以外もOFF
                        this.form.Ichiran.Rows[e.RowIndex].Cells[MenuKengenHoshuConstans.AUTH_ADD].Value = false;
                        this.form.Ichiran.Rows[e.RowIndex].Cells[MenuKengenHoshuConstans.AUTH_EDIT].Value = false;
                        this.form.Ichiran.Rows[e.RowIndex].Cells[MenuKengenHoshuConstans.AUTH_DELETE].Value = false;
                    }
                    else if (!MenuKengenHoshuConstans.AUTH_READ.Equals(e.CellName) &&
                            (bool)this.form.Ichiran.Rows[e.RowIndex].Cells[e.CellName].EditedFormattedValue &&
                            !(bool)this.form.Ichiran.Rows[e.RowIndex].Cells[MenuKengenHoshuConstans.AUTH_READ].EditedFormattedValue)
                    {
                        // 参照以外の権限がONになったら参照もONにする
                        this.form.Ichiran.Rows[e.RowIndex].Cells[MenuKengenHoshuConstans.AUTH_READ].Value = true;
                    }

                    // 一括チェック処理（行）
                    MenuKengenHoshuLogic.SetToAllCheck_Row(row);
                }

                // 一括チェック(列)
                this.SetToAllCheck_Column();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellValueChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// メニュー一覧セル - セルクリック
        /// </summary>
        /// <param name="e"></param>
        internal bool Ichiran_CellContentClick(CellEventArgs e)
        {
            try
            {
                if (e.Scope == CellScope.ColumnHeader)
                {
                    // カラムヘッダーの場合
                    string targetCellName = string.Empty;
                    bool val = false;
                    switch (e.CellName)
                    {
                        case "HD_AUTH_READ":
                            targetCellName = MenuKengenHoshuConstans.AUTH_READ;
                            val = (bool)this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_READ].EditedFormattedValue;
                            break;

                        case "HD_AUTH_ADD":
                            targetCellName = MenuKengenHoshuConstans.AUTH_ADD;
                            val = (bool)this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_ADD].EditedFormattedValue;
                            break;

                        case "HD_AUTH_EDIT":
                            targetCellName = MenuKengenHoshuConstans.AUTH_EDIT;
                            val = (bool)this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_EDIT].EditedFormattedValue;
                            break;

                        case "HD_AUTH_DELETE":
                            targetCellName = MenuKengenHoshuConstans.AUTH_DELETE;
                            val = (bool)this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_DELETE].EditedFormattedValue;
                            break;

                        default:
                            // Nothing
                            break;
                    }

                    if (!string.IsNullOrEmpty(targetCellName))
                    {
                        if (e.CellName.Equals(MenuKengenHoshuConstans.HD_AUTH_READ) && !val)
                        {
                            // 参照がOFF場合は他の権限もOFFにするため、全ての項目でOFFの設定にする
                            foreach (var row in this.form.Ichiran.Rows)
                            {
                                if (row.Cells[MenuKengenHoshuConstans.AUTH_READ].Enabled)
                                {
                                    row.Cells[MenuKengenHoshuConstans.AUTH_READ].Value = val;
                                }

                                if (row.Cells[MenuKengenHoshuConstans.AUTH_ADD].Enabled)
                                {
                                    row.Cells[MenuKengenHoshuConstans.AUTH_ADD].Value = val;
                                }

                                if (row.Cells[MenuKengenHoshuConstans.AUTH_EDIT].Enabled)
                                {
                                    row.Cells[MenuKengenHoshuConstans.AUTH_EDIT].Value = val;
                                }

                                if (row.Cells[MenuKengenHoshuConstans.AUTH_DELETE].Enabled)
                                {
                                    row.Cells[MenuKengenHoshuConstans.AUTH_DELETE].Value = val;
                                }

                                MenuKengenHoshuLogic.SetToAllCheck_Row(row);
                            }

                            // 参照以外ののヘッダもOFF
                            this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_ADD].Value = false;
                            this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_EDIT].Value = false;
                            this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_DELETE].Value = false;
                        }
                        else
                        {
                            foreach (var row in this.form.Ichiran.Rows)
                            {
                                if (row.Cells[targetCellName].Enabled)
                                {
                                    row.Cells[targetCellName].Value = val;

                                    // 値がTrueに変更かつtargetCellNameが参照以外の場合、参照がFalseならば連動してTrueに変更
                                    if (val &&
                                        !targetCellName.Equals(MenuKengenHoshuConstans.AUTH_READ) &&
                                        !(bool)row.Cells[MenuKengenHoshuConstans.AUTH_READ].Value)
                                    {
                                        row.Cells[MenuKengenHoshuConstans.AUTH_READ].Value = true;
                                    }
                                }
                                MenuKengenHoshuLogic.SetToAllCheck_Row(row);
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellContentClick", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 一括チェック処理（行）
        /// [一括] ⇒ [参照/新規/修正/削除]
        /// </summary>
        /// <param name="eRow">行</param>
        private static void SetAllCheck_Row(Row row)
        {
            // モバイル項目は処理しない。
            string formId = row[MenuKengenHoshuConstans.FORM_ID].Value.ToString();
            if (formId.StartsWith("MOBILE"))
            {
                return;
            }

            var value = row[MenuKengenHoshuConstans.AUTH_ALL].Value;

            if (row[MenuKengenHoshuConstans.AUTH_READ].Enabled)
            {
                row[MenuKengenHoshuConstans.AUTH_READ].Value = value;
            }

            if (row[MenuKengenHoshuConstans.AUTH_ADD].Enabled)
            {
                row[MenuKengenHoshuConstans.AUTH_ADD].Value = value;
            }

            if (row[MenuKengenHoshuConstans.AUTH_EDIT].Enabled)
            {
                row[MenuKengenHoshuConstans.AUTH_EDIT].Value = value;
            }

            if (row[MenuKengenHoshuConstans.AUTH_DELETE].Enabled)
            {
                row[MenuKengenHoshuConstans.AUTH_DELETE].Value = value;
            }
        }

        /// <summary>
        /// 一括チェック処理（行）
        /// [参照/新規/修正/削除] ⇒ [一括]
        /// </summary>
        /// <param name="eRow">行</param>
        private static void SetToAllCheck_Row(Row row)
        {
            //SONNT #142901 メニュー権限 INXS連携 2020/10 START
            string formId = row[MenuKengenHoshuConstans.FORM_ID].Value.ToString();
            if (formId.StartsWith("S") || formId.StartsWith("MOBILE")) //INXS or モバイル
            {
                return;
            }
            //SONNT #142901 メニュー権限 INXS連携 2020/10 END

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

        /// <summary>
        /// 一括チェック処理(列)
        /// </summary>
        private void SetToAllCheck_Column()
        {
            if (this.form.Ichiran == null || this.form.Ichiran.Rows.Count == 0)
            {
                return;
            }

            bool read = true;
            bool add = true;
            bool edit = true;
            bool delete = true;

            foreach (var row in this.form.Ichiran.Rows)
            {
                if (row[MenuKengenHoshuConstans.AUTH_READ].Enabled && !(bool)row.Cells[MenuKengenHoshuConstans.AUTH_READ].EditedFormattedValue) read = false;
                if (row[MenuKengenHoshuConstans.AUTH_ADD].Enabled && !(bool)row.Cells[MenuKengenHoshuConstans.AUTH_ADD].EditedFormattedValue) add = false;
                if (row[MenuKengenHoshuConstans.AUTH_EDIT].Enabled && !(bool)row.Cells[MenuKengenHoshuConstans.AUTH_EDIT].EditedFormattedValue) edit = false;
                if (row[MenuKengenHoshuConstans.AUTH_DELETE].Enabled && !(bool)row.Cells[MenuKengenHoshuConstans.AUTH_DELETE].EditedFormattedValue) delete = false;

                if (!read && !add && !edit && !delete)
                {
                    break;
                }
            }

            this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_READ].Value = read;
            this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_ADD].Value = add;
            this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_EDIT].Value = edit;
            this.form.Ichiran.ColumnHeaders[0][MenuKengenHoshuConstans.HD_AUTH_DELETE].Value = delete;
        }

        /// <summary>
        /// 社員一覧セル値変更時処理
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranShain_CellContentClick(CellEventArgs e)
        {
            try
            {
                // カラムヘッダーの場合
                if (e.Scope == CellScope.ColumnHeader)
                {
                    if (MenuKengenHoshuConstans.HD_SHAIN_ALL_CHECK.Equals(e.CellName))
                    {
                        foreach (var row in this.form.Ichiran_Shain.Rows)
                        {
                            row.Cells[MenuKengenHoshuConstans.SHAIN_CHECK].Value = this.form.Ichiran_Shain.ColumnHeaders[e.SectionIndex][e.CellName].EditedFormattedValue;
                        }
                    }
                }
                // セルの場合
                else
                {
                    if (MenuKengenHoshuConstans.SHAIN_CHECK.Equals(e.CellName))
                    {
                        // 全行同じ値の場合はヘッダーの値を合わせる
                        if ((bool)this.form.Ichiran_Shain.ColumnHeaders[0][MenuKengenHoshuConstans.HD_SHAIN_ALL_CHECK].EditedFormattedValue)
                        {
                            foreach (var row in this.form.Ichiran_Shain.Rows)
                            {
                                if (!(bool)row.Cells[MenuKengenHoshuConstans.SHAIN_CHECK].EditedFormattedValue)
                                {
                                    this.form.Ichiran_Shain.ColumnHeaders[0][MenuKengenHoshuConstans.HD_SHAIN_ALL_CHECK].Value = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            bool check = true;
                            foreach (var row in this.form.Ichiran_Shain.Rows)
                            {
                                if (!(bool)row.Cells[MenuKengenHoshuConstans.SHAIN_CHECK].EditedFormattedValue)
                                {
                                    check = false;
                                }
                            }

                            if (check)
                            {
                                this.form.Ichiran_Shain.ColumnHeaders[0][MenuKengenHoshuConstans.HD_SHAIN_ALL_CHECK].Value = true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranShain_CellContentClick", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion 一覧セル編集関連処理

        #region 登録処理

        #region 登録前必須チェック

        /// <summary>
        /// 登録前必須チェックを実行します
        /// </summary>
        /// <returns>エラー無し：True　エラー有り：False</returns>
        internal bool RegistCheck()
        {
            try
            {
                bool check = true;

                if (this.form.MenuKbn == MenuKengenHoshuConstans.MENU_KBN_SINGLE)
                {
                    /* 個人モード時 */
                    var msgItem = new StringBuilder();
                    Control ctrlFocus = null;

                    // 部署、社員
                    if (string.IsNullOrEmpty(this.form.BUSHO_CD.Text) &&
                        string.IsNullOrEmpty(this.form.SHAIN_CD.Text))
                    {
                        msgItem.Append("部署、社員");
                        ctrlFocus = this.form.BUSHO_CD;
                    }
                    else if (string.IsNullOrEmpty(this.form.BUSHO_CD.Text))
                    {
                        msgItem.Append("部署");
                        ctrlFocus = this.form.BUSHO_CD;
                    }
                    else if (string.IsNullOrEmpty(this.form.SHAIN_CD.Text))
                    {
                        msgItem.Append("社員");
                        ctrlFocus = this.form.SHAIN_CD;
                    }

                    if (msgItem.Length > 0)
                    {
                        if (ctrlFocus != null)
                        {
                            ctrlFocus.Focus();
                        }
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E001", msgItem.ToString());
                        check = false;
                    }

                    // メニュー一覧
                    if (check && this.form.Ichiran.RowCount == 0)
                    {
                        MessageBoxShowLogic msg = new MessageBoxShowLogic();
                        msg.MessageBoxShow("E061");
                        check = false;
                    }
                }
                else if (this.form.MenuKbn == MenuKengenHoshuConstans.MENU_KBN_MULTIPLE)
                {
                    /* 複数モード時 */

                    bool chk = false;

                    // 社員一覧
                    foreach (var row in this.form.Ichiran_Shain.Rows)
                    {
                        if ((bool)row.Cells[MenuKengenHoshuConstans.SHAIN_CHECK].Value)
                        {
                            chk = true;
                            break;
                        }
                    }

                    // メニュー一覧
                    if (chk && this.form.Ichiran.RowCount == 0)
                    {
                        chk = false;
                    }

                    if (!chk)
                    {
                        MessageBoxShowLogic msg = new MessageBoxShowLogic();
                        msg.MessageBoxShow("E061");
                        check = false;
                    }
                }

                return check;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        #endregion 登録前必須チェック

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                // エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    bool canCommit = true;
                    int count = 1;
                    this.InitProgressBar(0, this.dto.MenuAuth.Length);

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        // データ存在チェック用に全件取得
                        M_MENU_AUTH dummy = new M_MENU_AUTH();
                        DataTable dt = this.daoAuth.GetDataBySqlFile(GET_ICHIRAN_AUTH_DATACHECK_SQL, dummy);

                        foreach (M_MENU_AUTH authEntity in this.dto.MenuAuth)
                        {
                            /* データ存在チェック */
                            StringBuilder whereString = new StringBuilder();
                            whereString.Append("BUSHO_CD = '");
                            whereString.Append(authEntity.BUSHO_CD);
                            whereString.Append("' AND SHAIN_CD = '");
                            whereString.Append(authEntity.SHAIN_CD);
                            whereString.Append("' AND FORM_ID = '");
                            whereString.Append(authEntity.FORM_ID);
                            whereString.Append("' AND WINDOW_ID = ");
                            whereString.Append(authEntity.WINDOW_ID);
                            DataRow[] rows = dt.Select(whereString.ToString());

                            if (rows != null && rows.Length > 0)
                            {
                                authEntity.CREATE_DATE = SqlDateTime.Parse(rows[0]["CREATE_DATE"].ToString());
                                authEntity.CREATE_PC = rows[0]["CREATE_PC"].ToString();
                                authEntity.CREATE_USER = rows[0]["CREATE_USER"].ToString();
                                authEntity.TIME_STAMP = (byte[])rows[0]["TIME_STAMP"];
                                this.daoAuth.Update(authEntity);
                            }
                            else
                            {
                                this.daoAuth.Insert(authEntity);
                            }

                            this.SetProgressBarValue(count);
                            count++;
                        }

                        // 全(ログイン可能)社員が権限設定を操作出来なくなるかのチェック
                        canCommit = this.CheckDisableMenuKengenHoshu();
                        if (canCommit)
                        {
                            // トランザクション終了
                            tran.Commit();
                        }
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (canCommit)
                    {
                        msgLogic.MessageBoxShow("I001", "登録");
                    }
                    else
                    {
                        msgLogic.MessageBoxShowWarn("メニュー権限入力画面を修正出来る社員が存在しなくなるため登録出来ません。");
                    }
                }

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        #region Entity作成

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                var entityList = new M_MENU_AUTH[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_MENU_AUTH();
                }

                // バインドロジック作成
                var dataBinderLogic = new DataBinderLogic<M_MENU_AUTH>(entityList);

                // 現在のデータソースを保存
                var dv = this.form.Ichiran.DataSource as DataView;

                // 初回全行更新 or 差分更新チェック
                M_MENU_AUTH data = new M_MENU_AUTH();
                data.BUSHO_CD = this.form.BUSHO_CD.Text;
                data.SHAIN_CD = this.form.SHAIN_CD.Text;
                DataTable checkDt = this.daoAuth.GetDataBySqlFile(GET_ICHIRAN_AUTH_DATACHECK_SQL, data);

                // 個人設定の場合のみ2回目以降の更新は差分更新を行う
                // ただし、パターンを読み込んでる場合は全行とする（一覧を再作成しているため差分が取れない）
                DataTable registDataTable = new DataTable();
                if (this.form.MenuKbn == MenuKengenHoshuConstans.MENU_KBN_SINGLE)
                {
                    if ((checkDt != null && checkDt.Rows.Count > 0) && string.IsNullOrEmpty(this.form.PATTERN_NAME.Text))
                    {
                        // 変更分のみ取得
                        registDataTable = GetDifferenceIchiranData(dv.Table);
                    }
                    else
                    {
                        // 全行取得
                        registDataTable = dv.Table;
                    }
                }
                else
                {
                    // 全行取得
                    registDataTable = dv.Table;
                }

                var addList = new List<M_MENU_AUTH>();
                if (registDataTable != null && registDataTable.Rows.Count > 0)
                {
                    if (this.form.MenuKbn == MenuKengenHoshuConstans.MENU_KBN_SINGLE)
                    {
                        // 部署、社員CDは画面から取得してエンティティに再設定
                        // パターン呼出⇒登録のフロー(検索未使用)で登録できるため、検索時に設定した値は無視
                        string bushoCd = this.form.BUSHO_CD.Text;
                        string shainCd = this.form.SHAIN_CD.Text;

                        // 個人モード時
                        foreach (DataRow row in registDataTable.Rows)
                        {
                            // 行データからエンティティを作成し、リストに追加
                            M_MENU_AUTH entity = this.CreateEntiry_ByRow(row, dataBinderLogic, bushoCd, shainCd);

                            // 伝票一覧などメニュー中に複数表示されるものは一意制約違反で登録できないため弾く
                            M_MENU_AUTH result = addList.Find(delegate(M_MENU_AUTH a)
                            {
                                return (bool)(a.FORM_ID.Equals(entity.FORM_ID) && (a.WINDOW_ID == entity.WINDOW_ID));
                            });

                            if (result == null)
                            {
                                addList.Add(entity);
                            }
                        }
                    }
                    else
                    {
                        // 複数モード時
                        foreach (var shainRow in this.form.Ichiran_Shain.Rows)
                        {
                            bool chk = (bool)shainRow.Cells[MenuKengenHoshuConstans.SHAIN_CHECK].Value;
                            if (chk)
                            {
                                string bushoCd = shainRow.Cells[MenuKengenHoshuConstans.CELL_BUSHO_CD].Value.ToString();
                                string shainCd = shainRow.Cells[MenuKengenHoshuConstans.CELL_SHAIN_CD].Value.ToString();

                                foreach (DataRow row in registDataTable.Rows)
                                {
                                    // 行データからエンティティを作成し、リストに追加
                                    M_MENU_AUTH entity = this.CreateEntiry_ByRow(row, dataBinderLogic, bushoCd, shainCd);

                                    // 伝票一覧などメニュー中に複数表示されるものは一意制約違反で登録できないため弾く
                                    M_MENU_AUTH result = addList.Find(delegate(M_MENU_AUTH a)
                                    {
                                        return (bool)(a.FORM_ID.Equals(entity.FORM_ID) && (a.WINDOW_ID == entity.WINDOW_ID)
                                            && (a.SHAIN_CD.Equals(entity.SHAIN_CD) && (a.BUSHO_CD.Equals(entity.BUSHO_CD))));
                                    });

                                    if (result == null)
                                    {
                                        addList.Add(entity);
                                    }
                                }
                            }
                        }
                    }
                }

                // 新規/修正対象データを設定
                this.dto.MenuAuth = addList.ToArray();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

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

        #endregion - Get Clone -

        /// <summary>
        /// 差分更新用に一覧中で検索実行時から変更があったデータを取得します
        /// </summary>
        /// <param name="dt">一覧のDataTable</param>
        /// <returns></returns>
        private DataTable GetDifferenceIchiranData(DataTable dt)
        {
            DataTable dataTable = dt.Clone();

            DataTable prevDataTable = this.prevIchiranDataView.Table;
            foreach (DataRow row in dt.Rows)
            {
                string formId = row[MenuKengenHoshuConstans.FORM_ID].ToString();
                string windowId = row[MenuKengenHoshuConstans.WINDOW_ID].ToString();
                string selectString = "FORM_ID = '" + formId + "' AND WINDOW_ID = " + windowId;
                DataRow[] dataRowList = prevDataTable.Select(selectString);

                // G055：伝票一覧が2行存在するので既に登録対象の場合は飛ばす
                if (formId.Equals("G055"))
                {
                    DataRow[] tmpList = dataTable.Select(selectString);
                    if (tmpList != null && tmpList.Length > 0)
                    {
                        continue;
                    }
                }

                if (((bool)row[MenuKengenHoshuConstans.AUTH_ADD] ^ (bool)dataRowList[0][MenuKengenHoshuConstans.AUTH_ADD]) ||
                    ((bool)row[MenuKengenHoshuConstans.AUTH_DELETE] ^ (bool)dataRowList[0][MenuKengenHoshuConstans.AUTH_DELETE]) ||
                    ((bool)row[MenuKengenHoshuConstans.AUTH_EDIT] ^ (bool)dataRowList[0][MenuKengenHoshuConstans.AUTH_EDIT]) ||
                    ((bool)row[MenuKengenHoshuConstans.AUTH_READ] ^ (bool)dataRowList[0][MenuKengenHoshuConstans.AUTH_READ]) ||
                    !row[MenuKengenHoshuConstans.BIKOU].ToString().Equals(dataRowList[0][MenuKengenHoshuConstans.BIKOU]))
                {
                    dataTable.ImportRow(row);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 行データからエンティティを作成
        /// </summary>
        /// <param name="eRow">行データ</param>
        /// <param name="logic">バインドロジック</param>
        /// <param name="bushoCd">部署CD</param>
        /// <param name="shainCd">社員CD</param>
        /// <returns></returns>
        private M_MENU_AUTH CreateEntiry_ByRow(DataRow row, DataBinderLogic<M_MENU_AUTH> logic, string bushoCd, string shainCd)
        {
            var entity = new M_MENU_AUTH();

            entity.FORM_ID = MenuKengenHoshuLogic.GetString_ByObject(row[MenuKengenHoshuConstans.FORM_ID]);
            entity.WINDOW_ID = MenuKengenHoshuLogic.GetInt_ByObject(row[MenuKengenHoshuConstans.WINDOW_ID], -1);
            entity.BUSHO_CD = bushoCd;
            entity.SHAIN_CD = shainCd;
            entity.AUTH_READ = MenuKengenHoshuLogic.GetBool_ByObject(row[MenuKengenHoshuConstans.AUTH_READ]);
            entity.AUTH_ADD = MenuKengenHoshuLogic.GetBool_ByObject(row[MenuKengenHoshuConstans.AUTH_ADD]);
            entity.AUTH_EDIT = MenuKengenHoshuLogic.GetBool_ByObject(row[MenuKengenHoshuConstans.AUTH_EDIT]);
            entity.AUTH_DELETE = MenuKengenHoshuLogic.GetBool_ByObject(row[MenuKengenHoshuConstans.AUTH_DELETE]);
            entity.BIKOU = MenuKengenHoshuLogic.GetString_ByObject(row[MenuKengenHoshuConstans.BIKOU]);

            logic.SetSystemProperty(entity, false);
            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), entity);

            entity.DELETE_FLG = MenuKengenHoshuLogic.GetBool_ByObject(row[Const.MenuKengenHoshuConstans.DELETE_FLG]);
            // 20151016 TIME_STAMP個別対応 Start
            var time_stamp = row[Const.MenuKengenHoshuConstans.TIME_STAMP] as byte[];
            if (time_stamp != null && time_stamp.Length == 8)
            {
                entity.TIME_STAMP = time_stamp;
            }
            // 20151016 TIME_STAMP個別対応 End

            return entity;
        }

        #endregion Entity作成

        #region プログレスバー表示関連処理

        /// <summary>
        /// プログレスバーの初期化
        /// </summary>
        /// <param name="min">プログレスバーに反映する最小の値</param>
        /// <param name="max">プログレスバーに反映する最大の値</param>
        internal void InitProgressBar(int min, int max)
        {
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.progresBar.Maximum = max;
            parentForm.progresBar.Minimum = min;
            parentForm.progresBar.Value = 0;
        }

        /// <summary>
        /// プログレスバーの値を設定します
        /// </summary>
        /// <param name="val">プログレスバーの値</param>
        internal void SetProgressBarValue(int val)
        {
            var parentForm = (MasterBaseForm)this.form.Parent;
            if (parentForm.progresBar.Maximum >= val)
            {
                parentForm.progresBar.Value = val;
            }
        }

        /// <summary>
        /// プログレスバーをリセット
        /// </summary>
        internal bool ResetProgBar()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.form.Parent;
                parentForm.progresBar.Value = 0;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ResetProgBar", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion プログレスバー表示関連処理

        #region 全社員権限設定操作不可チェック

        /// <summary>
        /// 全ログイン社員がメニュー権限入力を操作不可能になるかのチェックを行います
        /// </summary>
        /// <returns>全社員操作不可：False　何れかの社員が操作可能：True</returns>
        private bool CheckDisableMenuKengenHoshu()
        {
            bool chk = false;

            M_SHAIN dummy = new M_SHAIN();
            DataTable dt = daoShain.GetShainDataSqlFile(this.CHECK_REGIST_DATA_SQL, dummy);

            if (dt == null || dt.Rows.Count == 0)
            {
                // データが無い場合はフルコントロール
                chk = true;
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    bool edit = false;
                    if (row["AUTH_EDIT"] == null || row["AUTH_EDIT"].ToString().Length == 0)
                    {
                        edit = true;
                    }
                    else
                    {
                        edit = (bool)row["AUTH_EDIT"];
                    }

                    if (edit)
                    {
                        // 誰か一人でもメニュー権限入力画面の編集権限があればOK
                        chk = true;
                        break;
                    }
                }
            }

            return chk;
        }

        #endregion 全社員権限設定操作不可チェック

        #endregion 登録処理

        #region パターン登録処理

        /// <summary>
        /// パターン登録処理
        /// </summary>
        public bool PatternRegist()
        {
            try
            {
                List<M_MENU_AUTH_PT_DETAIL> list = this.CreatePatternDetailEntity();

                var ribbonMenu = MasterCommonLogic.GetRibbonMainMenu(this.form);
                var loginUserName = ribbonMenu.GlobalCommonInformation.CurrentShain.SHAIN_NAME_RYAKU;

                MenuKengenPtTorokuForm form = new MenuKengenPtTorokuForm(loginUserName, list, this.patternID);
                form.ShowDialog();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("PatternRegist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// パターン登録用Entityを作成します
        /// </summary>
        private List<M_MENU_AUTH_PT_DETAIL> CreatePatternDetailEntity()
        {
            List<M_MENU_AUTH_PT_DETAIL> detailList = new List<M_MENU_AUTH_PT_DETAIL>();

            DataView dv = this.GetCloneDataView((DataView)this.form.Ichiran.DataSource);
            dv.Sort = MenuKengenHoshuConstans.FORM_ID + "," + MenuKengenHoshuConstans.WINDOW_ID;

            string tmpFormId = string.Empty;
            int tmpWindowId = -100;
            foreach (DataRowView row in dv)
            {
                if (tmpFormId.Equals(MenuKengenHoshuLogic.GetString_ByObject(row[MenuKengenHoshuConstans.FORM_ID])) &&
                    tmpWindowId == MenuKengenHoshuLogic.GetInt_ByObject(row[MenuKengenHoshuConstans.WINDOW_ID], -1))
                {
                    // 同じFormIDは積まない
                    continue;
                }

                M_MENU_AUTH_PT_DETAIL data = new M_MENU_AUTH_PT_DETAIL();
                data.FORM_ID = MenuKengenHoshuLogic.GetString_ByObject(row[MenuKengenHoshuConstans.FORM_ID]);
                data.WINDOW_ID = MenuKengenHoshuLogic.GetInt_ByObject(row[MenuKengenHoshuConstans.WINDOW_ID], -1);
                data.AUTH_READ = MenuKengenHoshuLogic.GetBool_ByObject(row[MenuKengenHoshuConstans.AUTH_READ]);
                data.AUTH_ADD = MenuKengenHoshuLogic.GetBool_ByObject(row[MenuKengenHoshuConstans.AUTH_ADD]);
                data.AUTH_EDIT = MenuKengenHoshuLogic.GetBool_ByObject(row[MenuKengenHoshuConstans.AUTH_EDIT]);
                data.AUTH_DELETE = MenuKengenHoshuLogic.GetBool_ByObject(row[MenuKengenHoshuConstans.AUTH_DELETE]);
                data.BIKOU = MenuKengenHoshuLogic.GetString_ByObject(row[MenuKengenHoshuConstans.BIKOU]);
                detailList.Add(data);

                tmpFormId = data.FORM_ID;
                tmpWindowId = int.Parse(data.WINDOW_ID.ToString());
            }

            return detailList;
        }

        #endregion パターン登録処理

        #region パターン呼出処理

        /// <summary>
        /// パターン呼出処理
        /// </summary>
        public bool PatternCall()
        {
            try
            {
                var headerForm = new r_framework.APP.Base.HeaderBaseForm();
                var callForm = new MenuKengenPtIchiranForm(this.menuItems);
                var popupForm = new Shougun.Core.Common.BusinessCommon.Base.BaseForm.BasePopForm(callForm, headerForm);
                var dialogResult = popupForm.ShowDialog();

                if (DialogResult.OK == dialogResult)
                {
                    this.patternID = callForm.PATTERN_ID;
                    this.SetPattern(this.patternID.ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("PatternCall", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        #region パターン反映処理

        /// <summary>
        /// パターンを画面に設定します
        /// </summary>
        /// <param name="patternId">パターンID</param>
        private void SetPattern(string patternId)
        {
            /*
             * 検索ロジックを使用してパターンの反映を行う。
             * （メニュー権限の保存情報をパターンのデータで置き換えている。）
             * 部署、社員、更新者情報は登録時には別途処理を行っているのでここでは空で設定している。
             */

            // パターン詳細 - 検索SQL
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("ET.PATTERN_NAME ");
            sb.Append(",ET.DELETE_FLG ");
            sb.Append(",DT.FORM_ID ");
            sb.Append(",DT.WINDOW_ID ");
            sb.Append(",DT.AUTH_READ ");
            sb.Append(",DT.AUTH_ADD ");
            sb.Append(",DT.AUTH_EDIT ");
            sb.Append(",DT.AUTH_DELETE ");
            sb.Append(",DT.BIKOU ");
            sb.Append("FROM M_MENU_AUTH_PT_ENTRY ET ");
            sb.Append("INNER JOIN M_MENU_AUTH_PT_DETAIL DT ON ET.PATTERN_ID = DT.PATTERN_ID ");
            sb.Append("WHERE ");
            sb.Append("ET.PATTERN_ID = " + patternId + " AND DELETE_FLG = 0 ");

            // パターンデータ取得
            IM_MENU_AUTH_PT_ENTRYDao dao = DaoInitUtility.GetComponent<IM_MENU_AUTH_PT_ENTRYDao>();
            DataTable patternDt = dao.GetDateForStringSql(sb.ToString());

            // 権限テーブル作成
            DataTable tblMenuAuth = new DataTable();
            tblMenuAuth.Columns.Add("BUSHO_CD", typeof(String));
            tblMenuAuth.Columns.Add("SHAIN_CD", typeof(String));
            tblMenuAuth.Columns.Add("FORM_ID", typeof(String));
            tblMenuAuth.Columns.Add("WINDOW_ID", typeof(Int32));
            tblMenuAuth.Columns.Add("AUTH_READ", typeof(Boolean));
            tblMenuAuth.Columns.Add("AUTH_ADD", typeof(Boolean));
            tblMenuAuth.Columns.Add("AUTH_EDIT", typeof(Boolean));
            tblMenuAuth.Columns.Add("AUTH_DELETE", typeof(Boolean));
            tblMenuAuth.Columns.Add("BIKOU", typeof(String));
            tblMenuAuth.Columns.Add("CREATE_USER", typeof(String));
            tblMenuAuth.Columns.Add("CREATE_DATE", typeof(DateTime));
            tblMenuAuth.Columns.Add("CREATE_PC", typeof(String));
            tblMenuAuth.Columns.Add("UPDATE_USER", typeof(String));
            tblMenuAuth.Columns.Add("UPDATE_DATE", typeof(DateTime));
            tblMenuAuth.Columns.Add("UPDATE_PC", typeof(String));
            tblMenuAuth.Columns.Add("DELETE_FLG", typeof(Boolean));
            tblMenuAuth.Columns.Add("TIME_STAMP", typeof(Byte[]));

            // メニュー権限データテーブル用初期化処理
            MenuKengenHoshuLogic.Init_ForAuthDataTable_Shain(tblMenuAuth);

            // 権限テーブルにパターンデータをマージ
            foreach (DataRow row in patternDt.Rows)
            {
                DataRow newRow = tblMenuAuth.NewRow();
                newRow[MenuKengenHoshuConstans.FORM_ID] = row[MenuKengenHoshuConstans.FORM_ID];
                newRow[MenuKengenHoshuConstans.WINDOW_ID] = row[MenuKengenHoshuConstans.WINDOW_ID];
                newRow[MenuKengenHoshuConstans.AUTH_READ] = row[MenuKengenHoshuConstans.AUTH_READ];
                newRow[MenuKengenHoshuConstans.AUTH_ADD] = row[MenuKengenHoshuConstans.AUTH_ADD];
                newRow[MenuKengenHoshuConstans.AUTH_EDIT] = row[MenuKengenHoshuConstans.AUTH_EDIT];
                newRow[MenuKengenHoshuConstans.AUTH_DELETE] = row[MenuKengenHoshuConstans.AUTH_DELETE];
                newRow[MenuKengenHoshuConstans.BIKOU] = row[MenuKengenHoshuConstans.BIKOU];
                newRow[MenuKengenHoshuConstans.DELETE_FLG] = row[MenuKengenHoshuConstans.DELETE_FLG];
                tblMenuAuth.Rows.Add(newRow);
            }

            // メニュー権限データテーブル設定
            this.SetAuthDataTable_ForMenuKbnShain(tblMenuAuth);

            if (this.SearchResult != null)
            {
                foreach (DataRow row in this.SearchResult.Rows)
                {
                    var formID = MenuKengenHoshuLogic.GetString_ByObject(row[MenuKengenHoshuConstans.FORM_ID]);
                    var windowID = MenuKengenHoshuLogic.GetNullableInt_ByObject(row[MenuKengenHoshuConstans.WINDOW_ID]);
                    DataRow[] rows;
                    if (MenuKengenHoshuLogic.FindDataRows_ByFormID(tblMenuAuth, formID, windowID, out rows))
                    {
                        rows[0][MenuKengenHoshuConstans.CREATE_USER] = row[MenuKengenHoshuConstans.CREATE_USER];
                        rows[0][MenuKengenHoshuConstans.CREATE_DATE] = row[MenuKengenHoshuConstans.CREATE_DATE];
                        rows[0][MenuKengenHoshuConstans.UPDATE_USER] = row[MenuKengenHoshuConstans.UPDATE_USER];
                        rows[0][MenuKengenHoshuConstans.UPDATE_DATE] = row[MenuKengenHoshuConstans.UPDATE_DATE];
                        rows[0][MenuKengenHoshuConstans.TIME_STAMP] = row[MenuKengenHoshuConstans.TIME_STAMP];
                    }
                }
            }

            // 検索結果設定
            this.SearchResult = tblMenuAuth;

            // 一覧に設定
            bool catchErr = this.SetIchiran();
            if (catchErr)
            {
                throw new Exception("");
            }

            // 一括チェックボックス反映
            foreach (var row in this.form.Ichiran.Rows)
            {
                MenuKengenHoshuLogic.SetToAllCheck_Row(row);
            }

            // 適用パターン名表示
            this.form.PATTERN_NAME.Text = patternDt.Rows[0]["PATTERN_NAME"].ToString();
        }

        #endregion パターン反映処理

        #endregion パターン呼出処理

        #region データテーブルへカラム追加、Null許容、Unique操作処理

        /// <summary>
        /// データテーブルへカラム追加
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="Type">データタイプ</param>
        private static void AddColumn_ForDataTable(DataTable table, string columnName, Type type)
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
        private static void SetAllowDBNull_ForDataTable(DataTable table, string columnName, bool isAllowDBNull)
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
        private static void SetUnique_ForDataTable(DataTable table, string columnName, bool isUnique)
        {
            if (table.Columns.Contains(columnName))
            {
                table.Columns[columnName].Unique = isUnique;
            }
        }

        #endregion データテーブルへカラム追加、Null許容、Unique操作処理

        #region 社員一覧 - 検索(フォーカスIN)処理

        /// <summary>
        /// 検索用社員項目の値で社員一覧を検索しフォーカスを当てます
        /// </summary>
        internal bool SearchIchiranShainRow()
        {
            try
            {
                string shainCd = this.form.SEARCH_SHAIN_CD.Text;
                string shainName = this.form.SEARCH_SHAIN_NAME.Text;

                if (string.IsNullOrEmpty(shainCd) || string.IsNullOrEmpty(shainName))
                {
                    return false;
                }

                foreach (var row in this.form.Ichiran_Shain.Rows)
                {
                    if (row.Cells[MenuKengenHoshuConstans.CELL_SHAIN_CD].Value.ToString().Equals(shainCd) &&
                        row.Cells[MenuKengenHoshuConstans.CELL_SHAIN_NAME].Value.ToString().Equals(shainName))
                    {
                        int rowIndex = row.Index;
                        this.form.Ichiran_Shain.CurrentCell = this.form.Ichiran_Shain[rowIndex, 0];
                        this.form.Ichiran_Shain.Focus();
                        break;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchIchiranShainRow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion 社員一覧 - 検索(フォーカスIN)処理

        #region 各名称取得

        /// <summary>
        /// 部署CDをキーにして業者マスターから部署名を取得する。
        /// </summary>
        /// <param name="bushoCD"></param>
        /// <param name="bushoNameRyaku"></param>
        /// <returns></returns>
        public bool GetBushoName(string bushoCD, out string bushoNameRyaku, out bool catchErr)
        {
            try
            {
                bushoNameRyaku = string.Empty;
                catchErr = false;
                LogUtility.DebugMethodStart(bushoCD, bushoNameRyaku, catchErr);

                var result = false;
                var search = new M_BUSHO();
                search.BUSHO_CD = bushoCD;
                var bushoArray = this.daoBusho.GetAllValidData(search);
                if (bushoArray != null && bushoArray.Length > 0)
                {
                    bushoNameRyaku = bushoArray[0].BUSHO_NAME_RYAKU;
                    result = true;
                }

                LogUtility.DebugMethodEnd(bushoCD, bushoNameRyaku, catchErr);
                return result;
            }
            catch (Exception ex)
            {
                bushoNameRyaku = "";
                catchErr = true;
                LogUtility.Error("GetBushoName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(bushoCD, bushoNameRyaku, catchErr);
                return false;
            }
        }

        /// <summary>
        /// 社員CDをキーにして現場マスターから社員名を取得する。
        /// 部署CDが一致する場合のみ値を返す。
        /// </summary>
        /// <param name="shainCD"></param>
        /// <param name="bushoCD"></param>
        /// <param name="shainNameRyaku"></param>
        /// <returns></returns>
        public bool GetShainName(string shainCD, ref string bushoCD, out string shainNameRyaku, out bool catchErr)
        {
            try
            {
                catchErr = false;
                shainNameRyaku = string.Empty;
                LogUtility.DebugMethodStart(shainCD, bushoCD, shainNameRyaku);

                var result = false;
                var search = new M_SHAIN();
                search.SHAIN_CD = shainCD;
                if (!string.IsNullOrEmpty(bushoCD))
                {
                    search.BUSHO_CD = bushoCD;
                }
                var shainArray = this.daoShain.GetAllValidData(search);
                if (shainArray != null && shainArray.Length > 0)
                {
                    var shain = shainArray[0];
                    if (string.IsNullOrEmpty(bushoCD))
                    {
                        bushoCD = shain.BUSHO_CD;
                        shainNameRyaku = shain.SHAIN_NAME;
                        result = true;
                    }
                    else if (bushoCD.Equals(shain.BUSHO_CD))
                    {
                        shainNameRyaku = shain.SHAIN_NAME;
                        result = true;
                    }
                }

                LogUtility.DebugMethodEnd(shainCD, bushoCD, shainNameRyaku, catchErr);
                return result;
            }
            catch (Exception ex)
            {
                catchErr = true;
                shainNameRyaku = string.Empty;
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(shainCD, bushoCD, shainNameRyaku, catchErr);
                return false;
            }
        }

        #endregion 各名称取得

        #region Utility

        /// <summary>
        /// 文字列取得
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetString_ByObject(object value)
        {
            var result = value as string;
            if (result == null)
            {
                result = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// INT取得
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static int GetInt_ByObject(object value, int defaultValue = 0)
        {
            int result = defaultValue;
            if (value is int)
            {
                result = (int)value;
            }

            return result;
        }

        /// <summary>
        /// NULL許容INT取得
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int? GetNullableInt_ByObject(object value)
        {
            int? result = null;
            int iTemp;
            if (value != null && int.TryParse(value.ToString(), out iTemp))
            {
                result = iTemp;
            }
            return result;
        }

        /// <summary>
        /// Bool取得
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool GetBool_ByObject(object value)
        {
            var result = value as bool?;
            if (!result.HasValue)
            {
                result = false;
            }

            return result.Value;
        }

        #endregion Utility

        #region Equals/GetHashCode/ToString

        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            MenuKengenHoshuLogic localLogic = other as MenuKengenHoshuLogic;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion Equals/GetHashCode/ToString

        #region 未使用

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

        #endregion 未使用

        #endregion - Method -
    }
}