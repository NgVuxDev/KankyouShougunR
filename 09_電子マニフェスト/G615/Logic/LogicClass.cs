// $Id: LogicClass.cs 42593 2015-02-18 02:29:13Z takeda $
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Inspection.KongouHaikibutsuJoukyouIchiran;

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuJoukyouIchiran
{
    /// <summary>
    /// 混合廃棄物状況一覧Logic
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region - Field -

        /// <summary>メインフォーム</summary>
        private UIForm form;

        /// <summary>ベースフォーム</summary>
        private BusinessBaseForm parentForm;

        /// <summary>ヘッダフォーム</summary>
        private HeaderBaseForm headerForm;

        /// <summary>リボンメニュー</summary>
        private RibbonMainMenu ribbon;

        /// <summary>DBAccessor</summary>
        private DBAccessor accessor;

        /// <summary>メッセージ表示Logic</summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>検索条件前回値</summary>
        private findConditionDTO oldCondition;

        #endregion - Field -

        #region - Constructor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            // フィールドの初期化
            this.form = targetForm;
            this.accessor = new DBAccessor();
            this.msgLogic = new MessageBoxShowLogic();
            this.oldCondition = new findConditionDTO();
        }

        #endregion - Constructor -

        #region - Initialize -

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                // ParentFormのSet
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // HeaderFormのSet
                this.headerForm = (HeaderBaseForm)this.parentForm.headerForm;

                // RibbonMenuのSet
                this.ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }

                // モードラベルは非表示
                this.headerForm.windowTypeLabel.Visible = false;

                // タイトル設定
                this.headerForm.lb_title.Location = new Point(0, this.headerForm.lb_title.Location.Y);
                this.headerForm.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_KONGOU_HAIKIBUTSU_JOUKYOU_ICHIRAN);
                this.parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_KONGOU_HAIKIBUTSU_JOUKYOU_ICHIRAN));
                ControlUtility.AdjustTitleSize(this.headerForm.lb_title, this.headerForm.lb_title.Width);

                // 一覧設定
                this.form.Ichiran.Anchor |= AnchorStyles.Bottom | AnchorStyles.Right;

                // 検索条件クリア
                this.clearCondition();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
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
            // F1 振分登録
            this.parentForm.bt_func1.Click += new EventHandler(this.form.functionKeyClick);
            // F2 新規
            this.parentForm.bt_func2.Click += new EventHandler(this.form.functionKeyClick);
            // F3 修正
            this.parentForm.bt_func3.Click += new EventHandler(this.form.functionKeyClick);
            // F4 削除
            this.parentForm.bt_func4.Click += new EventHandler(this.form.functionKeyClick);
            // F6 CSV出力
            this.parentForm.bt_func6.Click += new EventHandler(this.form.functionKeyClick);
            // F7 条件クリア
            this.parentForm.bt_func7.Click += new EventHandler(this.form.functionKeyClick);
            // F8 検索
            this.parentForm.bt_func8.Click += new EventHandler(this.form.functionKeyClick);
            // F10 並び替え
            this.parentForm.bt_func10.Click += new EventHandler(this.form.functionKeyClick);
            // F12 閉じる
            this.parentForm.bt_func12.Click += new EventHandler(this.form.functionKeyClick);
            // 抽出日付区分
            this.form.DATE_KBN.TextChanged += new EventHandler(this.form.DATE_KBN_TextChanged);
            // 排出事業者
            this.form.HST_GYOUSHA_CD.Validating += new CancelEventHandler(this.form.searchConditionCtrlValidating);
            // 排出事業場
            this.form.HST_GENBA_CD.Validating += new CancelEventHandler(this.form.searchConditionCtrlValidating);
            // 処分受託者
            this.form.SBN_GYOUSHA_CD.Validating += new CancelEventHandler(this.form.searchConditionCtrlValidating);
            // 運搬先の事業場
            this.form.UPN_SAKI_GENBA_CD.Validating += new CancelEventHandler(this.form.searchConditionCtrlValidating);
            // 運搬受託者
            this.form.UPN_JYUTAKUSHA_CD.Validating += new CancelEventHandler(this.form.searchConditionCtrlValidating);
            // 報告書分類
            this.form.HOUKOKUSHO_BUNRUI_CD.Validating += new CancelEventHandler(this.form.searchConditionCtrlValidating);
            // 電子廃棄物種類
            this.form.DENSHI_HAIKI_SHURUI_CD.Validating += new CancelEventHandler(this.form.searchConditionCtrlValidating);
            // 明細ダブルクリック処理
            this.form.Ichiran.CellDoubleClick += new DataGridViewCellEventHandler(this.form.Ichiran_CellDoubleClick);
            // 日付TOダブルクリック処理
            this.form.DATE_TO.DoubleClick += new EventHandler(this.form.fromToConditionCtrlDoubleClick);
            // マニフェスト番号TOダブルクリック処理
            this.form.MANIFEST_ID_TO.DoubleClick += new EventHandler(this.form.fromToConditionCtrlDoubleClick);
        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 一覧の「紐付数」、「完了数」を非表示
            this.form.Ichiran.Columns["RELATION_NUM"].Visible = false;
            this.form.Ichiran.Columns["COMPLETE_NUM"].Visible = false;

            //  「紐付の状態」、「二次マニの状態」項目を非表示
            // 紐付の状態
            this.form.RELATION_SHOW_LBL.Visible = false;
            this.form.customPanel2.Visible = false;
            this.form.RELATION_SHOW_KBN.Visible = false;
            this.form.RELATION_SHOW_KBN.Text = ConstClass.SHOW_KBN_ALL; // 「3.全て」固定

            // 二次マニの状態
            this.form.NEXT_SHOW_LBL.Visible = false;
            this.form.customPanel3.Visible = false;
            this.form.NEXT_SHOW_KBN.Visible = false;
            this.form.NEXT_SHOW_KBN.Text = ConstClass.SHOW_KBN_ALL; // 「3.全て」固定

            // Location調整
            // 排出事業者
            LocationAdjustmentForManiLite(this.form.HST_GYOUSHA_LBL);
            LocationAdjustmentForManiLite(this.form.HST_GYOUSHA_CD);
            LocationAdjustmentForManiLite(this.form.HST_GYOUSHA_NAME);
            LocationAdjustmentForManiLite(this.form.HST_GYOUSHA_SEARCH);

            // 排出事業場
            LocationAdjustmentForManiLite(this.form.HST_GENBA_LBL);
            LocationAdjustmentForManiLite(this.form.HST_GENBA_CD);
            LocationAdjustmentForManiLite(this.form.HST_GENBA_NAME);
            LocationAdjustmentForManiLite(this.form.HST_GENBA_SEARCH);

            // 処分受託者
            LocationAdjustmentForManiLite(this.form.SBN_GYOUSHA_LBL);
            LocationAdjustmentForManiLite(this.form.SBN_GYOUSHA_CD);
            LocationAdjustmentForManiLite(this.form.SBN_GYOUSHA_NAME);
            LocationAdjustmentForManiLite(this.form.SBN_GYOUSHA_SEARCH);

            // 運搬先の事業場
            LocationAdjustmentForManiLite(this.form.UPN_SAKI_GENBA_LBL);
            LocationAdjustmentForManiLite(this.form.UPN_SAKI_GENBA_CD);
            LocationAdjustmentForManiLite(this.form.UPN_SAKI_GENBA_NAME);
            LocationAdjustmentForManiLite(this.form.UPN_SAKI_GENBA_SEARCH);

            // 運搬受託者
            LocationAdjustmentForManiLite(this.form.UPN_JYUTAKUSHA_LBL);
            LocationAdjustmentForManiLite(this.form.UPN_JYUTAKUSHA_CD);
            LocationAdjustmentForManiLite(this.form.UPN_JYUTAKUSHA_NAME);
            LocationAdjustmentForManiLite(this.form.UPN_JYUTAKUSHA_SEARCH);

            // 報告書分類
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_BUNRUI_LBL);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_BUNRUI_CD);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_BUNRUI_NAME);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_BUNRUI_SEARCH);

            // 電子廃棄物種類
            LocationAdjustmentForManiLite(this.form.DENSHI_HAIKI_SHURUI_LBL);
            LocationAdjustmentForManiLite(this.form.DENSHI_HAIKI_SHURUI_CD);
            LocationAdjustmentForManiLite(this.form.DENSHI_HAIKI_SHURUI_NAME);
            LocationAdjustmentForManiLite(this.form.DENSHI_HAIKI_SHURUI_SEARCH);

            // マニフェスト番号
            LocationAdjustmentForManiLite(this.form.MANIFEST_ID_LBL);
            LocationAdjustmentForManiLite(this.form.MANIFEST_ID_FROM);
            LocationAdjustmentForManiLite(this.form.label1);
            LocationAdjustmentForManiLite(this.form.MANIFEST_ID_TO);
        }

        /// <summary>
        /// マニライト用にLocation調整
        /// </summary>
        /// <param name="ctrl"></param>
        private void LocationAdjustmentForManiLite(Control ctrl)
        {
            ctrl.Location = new System.Drawing.Point(ctrl.Location.X, ctrl.Location.Y - 22);
        }

        #endregion - Initialize -

        #region - Utility -

        #region - FunctionProc -

        /// <summary>
        /// 振分登録画面遷移処理
        /// </summary>
        internal void showDistributeDisp()
        {
            try
            {
                // CurrentCellの情報を基に振り分け画面表示
                var curCell = this.form.Ichiran.CurrentCell;
                if (curCell != null)
                {
                    if (curCell.RowIndex >= 0)
                    {
                        // 引数情報取得
                        var row = this.form.Ichiran.Rows[curCell.RowIndex];
                        var sysID = row.Cells["SYSTEM_ID"].Value.ToString();
                        var kanriID = row.Cells["KANRI_ID"].Value.ToString();
                        bool isLastSbnEndrepFlg = false;
                        if (row.Cells["LAST_SBN_ENDREP_FLAG"].Value != null && row.Cells["LAST_SBN_ENDREP_FLAG"].Value.ToString().Equals("1"))
                            isLastSbnEndrepFlg = true;

                        // 混廃振分、紐付け済みフラグ
                        bool isRelationalMixMani = string.IsNullOrEmpty(row.Cells["RELATION_NUM"].Value.ToString()) ? false : true;
                        if (isRelationalMixMani)
                        {
                            isRelationalMixMani = row.Cells["RELATION_NUM"].Value.ToString().Substring(0, 1) == "0" ? false : true;
                        }


                        if (isRelationalMixMani && isLastSbnEndrepFlg)
                        {
                            // 混廃振分済み、紐付け済み、最終処分済みの場合も振分画面を表示する。
                            FormManager.OpenFormModal("G616", sysID, kanriID, isLastSbnEndrepFlg, isRelationalMixMani);
                        }
                        else if ((false == string.IsNullOrEmpty(sysID)) && (false == string.IsNullOrEmpty(kanriID)))
                        {
                            // 振分画面表示
                            FormManager.OpenFormModal("G616", sysID, kanriID, isLastSbnEndrepFlg, isRelationalMixMani);

                            // 振分対象のセル位置を保持
                            var colIndex = curCell.ColumnIndex;
                            var rowIndex = curCell.RowIndex;

                            // 再検索
                            if (!this.SearchIchiran())
                            {
                                return;
                            }

                            if (this.form.Ichiran.RowCount > 0)
                            {
                                // 再検索時にCurrentCellがリセットされるため、振分時のセルにフォーカスをセットする
                                this.form.Ichiran.CurrentCell = this.form.Ichiran[colIndex, rowIndex];
                            }
                        }
                        else
                        {
                            // 情報が取得できなかった場合はエラー表示
                            this.msgLogic.MessageBoxShow("E045");
                        }
                    }
                    else
                    {
                        // 対象データが選択されていない場合はエラー表示
                        this.msgLogic.MessageBoxShow("E051", "振分対象データ");
                    }
                }
                else
                {
                    // 対象データが選択されていない場合はエラー表示
                    this.msgLogic.MessageBoxShow("E051", "振分対象データ");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("showDistributeDisp", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("showDistributeDisp", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 新規登録画面遷移処理
        /// </summary>
        internal void showNewDisp()
        {
            try
            {
                // 新規権限がある場合、電マニ入力画面に新規モードにて遷移
                FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, string.Empty, string.Empty);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("showNewDisp", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("showNewDisp", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 修正登録画面遷移処理
        /// </summary>
        internal void showModifyDisp()
        {
            try
            {
                // CurrentCellの情報を基に修正画面表示
                var curCell = this.form.Ichiran.CurrentCell;
                if (curCell != null)
                {
                    if (curCell.RowIndex >= 0)
                    {
                        // 引数情報取得
                        var row = this.form.Ichiran.Rows[curCell.RowIndex];
                        var kanriID = row.Cells["KANRI_ID"].Value.ToString();

                        // 対象の電マニが存在するかのチェック
                        // ※複数画面を起動した状態で裏で更新があった場合等、選択している情報が古い場合があるため
                        if (true == this.accessor.denManiDataExistCheck(kanriID))
                        {
                            if (false == string.IsNullOrEmpty(kanriID))
                            {
                                // 修正権限チェック
                                if (Manager.CheckAuthority("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                                {
                                    // 修正画面表示
                                    FormManager.OpenForm("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, kanriID, string.Empty);
                                }
                                else if (Manager.CheckAuthority("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                                {
                                    // 修正権限が無く、参照権限がある場合は参照モードで起動する
                                    FormManager.OpenForm("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kanriID, string.Empty);
                                }
                                else
                                {
                                    // 修正・参照権限が無い場合は、修正権限なしのエラーを表示する
                                    var msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E158", "修正");
                                }
                            }
                            else
                            {
                                // 情報が取得できなかった場合はエラー表示
                                this.msgLogic.MessageBoxShow("E045");
                            }
                        }
                        else
                        {
                            // 情報が取得できなかった場合はエラー表示
                            this.msgLogic.MessageBoxShow("E045");
                        }
                    }
                    else
                    {
                        // 対象データが選択されていない場合はエラー表示
                        this.msgLogic.MessageBoxShow("E051", "修正対象データ");
                    }
                }
                else
                {
                    // 対象データが選択されていない場合はエラー表示
                    this.msgLogic.MessageBoxShow("E051", "修正対象データ");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("showModifyDisp", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("showModifyDisp", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 削除登録画面遷移処理
        /// </summary>
        internal void showDeleteDisp()
        {
            try
            {
                // CurrentCellの情報を基に削除画面表示
                var curCell = this.form.Ichiran.CurrentCell;
                if (curCell != null)
                {
                    if (curCell.RowIndex >= 0)
                    {
                        // 引数情報取得
                        var row = this.form.Ichiran.Rows[curCell.RowIndex];
                        var kanriID = row.Cells["KANRI_ID"].Value.ToString();

                        // 対象の電マニが存在するかのチェック
                        // ※複数画面を起動した状態で裏で更新があった場合等、選択している情報が古い場合があるため
                        if (true == this.accessor.denManiDataExistCheck(kanriID))
                        {
                            if (false == string.IsNullOrEmpty(kanriID))
                            {
                                // 削除権限がある場合、電マニ入力画面に削除モードにて遷移
                                FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.DELETE_WINDOW_FLAG, WINDOW_TYPE.DELETE_WINDOW_FLAG, kanriID, string.Empty);
                            }
                            else
                            {
                                // 情報が取得できなかった場合はエラー表示
                                this.msgLogic.MessageBoxShow("E045");
                            }
                        }
                        else
                        {
                            // 情報が取得できなかった場合はエラー表示
                            this.msgLogic.MessageBoxShow("E045");
                        }
                    }
                    else
                    {
                        // 対象データが選択されていない場合はエラー表示
                        this.msgLogic.MessageBoxShow("E051", "削除対象データ");
                    }
                }
                else
                {
                    // 対象データが選択されていない場合はエラー表示
                    this.msgLogic.MessageBoxShow("E051", "削除対象データ");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("showDeleteDisp", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("showDeleteDisp", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// CSV出力処理
        /// </summary>
        internal void csvOutput()
        {
            try
            {
                if (this.form.Ichiran.RowCount > 0)
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
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("csvOutput", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("csvOutput", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 条件クリア処理
        /// </summary>
        internal void clearCondition()
        {
            try
            {
                // 日付設定
                this.form.DATE_KBN.Text = ConstClass.DATE_KBN_KOUFU;
                this.form.DATE_FROM.Value = this.parentForm.sysDate;
                this.form.DATE_TO.Value = this.parentForm.sysDate;

                // 表示区分設定
                this.form.RELATION_SHOW_KBN.Text = ConstClass.SHOW_KBN_COMPLETE;
                this.form.NEXT_SHOW_KBN.Text = ConstClass.SHOW_KBN_COMPLETE;
                if (AppConfig.IsManiLite)
                {
                    // マニライトの場合、「3.全て」固定で項目非表示のため
                    this.form.RELATION_SHOW_KBN.Text = ConstClass.SHOW_KBN_ALL;
                    this.form.NEXT_SHOW_KBN.Text = ConstClass.SHOW_KBN_ALL;
                }

                // 検索条件を全てクリア
                this.form.HST_GYOUSHA_CD.Text = "";
                this.form.HST_GYOUSHA_NAME.Text = "";
                this.form.HST_GENBA_CD.Text = "";
                this.form.HST_GENBA_NAME.Text = "";
                this.form.SBN_GYOUSHA_CD.Text = "";
                this.form.SBN_GYOUSHA_NAME.Text = "";
                this.form.UPN_SAKI_GENBA_CD.Text = "";
                this.form.UPN_SAKI_GENBA_NAME.Text = "";
                this.form.UPN_JYUTAKUSHA_CD.Text = "";
                this.form.UPN_JYUTAKUSHA_NAME.Text = "";
                this.form.HOUKOKUSHO_BUNRUI_CD.Text = "";
                this.form.HOUKOKUSHO_BUNRUI_NAME.Text = "";
                this.form.DENSHI_HAIKI_SHURUI_CD.Text = "";
                this.form.DENSHI_HAIKI_SHURUI_NAME.Text = "";
                this.form.MANIFEST_ID_FROM.Text = "";
                this.form.MANIFEST_ID_TO.Text = "";
                this.form.SORT_HEADER.ClearCustomSortSetting();

                // 前回値をクリア
                this.oldCondition = new findConditionDTO();

                this.setDateLabelTitle();

                // 日付区分にフォーカス
                this.form.DATE_KBN.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("clearCondition", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("clearCondition", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        internal bool SearchIchiran()
        {
            try
            {
                if ((true == this.dateIntegrityCheck()) && (true == this.maniIdIntegrityCheck()))
                {
                    // 画面の表示内容から検索条件を作成
                    var condition = this.createSearchCondition();

                    // 一覧表示用Dataを取得
                    var table = this.accessor.getIchiranData(condition);

                    // 抽出結果を一覧にセット
                    this.form.Ichiran.AutoGenerateColumns = false;
                    this.form.Ichiran.IsBrowsePurpose = false;
                    if (table != null)
                    {
                        this.form.SORT_HEADER.SortDataTable(table);
                    }

                    this.form.Ichiran.DataSource = table;

                    if (table == null)
                    {
                        // 該当結果が存在しない場合はエラー表示
                        this.msgLogic.MessageBoxShow("E044");
                    }
                    this.form.Ichiran.IsBrowsePurpose = true;
                }
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("search", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("search", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 並び替え処理
        /// </summary>
        internal void sort()
        {
            // 並び替え処理
            this.form.SORT_HEADER.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// 画面Close処理
        /// </summary>
        internal void closeDisp()
        {
            // 画面Close
            this.parentForm.Close();
        }

        #endregion - FunctionProc -

        #region - CDSetting -

        /// <summary>
        /// 排出事業者CDセット
        /// </summary>
        /// <returns name="bool">TRUE:成功, FALSE:エラー</returns>
        internal bool setHstGyoushaCD(out bool catchErr)
        {
            catchErr = false;
            try
            {
                bool bRet = true;

                // 前回値との差分があれば処理続行
                if (true == this.diffCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA, this.form.HST_GYOUSHA_CD.Text))
                {
                    // 一旦クリア
                    this.form.HST_GYOUSHA_NAME.Text = "";
                    this.form.HST_GENBA_NAME.Text = "";
                    this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA, "");

                    if (false == string.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
                    {
                        // 排出事業者情報取得
                        var entity = this.accessor.getHstGyoushaEntity(this.form.HST_GYOUSHA_CD.Text);
                        if (entity != null)
                        {
                            // 該当情報をセット
                            this.form.HST_GYOUSHA_NAME.Text = entity.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            // 該当情報が存在しないためエラー表示
                            this.msgLogic.MessageBoxShow("E020", "業者");
                            bRet = false;
                        }
                    }

                    if (bRet == true)
                    {
                        // 前回値の更新
                        this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA, this.form.HST_GYOUSHA_CD.Text);
                    }
                }

                return bRet;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("setHstGyoushaCD", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setHstGyoushaCD", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// 排出事業場CDセット
        /// </summary>
        /// <returns name="bool">TRUE:成功, FALSE:エラー</returns>
        internal bool setHstGenbaCD(out bool catchErr)
        {
            catchErr = false;
            try
            {
                bool bRet = true;

                // 前回値との差分があれば処理続行
                if (true == this.diffCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA, this.form.HST_GENBA_CD.Text))
                {
                    // 一旦クリア
                    this.form.HST_GENBA_NAME.Text = "";

                    if (false == string.IsNullOrEmpty(this.form.HST_GENBA_CD.Text))
                    {
                        if (false == string.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
                        {
                            // 排出事業場情報取得
                            var entity = this.accessor.getHstGenbaEntity(this.form.HST_GYOUSHA_CD.Text, this.form.HST_GENBA_CD.Text);
                            if (entity != null)
                            {
                                // 該当情報をセット
                                this.form.HST_GENBA_NAME.Text = entity.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                // 該当情報が存在しないためエラー表示
                                this.msgLogic.MessageBoxShow("E020", "現場");
                                bRet = false;
                            }
                        }
                        else
                        {
                            // 紐付き元が存在しないためエラー表示
                            this.msgLogic.MessageBoxShow("E051", "排出事業者");
                            this.form.HST_GENBA_CD.Text = string.Empty;
                            bRet = false;
                        }
                    }

                    if (bRet == true)
                    {
                        // 前回値の更新
                        this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA, this.form.HST_GYOUSHA_CD.Text);
                        this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA, this.form.HST_GENBA_CD.Text);
                    }
                }

                return bRet;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("setHstGenbaCD", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setHstGenbaCD", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// 処分受託者CDセット
        /// </summary>
        /// <returns name="bool">TRUE:成功, FALSE:エラー</returns>
        internal bool setSbnGyoushaCD(out bool catchErr)
        {
            catchErr = false;
            try
            {
                bool bRet = true;

                // 前回値との差分があれば処理続行
                if (true == this.diffCD(ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA, this.form.SBN_GYOUSHA_CD.Text))
                {
                    // 一旦クリア
                    this.form.SBN_GYOUSHA_NAME.Text = "";
                    this.form.UPN_SAKI_GENBA_NAME.Text = "";
                    this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA, "");

                    if (false == string.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
                    {
                        // 処分受託者情報取得
                        var entity = this.accessor.getSbnGyoushaEntity(this.form.SBN_GYOUSHA_CD.Text);
                        if (entity != null)
                        {
                            // 該当情報をセット
                            this.form.SBN_GYOUSHA_NAME.Text = entity.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            // 該当情報が存在しないためエラー表示
                            this.msgLogic.MessageBoxShow("E020", "業者");
                            bRet = false;
                        }
                    }

                    if (bRet == true)
                    {
                        // 前回値の更新
                        this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA, this.form.SBN_GYOUSHA_CD.Text);
                    }
                }

                return bRet;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("setSbnGyoushaCD", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setSbnGyoushaCD", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// 運搬先の事業場CDセット
        /// </summary>
        /// <returns name="bool">TRUE:成功, FALSE:エラー</returns>
        internal bool setUpnSakiGenbaCD(out bool catchErr)
        {
            catchErr = false;
            try
            {
                bool bRet = true;

                // 前回値との差分があれば処理続行
                if (true == this.diffCD(ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA, this.form.UPN_SAKI_GENBA_CD.Text))
                {
                    // 一旦クリア
                    this.form.UPN_SAKI_GENBA_NAME.Text = "";

                    if (false == string.IsNullOrEmpty(this.form.UPN_SAKI_GENBA_CD.Text))
                    {
                        if (false == string.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
                        {
                            // 運搬先の事業場情報取得
                            var entity = this.accessor.getUpnSakiGenbaEntity(this.form.SBN_GYOUSHA_CD.Text, this.form.UPN_SAKI_GENBA_CD.Text);
                            if (entity != null)
                            {
                                // 該当情報をセット
                                this.form.UPN_SAKI_GENBA_NAME.Text = entity.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                // 該当情報が存在しないためエラー表示
                                this.msgLogic.MessageBoxShow("E020", "現場");
                                bRet = false;
                            }
                        }
                        else
                        {
                            // 紐付き元が存在しないためエラー表示
                            this.msgLogic.MessageBoxShow("E051", "処分受託者");
                            this.form.UPN_SAKI_GENBA_CD.Text = string.Empty;
                            bRet = false;
                        }
                    }

                    if (bRet == true)
                    {
                        // 前回値の更新
                        this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA, this.form.SBN_GYOUSHA_CD.Text);
                        this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA, this.form.UPN_SAKI_GENBA_CD.Text);
                    }
                }

                return bRet;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("setUpnSakiGenbaCD", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setUpnSakiGenbaCD", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// 運搬受託者CDセット
        /// </summary>
        /// <returns name="bool">TRUE:成功, FALSE:エラー</returns>
        internal bool setUpnJyutakushaCD(out bool catchErr)
        {
            catchErr = false;
            try
            {
                bool bRet = true;

                // 前回値との差分があれば処理続行
                if (true == this.diffCD(ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_JYUTAKUSHA, this.form.UPN_JYUTAKUSHA_CD.Text))
                {
                    // 一旦クリア
                    this.form.UPN_JYUTAKUSHA_NAME.Text = "";

                    if (false == string.IsNullOrEmpty(this.form.UPN_JYUTAKUSHA_CD.Text))
                    {
                        // 運搬受託者情報取得
                        var entity = this.accessor.getUpnJyutakushaEntity(this.form.UPN_JYUTAKUSHA_CD.Text);
                        if (entity != null)
                        {
                            // 該当情報をセット
                            this.form.UPN_JYUTAKUSHA_NAME.Text = entity.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            // 該当情報が存在しないためエラー表示
                            this.msgLogic.MessageBoxShow("E020", "業者");
                            bRet = false;
                        }
                    }

                    if (bRet == true)
                    {
                        // 前回値の更新
                        this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_JYUTAKUSHA, this.form.UPN_JYUTAKUSHA_CD.Text);
                    }
                }

                return bRet;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("setUpnJyutakushaCD", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setUpnJyutakushaCD", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// 報告書分類CDセット
        /// </summary>
        /// <returns name="bool">TRUE:成功, FALSE:エラー</returns>
        internal bool setHoukokushoBunruiCD(out bool catchErr)
        {
            catchErr = false;
            try
            {
                bool bRet = true;

                // 前回値との差分があれば処理続行
                if (true == this.diffCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HOUKOKUSHO_BUNRUI, this.form.HOUKOKUSHO_BUNRUI_CD.Text))
                {
                    // 一旦クリア
                    this.form.HOUKOKUSHO_BUNRUI_NAME.Text = "";

                    if (false == string.IsNullOrEmpty(this.form.HOUKOKUSHO_BUNRUI_CD.Text))
                    {
                        // 報告書分類情報取得
                        var entity = this.accessor.getHoukokushoBunruiEntity(this.form.HOUKOKUSHO_BUNRUI_CD.Text);
                        if (entity != null)
                        {
                            // 該当情報をセット
                            this.form.HOUKOKUSHO_BUNRUI_NAME.Text = entity.HOUKOKUSHO_BUNRUI_NAME_RYAKU;
                        }
                        else
                        {
                            // 該当情報が存在しないためエラー表示
                            this.msgLogic.MessageBoxShow("E020", "報告書分類");
                            bRet = false;
                        }
                    }

                    if (bRet == true)
                    {
                        // 前回値の更新
                        this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HOUKOKUSHO_BUNRUI, this.form.HOUKOKUSHO_BUNRUI_CD.Text);
                    }
                }

                return bRet;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("setHoukokushoBunruiCD", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setHoukokushoBunruiCD", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// 電子廃棄物種類CDセット
        /// </summary>
        /// <returns name="bool">TRUE:成功, FALSE:エラー</returns>
        internal bool setDenshiHaikiShuruiCD(out bool catchErr)
        {
            catchErr = false;
            try
            {
                bool bRet = true;

                // 前回値との差分があれば処理続行
                if (true == this.diffCD(ConstClass.INPUT_TYPE.INPUT_TYPE_DENSHI_HAIKI_SHURUI, this.form.DENSHI_HAIKI_SHURUI_CD.Text))
                {
                    // 一旦クリア
                    this.form.DENSHI_HAIKI_SHURUI_NAME.Text = "";

                    if (false == string.IsNullOrEmpty(this.form.DENSHI_HAIKI_SHURUI_CD.Text))
                    {
                        // 電子廃棄物種類情報取得
                        var entity = this.accessor.getDenshiHaikiShuruiEntity(this.form.DENSHI_HAIKI_SHURUI_CD.Text);
                        if (entity != null)
                        {
                            // 該当情報をセット
                            this.form.DENSHI_HAIKI_SHURUI_NAME.Text = entity.HAIKI_SHURUI_NAME;
                        }
                        else
                        {
                            // 該当情報が存在しないためエラー表示
                            this.msgLogic.MessageBoxShow("E020", "電子廃棄物種類");
                            bRet = false;
                        }
                    }

                    if (bRet == true)
                    {
                        // 前回値の更新
                        this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_DENSHI_HAIKI_SHURUI, this.form.DENSHI_HAIKI_SHURUI_CD.Text);
                    }
                }

                return bRet;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("setDenshiHaikiShuruiCD", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDenshiHaikiShuruiCD", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        #endregion - CDSetting -

        #region - Other -

        /// <summary>
        /// 日付項目ラベルTitle設定
        /// </summary>
        internal bool setDateLabelTitle()
        {
            var titleStr = string.Empty;

            switch (this.form.DATE_KBN.Text)
            {
                case ConstClass.DATE_KBN_KOUFU:
                    // 交付年月日
                    titleStr = "交付年月日";
                    break;

                case ConstClass.DATE_KBN_UNPAN:
                    // 運搬終了日
                    titleStr = "運搬終了日";
                    break;

                case ConstClass.DATE_KBN_SHOBUN:
                    // 処分終了日
                    titleStr = "処分終了日";
                    break;

                case ConstClass.DATE_KBN_LAST:
                    // 最終処分終了日
                    titleStr = "最終処分終了日";
                    break;

                default:
                    // DO NOTHING
                    break;
            }

            if (false == string.IsNullOrEmpty(titleStr))
            {
                // タイトル設定
                this.form.DATE_LBL.Text = titleStr + "※";
                this.form.DATE_FROM.DisplayItemName = titleStr + "FROM";
                this.form.DATE_TO.DisplayItemName = titleStr + "TO";
            }

            return true;
        }

        /// <summary>
        /// CD検索ポップアップ表示後処理
        /// </summary>
        /// <param name="type">入力種別</param>
        internal bool cdSearchPopupAfterProc(ConstClass.INPUT_TYPE type)
        {
            try
            {
                switch (type)
                {
                    case ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA:
                        // 前回値との差分があれば前回値の更新を行う
                        if (true == this.diffCD(type, this.form.HST_GYOUSHA_CD.Text))
                        {
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA, this.form.HST_GYOUSHA_CD.Text);

                            // 排出事業場情報のクリア
                            this.form.HST_GENBA_NAME.Text = "";
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA, "");
                        }
                        break;

                    case ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA:
                        // 前回値との差分があれば前回値の更新を行う
                        if (true == this.diffCD(type, this.form.HST_GENBA_CD.Text))
                        {
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA, this.form.HST_GYOUSHA_CD.Text);
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA, this.form.HST_GENBA_CD.Text);
                        }
                        break;

                    case ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA:
                        // 前回値との差分があれば前回値の更新を行う
                        if (true == this.diffCD(type, this.form.SBN_GYOUSHA_CD.Text))
                        {
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA, this.form.SBN_GYOUSHA_CD.Text);

                            // 運搬先の事業場情報のクリア
                            this.form.UPN_SAKI_GENBA_NAME.Text = "";
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA, "");
                        }
                        break;

                    case ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA:
                        // 前回値との差分があれば前回値の更新を行う
                        if (true == this.diffCD(type, this.form.UPN_SAKI_GENBA_CD.Text))
                        {
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA, this.form.SBN_GYOUSHA_CD.Text);
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA, this.form.UPN_SAKI_GENBA_CD.Text);
                        }
                        break;

                    case ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_JYUTAKUSHA:
                        // 前回値との差分があれば前回値の更新を行う
                        if (true == this.diffCD(type, this.form.UPN_JYUTAKUSHA_CD.Text))
                        {
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_JYUTAKUSHA, this.form.UPN_JYUTAKUSHA_CD.Text);
                        }
                        break;

                    case ConstClass.INPUT_TYPE.INPUT_TYPE_HOUKOKUSHO_BUNRUI:
                        // 前回値との差分があれば前回値の更新を行う
                        if (true == this.diffCD(type, this.form.HOUKOKUSHO_BUNRUI_CD.Text))
                        {
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_HOUKOKUSHO_BUNRUI, this.form.HOUKOKUSHO_BUNRUI_CD.Text);
                        }
                        break;

                    case ConstClass.INPUT_TYPE.INPUT_TYPE_DENSHI_HAIKI_SHURUI:
                        // 前回値との差分があれば前回値の更新を行う
                        if (true == this.diffCD(type, this.form.DENSHI_HAIKI_SHURUI_CD.Text))
                        {
                            this.setCD(ConstClass.INPUT_TYPE.INPUT_TYPE_DENSHI_HAIKI_SHURUI, this.form.DENSHI_HAIKI_SHURUI_CD.Text);
                        }
                        break;

                    default:
                        // DO NOTHING
                        break;
                }
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("cdSearchPopupAfterProc", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("cdSearchPopupAfterProc", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// FROMの情報をTOにコピーする
        /// </summary>
        /// <param name="type">入力種別</param>
        internal void fromToCopy(ConstClass.INPUT_TYPE type)
        {
            switch (type)
            {
                case ConstClass.INPUT_TYPE.INPUT_TYPE_DATE_TO:
                    // 日付FROMをTOにコピー
                    this.form.DATE_TO.Text = this.form.DATE_FROM.Text;
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_MANIFEST_ID_TO:
                    // マニフェスト番号FROMをTOにコピー
                    this.form.MANIFEST_ID_TO.Text = this.form.MANIFEST_ID_FROM.Text;
                    break;

                default:
                    // DO NOTHING
                    break;
            }
        }

        /// <summary>
        /// コントロール名から入力種別に変換
        /// </summary>
        /// <param name="ctrlName">コントロール名</param>
        /// <returns name="INPUT_TYPE">入力種別</returns>
        internal ConstClass.INPUT_TYPE getInputType(string ctrlName)
        {
            var retType = ConstClass.INPUT_TYPE.INPUT_TYPE_MAX;

            switch (ctrlName)
            {
                case "HST_GYOUSHA_CD":
                case "HST_GYOUSHA_NAME":
                case "HST_GYOUSHA_SEARCH":
                    // 排出事業者
                    retType = ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA;
                    break;

                case "HST_GENBA_CD":
                case "HST_GENBA_NAME":
                case "HST_GENBA_SEARCH":
                    // 排出事業場
                    retType = ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA;
                    break;

                case "SBN_GYOUSHA_CD":
                case "SBN_GYOUSHA_NAME":
                case "SBN_GYOUSHA_SEARCH":
                    // 処分受託者
                    retType = ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA;
                    break;

                case "UPN_SAKI_GENBA_CD":
                case "UPN_SAKI_GENBA_NAME":
                case "UPN_SAKI_GENBA_SEARCH":
                    // 運搬先の事業場
                    retType = ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA;
                    break;

                case "UPN_JYUTAKUSHA_CD":
                case "UPN_JYUTAKUSHA_NAME":
                case "UPN_JYUTAKUSHA_SEARCH":
                    // 運搬受託者
                    retType = ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_JYUTAKUSHA;
                    break;

                case "HOUKOKUSHO_BUNRUI_CD":
                case "HOUKOKUSHO_BUNRUI_NAME":
                case "HOUKOKUSHO_BUNRUI_SEARCH":
                    // 報告書分類
                    retType = ConstClass.INPUT_TYPE.INPUT_TYPE_HOUKOKUSHO_BUNRUI;
                    break;

                case "DENSHI_HAIKI_SHURUI_CD":
                case "DENSHI_HAIKI_SHURUI_NAME":
                case "DENSHI_HAIKI_SHURUI_SEARCH":
                    // 電子廃棄物種類
                    retType = ConstClass.INPUT_TYPE.INPUT_TYPE_DENSHI_HAIKI_SHURUI;
                    break;

                case "DATE_TO":
                    // 日付TO
                    retType = ConstClass.INPUT_TYPE.INPUT_TYPE_DATE_TO;
                    break;

                case "MANIFEST_ID_TO":
                    // マニフェスト番号TO
                    retType = ConstClass.INPUT_TYPE.INPUT_TYPE_MANIFEST_ID_TO;
                    break;

                default:
                    // DO NOTHING
                    break;
            }

            return retType;
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
        /// 画面表示内容から検索条件を作成する
        /// </summary>
        /// <returns name="findConditionDTO">検索条件</returns>
        private findConditionDTO createSearchCondition()
        {
            var retDto = new findConditionDTO();

            // マニ帳票数量区分を取得
            retDto.MANIFEST_REPORT_SUU_KBN = this.ribbon.GlobalCommonInformation.SysInfo.MANIFEST_REPORT_SUU_KBN.Value;

            // 検索条件セット
            // 必須項目
            retDto.DATE_KBN = this.form.DATE_KBN.Text;
            retDto.DATE_FROM = DateTime.Parse(this.form.DATE_FROM.Value.ToString());
            retDto.DATE_TO = DateTime.Parse(this.form.DATE_TO.Value.ToString());

            // 通常項目
            if (false == string.IsNullOrEmpty(this.form.RELATION_SHOW_KBN.Text))
            {
                // 紐付表示区分
                retDto.RELATION_SHOW_KBN = this.form.RELATION_SHOW_KBN.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.NEXT_SHOW_KBN.Text))
            {
                // 二次マニ表示区分
                retDto.NEXT_SHOW_KBN = this.form.NEXT_SHOW_KBN.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
            {
                // 排出事業者
                retDto.HST_GYOUSHA_CD = this.form.HST_GYOUSHA_CD.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.HST_GENBA_CD.Text))
            {
                // 排出事業場
                retDto.HST_GENBA_CD = this.form.HST_GENBA_CD.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
            {
                // 処分受託者
                retDto.SBN_GYOUSHA_CD = this.form.SBN_GYOUSHA_CD.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.UPN_SAKI_GENBA_CD.Text))
            {
                // 運搬先の事業場
                retDto.UPN_SAKI_GENBA_CD = this.form.UPN_SAKI_GENBA_CD.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.UPN_JYUTAKUSHA_CD.Text))
            {
                // 運搬受託者
                retDto.UPN_JYUTAKUSHA_CD = this.form.UPN_JYUTAKUSHA_CD.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.HOUKOKUSHO_BUNRUI_CD.Text))
            {
                // 報告書分類
                retDto.HOUKOKUSHO_BUNRUI_CD = this.form.HOUKOKUSHO_BUNRUI_CD.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.DENSHI_HAIKI_SHURUI_CD.Text))
            {
                // 電子廃棄物種類
                retDto.DENSHI_HAIKI_SHURUI_CD = this.form.DENSHI_HAIKI_SHURUI_CD.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.MANIFEST_ID_FROM.Text))
            {
                // マニフェスト番号FROM
                retDto.MANIFEST_ID_FROM = this.form.MANIFEST_ID_FROM.Text;
            }
            if (false == string.IsNullOrEmpty(this.form.MANIFEST_ID_TO.Text))
            {
                // マニフェスト番号TO
                retDto.MANIFEST_ID_TO = this.form.MANIFEST_ID_TO.Text;
            }

            return retDto;
        }

        /// <summary>
        /// 日付入力不正チェック
        /// </summary>
        /// <returns name="bool">TRUE:入力OK, FALSE:エラー</returns>
        private bool dateIntegrityCheck()
        {
            var bRet = true;
            var fromDateStr = this.form.DATE_FROM.GetResultText();
            var toDateStr = this.form.DATE_TO.GetResultText();

            // エラー状態初期化
            this.form.DATE_FROM.IsInputErrorOccured = false;
            this.form.DATE_TO.IsInputErrorOccured = false;

            if ((false == string.IsNullOrEmpty(fromDateStr)) && (false == string.IsNullOrEmpty(toDateStr)))
            {
                // 日付変換後、比較を行う
                var fromDate = DateTime.Parse(fromDateStr);
                var toDate = DateTime.Parse(toDateStr);
                if (0 < fromDate.CompareTo(toDate))
                {
                    // 日付入力不正のため、エラー表示
                    this.form.DATE_FROM.IsInputErrorOccured = true;
                    this.form.DATE_TO.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E030", this.form.DATE_FROM.DisplayItemName, this.form.DATE_TO.DisplayItemName);
                    this.form.DATE_FROM.Focus();
                    bRet = false;
                }
            }

            return bRet;
        }

        /// <summary>
        /// マニフェスト番号不正チェック
        /// </summary>
        /// <returns name="bool">TRUE:入力OK, FALSE:エラー</returns>
        private bool maniIdIntegrityCheck()
        {
            var bRet = true;
            var fromIdStr = this.form.MANIFEST_ID_FROM.GetResultText();
            var toIdStr = this.form.MANIFEST_ID_TO.GetResultText();

            // エラー状態初期化
            this.form.MANIFEST_ID_FROM.IsInputErrorOccured = false;
            this.form.MANIFEST_ID_TO.IsInputErrorOccured = false;

            if ((false == string.IsNullOrEmpty(fromIdStr)) && (false == string.IsNullOrEmpty(toIdStr)))
            {
                // 数値変換後、比較を行う
                var fromId = Int64.Parse(fromIdStr);
                var toId = Int64.Parse(toIdStr);
                if (0 < fromId.CompareTo(toId))
                {
                    // マニフェスト番号入力不正のため、エラー表示
                    this.form.MANIFEST_ID_FROM.IsInputErrorOccured = true;
                    this.form.MANIFEST_ID_TO.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E032", this.form.MANIFEST_ID_FROM.DisplayItemName, this.form.MANIFEST_ID_TO.DisplayItemName);
                    this.form.MANIFEST_ID_FROM.Focus();
                    bRet = false;
                }
            }

            return bRet;
        }

        #region - OldParamsControl -

        /// <summary>
        /// CDのセット
        /// </summary>
        /// <param name="type">入力種別</param>
        /// <param name="cd">セットを行うCD</param>
        /// <remarks>
        /// 入力種別に応じたCtrlに対してCDのセットを行う
        /// 前回値も更新する
        /// </remarks>
        private void setCD(ConstClass.INPUT_TYPE type, string cd)
        {
            switch (type)
            {
                case ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA:
                    // 排出事業者CDセット
                    this.form.HST_GYOUSHA_CD.Text = cd;
                    this.oldCondition.HST_GYOUSHA_CD = cd;
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA:
                    // 排出事業場CDセット
                    this.form.HST_GENBA_CD.Text = cd;
                    this.oldCondition.HST_GENBA_CD = cd;
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA:
                    // 処分受託者CDセット
                    this.form.SBN_GYOUSHA_CD.Text = cd;
                    this.oldCondition.SBN_GYOUSHA_CD = cd;
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA:
                    // 運搬先の事業場CDセット
                    this.form.UPN_SAKI_GENBA_CD.Text = cd;
                    this.oldCondition.UPN_SAKI_GENBA_CD = cd;
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_JYUTAKUSHA:
                    // 運搬受託者CDセット
                    this.form.UPN_JYUTAKUSHA_CD.Text = cd;
                    this.oldCondition.UPN_JYUTAKUSHA_CD = cd;
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_HOUKOKUSHO_BUNRUI:
                    // 報告書分類CDセット
                    this.form.HOUKOKUSHO_BUNRUI_CD.Text = cd;
                    this.oldCondition.HOUKOKUSHO_BUNRUI_CD = cd;
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_DENSHI_HAIKI_SHURUI:
                    // 電子廃棄物種類CDセット
                    this.form.DENSHI_HAIKI_SHURUI_CD.Text = cd;
                    this.oldCondition.DENSHI_HAIKI_SHURUI_CD = cd;
                    break;

                default:
                    // DO NOTHING
                    break;
            }
        }

        /// <summary>
        /// 前回値との比較
        /// </summary>
        /// <param name="type">入力種別</param>
        /// <param name="cd">前回値との比較を行うCD</param>
        /// <returns name="bool">TRUE:差分あり, FALSE:差分無し</returns>
        /// <remarks>
        /// 入力種別に応じた前回値と入力CDの比較を行う
        /// </remarks>
        private bool diffCD(ConstClass.INPUT_TYPE type, string cd)
        {
            bool bRet = false;

            switch (type)
            {
                case ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA:
                    // 排出事業者CDとの比較
                    if (this.oldCondition.HST_GYOUSHA_CD != cd)
                    {
                        // 差分あり
                        bRet = true;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA:
                    // 排出事業場CDとの比較
                    if (this.oldCondition.HST_GENBA_CD != cd)
                    {
                        // 差分あり
                        bRet = true;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA:
                    // 処分受託者CDとの比較
                    if (this.oldCondition.SBN_GYOUSHA_CD != cd)
                    {
                        // 差分あり
                        bRet = true;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA:
                    // 運搬先の事業場CDとの比較
                    if (this.oldCondition.UPN_SAKI_GENBA_CD != cd)
                    {
                        // 差分あり
                        bRet = true;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_JYUTAKUSHA:
                    // 運搬受託者CDとの比較
                    if (this.oldCondition.UPN_JYUTAKUSHA_CD != cd)
                    {
                        // 差分あり
                        bRet = true;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_HOUKOKUSHO_BUNRUI:
                    // 報告書分類CDとの比較
                    if (this.oldCondition.HOUKOKUSHO_BUNRUI_CD != cd)
                    {
                        // 差分あり
                        bRet = true;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_DENSHI_HAIKI_SHURUI:
                    // 電子廃棄物種類CDとの比較
                    if (this.oldCondition.DENSHI_HAIKI_SHURUI_CD != cd)
                    {
                        // 差分あり
                        bRet = true;
                    }
                    break;

                default:
                    // DO NOTHING
                    break;
            }

            return bRet;
        }

        #endregion - OldParamsControl -

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