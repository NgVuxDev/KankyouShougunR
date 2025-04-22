// $Id: LogicCls.cs 24958 2014-07-08 06:41:18Z nagata $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.APP;
using Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.Const;
using Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.DAO;
using Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.DTO;
using Shougun.Core.Message;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.Logic
{
    /// <summary>
    /// 社内経路名入力（電子）画面のビジネスロジック
    /// </summary>
    public class LogicClass
    {
        #region フィールド

        /// <summary>
        /// 社内経路入力（電子）画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 親Form
        /// </summary>
        private MasterBaseForm parentForm;

        /// <summary>
        /// 社内経路入力のDao
        /// </summary>
        private DaoClass dao;

        /// <summary>
        /// 社内経路Dao
        /// </summary>
        private IM_DENSHI_KEIYAKU_SHANAI_KEIRODao denshiKeiyakuShanaiKeiroDao;

        /// <summary>
        /// メッセージ表示ロジック
        /// </summary>
        private MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// DataGridViewの前回値(検索時に取得用)
        /// </summary>
        private DataGridView DGVSearchResult = new DataGridView();

        /// <summary>初期表示フラグ - 初期表示時の検索アラート回避用</summary>
        private bool initFlg = false;

        private GET_SYSDATEDao dateDao;

        /// <summary>一覧の前回入力値</summary>
        private Dictionary<string, string> preValueDic = new Dictionary<string, string>();

        /// <summary>入力時のエラー状態判定フラグ</summary>
        private bool hasErrorInput = false;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<DaoClass>();
            this.denshiKeiyakuShanaiKeiroDao = DaoInitUtility.GetComponent<IM_DENSHI_KEIYAKU_SHANAI_KEIRODao>();
            this.msgLogic = new MessageBoxShowLogic();
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
        }

        #endregion コンストラクタ

        #region 初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                this.initFlg = true;

                // 画面情報セット
                this.parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 初期値読込
                this.GetPropertiesSettings();

                // 再検索
                this.Search();

                // 処理No制御
                this.parentForm.txb_process.Enabled = false;

                this.initFlg = false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E093");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            // 読み込んだボタン設定をセット
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, this.form.WindowType);

            /* 初期表示はF4,6,9は非活性、検索が実行されたら活性 */
            this.SetFunctionButtonEnabled(false);
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            // ボタン設定ファイル読込
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, UIConstans.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            // 行挿入ボタン(F1)イベント生成
            this.parentForm.bt_func1.Click += new EventHandler(this.form.AddRow);

            // 行削除ボタン(F2)イベント生成
            this.parentForm.bt_func2.Click += new EventHandler(this.form.DeleteRow);

            // 削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(this.parentForm.bt_func4);
            this.parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);

            //CSV出力ボタン(F6)イベント生成
            this.parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

            //条件クリアボタン(F7)イベント生成
            this.parentForm.bt_func7.Click += new EventHandler(this.form.ClearCondition);

            //検索ボタン(F8)イベント生成
            this.parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_MasterRegist(this.parentForm.bt_func9);
            this.parentForm.bt_func9.Click += new EventHandler(this.form.Regist);

            //取消ボタン(F11)イベント生成
            this.parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            this.parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 検索条件Enterイベント
            this.form.CONDITION_VALUE.Enter += new EventHandler(this.form.CONDITION_VALUE_Enter);

            // 一覧のEnterイベント
            this.form.Ichiran.CellEnter += new DataGridViewCellEventHandler(this.form.Ichiran_CellEnter);

            // 一覧のValidatingイベント
            this.form.Ichiran.CellValidating += new DataGridViewCellValidatingEventHandler(this.form.Ichiran_CellValidating);

            // 社内経路名CDイベント
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Enter += new EventHandler(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD_Enter);
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Validating += new CancelEventHandler(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD_Validating);
        }

        #endregion 初期化処理

        #region 参照モード表示

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.Ichiran.ReadOnly = true;
            this.form.Ichiran.AllowUserToAddRows = false;
            this.form.Ichiran.IsBrowsePurpose = true;

            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func1.Enabled = false;
            parentForm.bt_func2.Enabled = false;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func9.Enabled = false;
        }

        #endregion

        #region FunctionKeyイベント

        /// <summary>
        /// 行挿入処理
        /// </summary>
        /// <param name="index">挿入先行番号</param>
        internal void AddRow(int index)
        {
            this.form.Ichiran.Rows.Insert(index, 1);
        }

        /// <summary>
        /// 行削除処理
        /// </summary>
        /// <param name="index">削除行番号</param>
        internal void DeleteRow(int index)
        {
            if (this.form.Ichiran == null || this.form.Ichiran.Rows[index].IsNewRow)
            {
                return;
            }

            this.form.Ichiran.Rows.RemoveAt(index);
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        internal void PhysicalDelete()
        {
            bool ret = true;
            try
            {
                // 削除チェックボックスONの項目が1件以上あるかチェック
                bool check = false;
                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    if (row.Index == this.form.Ichiran.RowCount - 1)
                    {
                        // 最終行は何もしない
                        break;
                    }

                    if (this.ConvertToBool(row.Cells["chb_delete"].Value))
                    {
                        check = true;
                    }
                }

                if (!check)
                {
                    // 削除対象データなし
                    this.msgLogic.MessageBoxShow("E075");
                    return;
                }

                /* DeleteInsertを行うため、全行削除した後に削除対象外のデータを改めてInsertしなおす */

                // 削除実行確認
                var result = this.msgLogic.MessageBoxShow("C021");
                if (result != DialogResult.Yes)
                {
                    return;
                }

                // 削除対象Entityリスト作成
                var delList = this.CreateDeleteList();
                // 登録対象Entityリスト作成
                var regList = this.CreateRegistList(true);

                using (Transaction tran = new Transaction())
                {
                    foreach (var delData in delList)
                    {
                        // 物理削除
                        this.denshiKeiyakuShanaiKeiroDao.Delete(delData.entity);
                    }

                    // 登録実行
                    foreach (var regData in regList)
                    {
                        // 新規登録
                        this.denshiKeiyakuShanaiKeiroDao.Insert(regData.entity);
                    }

                    // コミット
                    tran.Commit();
                }

                // 削除完了
                this.msgLogic.MessageBoxShow("I001", "選択データの削除");
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //SQL登録異常
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E245");
                }
                ret = false;
            }

            if (ret)
            {
                // 再検索
                this.Search();
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        internal void CSVOutput()
        {
            if (this.form.Ichiran.RowCount <= 1)
            {
                // 出力対象データなし
                this.msgLogic.MessageBoxShow("E044");
            }
            else
            {
                // 画面表示内容をCSV出力しますか？
                if (this.msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    // 「はい」を選択した場合はCSV出力を行う
                    var csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENSHI_KEIYAKU_SHANAI_KEIRO), this.form);
                }
            }
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        internal void InitCondition()
        {
            // 社内経路名
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text = string.Empty;
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = string.Empty;

            // 検索条件
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            // 社内経路名CDにSetFocus
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Focus();

            // 一覧をクリア
            this.IchiranClear();

            // F4,6,9を非活性
            this.SetFunctionButtonEnabled(false);

            // 明細操作不可
            this.SetIchiranEnavled(false);
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        internal bool Search()
        {
            try
            {
                if (true == string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text))
                {
                    // 一覧をクリア
                    this.IchiranClear();

                    // F4,6,9を非活性
                    this.SetFunctionButtonEnabled(false);

                    // 明細操作不可
                    this.SetIchiranEnavled(false);

                    // 初期表示時以外はアラート表示
                    if (!initFlg)
                    {
                        this.msgLogic.MessageBoxShow("E001", "社内経路名");
                    }

                    return true;
                }

                // 検索結果で明細を作成
                this.CreateIchiranRowData(this.CreateIchiranData());

                // 条件保存
                this.SetPropertiesSettings();

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M719", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // F4,6,9を活性
                    this.SetFunctionButtonEnabled(true);
                }
                else
                {
                    this.DispReferenceMode();
                }

                // 明細操作可
                this.SetIchiranEnavled(true);

                // 検索時に0件ヒットだとCurrentRowがNullになるのでここで設定しておく
                // CurrentRowがNullでも各操作ではCurrentRowのNullチェックをしているので問題はない（見た目の問題）
                this.form.Ichiran.CurrentCell = this.form.Ichiran[0, 0];

                // 前回値を保存
                this.GetDataGridView(out this.DGVSearchResult);
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }
        }

        /// <summary>
        /// 現在のDataGridViewを取得します
        /// </summary>
        /// <param name="dgv">データグリッドビュー</param>
        private void GetDataGridView(out DataGridView dgv)
        {
            // 初期化
            dgv = new DataGridView();

            // 前回値を保存
            var cols = this.form.Ichiran.Columns;
            foreach (DataGridViewColumn c in cols)
            {
                DataGridViewColumn addColumn = (DataGridViewColumn)c.Clone();
                if (c.ValueType != null)
                {
                    dgv.Columns.Add(addColumn.Name, addColumn.HeaderText);
                }
                else
                {
                    dgv.Columns.Add(addColumn);
                }
            }

            foreach (DataGridViewRow row in this.form.Ichiran.Rows)
            {
                DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    clonedRow.Cells[i].Value = row.Cells[i].Value;
                }
                dgv.Rows.Add(clonedRow);
            }
        }

        /// <summary>
        /// 指定されたDataTableを元に画面 - 明細を作成します
        /// </summary>
        /// <param name="dt">明細用DataTable</param>
        private void CreateIchiranRowData(DataTable dt)
        {
            this.IchiranClear();

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.form.Ichiran.Rows.Add();
                this.form.Ichiran.Rows[i].Cells["chb_delete"].Value = Convert.ToBoolean(dt.Rows[i]["DELETE_FLG"]);
                this.form.Ichiran.Rows[i].Cells["SHAIN_CD"].Value = dt.Rows[i]["SHAIN_CD"] == null ? null : dt.Rows[i]["SHAIN_CD"].ToString();
                this.form.Ichiran.Rows[i].Cells["SHAIN_NAME"].Value = dt.Rows[i]["SHAIN_NAME"] == null ? null : dt.Rows[i]["SHAIN_NAME"].ToString();
                this.form.Ichiran.Rows[i].Cells["UPDATE_USER"].Value = dt.Rows[i]["UPDATE_USER"] == null ? null : dt.Rows[i]["UPDATE_USER"].ToString();
                this.form.Ichiran.Rows[i].Cells["UPDATE_DATE"].Value = dt.Rows[i]["UPDATE_DATE"] == null ? null : dt.Rows[i]["UPDATE_DATE"].ToString();
                this.form.Ichiran.Rows[i].Cells["UPDATE_PC"].Value = dt.Rows[i]["UPDATE_PC"] == null ? null : dt.Rows[i]["UPDATE_PC"].ToString();
                this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value = dt.Rows[i]["CREATE_USER"] == null ? null : dt.Rows[i]["CREATE_USER"].ToString();
                this.form.Ichiran.Rows[i].Cells["CREATE_DATE"].Value = dt.Rows[i]["CREATE_DATE"] == null ? null : dt.Rows[i]["CREATE_DATE"].ToString();
                this.form.Ichiran.Rows[i].Cells["CREATE_PC"].Value = dt.Rows[i]["CREATE_PC"] == null ? null : dt.Rows[i]["CREATE_PC"].ToString();
                this.form.Ichiran.Rows[i].Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO"].Value = dt.Rows[i]["DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO"] == null ? null : dt.Rows[i]["DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO"].ToString();
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        internal void Regist()
        {
            bool ret = true;
            try
            {
                /* DeleteInsertを行うため、全行削除した後に削除対象外のデータを改めてInsertしなおす */

                // 削除対象Entityリスト作成
                var delList = this.CreateDeleteList();
                // 登録対象Entityリスト作成
                var regList = this.CreateRegistList(false);

                if (regList.Count <= 0)
                {
                    // 登録対象データなし
                    this.msgLogic.MessageBoxShow("E061");
                }
                else
                {
                    using (Transaction tran = new Transaction())
                    {
                        foreach (var delData in delList)
                        {
                            // 物理削除
                            this.denshiKeiyakuShanaiKeiroDao.Delete(delData.entity);
                        }

                        // 登録実行
                        foreach (var regData in regList)
                        {
                            // 新規登録
                            this.denshiKeiyakuShanaiKeiroDao.Insert(regData.entity);
                        }

                        // コミット
                        tran.Commit();
                    }

                    // 登録完了
                    this.msgLogic.MessageBoxShow("I001", "登録");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //SQL登録異常
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E245");
                }
                ret = false;
            }

            if (ret)
            {
                // 再検索
                this.Search();
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        internal void Cancel()
        {
            // 初期値読込
            this.InitCondition();
            this.IchiranClear();
            this.GetPropertiesSettings();
        }

        #endregion FunctionKeyイベント

        #region Utility

        /// <summary>
        /// 検索条件の取得
        /// </summary>
        /// <returns name="M_DENSHI_KEIYAKU_SHANAI_KEIRO">検索条件用DTO</returns>
        private DenshiKeiyakuShanaiKeiroFindDto GetSearchCondition()
        {
            var dto = new DenshiKeiyakuShanaiKeiroFindDto();

            // 社内経路名CD
            dto.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = Int16.Parse(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text);

            return dto;
        }

        /// <summary>
        /// IMEモードのセット
        /// </summary>
        internal void SetConditionValueImeMode()
        {
            switch (this.form.CONDITION_VALUE.DBFieldsName)
            {
                case "TEKIYOU_BEGIN":
                case "TEKIYOU_END":
                case "DELETE_FLG":
                case "SHAIN_CD":
                case "CREATE_DATE":
                case "UPDATE_DATE":
                    // IME 無効
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Disable;
                    break;

                default:
                    // IME ON
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.On;
                    break;
            }
        }

        /// <summary>
        /// デフォルト設定値の取得
        /// </summary>
        private void GetPropertiesSettings()
        {
            // 検索条件読込
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text = Properties.Settings.Default.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD;
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = Properties.Settings.Default.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME;
        }

        /// <summary>
        /// デフォルト設定値の保存
        /// </summary>
        internal void SetPropertiesSettings()
        {
            // 検索条件書込
            Properties.Settings.Default.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text;
            Properties.Settings.Default.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME = this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text;

            // 保存
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// オブジェクトをboolに変換します
        /// </summary>
        /// <param name="value">変換対象のオブジェクト</param>
        /// <returns>変換後の値（nullはfalse）</returns>
        private bool ConvertToBool(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = false;

            if (null != value)
            {
                if (false == bool.TryParse(value.ToString(), out ret))
                {
                    if (value.Equals(1))
                    {
                        ret = true;
                    }
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 削除用Entityのリストを作成
        /// </summary>
        /// <returns name="List<UIConstans.ST_DATA_LIST>">削除対象Entityのリスト</returns>
        /// <remarks>該当データがない場合は空Listを返却する</remarks>
        private List<UIConstans.ST_DATA_LIST> CreateDeleteList()
        {
            var delList = new List<UIConstans.ST_DATA_LIST>();
            if (this.form.Ichiran.RowCount > 1)
            {
                // 全行一旦削除するため、全データ取得
                var findEntity = new M_DENSHI_KEIYAKU_SHANAI_KEIRO();
                findEntity.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = SqlInt16.Parse(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text);
                var entitys = this.denshiKeiyakuShanaiKeiroDao.GetAllValidData(findEntity);
                if ((entitys != null) && entitys.Length > 0)
                {
                    for (int i = 0; i < entitys.Length; i++)
                    {
                        var delData = new UIConstans.ST_DATA_LIST();
                        delData.existFlag = true;
                        delData.entity = entitys[i];
                        delList.Add(delData);
                    }
                }
            }
            return delList;
        }

        /// <summary>
        /// 検索結果から値が変わった行を取得します
        /// </summary>
        /// <returns></returns>
        private DataGridView GetChangeRows()
        {
            // 作業用に現在のDGVの複製を作成
            DataGridView dgv = new DataGridView();
            this.GetDataGridView(out dgv);
            int removeRowIndex = 0;
            bool isValueChanged = false;
            var newValue = string.Empty;
            var oldValue = string.Empty;

            // 検索時のDataGridView と比較
            for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
            {
                // 最終行は何もしない
                if (i == this.form.Ichiran.RowCount - 1) continue;

                if (i < this.DGVSearchResult.Rows.Count)
                {
                    for (int j = 1; j < this.form.Ichiran.Columns.Count; j++)
                    {
                        newValue = this.form.Ichiran.Rows[i].Cells[j].Value == null ? string.Empty : this.form.Ichiran.Rows[i].Cells[j].Value.ToString();
                        oldValue = this.DGVSearchResult.Rows[i].Cells[j].Value == null ? string.Empty : this.DGVSearchResult.Rows[i].Cells[j].Value.ToString();

                        if (newValue != oldValue)
                        {
                            isValueChanged = true;
                        }
                    }
                }

                // 変更が無い行を削除
                if (!isValueChanged)
                {
                    dgv.Rows.RemoveAt(0 + removeRowIndex);
                }
                else
                {
                    removeRowIndex++;
                }

                isValueChanged = false;
            }

            return dgv;
        }

        /// <summary>
        /// 登録用Entityのリストを作成
        /// </summary>
        /// <param name="isDeleteAction">削除処理から呼び出しかのフラグ</param>
        /// <returns>登録対象Entityのリスト</returns>
        /// <remarks>該当データがない場合は空Listを返却する</remarks>
        private List<UIConstans.ST_DATA_LIST> CreateRegistList(bool isDeleteAction)
        {
            var regList = new List<UIConstans.ST_DATA_LIST>();
            if (this.form.Ichiran.RowCount > 1)
            {
                // ROW_NO降りなおしのため1から採番
                int maxRowNo = 1;

                // 変更のあった行を取得
                DataGridView dgv = new DataGridView();
                dgv = GetChangeRows();

                // 空白行含む項目が存在する場合、一覧より登録用Entityを作成
                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    /* チェック */
                    if (row.Index == this.form.Ichiran.RowCount - 1)
                    {
                        // 最終行は何もしない
                        continue;
                    }

                    if ((row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO"].Value == null || string.IsNullOrEmpty(row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO"].Value.ToString()))
                        && isDeleteAction)
                    {
                        // 削除処理時、新規行は破棄する
                        continue;
                    }

                    if (this.ConvertToBool(row.Cells["chb_delete"].Value))
                    {
                        // 削除処理時、削除チェックボックスONのものは登録しない
                        continue;
                    }

                    /* 登録データ作成 */
                    var regEntity = new M_DENSHI_KEIYAKU_SHANAI_KEIRO();
                    var regData = new UIConstans.ST_DATA_LIST();

                    regEntity.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = SqlInt16.Parse(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text);
                    regEntity.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO = maxRowNo;
                    regEntity.SHAIN_CD = row.Cells["SHAIN_CD"].Value.ToString();
                    regEntity.DELETE_FLG = false;

                    // ROW_NOの有無から登録済みのデータかチェック
                    if (row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO"].Value == null || string.IsNullOrEmpty(row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO"].Value.ToString()))
                    {
                        // 未登録の場合は新規登録のため作成・更新情報をセット
                        var dataBinderLogic = new DataBinderLogic<M_DENSHI_KEIYAKU_SHANAI_KEIRO>(regEntity);
                        dataBinderLogic.SetSystemProperty(regEntity, false);
                        regData.insertFlag = true;
                    }
                    else
                    {
                        // 登録済み場合、作成情報は引き継ぐ
                        regEntity.CREATE_USER = row.Cells["CREATE_USER"].Value.ToString();
                        regEntity.CREATE_DATE = SqlDateTime.Parse(row.Cells["CREATE_DATE"].Value.ToString());
                        regEntity.CREATE_PC = row.Cells["CREATE_PC"].Value.ToString();

                        // 更新情報を引き継ぐ
                        // (値に変更がある場合は下記のロジックで更新情報を上書き)
                        regEntity.UPDATE_DATE = SqlDateTime.Parse(row.Cells["UPDATE_DATE"].Value.ToString());
                        regEntity.UPDATE_USER = row.Cells["UPDATE_USER"].Value.ToString();
                        regEntity.UPDATE_PC = row.Cells["UPDATE_PC"].Value.ToString();

                        // 値に変更があった行は更新情報を更新する
                        foreach (DataGridViewRow r in dgv.Rows)
                        {
                            // 最終行は何もしない
                            if (r.Index == dgv.RowCount - 1) continue;

                            if (row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO"].Value == r.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO"].Value)
                            {
                                // 更新情報を更新
                                regEntity.UPDATE_DATE = SqlDateTime.Parse(this.GetDBDateTime().ToString());
                                regEntity.UPDATE_USER = SystemProperty.UserName;
                                regEntity.UPDATE_PC = SystemInformation.ComputerName;
                                break;
                            }
                        }

                        regData.insertFlag = true;
                    }

                    // リストに追加
                    regData.entity = regEntity;
                    regList.Add(regData);

                    maxRowNo++;
                }
            }

            return regList;
        }

        /// <summary>
        /// 一覧クリア処理
        /// </summary>
        internal void IchiranClear()
        {
            // 一覧をクリア
            this.form.Ichiran.Rows.Clear();
        }

        /// <summary>
        /// 一覧表示用データ作成
        /// </summary>
        /// <returns name="DataTable">一覧表示用データ</returns>
        private DataTable CreateIchiranData()
        {
            var table = new DataTable();

            // 検索条件に基づいたマスタ情報を取得
            table = this.dao.GetIchiranDataSql(this.GetSearchCondition());

            foreach (DataColumn column in table.Columns)
            {
                // NOT NULL制約を一時的に解除(新規追加行対策)
                column.AllowDBNull = true;
            }

            return table;
        }

        #endregion Utility

        #region CELLのEnterイベント

        /// <summary>
        /// 一覧 - CELL_ENTER_EVENT
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        internal void CellEnter(int rowIndex, int columnIndex)
        {
            string cellName = this.form.Ichiran.Columns[columnIndex].Name;
            var row = this.form.Ichiran.Rows[rowIndex];

            if (string.IsNullOrEmpty(cellName) || row == null)
            {
                return;
            }

            // IME制御
            switch (cellName)
            {
                case "chb_delete":
                case "SHAIN_CD":
                    this.form.Ichiran.ImeMode = ImeMode.Disable;
                    break;

                default:
                    this.form.Ichiran.ImeMode = ImeMode.NoControl;
                    break;
            }

            // 前回値保存
            if (!this.hasErrorInput) // エラー中は保存対象外
            {
                switch (cellName)
                {
                    case "SHAIN_CD":
                        string value = Convert.ToString(this.form.Ichiran.Rows[rowIndex].Cells[cellName].Value);
                        SavePreValueDic(cellName, value);

                        // 名称も保存
                        string nameValue = Convert.ToString(this.form.Ichiran.Rows[rowIndex].Cells["SHAIN_NAME"].Value);
                        SavePreValueDic("SHAIN_NAME", nameValue);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region 前回入力値保存

        /// <summary>
        /// 前回入力値保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SavePreValueDic(string key, string value)
        {
            if (this.preValueDic.ContainsKey(key))
            {
                this.preValueDic[key] = value;
            }
            else
            {
                this.preValueDic.Add(key, value);
            }
        }

        #endregion

        #region CELLのValidatingイベント

        /// <summary>
        /// 一覧 - CELL_VALIDATING_EVENT
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns>true:エラー有, false:エラー無</returns>
        internal bool HasErrorCellValidating(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0)
            {
                return false;
            }

            string columnName = this.form.Ichiran.Columns[columnIndex].Name;
            if (columnName.Equals("SHAIN_CD"))
            {
                string shainCd = Convert.ToString(this.form.Ichiran.Rows[rowIndex].Cells[columnName].Value);
                // 前回値と比較
                if (shainCd.Equals(this.preValueDic[columnName]))
                {
                    this.hasErrorInput = false;
                    this.form.Ichiran.Rows[rowIndex].Cells["SHAIN_NAME"].Value = this.preValueDic["SHAIN_NAME"];
                    return false;
                }

                // 名称初期化
                this.form.Ichiran.Rows[rowIndex].Cells["SHAIN_NAME"].Value = string.Empty;

                if (shainCd == string.Empty)
                {
                    this.hasErrorInput = false;
                    return false;
                }
                else
                {
                    shainCd = shainCd.ToUpper();
                    this.form.Ichiran.Rows[rowIndex].Cells[columnName].Value = shainCd;

                    var shain = DaoInitUtility.GetComponent<IM_SHAINDao>().GetDataByCd(shainCd);

                    if (shain == null || shain.DELETE_FLG.IsTrue || string.IsNullOrEmpty(shain.MAIL_ADDRESS))
                    {
                        this.hasErrorInput = true;
                        this.msgLogic.MessageBoxShow("E020", "社員マスタ");
                        return true;
                    }
                    this.hasErrorInput = false;
                    this.form.Ichiran.Rows[rowIndex].Cells["SHAIN_NAME"].Value = shain.SHAIN_NAME_RYAKU;
                }
            }
            return false;
        }

        #endregion

        #region 社員検索用DataSourceセット

        /// <summary>
        /// 社員検索ポップアップ用のDataSourceをセットする
        /// </summary>
        internal bool SetShainPopupProperty()
        {
            try
            {
                var shainCd = this.form.Ichiran.CurrentRow.Cells["SHAIN_CD"] as DgvCustomTextBoxCell;
                if (shainCd != null)
                {
                    shainCd.PopupWindowId = WINDOW_ID.M_SHAIN;
                    shainCd.PopupWindowName = "マスタ共通ポップアップ";
                    shainCd.PopupGetMasterField = "SHAIN_CD, SHAIN_NAME";
                    shainCd.PopupSetFormField = string.Format("{0}, {1}", "SHAIN_CD", "SHAIN_NAME");
                    shainCd.PopupDataHeaderTitle = new string[] { "社員CD", "社員名" };
                    shainCd.PopupDataSource = this.CreateShianDataSource(shainCd.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()).ToArray());
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetShainPopupProperty", ex1);
                this.msgLogic.MessageBoxShow("E093");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShainPopupProperty", ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 社員検索用DataSourceを生成する
        /// </summary>
        /// <param name="dispColumn">表示カラム</param>
        /// <returns></returns>
        private DataTable CreateShianDataSource(string[] dispColumn)
        {
            var returnVal = new DataTable();

            var allShainData = DaoInitUtility.GetComponent<IM_SHAINDao>().GetAllValidData(new M_SHAIN());

            // メールアドレス未入力の社員マスタは除外
            allShainData = allShainData.Where(n => !string.IsNullOrEmpty(n.MAIL_ADDRESS)).ToArray();

            var dt = EntityUtility.EntityToDataTable(allShainData.ToArray());

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (var col in dispColumn)
                {
                    // 表示対象の列だけを順番に追加
                    returnVal.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
                }

                foreach (DataRow r in dt.Rows)
                {
                    returnVal.Rows.Add(returnVal.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }
            }

            return returnVal;
        }

        #endregion

        #region 社員CDチェック

        /// <summary>
        /// 社員CDチェック
        /// メッセージついてはこのメソッド内で表示する。
        /// </summary>
        /// <param name="shainCd"></param>
        /// <returns>true:正常な社員CD、false:不正な社員CD</returns>
        private bool CheckShainCd(string shainCd)
        {
            bool returnVal = true;

            if (string.IsNullOrEmpty(shainCd))
            {
                return returnVal;
            }

            var shainData = DaoInitUtility.GetComponent<IM_SHAINDao>().GetDataByCd(shainCd);

            // DELETE_FLGや適用日についてはFWのフォーカスアウトチェックに任せるため
            // それ以外のチェックを実装
            if (string.IsNullOrWhiteSpace(shainData.LOGIN_ID))
            {
                MessageBoxUtility.MessageBoxShow("E028");
                returnVal = false;
            }

            return returnVal;
        }

        #endregion

        #region ボタン非活性・活性制御

        /// <summary>
        /// [F4]削除、[F5]CSV出力、[F9]登録ボタン非活性、活性制御を行います
        /// </summary>
        /// <param name="isEnabled">True：活性　False：非活性</param>
        internal void SetFunctionButtonEnabled(bool isEnabled)
        {
            this.parentForm.bt_func1.Enabled = isEnabled;
            this.parentForm.bt_func2.Enabled = isEnabled;
            this.parentForm.bt_func4.Enabled = isEnabled;
            this.parentForm.bt_func6.Enabled = isEnabled;
            this.parentForm.bt_func9.Enabled = isEnabled;
        }

        #endregion

        #region 一覧操作可能プロパティ制御

        /// <summary>
        /// 明細の新規行が追加可能かを制御します。
        /// </summary>
        /// <param name="val">True：操作可　False：操作不可</param>
        internal void SetIchiranEnavled(bool val)
        {
            this.form.Ichiran.Enabled = val;

            // 仮対応
            // 現状、TabStopが有効なコントロールが経路CDしかないため直接入力時にフォーカス移動での名称表示が出来ない。
            // これを回避するために、経路名にフォーカス移動できるようにする。
            // 将来的にはAllowUserToAddRowsの制御に変更するのでここを削除する。
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.TabStop = !val;

            this.form.Ichiran.AllowUserToAddRows = val;//thongh 2015/12/28 #1982
        }

        #endregion

        #region Interface

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

        #endregion Interface

        private DateTime GetDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
    }
}