// $Id: ZipCodeHoshuLogic.cs 49662 2015-05-14 11:05:54Z d-sato $
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using Shougun.Core.Common.BusinessCommon;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using ZipCodeHoshu.APP;
using r_framework.Dto;
using Seasar.Framework.Exceptions;
using Seasar.Dao;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace ZipCodeHoshu.Logic
{
    /// <summary>
    /// 郵便辞書保守画面のビジネスロジック
    /// </summary>
    public class ZipCodeHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "ZipCodeHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_DATA_SQL = "ZipCodeHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_ZIP_CODE_DATA_BY_SYS_ID_SQL = "ZipCodeHoshu.Sql.GetZipCodeDataBySysId.sql";

        private readonly string DELETE_ZIP_CODE_DATA_SQL = "ZipCodeHoshu.Sql.DeleteZipDataSql.sql";

        private readonly string UPDATE_LIST_ZIP_DATA_SQL = "ZipCodeHoshu.Sql.UpdateListZipDataSql.sql";

        /// <summary>
        /// 社員保守画面Form
        /// </summary>
        private ZipCodeHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 郵便番号辞書のDao
        /// </summary>
        private IS_ZIP_CODEDao dao;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public S_ZIP_CODE SearchString { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public ZipCodeHoshuLogic(ZipCodeHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();

            LogUtility.DebugMethodEnd();
        }

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

                this.allControl = this.form.allControl;

                this.form.POST3.Text = Properties.Settings.Default.LIST_POST3_TEXT;
                this.form.POST7.Text = Properties.Settings.Default.LIST_POST7_TEXT;
                if (!string.IsNullOrWhiteSpace(this.form.POST3.Text))
                {
                    this.form.POST7.ReadOnly = true;
                }
                if (!string.IsNullOrWhiteSpace(this.form.POST7.Text))
                {
                    this.form.POST3.ReadOnly = true;
                }

                // ターミナルモードの場合、郵便ホームページへのリンクを非表示にする
                if (r_framework.Dto.SystemProperty.IsTerminalMode)
                {
                    this.form.SetYubinSiteLinkVisible(false);
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M253", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
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
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func9.Enabled = false;

            parentForm.bt_process1.Enabled = false;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                this.SearchResult = dao.GetDataBySqlFile(this.GET_ICHIRAN_DATA_SQL, this.SearchString);

                Properties.Settings.Default.LIST_POST3_TEXT = this.form.POST3.Text;
                Properties.Settings.Default.LIST_POST7_TEXT = this.form.POST7.Text;
                Properties.Settings.Default.Save();

                int count = this.SearchResult.Rows == null ? 0 : 1;

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
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                this.form.POST7_OLD.Text = string.Empty;
                this.form.SIKUCHOUSON_OLD.Text = string.Empty;
                this.form.POST7_NEW.Text = string.Empty;
                this.form.SIKUCHOUSON_NEW.Text = string.Empty;
                SetSearchString();
                this.form.Ichiran.DataSource = null;
                if (this.SearchResult != null)
                {
                    this.SearchResult.Rows.Clear();
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        public void Preview()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("C011", "郵便辞書一覧表");

            MessageBox.Show("未実装");

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV
        /// </summary>
        public bool CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    MultiRowIndexCreateLogic multirowLocationLogic = new MultiRowIndexCreateLogic();
                    multirowLocationLogic.multiRow = (r_framework.CustomControl.GcCustomMultiRow)this.form.Ichiran;

                    multirowLocationLogic.CreateLocations();

                    CSVFileLogic csvLogic = new CSVFileLogic();

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = (r_framework.CustomControl.GcCustomMultiRow)this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);

                    #region 新しいCSV出力利用するように、余計なメッセージを削除
                    //msgLogic.MessageBoxShow("I000");
                    #endregion
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        public bool CancelCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                SetSearchString();
                //20150416 minhhoang edit #1748
                //do not reload search result when F7 press
                //this.form.Ichiran.DataSource = null;
                //if (this.SearchResult != null)
                //{
                //    this.SearchResult.Rows.Clear();
                //}
                //20150416 minhhoang end edit #1748
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #region 登録/更新/削除
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
                //独自チェックの記述例を書く
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                MessageUtility msgUtil = new MessageUtility();
                M_ERROR_MESSAGE errorData = msgUtil.GetMessage("E001");
                List<string> msg = new List<string>();
                if (string.IsNullOrWhiteSpace(this.form.POST7_OLD.Text) && string.IsNullOrWhiteSpace(this.form.POST7_NEW.Text) && string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_OLD.Text) && string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_NEW.Text))
                {
                    MessageBox.Show("変更を行う条件を指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LogUtility.DebugMethodEnd();
                    return;
                }
                if (!string.IsNullOrWhiteSpace(this.form.POST7_OLD.Text) && string.IsNullOrWhiteSpace(this.form.POST7_NEW.Text))
                {
                    msg.Add(string.Format(errorData.MESSAGE, "新郵便番号"));
                    errorFlag = true;
                }
                if (string.IsNullOrWhiteSpace(this.form.POST7_OLD.Text) && !string.IsNullOrWhiteSpace(this.form.POST7_NEW.Text))
                {
                    msg.Add(string.Format(errorData.MESSAGE, "旧郵便番号"));
                    errorFlag = true;
                }
                if (!string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_OLD.Text) && string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_NEW.Text))
                {
                    msg.Add(string.Format(errorData.MESSAGE, "新住所"));
                    errorFlag = true;
                }
                if (string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_OLD.Text) && !string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_NEW.Text))
                {
                    msg.Add(string.Format(errorData.MESSAGE, "旧住所"));
                    errorFlag = true;
                }
                if (errorFlag)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, msg.ToArray()), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LogUtility.DebugMethodEnd();
                    return;
                }

                //エラーではない場合登録処理を行う
                string oldPost = string.Empty;
                string oldAddress = null;
                string newPost = string.Empty;
                string newAddress = null;
                if (!string.IsNullOrWhiteSpace(this.form.POST7_OLD.Text)) oldPost = this.form.POST7_OLD.Text;
                if (!string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_OLD.Text)) oldAddress = this.form.SIKUCHOUSON_OLD.Text;
                if (!string.IsNullOrWhiteSpace(this.form.POST7_NEW.Text)) newPost = this.form.POST7_NEW.Text;
                if (!string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_NEW.Text)) newAddress = this.form.SIKUCHOUSON_NEW.Text;

                // トランザクション開始
                using (var tran = new Transaction())
                {
                    this.dao.UpdatePartData(this.UPDATE_LIST_ZIP_DATA_SQL, oldPost, oldAddress, newPost, newAddress);
                    // トランザクション終了
                    tran.Commit();
                }

                msgLogic.MessageBoxShow("I001", "登録");
                this.form.Search(null, null);

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
            try
            {
                LogUtility.DebugMethodStart();

                DataRowView drv;
                DataRow r;
                S_ZIP_CODE zipCode;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C096");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        // 選択された行を取得する
                        SelectedRowCollection srow = this.form.Ichiran.SelectedRows;
                        foreach (Row row in srow)
                        {
                            // 削除用エンティティの作成
                            drv = (DataRowView)row.DataBoundItem;
                            r = drv.Row;
                            zipCode = new S_ZIP_CODE();
                            zipCode.SYS_ID = Int32.Parse(r["SYS_ID"].ToString());

                            // データが存在すれば削除
                            DataTable table = this.dao.GetDataBySqlFile(this.GET_ZIP_CODE_DATA_BY_SYS_ID_SQL, zipCode);
                            if (table.Rows.Count > 0)
                            {
                                zipCode.TIME_STAMP = (Byte[])r["TIME_STAMP"];
                                this.dao.Delete(zipCode);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "削除");
                }

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("PhysicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("PhysicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("PhysicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 郵便辞書読込処理
        /// </summary>
        [Transaction]
        public virtual bool RegistFromCsvFile(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();

                catchErr = false;
                bool ret = false;
                var dataBinderLogic = new DataBinderLogic<S_ZIP_CODE>(new S_ZIP_CODE());

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するファイルを選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = true;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!String.IsNullOrEmpty(filePath))
                {
                    //20151014 hoanghm #13495 start
                    // try to open file, when file in opening or used by other process then return an show messages
                    try
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(filePath, System.Text.Encoding.GetEncoding(0));
                    }
                    catch
                    {
                        MessageUtility messageUtil = new MessageUtility();
                        string errorMessage = messageUtil.GetMessage("E120").MESSAGE;
                        errorMessage = String.Format(errorMessage);
                        MessageBox.Show(errorMessage, Constans.WORNING_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    //20151014 hoanghm #13495 end

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        // 郵便辞書データを全件削除
                        this.dao.GetDataBySqlFile(this.DELETE_ZIP_CODE_DATA_SQL, new S_ZIP_CODE());

                        // CSVファイルをオープンする
                        System.IO.StreamReader sr = new System.IO.StreamReader(filePath, System.Text.Encoding.GetEncoding(0));

                        // データをインポートする
                        int sysId = 0;
                        while (sr.EndOfStream == false)
                        {
                            string line = sr.ReadLine();
                            string[] fields = line.Split(',');

                            // 空行は無視する
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            // カラム数が15でない場合、適切ではないので例外処理
                            if (fields.Length != 15)
                            {
                                //throw;
                            }

                            // データをセットする
                            sysId++;
                            S_ZIP_CODE zipCode = new S_ZIP_CODE();
                            zipCode.SYS_ID = sysId;
                            zipCode.POST3 = fields[1].Replace("\"", string.Empty).Substring(0, 3);
                            zipCode.POST5 = fields[2].Replace("\"", string.Empty).Substring(0, 3) + "-" + fields[2].Substring(3, 2);
                            zipCode.POST7 = fields[2].Replace("\"", string.Empty).Substring(0, 3) + "-" + fields[2].Replace("\"", string.Empty).Substring(3, 4);
                            zipCode.TODOUFUKEN = fields[6].Replace("\"", string.Empty);
                            zipCode.SIKUCHOUSON = fields[7].Replace("\"", string.Empty);
                            zipCode.OTHER1 = fields[8].Replace("\"", string.Empty);
                            zipCode.OTHER2 = string.Empty;
                            dataBinderLogic.SetSystemProperty(zipCode, false);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), zipCode);
                            this.dao.Insert(zipCode);
                        }

                        // 1件もデータ作成をしていない場合、適切ではないので例外処理
                        if (sysId == 0)
                        {
                            //throw;
                        }

                        // トランザクション終了
                        tran.Commit();
                    }

                    //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");

                    ret = true;
                }

                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                catchErr = true;
                LogUtility.Error("RegistFromCsvFile", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("RegistFromCsvFile", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("RegistFromCsvFile", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
        }
        #endregion

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

            ZipCodeHoshuLogic localLogic = other as ZipCodeHoshuLogic;
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
        #endregion

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

                this.form.Ichiran.DataSource = table;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            //個別切替(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.ChangeForm);

            //削除ボタン(F4)イベント生成
            //this.form.C_Regist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.PhysicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //プレビュ機能削除
            ////ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            //parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //条件取消ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.CancelCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            //this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //プロセスキー1ボタンイベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.RegistFromCsvFile);
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

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            S_ZIP_CODE entity = new S_ZIP_CODE();

            if (!string.IsNullOrEmpty(this.form.POST3.Text))
            {
                entity.POST3 = this.form.POST3.Text;
            }
            if (!string.IsNullOrEmpty(this.form.POST7.Text))
            {
                entity.POST7 = this.form.POST7.Text;
            }

            this.SearchString = entity;
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

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearCondition()
        {
            this.form.POST3.Text = string.Empty;
            this.form.POST7.Text = string.Empty;
            this.form.POST7_NEW.Text = string.Empty;
            this.form.POST7_OLD.Text = string.Empty;
            this.form.SIKUCHOUSON_NEW.Text = string.Empty;
            this.form.SIKUCHOUSON_OLD.Text = string.Empty;

        }
    }
}
