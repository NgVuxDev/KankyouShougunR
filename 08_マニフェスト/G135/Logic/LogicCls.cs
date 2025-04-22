// $Id: LogicCls.cs 24197 2014-06-27 12:04:15Z takeda $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.PaperManifest.JissekiHokokuIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {

        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.JissekiHokokuIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 設置コンテナ一覧画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeader headForm;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 実績報告書一覧Dao
        /// </summary>
        public DAOCls dao;

        /// <summary>
        /// 実績報告書一覧Dto
        /// </summary>
        private DTOCls dto;

        private MessageBoxShowLogic MsgBox;
        #endregion

        #region プロパティ
        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOCls SearchString { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// アラート件数
        /// </summary>
        public int AlertCount { get; set; }

        #endregion

        #region 初期化処理

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOCls();
            this.dao = DaoInitUtility.GetComponent<DAOCls>();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }
 
                this.allControl = this.form.allControl;
                this.form.Ichiran.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                // 画面情報を設定する
                this.setLoadPage();

                this.form.Ichiran.AutoGenerateColumns = false;
                this.parentForm.txb_process.Enabled = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
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

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン設定の読込
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

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            //修正ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(bt_func3_Click);

            //削除ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(bt_func4_Click);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);

            //取消ボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(bt_func10_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

        }
        #endregion

        
        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 実績報告書種類 「1.処分実績」「2.処理施設実績」項目を非表示
            this.headForm.rdoSyobunJiseiki.Visible = false;
            this.headForm.rdoSisetsuJiseiki.Visible = false;

            // Location調整
            this.headForm.rdoUnbanJiseiki.Location = new System.Drawing.Point(25, 1);
            this.headForm.customPanel8.Size = new System.Drawing.Size(120, 20);

            // 制御変更
            this.headForm.txtHokokuSyurui.CharacterLimitList = new char[] { '3' };
        }

        /// <summary>
        /// マニライト用にLocation調整
        /// </summary>
        /// <param name="ctrl"></param>
        private void LocationAdjustmentForManiLite(Control ctrl)
        {
            ctrl.Location = new System.Drawing.Point(ctrl.Location.X, ctrl.Location.Y - 23);
        }


        #region 初期画面設定
        /// <summary>
        /// 初期画面設定
        /// </summary>
        private void setLoadPage()
        {
            if (AppConfig.IsManiLite)
            {
                // マニライト版(C8)の場合は、「3.運搬実績のみ」
                this.headForm.txtHokokuSyurui.Text = "3";
            }
            else
            {
                this.headForm.txtHokokuSyurui.Text = "1";
            }
            this.form.txtHokokuNento.Text = "1";

            this.form.cantxt_Nento.Text = this.parentForm.sysDate.AddYears(-1).Year.ToString();
            this.headForm.txtHokokuSyurui.Focus();
        }
        #endregion

        #endregion

        #region 業務処理

        #region 修正
        /// <summary>
        /// 「F3 修正ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                /// 20141027 Houkakou 「実績報告書一覧」のクリック イベントを追加する　start
                if (this.form.Ichiran.SelectedRows.Count > 0)
                {
                    string reportkbn = this.form.Ichiran.SelectedRows[0].Cells["REPORT_KBN"].Value.ToString();
                    string systemid = this.form.Ichiran.SelectedRows[0].Cells["SYSTEM_ID"].Value.ToString();

                    DataTable dt = dao.GetDeleteJissekiHokokuData(systemid);
                    if (dt.Rows.Count <= 0)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E045");
                        return;
                    }
                    if (reportkbn.Equals("1"))
                    {
                        r_framework.FormManager.FormManager.OpenForm("G136", WINDOW_TYPE.UPDATE_WINDOW_FLAG, systemid); 
                    }
                    else if (reportkbn.Equals("2"))
                    {
                        r_framework.FormManager.FormManager.OpenForm("G604", WINDOW_TYPE.UPDATE_WINDOW_FLAG, systemid);
                    }
                    else if (reportkbn.Equals("3"))
                    {
                        r_framework.FormManager.FormManager.OpenForm("G607", WINDOW_TYPE.UPDATE_WINDOW_FLAG, systemid);
                    }
                }
                /// 20141027 Houkakou 「実績報告書一覧」のクリック イベントを追加する　end
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func3_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region 削除
        /// <summary>
        /// 「F4 削除ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                /// 20141027 Houkakou 「実績報告書一覧」のクリック イベントを追加する　start
                if (this.form.Ichiran.SelectedRows.Count > 0)
                {
                    string reportkbn = this.form.Ichiran.SelectedRows[0].Cells["REPORT_KBN"].Value.ToString();
                    string systemid = this.form.Ichiran.SelectedRows[0].Cells["SYSTEM_ID"].Value.ToString();

                    DataTable dt = dao.GetDeleteJissekiHokokuData(systemid);
                    if (dt.Rows.Count <= 0)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E045");
                        return;
                    }

                    if (reportkbn.Equals("1"))
                    {
                        r_framework.FormManager.FormManager.OpenForm("G136", WINDOW_TYPE.DELETE_WINDOW_FLAG, systemid);
                    }
                    else if (reportkbn.Equals("2"))
                    {
                        r_framework.FormManager.FormManager.OpenForm("G604", WINDOW_TYPE.DELETE_WINDOW_FLAG, systemid);
                    }
                    else if (reportkbn.Equals("3"))
                    {
                        r_framework.FormManager.FormManager.OpenForm("G607", WINDOW_TYPE.DELETE_WINDOW_FLAG, systemid);
                    }
                }
                /// 20141027 Houkakou 「実績報告書一覧」のクリック イベントを追加する　end
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func4_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region 検索条件初期化
        /// <summary>
        /// 「F7 条件クリアボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 検索条件クリア
                this.form.customSortHeader1.ClearCustomSortSetting();
                this.headForm.txtHokokuSyurui.Text = "1";
                this.form.txtHokokuNento.Text = "1";
                this.form.cantxt_Nento.Text = this.parentForm.sysDate.AddYears(-1).Year.ToString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func7_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region 検索
        // 検索結果のカラム名
        private string[] columnNames = { "HOKOKU_NENDO", "HOZON_NAME", "HOKOKU_SYOSHIKI", "HOKOKU_SYOSHIKI_NAME", "TEISHUTSU_CHIIKI_CD", "TEISHUTSU_NAME"
                                       , "GOV_OR_MAY_NAME", "TOKUBETSU_KANRI_KBN", "TOKUBETSU_KANRI_SYURUI", "GYOUSHA_KBN", "GYOUSHA_KBN_NAME", "KEN_KBN", "KEN_KBN_NAME", "CREATE_DATE", "CREATE_USER"};
        // 検索結果のタイプ名(検索結果のカラム名に対応)
        private string[] columnTyepes = { "System.String","System.String","System.Int16","System.String","System.String","System.String"
                                        ,"System.String","System.String","System.Int16","System.String","System.Int16","System.String","System.Int16","System.String","System.String","System.String"};

        /// <summary>
        /// 「F8 検索ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 検索条件を設定する
                SetSearchString();

                this.SearchResult = this.dao.GetJissekiHokokuData(this.SearchString);

                // 検索結果を画面に設定する
                int count = this.SearchResult.Rows.Count;
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    //明細をクリア
                    this.form.Ichiran.DataSource = null;

                    this.headForm.txtHokokuSyurui.Focus();
                    return;
                }
                else
                {
                    // 検索結果を表示する
                    this.ShowData(this.SearchResult);
                }

                // フォーカス設定
                this.headForm.txtHokokuSyurui.Focus();
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

        #region 並び替え
        /// <summary>
        /// 「F10 並び替えボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //一覧に明細行がない場合
                if (this.form.Ichiran.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E076");
                }
                else
                {
                    //ソート設定ダイアログを呼び出す
                    this.form.customSortHeader1.ShowCustomSortSettingDialog();
                }

                // フォーカス設定
                this.headForm.txtHokokuSyurui.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func10_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region 閉じる
        /// <summary>
        /// 「F12 閉じるボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }
        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public void SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();
                DTOCls searchCondition = new DTOCls();

                searchCondition.REPORT_KBN = Convert.ToInt16(this.headForm.txtHokokuSyurui.Text);

                searchCondition.YEAR_KBN = Convert.ToInt16(this.form.txtHokokuNento.Text);

                searchCondition.HOKOKU_NENDO = string.Format("{0}年度", this.form.cantxt_Nento.Text);

                this.SearchString = searchCondition;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSearchString", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region header設定
        /// <summary>
        /// header設定
        /// </summary>
        public void SetHeader(UIHeader headForm)
        {
            try
            {
                LogUtility.DebugMethodStart(headForm);
                this.headForm = headForm;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(headForm);
            }
        }
        #endregion

        #region 検索結果表示処理
        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData(DataTable searchResult)
        {
            try
            {
                LogUtility.DebugMethodStart(searchResult);

                DataTable dt = searchResult;

                // DataGridViewに値の設定を行う
                this.CreateDataGridView(dt);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(searchResult);
            }
        }
        #endregion

        #region DataGridViewに値の設定
        /// <summary>
        /// DataGridViewに値の設定を行う
        /// </summary>
        /// <param name="table"></param>
        public void CreateDataGridView(DataTable table)
        {
            try
            {
                LogUtility.DebugMethodStart(table);
                DialogResult result = DialogResult.Yes;

                if (this.AlertCount != 0 && this.AlertCount < table.Rows.Count)
                {
                    MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                    result = showLogic.MessageBoxShow("C025");
                }
                if (result == DialogResult.Yes)
                {
                    this.form.customSortHeader1.SortDataTable(table);

                    this.form.Ichiran.IsBrowsePurpose = false;
                    this.form.Ichiran.DataSource = table;
                    foreach (DataGridViewColumn column in this.form.Ichiran.Columns)
                    {
                        column.ReadOnly = true;
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    this.form.Ichiran.IsBrowsePurpose = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateDataGridView", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(table);
            }
        }
        #endregion

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

        #region 自動生成（実装なし）
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
    }
}
