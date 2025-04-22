// $Id: LogicCls.cs 50629 2015-05-26 04:38:10Z wuq@oec-h.com $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Master.ZaikoHiritsuHoshu.APP;
using Shougun.Core.Master.ZaikoHiritsuHoshu.DAO;
using Shougun.Core.Master.ZaikoHiritsuHoshu.DTO;

namespace Shougun.Core.Master.ZaikoHiritsuHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.ZaikoHiritsuHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public MasterBaseForm parentForm;

        /// <summary>
        /// 品名dao
        /// </summary>
        private IM_HINMEIDao HinmeiDao;

        /// <summary>
        /// 在庫比率dao
        /// </summary>
        private DaoCls ZaikoHiritsuHoshuDao;

        /// <summary>
        /// 在庫品名dao
        /// </summary>
        private ZaikoHinmeiDao ZaikoHinmeiDao;

        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// メッセージ出力用のユーティリティ
        /// </summary>
        private MessageUtility MessageUtil;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果Copy
        /// </summary>
        public DataTable SearchResultCopy { get; set; }

        /// <summary>
        /// 在庫品名
        /// </summary>
        public DataTable SearchZaikoHinmei { get; set; }

        /// <summary>
        /// テーブルクリア用
        /// </summary>
        public DataTable ClearTable { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        private M_ZAIKO_HIRITSU SearchStringHiritsu { get; set; }

        private string ZaikoHinmei = "";
        private MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;

                this.HinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
                this.ZaikoHiritsuHoshuDao = DaoInitUtility.GetComponent<DaoCls>();
                this.ZaikoHinmeiDao = DaoInitUtility.GetComponent<ZaikoHinmeiDao>();
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                // メッセージ出力用のユーティリティ
                MessageUtil = new MessageUtility();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                this.parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //PopUpDataInit();
                //画面初期値設定
                this.DefaultInit();

                //前の表示条件を初期値に設定する
                if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED)
                {
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M245", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }

                this.form.Ichiran.AllowUserToAddRows = false;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return true;
        }

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

            this.form.Ichiran.CausesValidation = false;

            // FunctionButton
            this.parentForm.bt_func4.Enabled = false;
            this.parentForm.bt_func6.Enabled = true;
            this.parentForm.bt_func9.Enabled = false;
        }

        #endregion

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();

                ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, this.form.WindowType);
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
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();

                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 削除ボタン(F4)イベント生成
                this.form.C_MasterRegist(this.parentForm.bt_func4);
                this.parentForm.bt_func4.Click -= this.form.LogicalDelete;
                this.parentForm.bt_func4.Click += this.form.LogicalDelete;
                this.parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;
                this.parentForm.bt_func4.Enabled = false;

                //CSV出力ボタン(F6)イベント生成
                this.parentForm.bt_func6.Click -= this.form.CSVOutput;
                this.parentForm.bt_func6.Click += this.form.CSVOutput;
                this.parentForm.bt_func6.Enabled = false;

                //条件クリアボタン(F7)イベント生成
                this.parentForm.bt_func7.Click -= this.form.ClearCondition;
                this.parentForm.bt_func7.Click += this.form.ClearCondition;

                //検索ボタン(F8)イベント生成
                this.parentForm.bt_func8.Click -= this.form.Search;
                this.parentForm.bt_func8.Click += this.form.Search;

                //登録ボタン(F9)イベント生成
                this.form.C_MasterRegist(this.parentForm.bt_func9);
                this.parentForm.bt_func9.Click -= this.form.Regist;
                this.parentForm.bt_func9.Click += this.form.Regist;
                this.parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;
                this.parentForm.bt_func9.Enabled = false;

                //取消ボタン(F11)イベント生成
                this.parentForm.bt_func11.Click -= this.form.Cancel;
                this.parentForm.bt_func11.Click += this.form.Cancel;

                //閉じるボタン(F12)イベント生成
                this.parentForm.bt_func12.Click -= this.form.FormClose;
                this.parentForm.bt_func12.Click += this.form.FormClose;
                //this.parentForm.bt_func12.CausesValidation = false;

                this.parentForm.bt_process1.Click -= this.form.process1Click;
                this.parentForm.bt_process1.Click += this.form.process1Click;
                if (this.form.DENSHU_KBN_CD.Text == "1")
                {
                    this.parentForm.bt_process1.Text = "[1]出荷";
                }
                else
                {
                    this.parentForm.bt_process1.Text = "[1]受入";
                }
                this.parentForm.bt_process2.Enabled = false;
                this.parentForm.bt_process3.Enabled = false;
                this.parentForm.bt_process4.Enabled = false;
                this.parentForm.bt_process5.Enabled = false;
                this.parentForm.txb_process.Enabled = true;
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

        #endregion

        #region Func処理

        /// <summary>
        /// CSV出力
        /// </summary>
        public bool CSVOutput()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                if (this.form.Ichiran.RowCount <= 1)
                {
                    msgLogic.MessageBoxShow("E044");
                }
                else if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_ZAIKO_HIRITSU), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 画面データ検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();
                if (this.form.HINMEI_CD.Text.ToString() == string.Empty)
                {
                    return 0;
                }

                this.form.Ichiran.AllowUserToAddRows = true;

                if (!SetSearchString())
                {
                    this.SearchResultCopy.Rows.Clear();
                    if (!SetIchiranData())
                    {
                        LogUtility.DebugMethodEnd(-1);
                        return -1;
                    }
                    return 0;
                }
                this.SearchResult = this.ZaikoHiritsuHoshuDao.GetIchiranDataSqlFile(
                    this.SearchStringHiritsu,
                    ZaikoHinmei,
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                    );

                this.SearchResultCopy = this.SearchResult.Copy();
                foreach (DataGridViewColumn DGVcolumn in this.form.Ichiran.Columns)
                {
                    switch (DGVcolumn.Name)
                    {
                        case "DELETE_FLG":
                        case "ZAIKO_HINMEI_CD":
                        case "ZAIKO_HIRITSU":
                        case "BIKOU":
                            DGVcolumn.ReadOnly = false;
                            break;
                    }
                }
                if (!SetIchiranData())
                {
                    LogUtility.DebugMethodEnd(-1);
                    return -1;
                }
                count = this.SearchResult.Rows == null ? 0 : 1;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }
            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            bool ret = true;
            // 重複エラー時用のメッセージで使用
            string dispZaikoHinmeiCd = string.Empty;

            try
            {
                if (!CheckBeforeUpdate())
                {
                    //入力チェック失敗、処理を終了する。
                    return;
                }
                bool catchErr = true;
                List<DataDto> entitys = GetIchiranList(out catchErr);
                if (!catchErr)
                {
                    errorFlag = true;
                    ret = true;
                }
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        // トランザクション開始
                        foreach (DataDto data in entitys)
                        {
                            dispZaikoHinmeiCd = string.Empty;

                            DataTable entity = this.ZaikoHiritsuHoshuDao.GetDataByPKSqlFile(data.updateKey);

                            if (entity == null || entity.Rows.Count == 0)
                            {
                                this.ZaikoHiritsuHoshuDao.Insert(data.entity);
                            }
                            else
                            {
                                dispZaikoHinmeiCd = data.entity.ZAIKO_HINMEI_CD;
                                this.ZaikoHiritsuHoshuDao.UpdateBySqlFile(data.entity, data.updateKey);
                            }
                        }
                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラーの場合
                    LogUtility.Warn(ex); //排他は警告
                    this.form.errmessage.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //登録はエラー
                    var tempEx = ((SQLRuntimeException)ex).Args[0] as SqlException;
                    if (!string.IsNullOrEmpty(dispZaikoHinmeiCd)
                        && (tempEx != null && tempEx.Number == Constans.SQL_EXCEPTION_NUMBER_DUPLICATE))
                    {
                        this.form.errmessage.MessageBoxShow("E259", "比率または備考", "・在庫品名CD：" + dispZaikoHinmeiCd);
                    }
                    else
                    {
                        this.form.errmessage.MessageBoxShow("E093");
                    }
                }
                else
                {
                    //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                ret = false;
            }

            if (ret)
            {
                msgLogic.MessageBoxShow("I001", "登録");
                this.Search();
                //setDefaultCondition();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                CancelInit();
                if (this.Search() == -1)
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// クリア
        /// </summary>
        public void ClearCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();

                this.form.HINMEI_CD.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region データ設定

        /// <summary>
        ///  画面初期値設定
        /// </summary>
        private void DefaultInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //[表示条件]システム設定(M_SYS_INFO)より値を取得、初期値としてセット。
                GetSysInfoInit();

                //品名CD、品名を設定
                this.form.HINMEI_CD.Text = Properties.Settings.Default.HINMEI_CD;
                this.form.HINMEI_NAME_RYAKU.Text = Properties.Settings.Default.HINMEI_NAME_RYAKU;

                //受入/出荷を設定
                if (Properties.Settings.Default.DENSHU_KBN_CD != null && Properties.Settings.Default.DENSHU_KBN_CD != "")
                {
                    this.form.DENSHU_KBN_CD.Text = Properties.Settings.Default.DENSHU_KBN_CD;
                    if (this.form.DENSHU_KBN_CD.Text.ToString().Equals("1"))
                    {
                        this.form.radioButton1.Checked = true;
                        this.HeaderInit(1);
                    }
                    else if (this.form.DENSHU_KBN_CD.Text.ToString().Equals("2"))
                    {
                        this.form.radioButton2.Checked = true;
                        this.HeaderInit(2);
                    }
                }
                else
                {
                    this.form.DENSHU_KBN_CD.Text = "1";
                    this.form.radioButton1.Checked = true;
                    this.HeaderInit(1);
                }

                //明細部クリア
                this.CreateNoDataRecord();
            }
            catch (Exception ex)
            {
                LogUtility.Error("DefaultInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        ///  ヘットタイトル設定
        /// </summary>
        /// <param name="type">1、受入　2、出荷</param>
        public void HeaderInit(int type)
        {
            ControlUtility controlUtil = new ControlUtility();
            var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");

            if (type == 1)
            {
                titleControl.Text = "在庫比率入力（受入）";

                this.form.DENSHU_KBN_CD.Text = "1";
            }
            else
            {
                titleControl.Text = "在庫比率入力（出荷）";

                this.form.DENSHU_KBN_CD.Text = "2";
            }
            if (this.form.DENSHU_KBN_CD.Text == "1")
            {
                this.parentForm.bt_process1.Text = "[1]出荷";
            }
            else
            {
                this.parentForm.bt_process1.Text = "[1]受入";
            }
        }

        /// <summary>
        ///  取り消し設定
        /// </summary>
        private void CancelInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //[表示条件]システム設定(M_SYS_INFO)より値を取得、初期値としてセット。
                GetSysInfoInit();

                //品名CD、品名を設定
                this.form.HINMEI_CD.Text = string.Empty;
                this.form.HINMEI_NAME_RYAKU.Text = string.Empty;

                //受入/出荷を設定
                this.form.DENSHU_KBN_CD.Text = "1";
                this.form.radioButton1.Checked = true;

                //明細部クリア
                this.CreateNoDataRecord();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 一覧データの検索条件設定
        /// </summary>
        private bool SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();
                M_ZAIKO_HIRITSU entityHiritsu = new M_ZAIKO_HIRITSU();
                this.ZaikoHinmei = string.Empty;

                // 品名CD
                if (!this.form.HINMEI_CD.Text.Equals(null) &&
                    !this.form.HINMEI_CD.Text.ToString().Trim().Equals(string.Empty))
                {
                    entityHiritsu.HINMEI_CD = this.form.HINMEI_CD.Text.ToString().Trim();
                }

                // 受入/出荷
                if (this.form.radioButton1.Checked)
                {
                    entityHiritsu.DENSHU_KBN_CD = 1;
                }
                else if (this.form.radioButton2.Checked)
                {
                    entityHiritsu.DENSHU_KBN_CD = 2;
                }

                this.SearchStringHiritsu = entityHiritsu;
                return true;
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

        /// <summary>
        /// Properties設定
        /// </summary>
        private void setDefaultCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.HINMEI_CD = this.form.HINMEI_CD.Text;
                Properties.Settings.Default.HINMEI_NAME_RYAKU = this.form.HINMEI_NAME_RYAKU.Text;

                Properties.Settings.Default.DENSHU_KBN_CD = this.form.DENSHU_KBN_CD.Text;

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDefaultCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 表示条件の初期値設定
        /// </summary>
        private void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 一覧データの設定
        /// </summary>
        private bool SetIchiranData()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                this.form.Ichiran.CellValidating -= this.form.Ichiran_CellValidating;
                this.form.Ichiran.DataSource = this.SearchResultCopy;
                this.form.Ichiran.CellValidating += this.form.Ichiran_CellValidating;
                this.ColumnAllowDBNull(this.SearchResultCopy);

                // 削除ボタン(F4)イベント生成
                this.parentForm.bt_func4.Enabled = true;
                // CSV出力ボタン(F6)イベント生成
                this.parentForm.bt_func6.Enabled = true;
                // 登録ボタン(F9)イベント生成
                this.parentForm.bt_func9.Enabled = true;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M245", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        ///  画面から一覧データを取得する
        /// </summary>
        private List<DataDto> GetIchiranList(out bool catchErr)
        {
            catchErr = true;
            LogUtility.DebugMethodStart();
            List<DataDto> entitys = new List<DataDto>();
            try
            {
                DataTable dtIchiran = this.form.Ichiran.DataSource as DataTable;
                if (dtIchiran == null || dtIchiran.Rows.Count == 0)
                {
                    return entitys;
                }

                ColumnAllowDBNull(dtIchiran);
                dtIchiran.BeginLoadData();

                //変更したデータを取得
                if (dtIchiran.GetChanges() == null)
                {
                    return entitys;
                }
                else
                {
                    dtIchiran = dtIchiran.GetChanges();
                }

                // 変更分のみ取得
                DataTable addList = new DataTable();
                addList = dtIchiran.Clone();
                for (int i = 0; i < dtIchiran.Rows.Count; i++)
                {
                    if ((!DBNull.Value.Equals(dtIchiran.Rows[i]["ZAIKO_HINMEI_CD"])) &&
                              dtIchiran.Rows[i]["ZAIKO_HINMEI_CD"] != null &&
                              (!"".Equals(dtIchiran.Rows[i]["ZAIKO_HINMEI_CD"])))
                    {
                        if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
                        {
                            DataRow row = addList.NewRow();
                            row.ItemArray = dtIchiran.Rows[i].ItemArray;
                            addList.Rows.Add(row);
                            continue;
                        }

                        //新規データかつ削除フラグ=falseの場合
                        bool deleteFlg = string.IsNullOrEmpty(Convert.ToString(dtIchiran.Rows[i]["DELETE_FLG"])) ? false : Convert.ToBoolean(Convert.ToString(dtIchiran.Rows[i]["DELETE_FLG"]));
                        if (!deleteFlg)
                        {
                            DataRow row = addList.NewRow();
                            row.ItemArray = dtIchiran.Rows[i].ItemArray;
                            addList.Rows.Add(row);
                        }
                    }
                }

                if (addList.Rows.Count > 0)
                {
                    DataDto data = new DataDto();
                    entitys = CreateEntityData(addList, out catchErr);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetIchiranList", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(entitys, catchErr);
            }
            return entitys;
        }

        /// <summary>
        /// 登録データ準備
        /// </summary>
        /// <param name="addList"></param>
        /// <returns></returns>
        public List<DataDto> CreateEntityData(DataTable addList, out bool catchErr, bool isDeleteFlg = false)
        {
            catchErr = true;
            LogUtility.DebugMethodStart(addList, catchErr, isDeleteFlg);
            var entityList = new List<DataDto>();
            try
            {
                for (int i = 0; i < addList.Rows.Count; i++)
                {
                    DataDto data = new DataDto();
                    M_ZAIKO_HIRITSU mZaikoHiritsu = new M_ZAIKO_HIRITSU();
                    DataRow row = addList.Rows[i];
                    if (!isDeleteFlg)
                    {
                        //保存と更新データを取得
                        bool deleteFlg = string.IsNullOrEmpty(Convert.ToString(row["DELETE_FLG"])) ? false : Convert.ToBoolean(Convert.ToString(row["DELETE_FLG"]));
                        if (deleteFlg)
                        {
                            continue;
                        }
                        if (string.IsNullOrEmpty(row["ZAIKO_HINMEI_CD"].ToString()))
                        {
                            continue;
                        }

                        data.entity = GetEntity(row, isDeleteFlg);
                        if (row["UK_DENSHU_KBN_CD"] != DBNull.Value)
                        {
                            data.updateKey.DENSHU_KBN_CD = (Int16)row["UK_DENSHU_KBN_CD"];
                        }
                        data.updateKey.HINMEI_CD = row["UK_HINMEI_CD"] != DBNull.Value ? row["UK_HINMEI_CD"].ToString() : null;
                        data.updateKey.ZAIKO_HINMEI_CD = row["UK_ZAIKO_HINMEI_CD"] != DBNull.Value ? row["UK_ZAIKO_HINMEI_CD"].ToString() : null;
                        entityList.Add(data);
                    }
                    else
                    {
                        bool deleteFlg = string.IsNullOrEmpty(Convert.ToString(row["DELETE_FLG"])) ? false : Convert.ToBoolean(Convert.ToString(row["DELETE_FLG"]));
                        //削除データを取得
                        if (deleteFlg)
                        {
                            data.entity = GetEntity(row, isDeleteFlg);
                            data.updateKey = null;
                            entityList.Add(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(entityList, catchErr);
            }
            return entityList;
        }

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="disPlayRow"></param>
        /// <param name="isDeleteFlg"></param>
        /// <returns></returns>
        internal M_ZAIKO_HIRITSU GetEntity(DataRow disPlayRow, bool isDeleteFlg)
        {
            M_ZAIKO_HIRITSU mZaikoHiritsu = new M_ZAIKO_HIRITSU();
            var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_ZAIKO_HIRITSU>(mZaikoHiritsu);
            //伝種区分CD
            if (this.form.DENSHU_KBN_CD.Text != null && this.form.DENSHU_KBN_CD.Text.ToString().Trim() != "")
            {
                mZaikoHiritsu.DENSHU_KBN_CD = Convert.ToInt16(this.form.DENSHU_KBN_CD.Text.ToString());
            }
            //品名CD
            if (this.form.HINMEI_CD.Text != null && this.form.HINMEI_CD.Text.ToString().Trim() != "")
            {
                mZaikoHiritsu.HINMEI_CD = this.form.HINMEI_CD.Text.ToString();
            }
            //品名
            if (this.form.HINMEI_CD.Text != null && this.form.HINMEI_CD.Text.ToString().Trim() != "")
            {
                mZaikoHiritsu.HINMEI_NAME = this.form.HINMEI_NAME_RYAKU.Text.ToString();
            }

            //在庫品名CD
            if (!DBNull.Value.Equals(disPlayRow["ZAIKO_HINMEI_CD"])
                && disPlayRow["ZAIKO_HINMEI_CD"] != null
                && !string.IsNullOrEmpty(disPlayRow["ZAIKO_HINMEI_CD"].ToString()))
            {
                mZaikoHiritsu.ZAIKO_HINMEI_CD = disPlayRow["ZAIKO_HINMEI_CD"].ToString();
            }
            //在庫品名
            if (!DBNull.Value.Equals(disPlayRow["ZAIKO_HINMEI_NAME"])
                && disPlayRow["ZAIKO_HINMEI_NAME"] != null
                && !string.IsNullOrEmpty(disPlayRow["ZAIKO_HINMEI_NAME"].ToString()))
            {
                mZaikoHiritsu.ZAIKO_HINMEI_NAME = disPlayRow["ZAIKO_HINMEI_NAME"].ToString();
            }
            //在庫比率
            if (!DBNull.Value.Equals(disPlayRow["ZAIKO_HIRITSU"])
                 && disPlayRow["ZAIKO_HIRITSU"] != null
                && !string.IsNullOrEmpty(disPlayRow["ZAIKO_HIRITSU"].ToString()))
            {
                mZaikoHiritsu.ZAIKO_HIRITSU = Convert.ToInt16(disPlayRow["ZAIKO_HIRITSU"]);
            }
            //備考
            if (!DBNull.Value.Equals(disPlayRow["BIKOU"])
                && disPlayRow["BIKOU"] != null
                && !string.IsNullOrEmpty(disPlayRow["BIKOU"].ToString()))
            {
                mZaikoHiritsu.BIKOU = disPlayRow["BIKOU"].ToString();
            }
            //削除フラグ
            mZaikoHiritsu.DELETE_FLG = isDeleteFlg;
            if (!DBNull.Value.Equals(disPlayRow["TIME_STAMP"]))
            {
                mZaikoHiritsu.TIME_STAMP = (byte[])disPlayRow["TIME_STAMP"];
            }
            dataBinderLogic.SetSystemProperty(mZaikoHiritsu, isDeleteFlg);
            return mZaikoHiritsu;
        }

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals("TIME_STAMP"))
                    {
                        column.Unique = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ColumnAllowDBNull", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 入力チェック

        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        private bool CheckBeforeUpdate()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                //DataGridViewデータ0件チェック
                if (this.form.Ichiran.Rows.Count < 2)
                {
                    msgLogic.MessageBoxShow("E061");
                    return ret;
                }

                //検索時の品名CDと登録時の品名CDのチェック
                if (SearchStringHiritsu != null)
                {
                    string msgPram = "";
                    if (!this.form.HINMEI_CD.Text.Equals(SearchStringHiritsu.HINMEI_CD))
                    {
                        msgPram += "品名CD";
                        this.form.HINMEI_CD.Focus();
                    }
                    if (msgPram != "")
                    {
                        msgLogic.MessageBoxShow("E095", msgPram);
                        return ret;
                    }
                }

                //比率チェック(在庫比率の合計が100％になるように入力してください)
                if (!hiritsuSumCHK())
                {
                    msgLogic.MessageBoxShow("E235", "在庫比率");
                    return ret;
                }
                ret = true;
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckBeforeUpdate", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 比率チェック
        /// </summary>
        /// <returns></returns>
        private bool hiritsuSumCHK()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                //修正した画面データの比率合計
                decimal hiritsuGamen = 0;

                for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                {
                    //比率加算
                    bool deleteFlg = string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[j].Cells["DELETE_FLG"].Value)) ? false : Convert.ToBoolean(this.form.Ichiran.Rows[j].Cells["DELETE_FLG"].Value.ToString());
                    if (!deleteFlg)
                    {
                        //画面のデータ加算
                        hiritsuGamen = hiritsuGamen + Convert.ToDecimal(this.form.Ichiran.Rows[j].Cells["ZAIKO_HIRITSU"].Value);
                    }
                }

                if (hiritsuGamen <= 100)
                {
                    ret = true;
                    return ret;
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("hiritsuSumCHK", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 明細部・在庫品名CD入力チェック
        /// </summary>
        /// <param name="zaikoHinmeiCd"></param>
        /// <returns></returns>
        public bool MandatoryCheck(string zaikoHinmeiCd)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(zaikoHinmeiCd);
                if (!DuplicationCheck(zaikoHinmeiCd))
                {
                    // 20150526 在庫品名CD重複チェックメッセージ修正(有価在庫不具合一覧68) Start
                    //msgLogic.MessageBoxShow("E022", "入力された在庫品名CD");
                    msgLogic.MessageBoxShow("E003", "在庫品名CD", zaikoHinmeiCd);
                    // 20150526 在庫品名CD重複チェックメッセージ修正(有価在庫不具合一覧68) End
                    return ret;
                }
                else if (!ExistCheck(zaikoHinmeiCd))
                {
                    msgLogic.MessageBoxShow("E020", "在庫品名");
                    return ret;
                }
                ret = true;
                return ret;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ZaikoHinmeCDCheck", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoHinmeCDCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 明細部・在庫品名CD存在チェック
        /// </summary>
        /// <param name="zaikoHinmeiCd"></param>
        /// <returns></returns>
        private bool ExistCheck(string zaikoHinmeiCd)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(zaikoHinmeiCd);
                //存在チェック
                this.SearchZaikoHinmei = this.ZaikoHiritsuHoshuDao.GetZaikoHinmeiSqlFile(zaikoHinmeiCd);
                if (this.SearchZaikoHinmei.Rows.Count < 1)
                {
                    ret = false;
                    return false;
                }
                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SonzaiCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 明細部・在庫品名CD重複チェック
        /// </summary>
        /// <returns></returns>
        private bool DuplicationCheck(string zaikoHinmeiCd)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(zaikoHinmeiCd);

                // 画面で在庫品名CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["ZAIKO_HINMEI_CD"].Value.ToString().PadLeft(6, '0').Equals(Convert.ToString(zaikoHinmeiCd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    ret = false;
                    return ret;
                }

                // 検索結果で在庫品名CD重複チェック
                if (this.SearchResult != null)
                {
                    for (int i = 0; i < this.SearchResult.Rows.Count; i++)
                    {
                        if (zaikoHinmeiCd.Equals(this.SearchResult.Rows[i][1]))
                        {
                            return ret;
                        }
                    }
                }

                // DBで在庫品名CD重複チェック
                M_ZAIKO_HIRITSU data = new M_ZAIKO_HIRITSU();
                data.DENSHU_KBN_CD = Convert.ToInt16(this.form.DENSHU_KBN_CD.Text.ToString());
                data.HINMEI_CD = this.form.HINMEI_CD.Text;
                data.ZAIKO_HINMEI_CD = zaikoHinmeiCd;
                DataTable entity = this.ZaikoHiritsuHoshuDao.GetDataByPKSqlFile(data);

                if (entity != null && entity.Rows.Count > 0)
                {
                    ret = false;
                    return ret;
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
        }

        #region Mulit行メッセージを生成(削除)

        /// <summary>
        /// Mulit行メッセージを生成
        /// </summary>
        /// <param name="msgID">メッセージID</param>
        /// <param name="str">整形時に利用する文言のリスト</param>
        /// <returns>整形済みメッセージ</returns>
        private string CreateMulitMessage(string msgID, params string[] str)
        {
            LogUtility.DebugMethodStart(msgID, str);
            // 整形済みメッセージ
            string msgResult = string.Empty;
            try
            {
                // メッセージ原本
                string msg = MessageUtil.GetMessage("E001").MESSAGE;

                for (int i = 0; i < str.Length; i++)
                {
                    string msgTmp = string.Format(msg, str[i]);
                    if (!string.IsNullOrEmpty(msgResult))
                    {
                        msgResult += "\r\n";
                    }
                    msgResult += msgTmp;
                }
                LogUtility.DebugMethodEnd();
                return msgResult;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMulitMessage", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(msgResult);
            }
        }

        #endregion

        #endregion

        #region 明細一覧のcellを結合する。

        //明細一覧のcellを結合する。
        public void ChangeCell(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // ガード句
            if (e.RowIndex > -1)
            {
                // ヘッダー以外は処理なし
                return;
            }

            //比率列目を結合する処理
            string columnName = this.form.Ichiran.Columns[e.ColumnIndex].DataPropertyName;
            // 処理対象セルが、2列目の場合のみ処理を行う
            if (columnName.Equals("ZAIKO_HIRITSU") || columnName.Equals("ZAIKO_HIRITSU_UNIT"))
            {
                // セルの矩形を取得
                Rectangle rect = e.CellBounds;
                DataGridView dgv = (DataGridView)sender;

                //★★★比率の場合
                if (columnName.Equals("ZAIKO_HIRITSU"))
                {
                    // 7列目の幅を取得して、6列目の幅に足す
                    rect.Width += dgv.Columns[e.ColumnIndex + 1].Width;
                    rect.Y = e.CellBounds.Y + 1;
                }
                else
                {
                    // 比率列目の幅を取得して、比率単位列目の幅に足す
                    rect.Width += dgv.Columns[e.ColumnIndex - 1].Width;
                    rect.Y = e.CellBounds.Y + 1;

                    //★★★Leftを1列目に合わせる
                    rect.X -= dgv.Columns[e.ColumnIndex - 1].Width;
                }

                // 背景、枠線、セルの値を描画
                using (SolidBrush brush = new SolidBrush(this.form.Ichiran.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    // 背景の描画
                    rect.Height = rect.Height - 1;
                    e.Graphics.FillRectangle(brush, rect);

                    using (Pen pen = new Pen(dgv.GridColor))
                    {
                        // 枠線の描画
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                }
                // セルに表示するテキストを描画
                TextRenderer.DrawText(e.Graphics,
                                "比率",
                                e.CellStyle.Font,
                                rect,
                                e.CellStyle.ForeColor,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);

                //================================
                // DataGridViewヘッダー結合セルの枠線の設定
                // Graphics オブジェクトを取得
                Graphics g = e.Graphics;

                // グレー，太さ 2 のペンを定義
                // 直線を描画(ヘッダ上部)
                int index = dgv.Columns["ZAIKO_HIRITSU"].Index;
                int startX = 0;
                for (int i = 0; i < index; i++)
                {
                    startX += dgv.Columns[i].Width;
                }
                //int startX = dgv.Columns[0].Width + dgv.Columns[1].Width + dgv.Columns[2].Width + dgv.Columns[3].Width + dgv.Columns[4].Width + dgv.Columns[5].Width;
                int endX = startX + dgv.Columns[index].Width + dgv.Columns[index + 1].Width;
                g.DrawLine(new Pen(Color.DarkGray, 1), startX, rect.Y - 1, endX, rect.Y - 1);
                //// 直線を描画(ヘッダ下部)
                g.DrawLine(new Pen(Color.DarkGray, 2), startX + 1, rect.Y + rect.Height, endX + 1, rect.Y + rect.Height);
                //================================
            }

            // 結合セル以外は既定の描画を行う
            if (!(columnName.Equals("ZAIKO_HIRITSU") || columnName.Equals("ZAIKO_HIRITSU_UNIT")))
            {
                e.Paint(e.ClipBounds, e.PaintParts);
            }
            // イベントハンドラ内で処理を行ったことを通知
            e.Handled = true;
        }

        #endregion

        #region 空白1レコードを追加

        /// <summary>
        /// 空白1レコードを追加する
        /// </summary>
        public bool CreateNoDataRecord()
        {
            bool ret = true;
            LogUtility.DebugMethodStart();
            try
            {
                #region テーブル作成

                this.ClearTable = new DataTable();
                // DELETE_FLG
                DataColumn column = new DataColumn();
                column.DataType = Type.GetType("System.Boolean");
                column.DefaultValue = false;
                column.ColumnName = "DELETE_FLG";
                ClearTable.Columns.Add(column);
                // ZAIKO_HINMEI_CD
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "ZAIKO_HINMEI_CD";
                ClearTable.Columns.Add(column);
                // ZAIKO_HINMEI_RYAKU
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "ZAIKO_HINMEI_NAME";
                ClearTable.Columns.Add(column);
                // ZAIKO_HIRITSU
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "ZAIKO_HIRITSU";
                ClearTable.Columns.Add(column);
                // ZAIKO_HIRITSU_UNIT
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "%";
                column.ColumnName = "ZAIKO_HIRITSU_UNIT";
                ClearTable.Columns.Add(column);
                // BIKOU
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "BIKOU";
                ClearTable.Columns.Add(column);
                // UPDATE_USER
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "UPDATE_USER";
                ClearTable.Columns.Add(column);
                // UPDATE_DATE
                column = new DataColumn();
                column.DataType = Type.GetType("System.DateTime");
                column.DefaultValue = DBNull.Value;
                column.ColumnName = "UPDATE_DATE";
                ClearTable.Columns.Add(column);
                // CREATE_USER
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "CREATE_USER";
                ClearTable.Columns.Add(column);
                // CREATE_DATE
                column = new DataColumn();
                column.DataType = Type.GetType("System.DateTime");
                column.DefaultValue = DBNull.Value;
                column.ColumnName = "CREATE_DATE";
                ClearTable.Columns.Add(column);
                // CREATE_PC
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "CREATE_PC";
                ClearTable.Columns.Add(column);
                // UPDATE_PC
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "UPDATE_PC";
                ClearTable.Columns.Add(column);
                // TIME_STAMP
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Byte[]");
                //column.DefaultValue = DateTime.Now;
                column.ColumnName = "TIME_STAMP";
                ClearTable.Columns.Add(column);
                // DENSHUKBNCD
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "DENSHUKBNCD";
                ClearTable.Columns.Add(column);
                // HINMEICD
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "HINMEICD";
                ClearTable.Columns.Add(column);
                // UK_DENSHU_KBN_CD
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "UK_DENSHU_KBN_CD";
                ClearTable.Columns.Add(column);
                // UK_HINMEI_CD
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "UK_HINMEI_CD";
                ClearTable.Columns.Add(column);
                // UK_ZAIKO_HINMEI_CD
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.DefaultValue = "";
                column.ColumnName = "UK_ZAIKO_HINMEI_CD";
                ClearTable.Columns.Add(column);

                #endregion

                this.form.Ichiran.CellValidating -= this.form.Ichiran_CellValidating;
                foreach (DataGridViewColumn DGVcolumn in this.form.Ichiran.Columns)
                {
                    switch (DGVcolumn.Name)
                    {
                        case "DELETE_FLG":
                        case "ZAIKO_HINMEI_CD":
                        case "ZAIKO_HIRITSU":
                        case "BIKOU":
                            DGVcolumn.ReadOnly = true;
                            break;
                    }
                }
                this.form.Ichiran.DataSource = this.ClearTable;
                this.form.Ichiran.AllowUserToAddRows = false;
                this.form.Ichiran.CellValidating += this.form.Ichiran_CellValidating;

                // 削除ボタン(F4)
                this.parentForm.bt_func4.Enabled = false;
                // CSV出力ボタン(F6)
                this.parentForm.bt_func6.Enabled = false;
                // 登録ボタン(F9)
                this.parentForm.bt_func9.Enabled = false;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M245", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateNoDataRecord", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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

        #region IBuisinessLogicで必須実装(論理削除以外未使用)

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete(List<DataDto> listDelete)
        {
            LogUtility.DebugMethodStart(listDelete);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            bool ret = true;
            try
            {
                using (Transaction tran = new Transaction())
                {
                    foreach (DataDto data in listDelete)
                    {
                        M_ZAIKO_HIRITSU entity = this.ZaikoHiritsuHoshuDao.GetDataByCd(data.entity.ZAIKO_HINMEI_CD, data.entity.HINMEI_CD, data.entity.DENSHU_KBN_CD.Value);
                        if (entity != null)
                        {
                            entity.DELETE_FLG = true;
                            this.ZaikoHiritsuHoshuDao.Update(data.entity);
                        }
                    }
                    tran.Commit();
                    msgLogic.MessageBoxShow("I001", "削除");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.form.errmessage.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(listDelete);
            }
            if (ret)
            {
                this.form.Search(null, null);
                this.SetIchiranData();
            }
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 品名入力制御
        /// </summary>
        internal bool HinmeiCdValidating(CancelEventArgs e)
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(this.form.HINMEI_CD.Text))
                {
                    M_HINMEI hinmei = new M_HINMEI();
                    hinmei.HINMEI_CD = this.form.HINMEI_CD.Text;
                    hinmei.DENSHU_KBN_CD = Convert.ToInt16(this.form.DENSHU_KBN_CD.Text);
                    M_HINMEI[] result = this.HinmeiDao.GetAllValidData(hinmei);
                    if (result == null || result.Length == 0)
                    {
                        hinmei.DENSHU_KBN_CD = 9;
                        M_HINMEI[] result2 = this.HinmeiDao.GetAllValidData(hinmei);
                        if (result2 == null || result2.Length == 0)
                        {
                            msgLogic.MessageBoxShow("E020", "品名");
                            this.form.HINMEI_NAME_RYAKU.Text = string.Empty;
                            this.form.HINMEI_CD.BackColor = Constans.ERROR_COLOR;
                            this.form.HINMEI_CD.SelectAll();
                            e.Cancel = true;
                            return false;
                        }
                        else
                        {
                            this.form.HINMEI_NAME_RYAKU.Text = result2[0].HINMEI_NAME_RYAKU;
                        }
                    }
                    else
                    {
                        this.form.HINMEI_NAME_RYAKU.Text = result[0].HINMEI_NAME_RYAKU;
                    }
                }
                else
                {
                    this.form.HINMEI_NAME_RYAKU.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HinmeiCdValidating", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HinmeiCdValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        // 20150424 品名入力変更する時一覧がクリアされるように Start
        /// <summary>
        ///
        /// </summary>
        internal bool HinmeiCdTextChanged(EventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // CancelInitとほぼ同じ動作
                // 品名を設定
                this.form.HINMEI_NAME_RYAKU.Text = string.Empty;

                // 明細部クリア
                if (!CreateNoDataRecord())
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("HinmeiCdTextChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret;
        }

        // 20150424 品名入力変更する時一覧がクリアされるように End
    }
}