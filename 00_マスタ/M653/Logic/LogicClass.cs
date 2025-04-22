// $Id: LogicClass.cs 56232 2015-07-21 06:20:31Z j-kikuchi $
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.DenManiKansanHoshu
{
    /// <summary>
    /// 電マニ換算値入力Logic
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region - Field -
        /// <summary>画面Load完了フラグ</summary>
        private bool dispLoadComplete;   // TRUE:画面Load完了, FALSE:画面Load中
        /// <summary>メインフォーム</summary>
        private UIForm form;
        /// <summary>ベースフォーム</summary>
        private MasterBaseForm parentForm;
        /// <summary>ヘッダフォーム</summary>
        private MasterHeaderForm headerForm;
        /// <summary>リボンメニュー</summary>
        private RibbonMainMenu ribbon;
        /// <summary>DBAccessor</summary>
        private DBAccessor accessor;
        /// <summary>メッセージ表示Logic</summary>
        private MessageBoxShowLogic msgLogic;
        /// <summary>細分類読込フラグ</summary>
        private bool saibunruiLoadFlag;   // TRUE:細分類読込時, FALSE:それ以外

        #endregion - Field -

        #region - Constructor -
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            // フィールドの初期化
            this.dispLoadComplete = false;
            this.saibunruiLoadFlag = false;
            this.form = targetForm;
            this.accessor = new DBAccessor();
            this.msgLogic = new MessageBoxShowLogic();
        }

        #endregion - Constructor -

        #region - Initialize -
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            // ParentFormのSet
            this.parentForm = (MasterBaseForm)this.form.Parent;

            // HeaderFormのSet
            this.headerForm = (MasterHeaderForm)this.parentForm.headerForm;

            // RibbonMenuのSet
            this.ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            // モードラベルは非表示
            this.headerForm.windowTypeLabel.Visible = false;

            // タイトル設定
            this.headerForm.lb_title.Location = new Point(0, this.headerForm.lb_title.Location.Y);
            this.headerForm.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENSHI_MANIFEST_KANSAN);
            this.parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENSHI_MANIFEST_KANSAN));
            ControlUtility.AdjustTitleSize(this.headerForm.lb_title, this.headerForm.lb_title.Width);

            // 一覧設定
            this.form.Ichiran.Anchor |= AnchorStyles.Bottom | AnchorStyles.Right;

            // 基本単位を初期化
            this.form.KIHON_UNIT_CD.Text = this.accessor.getUnitName(this.ribbon.GlobalCommonInformation.SysInfo.MANI_KANSAN_KIHON_UNIT_CD.ToString());

            // 画面初期化
            this.reLoad();

            // 画面Load完了
            this.dispLoadComplete = true;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            // ボタン名の初期化
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            // F4 削除
            this.form.C_Regist(this.parentForm.bt_func4);
            this.parentForm.bt_func4.Click += new EventHandler(this.form.functionKeyClick);
            // F6 CSV出力
            this.parentForm.bt_func6.Click += new EventHandler(this.form.functionKeyClick);
            // F7 条件クリア
            this.parentForm.bt_func7.Click += new EventHandler(this.form.functionKeyClick);
            // F8 検索
            this.parentForm.bt_func8.Click += new EventHandler(this.form.functionKeyClick);
            // F9 登録
            this.form.C_Regist(this.parentForm.bt_func9);
            this.parentForm.bt_func9.Click += new EventHandler(this.form.functionKeyClick);
            // F11 取消
            this.parentForm.bt_func11.Click += new EventHandler(this.form.functionKeyClick);
            // F12 閉じる
            this.parentForm.bt_func12.Click += new EventHandler(this.form.functionKeyClick);
            // SubFunction1 細分類読込
            this.parentForm.bt_process1.Click += new EventHandler(this.form.functionKeyClick);
            // 一覧系イベント
            this.form.Ichiran.RowValidating += new DataGridViewCellCancelEventHandler(this.form.Ichiran_RowValidating);
            this.form.Ichiran.CellEnter += new DataGridViewCellEventHandler(this.form.Ichiran_CellEnter);
            this.form.Ichiran.CellValidating += new DataGridViewCellValidatingEventHandler(this.form.Ichiran_CellValidating);
            this.form.Ichiran.CellFormatting += new DataGridViewCellFormattingEventHandler(this.form.Ichiran_CellFormatting);
            this.form.Ichiran.CurrentCellDirtyStateChanged += new EventHandler(this.form.Ichiran_CurrentCellDirtyStateChanged);
            // 加入者番号
            this.form.EDI_MEMBER_ID.TextChanged += new EventHandler(this.form.EDI_MEMBER_ID_TextChanged);
            // 電子廃棄物種類
            this.form.HAIKI_SHURUI_CD.TextChanged += new EventHandler(this.form.HAIKI_SHURUI_CD_TextChanged);
            // 検索条件
            this.form.SEARCH_CONDITION_ITEM.Validated += new EventHandler(this.form.SEARCH_CONDITION_ITEM_Validated);
        }

        #endregion - Initialize -

        #region - Utility -
        #region - FunctionProc -
        /// <summary>
        /// 削除処理
        /// </summary>
        internal void delete()
        {
            // 削除登録
            this.registDB(true);
        }

        /// <summary>
        /// CSV出力処理
        /// </summary>
        internal bool csvOutput()
        {
            bool ret = true;
            try
            {
                if (this.form.Ichiran.RowCount > 1)
                {
                    // CSV出力しますか？
                    if (this.msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        // 一覧の内容をCSV出力
                        var csv = new CSVExport();
                        csv.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, true, this.headerForm.lb_title.Text, this.form);
                    }
                }
                else
                {
                    // 出力対象が存在しない場合はエラー表示
                    this.msgLogic.MessageBoxShow("E044");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("csvOutput", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 条件クリア処理
        /// </summary>
        internal bool clearCondition()
        {
            bool ret = true;
            try
            {
                // 検索条件を全てクリア
                this.form.EDI_MEMBER_ID.Text = "";
                this.form.JIGYOUSHA_NAME.Text = "";
                this.form.HAIKI_SHURUI_CD.Text = "";
                this.form.HAIKI_SHURUI_NAME.Text = "";
                this.form.SEARCH_CONDITION_ITEM.Text = "";
                this.form.SEARCH_CONDITION_VALUE.Text = "";
                this.form.SEARCH_CONDITION_VALUE.DBFieldsName = "";

                // 表示条件を初期化
                this.form.SHOW_CONDITION_DELETED.Checked = this.ribbon.GlobalCommonInformation.SysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;

                // 加入者にフォーカス
                this.form.EDI_MEMBER_ID.Focus();

                // 明細クリア
                if (!this.ichiranClear())
                {
                    ret = false; 
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("clearCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        internal bool search()
        {
            bool ret = true;
            try
            {
                if (this.isSearchExec())
                {
                    // 画面内の情報を基に一覧情報を取得
                    var dto = this.createCondition();
                    dto.entity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                    dto.entity.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
                    dto.SHOW_CONDITION_DELETED = this.form.SHOW_CONDITION_DELETED.Checked;
                    var table = this.accessor.getIchiranData(dto);

                    // 抽出結果を一覧にセット
                    this.form.Ichiran.AutoGenerateColumns = false;
                    this.form.Ichiran.DataSource = table;

                    // Lock解除
                    this.dispLock(false);

                    // 検索条件を保存
                    this.conditionCtrl(true);

                    // 加入者にフォーカス
                    this.form.EDI_MEMBER_ID.Focus();

                    // フラグ初期化
                    this.saibunruiLoadFlag = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        internal void regist()
        {
            // 新規登録・更新
            this.registDB(false);
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        internal bool reLoad()
        {
            bool ret = true;
            try
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.EDI_MEMBER_ID))
                {
                    // 保存検索条件を復元
                    this.conditionCtrl(false);

                    // 再検索
                    this.search();
                }
                else
                {
                    // 保存設定が存在しなければ検索条件を初期状態にする
                    this.clearCondition();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("reLoad", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 画面Close処理
        /// </summary>
        internal void closeDisp()
        {
            // 画面Close
            this.parentForm.Close();
        }

        /// <summary>
        /// 細分類読込処理
        /// </summary>
        internal bool saibunruiLoad()
        {
            bool ret = true;

            try
            {
                if (this.isSearchExec())
                {
                    // 確認メッセージ表示
                    var result = this.msgLogic.MessageBoxShow("C066", "換算値", "細分類");
                    if (result == DialogResult.Yes)
                    {
                        // 「はい」選択時

                        // 表示条件を[適用中]に
                        this.form.SHOW_CONDITION_DELETED.Checked = false;

                        // 画面内の情報を基に一覧情報を取得
                        var dto = new findConditionDTO();
                        dto.entity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                        dto.entity.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
                        var table = this.accessor.getSaibunruiLoadData(dto);

                        // 抽出結果を一覧にセット
                        this.form.Ichiran.AutoGenerateColumns = false;
                        this.form.Ichiran.DataSource = table;

                        // Lock解除
                        this.dispLock(false);

                        // 検索条件を保存
                        this.conditionCtrl(true);

                        // 加入者にフォーカス
                        this.form.EDI_MEMBER_ID.Focus();

                        // フラグセット
                        this.saibunruiLoadFlag = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("saibunruiLoad", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("saibunruiLoad", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion - FunctionProc -

        #region - DetailProc -
        /// <summary>
        /// 一覧RowValidating処理
        /// </summary>
        /// <param name="e"></param>
        internal bool ichiranRowValidatingProc(DataGridViewCellCancelEventArgs e)
        {
            bool ret = true;
            try
            {
                // 編集対象のセット
                var row = this.form.Ichiran.Rows[e.RowIndex];

                if (!this.rowDuplicationCheck(row))
                {
                    // 重複ありのため編集キャンセル
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ichiranRowValidatingProc", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 一覧CellEnter処理
        /// </summary>
        /// <param name="e"></param>
        internal bool ichiranCellEnterProc(DataGridViewCellEventArgs e)
        {
            bool ret = true;
            try
            {
                // 編集対象のセット
                var row = this.form.Ichiran.Rows[e.RowIndex];

                if (row.IsNewRow)
                {
                    // 新規行の場合

                    // 削除チェック無効
                    row.Cells["clmCHECK_DELETE"].ReadOnly = true;
                    row.Cells["clmCHECK_DELETE"].Value = false;

                    // 単位区分の初期値をセット
                    var unitCD = this.ribbon.GlobalCommonInformation.SysInfo.MANI_KANSAN_UNIT_CD.ToString();
                    row.Cells["clmUNIT_CD"].Value = unitCD;
                    row.Cells["clmUNIT_NAME"].Value = this.accessor.getUnitName(unitCD);

                    //計算式を×に設定
                    row.Cells["clmKANSANSHIKI"].Value = "×";
                }
                else
                {
                    // 新規行以外

                    // 削除チェック有効
                    row.Cells["clmCHECK_DELETE"].ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ichiranCellEnterProc", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 一覧CellValidating処理
        /// </summary>
        /// <param name="e"></param>
        internal bool ichiranCellValidatingProc(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                // 編集対象のセット
                var row = this.form.Ichiran.Rows[e.RowIndex];
                var col = this.form.Ichiran.Columns[e.ColumnIndex];

                switch (col.Name)
                {
                    // 単位CD
                    case "clmUNIT_CD":
                        var unitName = "";
                        if (row.Cells["clmUNIT_CD"].Value != null)
                        {
                            // 入力された単位CDより単位名を取得
                            unitName = this.accessor.getUnitName(Convert.ToString(row.Cells["clmUNIT_CD"].Value));
                        }

                        // 単位名セット
                        row.Cells["clmUNIT_NAME"].Value = unitName;
                        break;
                    // 細分類CD
                    case "clmHAIKI_SHURUI_SAIBUNRUI_CD":
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Value)))
                        {
                            row.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Value = row.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Value.ToString().PadLeft(3, '0').ToUpper();

                            // 画面内の情報を基に細分類マスタを検索
                            var dto = new findConditionDTO();
                            dto.entity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                            dto.entity.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
                            dto.entity.HAIKI_SHURUI_SAIBUNRUI_CD = row.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Value.ToString();
                            var saibunruiName = this.accessor.getSaibunruiName(dto);

                            // 細分類名をセット
                            row.Cells["clmHAIKI_SHURUI_SAIBUNRUI_NAME"].Value = saibunruiName;
                            if (saibunruiName == "")
                            {
                                // 該当情報が存在しない場合はエラー表示
                                this.msgLogic.MessageBoxShow("E020", "電子廃棄物細分類");
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            // 細分類名クリア
                            row.Cells["clmHAIKI_SHURUI_SAIBUNRUI_NAME"].Value = "";
                        }
                        break;
                    default:
                        // DO NOTHING
                        break;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ichiranCellValidatingProc", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ichiranCellValidatingProc", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 一覧CellFormatting処理
        /// </summary>
        /// <param name="e"></param>
        internal bool ichiranCellFormattingProc(DataGridViewCellFormattingEventArgs e)
        {
            bool ret = true;
            try
            {
                // 編集対象のセット
                var row = this.form.Ichiran.Rows[e.RowIndex];
                var col = this.form.Ichiran.Columns[e.ColumnIndex];

                switch (col.Name)
                {
                    // 単位CD
                    case "clmUNIT_CD":
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["clmUNIT_NAME"].Value)))
                        {
                            // 単位CDを単位名に置き換え
                            e.Value = row.Cells["clmUNIT_NAME"].Value.ToString();
                        }
                        break;
                    default:
                        // DO NOTHING
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ichiranCellFormattingProc", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 一覧CurrentCellDirtyStateChanged処理
        /// </summary>
        internal bool ichiranCurrentCellDirtyStateChanged()
        {
            bool ret = true;
            try
            {
                // 編集対象のセット
                var col = this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex];

                switch (col.Name)
                {
                    // 単位CD
                    case "clmUNIT_CD":
                        if (this.form.Ichiran.IsCurrentCellDirty)
                        {
                            // 数値以外の入力があった場合それを取り消す
                            this.form.Ichiran.EditingControl.Text = Regex.Replace(this.form.Ichiran.EditingControl.Text, @"[^0-9]", string.Empty);
                            this.form.Ichiran.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        }
                        break;
                    default:
                        // DO NOTHING
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ichiranCurrentCellDirtyStateChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion - DetailProc -

        #region - Other -
        /// <summary>
        /// 明細一覧クリア
        /// </summary>
        internal bool ichiranClear()
        {
            bool ret = true;
            try
            {
                if (this.form.Ichiran.DataSource != null)
                {
                    // 明細一覧クリア
                    var table = (DataTable)this.form.Ichiran.DataSource;
                    this.form.Ichiran.DataSource = table.Clone();
                }

                // Lock
                this.dispLock(true);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ichiranClear", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 検索条件ImeMode制御
        /// </summary>
        internal void changeIME()
        {
            // 検索対象項目に応じてIMEを切り替える
            switch (this.form.SEARCH_CONDITION_ITEM.Text)
            {
                case "細分類名":
                case "単位区分※":
                case "換算式":
                case "備考":
                case "作成者":
                case "更新者":
                    // ひらがな入力
                    this.form.SEARCH_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;
                default:
                    // 上記以外は無効
                    this.form.SEARCH_CONDITION_VALUE.ImeMode = ImeMode.Disable;
                    break;
            }
        }

        #endregion - Other -

        #endregion - Utility -

        #region - PrivateUtility -
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            // ButtonSetting.xmlよりボタン情報の読込
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 画面内の情報を基に一覧情報を取得
        /// </summary>
        /// <returns name="findConditionDTO">検索条件</returns>
        private findConditionDTO createCondition()
        {
            var dto = new findConditionDTO();

            // 加入者番号
            dto.entity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
            // 電子廃棄物種類
            dto.entity.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
            // 検索条件
            if ((!string.IsNullOrEmpty(this.form.SEARCH_CONDITION_ITEM.Text)) && (!string.IsNullOrEmpty(this.form.SEARCH_CONDITION_VALUE.Text)))
            {
                switch (this.form.SEARCH_CONDITION_VALUE.DBFieldsName)
                {
                    case "HAIKI_SHURUI_SAIBUNRUI_CD":
                        // 廃棄種類細分類CD
                        dto.entity.HAIKI_SHURUI_SAIBUNRUI_CD = this.form.SEARCH_CONDITION_VALUE.Text;
                        break;
                    case "HAIKI_SHURUI_NAME":
                        // 廃棄種類名
                        dto.HAIKI_SHURUI_NAME = this.form.SEARCH_CONDITION_VALUE.Text;
                        break;
                    case "UNIT_CD":
                        // 単位区分を検索する場合は対象を単位略称名とする
                        dto.UNIT_NAME_RYAKU = this.form.SEARCH_CONDITION_VALUE.Text;
                        break;
                    case "KANSANCHI":
                        // 換算値
                        decimal val;
                        if (decimal.TryParse(this.form.SEARCH_CONDITION_VALUE.Text, out val))
                        {
                            dto.entity.KANSANCHI = val;
                        }
                        break;
                    case "MANIFEST_KANSAN_BIKOU":
                        // マニフェスト換算備考
                        dto.entity.MANIFEST_KANSAN_BIKOU = this.form.SEARCH_CONDITION_VALUE.Text;
                        break;
                    case "CREATE_USER":
                        // 作成者
                        dto.entity.CREATE_USER = this.form.SEARCH_CONDITION_VALUE.Text;
                        break;
                    case "CREATE_DATE":
                        // 作成日
                        dto.entity.SEARCH_CREATE_DATE = this.form.SEARCH_CONDITION_VALUE.Text;
                        break;
                    case "UPDATE_USER":
                        // 更新者
                        dto.entity.UPDATE_USER = this.form.SEARCH_CONDITION_VALUE.Text;
                        break;
                    case "UPDATE_DATE":
                        // 更新日
                        dto.entity.SEARCH_UPDATE_DATE = this.form.SEARCH_CONDITION_VALUE.Text;
                        break;
                    default:
                        //DO NOTHING
                        break;
                }
            }
            // 表示条件
            dto.SHOW_CONDITION_DELETED = this.form.SHOW_CONDITION_DELETED.Checked;

            var table = this.accessor.getIchiranData(dto);

            return dto;
        }

        /// <summary>
        /// 検索実行可否チェック
        /// </summary>
        /// <returns name="bool">TRUE:検索実行可, FALSE:検索実行不可</returns>
        private bool isSearchExec()
        {
            var bRet = true;
            var errStr = new StringBuilder(256);

            // 加入者番号入力チェック
            if (string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.Text))
            {
                // 未入力の場合は必須入力要求文字列をセット
                var str = Message.MessageUtility.GetMessageString("E001");
                errStr.AppendLine(String.Format(str, this.form.EDI_MEMBER_ID.DisplayItemName));
                if (this.dispLoadComplete)
                {
                    // 画面起動時以外は未入力のCtrlの背景を変更する
                    this.form.EDI_MEMBER_ID.BackColor = Constans.ERROR_COLOR;
                }
            }

            // 電子廃棄物種類入力チェック
            if (string.IsNullOrEmpty(this.form.HAIKI_SHURUI_CD.Text))
            {
                // 未入力の場合は必須入力要求文字列をセット
                var str = Message.MessageUtility.GetMessageString("E001");
                errStr.AppendLine(String.Format(str, this.form.HAIKI_SHURUI_CD.DisplayItemName));
                if (this.dispLoadComplete)
                {
                    // 画面起動時以外は未入力のCtrlの背景を変更する
                    this.form.HAIKI_SHURUI_CD.BackColor = Constans.ERROR_COLOR;
                }
            }

            if (errStr.Length > 0)
            {
                if (this.dispLoadComplete)
                {
                    // 画面起動時以外は必須入力要求エラーを表示
                    this.msgLogic.MessageBoxShowError(errStr.ToString());
                }

                // 加入者番号・廃棄物種類CD何れかの入力が無ければ検索実行不可
                bRet = false;
            }

            return bRet;
        }

        /// <summary>
        /// 画面一部Lock
        /// </summary>
        /// <param name="sts">TRUE:Lock, FALSE:Lock解除</param>
        /// <remarks>検索実行前は明細・[F9:登録]等をLockする</remarks>
        private void dispLock(bool sts)
        {
            if (sts)
            {
                // Lock

                // 削除機能
                this.parentForm.bt_func4.Enabled = false;
                // CSV出力機能
                this.parentForm.bt_func6.Enabled = false;
                // 登録機能
                this.parentForm.bt_func9.Enabled = false;
                // 明細
                this.form.Ichiran.Enabled = false;
            }
            else
            {
                // 権限チェック 修正権限が無い場合はLockの解除を行わない
                if (Manager.CheckAuthority("M653", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // Lock解除

                    // 削除機能
                    this.parentForm.bt_func4.Enabled = true;
                    // CSV出力機能
                    this.parentForm.bt_func6.Enabled = true;
                    // 登録機能
                    this.parentForm.bt_func9.Enabled = true;
                    // 明細
                    this.form.Ichiran.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 条件制御
        /// </summary>
        /// <param name="sts">TRUE:条件保存, FALSE:条件読込</param>
        private void conditionCtrl(bool sts)
        {
            if (sts)
            {
                // 条件保存
                Properties.Settings.Default.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                Properties.Settings.Default.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
                Properties.Settings.Default.SEARCH_CONDITION_VALUE_TEXT = this.form.SEARCH_CONDITION_VALUE.Text;
                Properties.Settings.Default.SEARCH_CONDITION_VALUE_DBFIELDSNAME = this.form.SEARCH_CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.SEARCH_CONDITION_ITEM = this.form.SEARCH_CONDITION_ITEM.Text;
                Properties.Settings.Default.SHOW_CONDITION_DELETED = this.form.SHOW_CONDITION_DELETED.Checked;
            }
            else
            {
                // 条件読込
                this.form.EDI_MEMBER_ID.Text = Properties.Settings.Default.EDI_MEMBER_ID;
                this.form.JIGYOUSHA_NAME.Text = this.accessor.getJigyoushaName(Properties.Settings.Default.EDI_MEMBER_ID);
                this.form.HAIKI_SHURUI_CD.Text = Properties.Settings.Default.HAIKI_SHURUI_CD;
                this.form.HAIKI_SHURUI_NAME.Text = this.accessor.getDenshiShuruiName(Properties.Settings.Default.HAIKI_SHURUI_CD);
                this.form.SEARCH_CONDITION_VALUE.Text = Properties.Settings.Default.SEARCH_CONDITION_VALUE_TEXT;
                this.form.SEARCH_CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.SEARCH_CONDITION_VALUE_DBFIELDSNAME;
                this.form.SEARCH_CONDITION_ITEM.Text = Properties.Settings.Default.SEARCH_CONDITION_ITEM;
                this.form.SHOW_CONDITION_DELETED.Checked = Properties.Settings.Default.SHOW_CONDITION_DELETED;

                // 検索条件ImeMode制御
                this.changeIME();
            }
        }

        /// <summary>
        /// DB登録処理
        /// </summary>
        /// <param name="delete">TRUE:削除登録, FALSE:新規登録・更新</param>
        private bool registDB(bool delete)
        {
            bool ret = true;
            if (this.registExecCheck())
            {
                try
                {
                    if (this.saibunruiLoadFlag)
                    {
                        // 細分類読込したものを登録する場合は適用中のマスタ情報を一括で削除する
                        this.enableDataAllDelete();
                    }

                    // 画面の内容からEntityのListを作成
                    var entityList = this.createEntity(delete);

                    // 新規登録・更新, 削除登録処理
                    this.accessor.registEntity(entityList, delete);

                    if (delete)
                    {
                        // 削除完了メッセージ表示
                        this.msgLogic.MessageBoxShow("I001", "削除");
                    }
                    else
                    {
                        // 登録完了メッセージ表示
                        this.msgLogic.MessageBoxShow("I001", "登録");
                    }

                    // 再検索
                    this.search();
                }
                catch (NotSingleRowUpdatedRuntimeException ex1)
                {
                    LogUtility.Error("registDB", ex1);
                    this.form.errmessage.MessageBoxShow("E080", "");
                    return false;
                }
                catch (SQLRuntimeException ex2)
                {
                    LogUtility.Error("registDB", ex2);
                    this.form.errmessage.MessageBoxShow("E093", "");
                    return false;
                }
                catch (Exception ex)
                {
                    // 登録エラーメッセージ表示
                    LogUtility.Error("registDB", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                    return false;
                }
            }
            return ret;
        }

        /// <summary>
        /// 画面の内容からEntityのListを作成
        /// </summary>
        /// <param name="delete">TRUE:削除登録, FALSE:新規登録・更新</param>
        /// <returns name="List<M_DENSHI_MANIFEST_KANSAN>">EntityList</returns>
        private List<M_DENSHI_MANIFEST_KANSAN> createEntity(bool delete)
        {
            var retList = new List<M_DENSHI_MANIFEST_KANSAN>();
            var dataBinderLogic = new DataBinderLogic<M_DENSHI_MANIFEST_KANSAN>(retList.ToArray());

            foreach (DataGridViewRow row in this.form.Ichiran.Rows)
            {
                var entity = new M_DENSHI_MANIFEST_KANSAN();
                if (!row.IsNewRow)
                {
                    // チェック状態と合致するもののみ処理
                    if (delete == (bool)row.Cells["clmCHECK_DELETE"].Value)
                    {
                        // 必須項目
                        entity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                        entity.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
                        entity.HAIKI_SHURUI_SAIBUNRUI_CD = row.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Value.ToString();
                        entity.UNIT_CD = short.Parse(row.Cells["clmUNIT_CD"].Value.ToString());

                        // 削除時は必須情報のみ
                        if (!delete)
                        {
                            // 新規登録・更新時

                            // 換算式は「×:0」固定
                            entity.KANSANSHIKI = 0;

                            // 任意項目
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["clmKANSANCHI"].Value)))
                            {
                                // 換算値
                                entity.KANSANCHI = decimal.Parse(row.Cells["clmKANSANCHI"].Value.ToString());
                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["clmMANIFEST_KANSAN_BIKOU"].Value)))
                            {
                                // マニフェスト換算備考
                                entity.MANIFEST_KANSAN_BIKOU = row.Cells["clmMANIFEST_KANSAN_BIKOU"].Value.ToString();
                            }

                            // SystemPropertyをセット
                            dataBinderLogic.SetSystemProperty(entity, false);
                        }

                        // 削除フラグ
                        entity.DELETE_FLG = delete;

                        // EntityListに追加
                        retList.Add(entity);
                    }
                }
            }

            // EntityListを返却
            return retList;
        }

        /// <summary>
        /// 登録更新処理実行チェック
        /// </summary>
        /// <returns name="bool">TRUE:実行可, FALSE:エラー発生のため不可</returns>
        private bool registExecCheck()
        {
            var bRet = true;

            // 新規行以外のデータがあれば続行
            if (this.form.Ichiran.RowCount == 1)
            {
                // 登録エラーメッセージ表示
                bRet = false;
                this.msgLogic.MessageBoxShow("E061");
            }

            return bRet;
        }

        /// <summary>
        /// 行単位の重複チェック
        /// </summary>
        /// <param name="row">編集行</param>
        /// <returns name="bool">TRUE:正常, FALSE:エラー</returns>
        /// <remarks>
        /// Cell編集時、今現在表示されている一覧から重複されているデータを検索する
        /// </remarks>
        private bool rowDuplicationCheck(DataGridViewRow editRow)
        {
            bool bRet = true;

            // エラー背景色をクリア
            editRow.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Style.BackColor = Constans.NOMAL_COLOR;
            editRow.Cells["clmUNIT_CD"].Style.BackColor = Constans.NOMAL_COLOR;

            if ((!string.IsNullOrEmpty(Convert.ToString(editRow.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Value))) &&
               (!string.IsNullOrEmpty(Convert.ToString(editRow.Cells["clmUNIT_CD"].Value))))
            {
                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    // 編集行はチェック対象外
                    if (editRow.Index != row.Index)
                    {
                        if ((!string.IsNullOrEmpty(Convert.ToString(row.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Value))) &&
                           (!string.IsNullOrEmpty(Convert.ToString(row.Cells["clmUNIT_CD"].Value))))
                        {
                            // 細分類CDが一致した場合
                            if (true == row.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Value.Equals(editRow.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Value))
                            {
                                // 単位CDが一致した場合
                                if (true == row.Cells["clmUNIT_CD"].Value.Equals(editRow.Cells["clmUNIT_CD"].Value))
                                {
                                    // 全てが一致したら、重複ありとして返却
                                    bRet = false;
                                }
                            }
                        }
                    }
                }
            }

            if (!bRet)
            {
                // 重複ありの場合、エラー表示
                editRow.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"].Style.BackColor = Constans.ERROR_COLOR;
                ((DgvCustomTextBoxCell)editRow.Cells["clmHAIKI_SHURUI_SAIBUNRUI_CD"]).IsInputErrorOccured = true;
                editRow.Cells["clmUNIT_CD"].Style.BackColor = Constans.ERROR_COLOR;
                ((DgvCustomTextBoxCell)editRow.Cells["clmUNIT_CD"]).IsInputErrorOccured = true;
                this.msgLogic.MessageBoxShow("E031", "細分類CD・単位CD");
            }

            return bRet;
        }

        /// <summary>
        /// 適用中のデータを全て削除
        /// </summary>
        /// <returns name="bool">TRUE:成功, FALSE:失敗</returns>
        /// <remarks>
        /// 細分類読込したものを登録する場合は適用中のマスタ情報を一括で削除する
        /// </remarks>
        private void enableDataAllDelete()
        {
            // 画面内の情報を基に適用中の一覧情報を取得
            var dto = new findConditionDTO();
            dto.entity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
            dto.entity.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
            dto.SHOW_CONDITION_DELETED = false;
            var table = this.accessor.getIchiranData(dto);

            if (table != null)
            {
                // 一括削除
                var delEntityList = new List<M_DENSHI_MANIFEST_KANSAN>();
                foreach (DataRow row in table.Rows)
                {
                    // 削除EntityList生成
                    var entity = new M_DENSHI_MANIFEST_KANSAN();
                    entity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                    entity.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
                    entity.HAIKI_SHURUI_SAIBUNRUI_CD = row["HAIKI_SHURUI_SAIBUNRUI_CD"].ToString();
                    entity.UNIT_CD = short.Parse(row["UNIT_CD"].ToString());
                    entity.DELETE_FLG = true;
                    delEntityList.Add(entity);
                }

                // 削除登録
                this.accessor.registEntity(delEntityList, true);
            }

            return;
        }

        #endregion - PrivateUtility -

        #region IF member
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion IF member
    }
}
