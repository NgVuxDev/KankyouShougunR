using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 搬入先休動マスタDAO
        /// </summary>
        private M_WORK_CLOSED_HANNYUUSAKIDao HannyuusakiDao;

        // 20141008 koukouei 休動管理機能 start

        /// <summary>
        /// 収集受付DAO
        /// </summary>
        private T_UKETSUKE_SS_ENTRYDao uketsukeSsDao;

        /// <summary>
        /// 持込受付DAO
        /// </summary>
        private T_UKETSUKE_MK_ENTRYDao uketsukeMkDao;

        /// <summary>
        /// 定期配車荷降DAO
        /// </summary>
        private T_TEIKI_HAISHA_NIOROSHIDao teikiHaishaNioDao;

        /// <summary>
        /// 定期実績荷降DAO
        /// </summary>
        private T_TEIKI_JISSEKI_NIOROSHIDao teikiJissekiNioDao;

        /// <summary>
        /// 受入入力DAO
        /// </summary>
        private T_UKEIRE_ENTRYDao ukeireDao;

        /// <summary>
        /// 売上支払入力DAO
        /// </summary>
        private T_UR_SH_ENTRYDao urShDao;

        // 20141008 koukouei 休動管理機能 end

        /// <summary>
        /// 搬入先情報リスト（業者CD/業者名、現場CD/現場名）
        /// </summary>
        private DataTable HannyuusakiList;

        /// <summary>
        /// 搬入先休動マスタ検索結果
        /// </summary>
        private DataTable HannyuusakiKyuudouData;

        /// <summary>
        /// カレンダ表示されている日付
        /// </summary>
        private DateTime calendarDate;

        /// <summary>
        /// 新規用エンティティリスト
        /// </summary>
        private List<M_WORK_CLOSED_HANNYUUSAKI> insertList;

        /// <summary>
        /// 更新用エンティティリスト
        /// </summary>
        private List<M_WORK_CLOSED_HANNYUUSAKI> updateList;

        /// <summary>
        /// 削除用エンティティリスト
        /// </summary>
        private List<M_WORK_CLOSED_HANNYUUSAKI> deleteList;

        /// <summary>
        /// 検索条件
        /// </summary>
        public SearchDTOClass SearchString { get; set; }

        // 20141112 koukouei 休動管理機能 start
        /// <summary>
        /// 検索フラグ
        /// </summary>
        private bool searchFlg = false;

        /// <summary>
        /// 変更前行
        /// </summary>
        internal int rowIndex;
        // 20141112 koukouei 休動管理機能 end

        private MessageBoxShowLogic MsgBox;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                // メインフォーム
                this.form = targetForm;

                // 搬入先休動マスタDAO
                this.HannyuusakiDao = DaoInitUtility.GetComponent<M_WORK_CLOSED_HANNYUUSAKIDao>();
                // 20141008 koukouei 休動管理機能 start
                // 収集受付DAO
                this.uketsukeSsDao = DaoInitUtility.GetComponent<T_UKETSUKE_SS_ENTRYDao>();
                // 持込受付DAO
                this.uketsukeMkDao = DaoInitUtility.GetComponent<T_UKETSUKE_MK_ENTRYDao>();
                // 定期配車荷降DAO
                this.teikiHaishaNioDao = DaoInitUtility.GetComponent<T_TEIKI_HAISHA_NIOROSHIDao>();
                // 定期実績荷降DAO
                this.teikiJissekiNioDao = DaoInitUtility.GetComponent<T_TEIKI_JISSEKI_NIOROSHIDao>();
                // 受入入力DAO
                this.ukeireDao = DaoInitUtility.GetComponent<T_UKEIRE_ENTRYDao>();
                // 売上支払入力DAO
                this.urShDao = DaoInitUtility.GetComponent<T_UR_SH_ENTRYDao>();
                // 20141008 koukouei 休動管理機能 end
                this.MsgBox = new MessageBoxShowLogic();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicClass", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面初期化

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダフォームオブジェクト取得
                this.headerForm = (UIHeader)this.parentForm.headerForm;

                // 画面項目を初期化
                this.initializeScreen();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 日付設定DataGridView初期化処理
                this.dataGridViewInit();

                // サブファンクション非表示
                this.parentForm.ProcessButtonPanel.Visible = false;

                // 権限チェック
                if (!Manager.CheckAuthority("G203", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目を初期化
        /// </summary>
        private void initializeScreen()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件をクリア
                this.form.CONDITION_ITEM.Text = string.Empty;
                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;

                // カレンダ日付を設定する
                this.form.monthCalendar.TodayDate = this.parentForm.sysDate;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.form.monthCalendar.SelectionStart = this.parentForm.sysDate;
                this.form.monthCalendar.SelectionEnd = this.parentForm.sysDate;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                this.calendarDate = this.form.monthCalendar.TodayDate;
                // ヘッダフォーム年月日を設定する
                this.headerForm.txt_nengappi.Text = this.form.monthCalendar.TodayDate.ToString("yyyy年MM月");
            }
            catch (Exception ex)
            {
                LogUtility.Error("initializeScreen", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        public void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // F1 前月ボタン
                parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);
                // F2 次月ボタン
                parentForm.bt_func2.Click += new EventHandler(bt_func2_Click);
                // F4 削除ボタン
                parentForm.bt_func4.Click += new EventHandler(bt_func4_Click);
                // F7 条件ｸﾘｱボタン
                parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);
                // F8 検索ボタン
                this.form.C_Regist(parentForm.bt_func8);
                parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);
                // F9 登録ボタン
                parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);
                // F11 取消ボタン
                parentForm.bt_func11.Click += new EventHandler(bt_func11_Click);
                // F12 閉じるボタン
                parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

                // プロセスボタン使用不可
                parentForm.bt_process1.Enabled = false;
                parentForm.bt_process2.Enabled = false;
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process4.Enabled = false;
                parentForm.bt_process5.Enabled = false;

                // 処理No(ESC)使用不可
                parentForm.txb_process.Enabled = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.checkBox1.Enabled = false;
            this.form.dgvHiduke1.ReadOnly = true;
            this.form.checkBox2.Enabled = false;
            this.form.dgvHiduke2.ReadOnly = true;

            // FunctionButton
            this.parentForm.bt_func4.Enabled = false;
            this.parentForm.bt_func9.Enabled = false;
        }

        #endregion

        #region Functionボタン 押下処理

        #region F1 前月ボタン押下処理

        /// <summary>
        /// 前月ボタン押下処理
        /// </summary>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 月変更処理を行う
                MonthsAdd(-1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region F2 次月ボタン押下処理

        /// <summary>
        /// 次月ボタン押下処理
        /// </summary>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 月変更処理を行う
                MonthsAdd(1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func2_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region F4 削除ボタン押下処理

        /// <summary>
        /// 削除ボタン押下処理
        /// </summary>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 搬入先が選択されていない場合、処理しない
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    msgLogic.MessageBoxShow("E051", "搬入先");
                    return;
                }

                // 日付設定DataGridViewのデータを取得する（チェックボックスがチェックオフのレコード）
                List<M_WORK_CLOSED_HANNYUUSAKI> entityList = getDataGridViewList(false);

                // 削除用エンティティリストを作成する
                CreateEntityList(entityList, true);

                // [チェックボックス]のチェック有⇒チェック無に遷移した、該当日付が無の場合
                if (this.deleteList.Count == 0)
                {
                    // アラート表示し、処理しない
                    msgLogic.MessageBoxShow("E075");
                    return;
                }

                // 確認メッセージを表示する
                var result = msgLogic.MessageBoxShow("C026");
                if (result == DialogResult.Yes)
                {
                    using (Transaction tran = new Transaction())
                    {
                        // レコード削除を行う
                        foreach (M_WORK_CLOSED_HANNYUUSAKI hannyuusakiEntity in this.deleteList)
                        {
                            this.HannyuusakiDao.Delete(hannyuusakiEntity);
                        }

                        // コミット
                        tran.Commit();
                    }

                    // 正常終了メッセージ
                    msgLogic.MessageBoxShow("I001", "選択データの削除");
                    // 画面再検索
                    this.GetHannyuusakiKyuudouData(this.calendarDate.ToString("yyyy/MM"));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region F7 条件ｸﾘｱボタン押下処理

        /// <summary>
        /// 条件ｸﾘｱボタン押下処理
        /// </summary>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 検索条件をクリアする
                this.form.CONDITION_ITEM.Text = string.Empty;
                this.form.CONDITION_VALUE.Text = string.Empty;
                // フォーカスを設定する
                this.form.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func7_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region F8 検索ボタン押下処理

        /// <summary>
        /// 検索処理
        /// </summary>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 20141112 koukouei 休動管理機能 start
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool henkouFlg = workCloseHenkouCheck();
                if (searchFlg && henkouFlg && msgLogic.MessageBoxShowConfirm("選択した内容が破棄されますがよろしいですか。") != DialogResult.Yes)
                {
                    return;
                }
                // 20141112 koukouei 休動管理機能 end

                // 検索条件以外項目をクリアする
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.gcHannyuusakiList.SelectionChanged -= new EventHandler(this.form.gcHannyuusakiList_SelectionChanged);
                this.form.gcHannyuusakiList.Rows.Clear();
                this.dataGridViewInit();

                //// 検索条件をDTOに設定する
                //DTOClass searchDto = new DTOClass();
                //// フィールド名
                //searchDto.FieldName = this.form.CONDITION_FIELD_NAME_HIDDEN.Text;
                //// 検索条件値(エスケープ後)
                //searchDto.ConditionValue = this.EscForLike(this.form.CONDITION_VALUE.Text);

                //検索条件設定
                SetSearchString();

                // 検索条件によって、業者マスタ、現場マスタから搬入先情報（業者CD/業者名、現場CD/現場名）を取得する
                this.HannyuusakiList = HannyuusakiDao.GetHannyuusakiList(SearchString);

                //　取得したデータが存在しない場合
                if (this.HannyuusakiList.Rows.Count == 0)
                {
                    // メッセージ通知
                    msgLogic.MessageBoxShow("C001");
                    // 20141112 koukouei 休動管理機能 start
                    searchFlg = false;
                    // 20141112 koukouei 休動管理機能 end
                    return;
                }

                this.form.gcHannyuusakiList.IsBrowsePurpose = false;
                // 検索結果のレコード数Rowを作る
                this.form.gcHannyuusakiList.BeginEdit(false);
                this.form.gcHannyuusakiList.Rows.Add(this.HannyuusakiList.Rows.Count);
                this.form.gcHannyuusakiList.SelectionChanged += new EventHandler(this.form.gcHannyuusakiList_SelectionChanged);

                // 検索結果をMultiRowへ設定
                this.HannyuusakiList.BeginLoadData();
                for (int i = 0; i < this.HannyuusakiList.Rows.Count; i++)
                {
                    // 業者CD
                    this.form.gcHannyuusakiList[i, ConstClass.ColName.GYOUSHA_CD].Value = this.HannyuusakiList.Rows[i][ConstClass.ColName.GYOUSHA_CD];
                    // 業者名
                    this.form.gcHannyuusakiList[i, ConstClass.ColName.GYOUSHA_NAME_RYAKU].Value = this.HannyuusakiList.Rows[i][ConstClass.ColName.GYOUSHA_NAME_RYAKU];
                    // 現場CD
                    this.form.gcHannyuusakiList[i, ConstClass.ColName.GENBA_CD].Value = this.HannyuusakiList.Rows[i][ConstClass.ColName.GENBA_CD];
                    // 現場名
                    this.form.gcHannyuusakiList[i, ConstClass.ColName.GENBA_NAME_RYAKU].Value = this.HannyuusakiList.Rows[i][ConstClass.ColName.GENBA_NAME_RYAKU];
                }
                this.form.gcHannyuusakiList.IsBrowsePurpose = true;

                // 業者CDを設定する
                this.form.GYOUSHA_CD.Text = this.HannyuusakiList.Rows[0][ConstClass.ColName.GYOUSHA_CD].ToString();
                // 業者名を設定する
                this.form.GYOUSHA_NAME_RYAKU.Text = this.HannyuusakiList.Rows[0][ConstClass.ColName.GYOUSHA_NAME_RYAKU].ToString();
                // 現場CDを設定する
                this.form.GENBA_CD.Text = this.HannyuusakiList.Rows[0][ConstClass.ColName.GENBA_CD].ToString();
                // 現場名を設定する
                this.form.GENBA_NAME_RYAKU.Text = this.HannyuusakiList.Rows[0][ConstClass.ColName.GENBA_NAME_RYAKU].ToString();

                // 検索日付を取得する
                String searchDate = this.form.monthCalendar.SelectionStart.ToString("yyyy/MM");
                // 搬入先休動データを取得
                this.GetHannyuusakiKyuudouData(searchDate);

                // 20141112 koukouei 休動管理機能 start
                this.form.checkBox1.CheckedChanged -= this.form.checkBox1_CheckedChanged;
                this.form.checkBox1.Checked = false;
                this.form.checkBox1.CheckedChanged += this.form.checkBox1_CheckedChanged;
                searchFlg = true;
                rowIndex = 0;
                // 20141112 koukouei 休動管理機能 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region F9 登録ボタン押下処理

        /// <summary>
        /// 登録ボタン押下処理
        /// </summary>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 搬入先が選択しない場合、アラート表示し、処理しない
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    msgLogic.MessageBoxShow("E051", "搬入先");
                    this.form.gcHannyuusakiList.Focus();
                    return;
                }

                // 日付設定DataGridViewのデータを取得する（チェックボックスがチェックオンのレコード）
                List<M_WORK_CLOSED_HANNYUUSAKI> entityList = getDataGridViewList(true);
                // チェックボックスに1件もチェックされていない場合
                if (entityList.Count == 0)
                {
                    // アラート表示し、処理しない
                    msgLogic.MessageBoxShow("E051", "日付");
                    return;
                }

                // 20141008 koukouei 休動管理機能 start
                if (!this.workCloseCheck())
                {
                    // アラート表示し、処理しない
                    msgLogic.MessageBoxShow("E205", "搬入先");
                    return;
                }
                // 20141008 koukouei 休動管理機能 end

                // 確認メッセージを表示する
                var result = msgLogic.MessageBoxShow("C046", "登録");
                if (result == DialogResult.Yes)
                {
                    // 更新、登録用エンティティリストを作成する
                    CreateEntityList(entityList, false);

                    bool isError = false;
                    using (Transaction tran = new Transaction())
                    {
                        // レコード登録を行う
                        foreach (M_WORK_CLOSED_HANNYUUSAKI insertEntity in this.insertList)
                        {
                            M_WORK_CLOSED_HANNYUUSAKI checkData = this.HannyuusakiDao.GetHannyuusakiKyuudouDataWithTableLock(insertEntity);

                            if (checkData == null)
                            {
                                this.HannyuusakiDao.Insert(insertEntity);
                            }
                            else
                            {
                                isError = true;
                                break;
                            }
                        }

                        if (!isError)
                        {
                            // レコード更新を行う
                            foreach (M_WORK_CLOSED_HANNYUUSAKI updateEntity in this.updateList)
                            {
                                this.HannyuusakiDao.Update(updateEntity);
                            }
                        }

                        if (!isError)
                        {
                            // コミット
                            tran.Commit();
                        }
                    }

                    if (!isError)
                    {
                        // 正常終了メッセージ
                        msgLogic.MessageBoxShow("I001", "登録");
                    }
                    else
                    {
                        // Insert時に既にデータがあった場合は排他エラーとする
                        msgLogic.MessageBoxShow("E080");
                    }

                    // 画面再検索
                    this.GetHannyuusakiKyuudouData(this.calendarDate.ToString("yyyy/MM"));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region F11 取消ボタン押下処理

        /// <summary>
        /// 取消ボタン押下処理
        /// </summary>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 搬入先が選択されていない場合
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    // 日付設定DataGridView初期化処理
                    this.dataGridViewInit();
                    return;
                }

                // 検索日付を取得する
                String searchDate = this.calendarDate.ToString("yyyy/MM");
                // 搬入先休動データを取得
                this.GetHannyuusakiKyuudouData(searchDate);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func11_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region F12 閉じるボタン押下処理

        /// <summary>
        /// 閉じるボタン押下処理
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 「前月」、「次月」ボタン押下処理

        /// <summary>
        /// 「前月」、「次月」ボタン押下処理
        /// </summary>
        public void MonthsAdd(int addedDays)
        {
            try
            {
                LogUtility.DebugMethodStart(addedDays);

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //DateTime calendarBefor = DateTime.Now.Date;
                DateTime calendarBefor = this.parentForm.sysDate.Date;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                // カレンダ表示されているの前月の日付を取得する
                calendarBefor = this.form.monthCalendar.SelectionStart.AddMonths(addedDays);

                // 前月の一日を設定する
                int day = calendarBefor.Day;
                calendarBefor = calendarBefor.AddDays(-(day - 1));
                // カレンダに前月の一日を設定する
                this.form.monthCalendar.SetDate(calendarBefor);
                // フォーカスを設定する
                this.form.monthCalendar.Focus();

                // ヘッダの年月日を設定する
                this.headerForm.txt_nengappi.Text = calendarBefor.ToString("yyyy年MM月");

                // 搬入先が選択されていない場合
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.dataGridViewInit();
                }
                else
                {
                    //検索日付(前月)を取得する
                    string searchDate = calendarBefor.ToString("yyyy/MM");
                    // 搬入先休動データを取得
                    this.GetHannyuusakiKyuudouData(searchDate);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("MonthsAdd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 日付設定DataGridViewのデータ取得

        /// <summary>
        /// 日付設定DataGridViewのデータを取得する
        /// </summary>
        /// <param name="isChecked">true:チェックボックスがチェックオン、false:チェックボックスがチェックオフ</param>
        /// <returns>搬入先休動エンティティリスト</returns>
        internal List<M_WORK_CLOSED_HANNYUUSAKI> getDataGridViewList(bool isChecked)
        {
            // 搬入先休動エンティティリスト
            List<M_WORK_CLOSED_HANNYUUSAKI> entityList = new List<M_WORK_CLOSED_HANNYUUSAKI>();

            try
            {
                LogUtility.DebugMethodStart(isChecked);

                // 搬入先休動エンティティ
                M_WORK_CLOSED_HANNYUUSAKI entity = null;

                // 日付設定DataGridView1のデータを取得する
                foreach (DataGridViewRow row in this.form.dgvHiduke1.Rows)
                {
                    // チェックボックスがチェックオンの場合
                    if (isChecked.Equals(row.Cells["CLOSED_DATE_FLG"].Value))
                    {
                        entity = new M_WORK_CLOSED_HANNYUUSAKI();
                        // 業者CD
                        entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                        // 現場CD
                        entity.GENBA_CD = this.form.GENBA_CD.Text;
                        // 休動日
                        entity.CLOSED_DATE = Convert.ToDateTime(this.calendarDate.ToString("yyyy/MM") + "/" + row.Cells["CLOSED_DATE"].Value.ToString());
                        // 備考
                        entity.BIKOU = row.Cells["BIKOU"].Value != null ? row.Cells["BIKOU"].Value.ToString() : "";
                        // 削除フラグ
                        entity.DELETE_FLG = false;
                        //更新時間、更新者、更新PCを設定
                        var dataBinder1 = new DataBinderLogic<M_WORK_CLOSED_HANNYUUSAKI>(entity);
                        dataBinder1.SetSystemProperty(entity, false);
                        // TIME_STAMP
                        if (!string.IsNullOrEmpty(row.Cells["TIME_STAMP"].FormattedValue.ToString()))
                        {
                            entity.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
                        }

                        entityList.Add(entity);
                    }
                }

                // 日付設定DataGridView2のデータを取得する
                foreach (DataGridViewRow row in this.form.dgvHiduke2.Rows)
                {
                    // チェックボックスがチェックオンの場合
                    if (isChecked.Equals(row.Cells["CLOSED_DATE_FLG2"].Value))
                    {
                        entity = new M_WORK_CLOSED_HANNYUUSAKI();
                        // 業者CD
                        entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                        // 現場CD
                        entity.GENBA_CD = this.form.GENBA_CD.Text;
                        // 休動日
                        entity.CLOSED_DATE = Convert.ToDateTime(this.calendarDate.ToString("yyyy/MM") + "/" + row.Cells["CLOSED_DATE2"].Value.ToString());
                        // 備考
                        entity.BIKOU = row.Cells["BIKOU2"].Value != null ? row.Cells["BIKOU2"].Value.ToString() : "";
                        // 削除フラグ
                        entity.DELETE_FLG = false;
                        //更新時間、更新者、更新PCを設定
                        var dataBinder1 = new DataBinderLogic<M_WORK_CLOSED_HANNYUUSAKI>(entity);
                        dataBinder1.SetSystemProperty(entity, false);

                        // TIME_STAMP
                        if (!string.IsNullOrEmpty(row.Cells["TIME_STAMP2"].FormattedValue.ToString()))
                        {
                            entity.TIME_STAMP = (byte[])row.Cells["TIME_STAMP2"].Value;
                        }

                        entityList.Add(entity);
                    }
                }

                return entityList;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(entityList);
            }
        }

        #endregion

        #region DB登録用エンティティリスト生成処理

        /// <summary>
        /// DB登録用エンティティリストを生成する
        /// </summary>
        /// <param name="entityList">搬入先休動エンティティリスト</param>
        /// <param name="deleteFlg">削除フラグ</param>
        internal void CreateEntityList(List<M_WORK_CLOSED_HANNYUUSAKI> entityList, bool deleteFlg)
        {
            try
            {
                LogUtility.DebugMethodStart(entityList, deleteFlg);

                // 更新用エンティティリスト
                this.updateList = new List<M_WORK_CLOSED_HANNYUUSAKI>();
                // 登録用エンティティリスト
                this.insertList = new List<M_WORK_CLOSED_HANNYUUSAKI>();
                // 削除用エンティティリスト
                this.deleteList = new List<M_WORK_CLOSED_HANNYUUSAKI>();
                // 画面上の日付
                String entityClosedDate = string.Empty;
                // DB側の日付
                String dbClosedDate = string.Empty;
                // 存在フラグ
                bool isExsit = false;

                // 更新用エンティティリストを繰り返す
                foreach (M_WORK_CLOSED_HANNYUUSAKI entity in entityList)
                {
                    // 画面上の日付を取得する
                    entityClosedDate = DateTime.Parse(entity.CLOSED_DATE.ToString()).ToShortDateString();

                    // 存在フラグを初期化
                    isExsit = false;
                    // 搬入先休動マスタ検索結果を繰り返す
                    for (int i = 0; i < this.HannyuusakiKyuudouData.Rows.Count; i++)
                    {
                        // DB側の日付を取得する
                        dbClosedDate = DateTime.Parse(this.HannyuusakiKyuudouData.Rows[i]["CLOSED_DATE"].ToString()).ToShortDateString();

                        // 画面上の日付と比較し、同じの場合
                        if (dbClosedDate.Equals(entityClosedDate))
                        {
                            if (deleteFlg)
                            {
                                deleteList.Add(entity);
                            }
                            else
                            {
                                updateList.Add(entity);
                            }
                            isExsit = true;
                            break;
                        }
                    }

                    // DBに存在しない場合、登録対象になります
                    if (!isExsit)
                    {
                        this.insertList.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityList", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            LogUtility.DebugMethodStart();

            SearchDTOClass entity = new SearchDTOClass();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    //検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }

            this.SearchString = entity;

            LogUtility.DebugMethodEnd();
        }

        #endregion

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

        #endregion

        #region 日付設定DataGridView初期化

        /// <summary>
        /// 日付設定DataGridView初期化
        /// </summary>
        private void dataGridViewInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 年を取得
                int year = this.form.monthCalendar.SelectionStart.Year;
                // 月を取得
                int month = this.form.monthCalendar.SelectionStart.Month;
                // 当月の日数
                int dayCount = DateTime.DaysInMonth(year, month);

                // 明細クリア
                this.form.dgvHiduke1.Rows.Clear();
                this.form.dgvHiduke2.Rows.Clear();

                // 明細行を追加
                this.form.dgvHiduke1.Rows.Add(16);
                this.form.dgvHiduke2.Rows.Add(dayCount - 16);

                // 20141112 koukouei 休動管理機能 start
                this.form.dgvHiduke1.CellValueChanged -= this.form.dgvHiduke1_CellValueChanged;
                this.form.dgvHiduke2.CellValueChanged -= this.form.dgvHiduke2_CellValueChanged;
                // 20141112 koukouei 休動管理機能 end
                // 日付設定DataGridView1を設定する
                for (int i = 0; i < 16; i++)
                {
                    // 日付設定DataGridViewの日付の日を取得する
                    String day = (i + 1).ToString("D2");
                    // 日付
                    this.form.dgvHiduke1["CLOSED_DATE", i].Value = day;
                    // 曜日
                    DateTime date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                    this.form.dgvHiduke1["WEEK", i].Value = ("日月火水木金土").Substring(int.Parse(date.Date.DayOfWeek.ToString("d")), 1);
                    // チェックボックスを設定する
                    this.form.dgvHiduke1["CLOSED_DATE_FLG", i].Value = false;
                    // 備考
                    this.form.dgvHiduke1["BIKOU", i].Value = string.Empty;
                }
                // 日付設定DataGridView2を設定する
                for (int i = 0; i < dayCount - 16; i++)
                {
                    // 日付設定DataGridViewの日付の日を取得する
                    String day = (i + 17).ToString("D2");
                    // 日付
                    this.form.dgvHiduke2["CLOSED_DATE2", i].Value = day;
                    // 曜日
                    DateTime date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                    this.form.dgvHiduke2["WEEK2", i].Value = ("日月火水木金土").Substring(int.Parse(date.Date.DayOfWeek.ToString("d")), 1);
                    // チェックボックスを設定する
                    this.form.dgvHiduke2["CLOSED_DATE_FLG2", i].Value = false;
                    // 備考
                    this.form.dgvHiduke2["BIKOU2", i].Value = string.Empty;
                }
                // 20141112 koukouei 休動管理機能 start
                this.form.dgvHiduke1.CellValueChanged += this.form.dgvHiduke1_CellValueChanged;
                this.form.dgvHiduke2.CellValueChanged += this.form.dgvHiduke2_CellValueChanged;
                // 20141112 koukouei 休動管理機能 end

                // 赤枠非表示する
                this.form.clearCurrentCell();
            }
            catch (Exception ex)
            {
                LogUtility.Error("dataGridViewInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region Like検索の文字列をエスケープ

        /// <summary>
        /// Like検索の文字列をエスケープ
        /// </summary>
        /// <param name="str">条件値</param>
        /// <returns>エスケープ後値</returns>
        private string EscForLike(string str)
        {
            string result = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(str);

                // Like検索の文字列をエスケープ
                result = Regex.Replace(str, @"[%_\[]", "[$0]");

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EscForLike", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }

        #endregion

        #region 搬入先休動データを取得処理

        /// <summary>
        /// 搬入先休動データを取得する
        /// </summary>
        public void GetHannyuusakiKyuudouData(String searchDate)
        {
            try
            {
                LogUtility.DebugMethodStart(searchDate);

                // 検索条件をDTOに設定する
                DTOClass searchDto = new DTOClass();
                // 業者CDを検索条件に設定
                searchDto.gyoushaCd = this.form.GYOUSHA_CD.Text;
                // 現場CDを検索条件に設定
                searchDto.genbaCd = this.form.GENBA_CD.Text;
                // 検索日付を検索条件に設定
                searchDto.searchDate = searchDate;

                // 検索条件によって、搬入先休動マスタのデータを取得する
                this.HannyuusakiKyuudouData = HannyuusakiDao.GetHannyuusakiKyuudouData(searchDto);

                // 取得できない場合、日付設定DataGridViewを初期化する
                if (this.HannyuusakiKyuudouData.Rows.Count == 0)
                {
                    dataGridViewInit();
                }
                // 取得できる場合、日付設定DataGridViewに取得したデータを設定する
                else
                {
                    setDataGridView();
                }
                // 20141112 koukouei 休動管理機能 start
                this.form.checkBox1.Checked = false;
                this.form.checkBox2.Checked = false;
                // 20141112 koukouei 休動管理機能 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHannyuusakiKyuudouData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 日付設定DataGridViewを設定処理

        /// <summary>
        /// 日付設定DataGridViewを設定する
        /// </summary>
        private void setDataGridView()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 年を取得
                int year = this.form.monthCalendar.SelectionStart.Year;
                // 月を取得
                int month = this.form.monthCalendar.SelectionStart.Month;
                // 当月の日数
                int dayCount = DateTime.DaysInMonth(year, month);

                // 明細クリア
                this.form.dgvHiduke1.Rows.Clear();
                this.form.dgvHiduke2.Rows.Clear();

                // 明細行を追加
                this.form.dgvHiduke1.Rows.Add(16);
                this.form.dgvHiduke2.Rows.Add(dayCount - 16);

                this.HannyuusakiKyuudouData.BeginLoadData();

                // 20141112 koukouei 休動管理機能 start
                this.form.dgvHiduke1.CellValueChanged -= this.form.dgvHiduke1_CellValueChanged;
                this.form.dgvHiduke2.CellValueChanged -= this.form.dgvHiduke2_CellValueChanged;
                // 20141112 koukouei 休動管理機能 end
                // 日付設定DataGridView1検索結果設定
                for (int i = 0; i < 16; i++)
                {
                    // 日付設定DataGridViewの日付の日を取得する
                    String day = (i + 1).ToString("D2");
                    // 日付
                    this.form.dgvHiduke1["CLOSED_DATE", i].Value = day;
                    // 曜日
                    DateTime date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                    this.form.dgvHiduke1["WEEK", i].Value = ("日月火水木金土").Substring(int.Parse(date.Date.DayOfWeek.ToString("d")), 1);

                    for (int j = 0; j < this.HannyuusakiKyuudouData.Rows.Count; j++)
                    {
                        // 休動日
                        String closedDate = DateTime.Parse(this.HannyuusakiKyuudouData.Rows[j]["CLOSED_DATE"].ToString()).ToShortDateString();
                        // 休動日の日を取得する
                        String closedDateDay = closedDate.Substring(8, 2);

                        if (closedDateDay.Equals(day))
                        {
                            // チェックボックスを設定する
                            this.form.dgvHiduke1["CLOSED_DATE_FLG", i].Value = true;
                            // 備考
                            this.form.dgvHiduke1["BIKOU", i].Value = this.HannyuusakiKyuudouData.Rows[j]["BIKOU"].ToString();
                            // タイムスタンプ
                            this.form.dgvHiduke1["TIME_STAMP", i].Value = this.HannyuusakiKyuudouData.Rows[j]["TIME_STAMP"];
                            break;
                        }
                        else
                        {
                            // チェックボックスを設定する
                            this.form.dgvHiduke1["CLOSED_DATE_FLG", i].Value = false;
                            // 備考
                            this.form.dgvHiduke1["BIKOU", i].Value = string.Empty;
                        }
                    }
                }
                // 日付設定DataGridView2検索結果設定
                for (int i = 0; i < dayCount - 16; i++)
                {
                    // 日付設定DataGridView2の日付の日を取得する
                    String day = (i + 17).ToString("D2");
                    // 日付
                    this.form.dgvHiduke2["CLOSED_DATE2", i].Value = day;
                    // 曜日
                    DateTime date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                    this.form.dgvHiduke2["WEEK2", i].Value = ("日月火水木金土").Substring(int.Parse(date.Date.DayOfWeek.ToString("d")), 1);

                    for (int j = 0; j < this.HannyuusakiKyuudouData.Rows.Count; j++)
                    {
                        // 休動日
                        String closedDate = DateTime.Parse(this.HannyuusakiKyuudouData.Rows[j]["CLOSED_DATE"].ToString()).ToShortDateString();
                        // 休動日の日を取得する
                        String closedDateDay = closedDate.Substring(8, 2);

                        if (closedDateDay.Equals(day))
                        {
                            // チェックボックスを設定する
                            this.form.dgvHiduke2["CLOSED_DATE_FLG2", i].Value = true;
                            // 備考
                            this.form.dgvHiduke2["BIKOU2", i].Value = this.HannyuusakiKyuudouData.Rows[j]["BIKOU"].ToString();
                            // TIME_STAMP
                            this.form.dgvHiduke2["TIME_STAMP2", i].Value = this.HannyuusakiKyuudouData.Rows[j]["TIME_STAMP"];
                            break;
                        }
                        else
                        {
                            // チェックボックスを設定する
                            this.form.dgvHiduke2["CLOSED_DATE_FLG2", i].Value = false;
                            // 備考
                            this.form.dgvHiduke2["BIKOU2", i].Value = string.Empty;
                        }
                    }
                }
                // 20141112 koukouei 休動管理機能 start
                this.form.dgvHiduke1.CellValueChanged += this.form.dgvHiduke1_CellValueChanged;
                this.form.dgvHiduke2.CellValueChanged += this.form.dgvHiduke2_CellValueChanged;
                // 20141112 koukouei 休動管理機能 end

                // 赤枠非表示する
                this.form.clearCurrentCell();
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDataGridView", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region カレンダ日付変更処理

        /// <summary>
        /// カレンダ日付変更処理
        /// </summary>
        public bool calendarDateChanged(bool calendarFlg)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(calendarFlg);

                if (calendarFlg)
                {
                    // 変更前の年月を取得する
                    String nowDate = this.calendarDate.ToString("yyyy/MM");

                    // 変更後年月を取得する
                    String seleteDate = this.form.monthCalendar.SelectionStart.ToString("yyyy/MM");

                    // 変更前の年月と変更後年月は同一月ではないの場合
                    if (!nowDate.Equals(seleteDate))
                    {
                        // 20141112 koukouei 休動管理機能 start
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        bool henkouFlg = this.workCloseHenkouCheck();
                        if (searchFlg && henkouFlg && msgLogic.MessageBoxShowConfirm("選択した内容が破棄されますがよろしいですか。") != DialogResult.Yes)
                        {
                            this.form.monthCalendar.SetSelectionRange(this.calendarDate, this.calendarDate);
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        // 20141112 koukouei 休動管理機能 end

                        // ヘッダフォーム年月日を設定する
                        this.headerForm.txt_nengappi.Text = this.form.monthCalendar.SelectionStart.ToString("yyyy年MM月");

                        // 変更後年月によって、搬入先休動データを再取得する
                        GetHannyuusakiKyuudouData(seleteDate);
                    }

                    // 変更後日によって、日付設定DataGridViewの行目を選択する
                    int selectedDay = this.form.monthCalendar.SelectionStart.Day;
                    if (selectedDay < 17)
                    {
                        this.form.dgvHiduke1.Rows[selectedDay - 1].Cells[0].Selected = true;
                        // 赤枠非表示する
                        this.form.dgvHiduke2.CurrentCell = null;
                    }
                    else
                    {
                        this.form.dgvHiduke2.Rows[selectedDay - 17].Cells[0].Selected = true;
                        // 赤枠非表示する
                        this.form.dgvHiduke1.CurrentCell = null;
                    }
                }

                // カレンダ表示されている日付に変更後日付を設定する
                this.calendarDate = this.form.monthCalendar.SelectionStart;
            }
            catch (Exception ex)
            {
                LogUtility.Error("calendarDateChanged", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 日付設定DataGridViewの行クリック処理

        /// <summary>
        /// 日付設定DataGridViewの行クリック処理
        /// </summary>
        public void dgvHidukeCellClick(String selectedDay)
        {
            try
            {
                LogUtility.DebugMethodStart(selectedDay);

                // 選択した行目の日付によって、カレンダを設定する
                int day = this.calendarDate.Day;
                int intSelectDay = int.Parse(selectedDay);
                DateTime setDate = this.calendarDate.AddDays(-(day - intSelectDay));

                // カレンダに選択した行目の日付を設定する
                this.form.monthCalendar.SetDate(setDate);
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvHidukeCellClick", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region Equals/GetHashCode/ToString

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

        #endregion

        // 20141008 koukouei 休動管理機能 start

        #region 休動チェック処理

        /// <summary>
        /// 休動チェック処理
        /// </summary>
        /// <returns></returns>
        private bool workCloseCheck()
        {
            LogUtility.DebugMethodStart();

            bool result = true;
            DateTime date = this.form.monthCalendar.SelectionStart;
            // 収集受付
            T_UKETSUKE_SS_ENTRY uketsukeSS = new T_UKETSUKE_SS_ENTRY();
            uketsukeSS.NIOROSHI_GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            uketsukeSS.NIOROSHI_GENBA_CD = this.form.GENBA_CD.Text;
            uketsukeSS.SAGYOU_DATE = date.ToString("yyyy-MM");
            DataTable dtUketsukeSs = this.uketsukeSsDao.GetUketsukeSSData(uketsukeSS);
            // 持込受付
            T_UKETSUKE_MK_ENTRY uketsukeMK = new T_UKETSUKE_MK_ENTRY();
            uketsukeMK.NIOROSHI_GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            uketsukeMK.NIOROSHI_GENBA_CD = this.form.GENBA_CD.Text;
            uketsukeMK.SAGYOU_DATE = date.ToString("yyyy-MM");
            DataTable dtUketsukeMk = this.uketsukeMkDao.GetUketsukeMKData(uketsukeMK);
            // 定期配車荷降
            NioRoShiDTO teikiHaishaNio = new NioRoShiDTO();
            teikiHaishaNio.NIOROSHI_GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            teikiHaishaNio.NIOROSHI_GENBA_CD = this.form.GENBA_CD.Text;
            teikiHaishaNio.SAGYOU_DATE = date;
            DataTable dtTeikiHaishaNio = this.teikiHaishaNioDao.GetTeikiHaishaNioData(teikiHaishaNio);
            // 定期実績荷降
            NioRoShiDTO teikiJissekiNio = new NioRoShiDTO();
            teikiJissekiNio.NIOROSHI_GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            teikiJissekiNio.NIOROSHI_GENBA_CD = this.form.GENBA_CD.Text;
            teikiJissekiNio.SAGYOU_DATE = date;
            DataTable dtTeikiJissekiNio = this.teikiJissekiNioDao.GetTeikiJissekiNioData(teikiJissekiNio);
            // 受入入力
            T_UKEIRE_ENTRY ukeire = new T_UKEIRE_ENTRY();
            ukeire.NIOROSHI_GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            ukeire.NIOROSHI_GENBA_CD = this.form.GENBA_CD.Text;
            ukeire.DENPYOU_DATE = date;
            DataTable dtUkeire = this.ukeireDao.GetUkeireData(ukeire);
            // 売上支払入力
            T_UR_SH_ENTRY urSh = new T_UR_SH_ENTRY();
            urSh.NIOROSHI_GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            urSh.NIOROSHI_GENBA_CD = this.form.GENBA_CD.Text;
            urSh.DENPYOU_DATE = date;
            DataTable dtURSH = this.urShDao.GetURSHData(urSh);

            DataRow[] rows;
            // dataGird1を設定する
            foreach (DataGridViewRow row in this.form.dgvHiduke1.Rows)
            {
                if (!(bool)row.Cells["CLOSED_DATE_FLG"].Value)
                {
                    continue;
                }
                string day = row.Cells["CLOSED_DATE"].Value.ToString();
                // 日付
                date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                // 収集受付
                rows = dtUketsukeSs.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 持込受付
                rows = dtUketsukeMk.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 定期配車荷降
                rows = dtTeikiHaishaNio.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 定期実績荷降
                rows = dtTeikiJissekiNio.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 受入入力
                rows = dtUkeire.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 売上支払入力
                rows = dtURSH.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
            }

            // dataGird2を設定する
            foreach (DataGridViewRow row in this.form.dgvHiduke2.Rows)
            {
                if (!(bool)row.Cells["CLOSED_DATE_FLG2"].Value)
                {
                    continue;
                }
                string day = row.Cells["CLOSED_DATE2"].Value.ToString();
                // 日付
                date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                // 収集受付
                rows = dtUketsukeSs.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 持込受付
                rows = dtUketsukeMk.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 定期配車荷降
                rows = dtTeikiHaishaNio.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 定期実績荷降
                rows = dtTeikiJissekiNio.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 受入入力
                rows = dtUkeire.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 売上支払入力
                rows = dtURSH.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
            }

            LogUtility.DebugMethodEnd();
            return result;
        }

        #endregion

        // 20141008 koukouei 休動管理機能 end

        // 20141112 koukouei 休動管理機能 start

        #region 休動変更チェック処理

        /// <summary>
        /// 休動変更チェック処理
        /// </summary>
        /// <returns></returns>
        internal bool workCloseHenkouCheck()
        {
            LogUtility.DebugMethodStart();

            // dataGird1を設定する
            foreach (DataGridViewRow row in this.form.dgvHiduke1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["HENKOU_FLG1"].Value))
                {
                    return true; ;
                }
            }

            // dataGird2を設定する
            foreach (DataGridViewRow row in this.form.dgvHiduke2.Rows)
            {
                if (Convert.ToBoolean(row.Cells["HENKOU_FLG2"].Value))
                {
                    return true; ;
                }
            }

            LogUtility.DebugMethodEnd();
            return false;
        }

        #endregion

        // 20141112 koukouei 休動管理機能 end
    }
}