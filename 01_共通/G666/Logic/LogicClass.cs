using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.CtiRenkeiSettei.DAO;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Common.CtiRenkeiSettei
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// DAOClass
        /// </summary>
        private DAOClass dbMaintenanceViewDao;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.msgLogic = new MessageBoxShowLogic();

            // View用のDao
            this.dbMaintenanceViewDao = DaoInitUtility.GetComponent<DAOClass>();
                
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // イベントの初期化処理
                this.eventInit();

                // 画面初期化
                this.formInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンのイベント初期化処理
        /// </summary>
        /// <returns></returns>
        private void eventInit()
        {
            LogUtility.DebugMethodStart();

            // 参照をクリックした
            this.form.btnBrowse.Click -= new EventHandler(this.btnBrowse_Click);
            this.form.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);

            // DBﾒﾝﾃ(F9)イベント生成
            this.form.bt_func1.Click -= new EventHandler(this.btnDbMaintenance_Click);
            this.form.bt_func1.Click += new EventHandler(this.btnDbMaintenance_Click);

            // 保存ボタン(F9)イベント生成
            this.form.bt_func9.Click -= new EventHandler(this.FormSave);
            this.form.bt_func9.Click += new EventHandler(this.FormSave);

            // 閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click -= new EventHandler(this.FormClose);
            this.form.bt_func12.Click += new EventHandler(this.FormClose);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void formInit()
        {
            LogUtility.DebugMethodStart();

            // タイトルラベル
            this.form.TITLE_LABEL.Text = WINDOW_ID.T_CTI_RENKEI_SETTEI.ToTitleString();

            // Formタイトル
            this.form.Text = SystemProperty.CreateWindowTitle(this.form.TITLE_LABEL.Text);

            // CTI情報を取得する
            this.GetCtiSaveSettings();

            // 初期値：2.しない
            if (string.IsNullOrEmpty(this.form.txtUse.Text))
            {
                this.form.txtUse.Text = "2";
            }
            // 初期値：1000
            if (string.IsNullOrEmpty(this.form.txtFileDetectTime.Text))
            {
                this.form.txtFileDetectTime.Text = "1000";
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// DBﾒﾝﾃボタンがクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベントの引数</param>
        private void btnDbMaintenance_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // はいが選択された場合以下の処理を実行
                if (System.Windows.Forms.DialogResult.Yes == this.msgLogic.MessageBoxShowConfirm("CTIコネクテルが環境将軍Rのデータを引用するために必要なデータを作成します。\n(CTIコネクテルの受付画面で環境将軍Rのデータが表示されない場合、この処理で修正される可能性があります。)"))
                {
                    using (Transaction tran = new Transaction()) {

                        // Viewが存在する場合、削除する。
                        dbMaintenanceViewDao.DropCticonnect(string.Empty);

                        // 新規VIEWをする
                        dbMaintenanceViewDao.CreateCticonnect(string.Empty);

                        tran.Commit();
                    }

                    this.msgLogic.MessageBoxShow("I001", new string[] { "データベースのメンテナンス" });
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("btnDbMaintenance_Click", ex);// 保存失敗
                this.msgLogic.MessageBoxShow("I007", new string[] { "必要なデータの作成" });
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 参照ボタンがクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベントの引数</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {

            LogUtility.DebugMethodStart(sender, e);
            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var title = "開くファイルを選択してください";
            var initialPath = @"C:\Temp";
            var windowHandle = this.form.Handle;
            var isFileSelect = true;
            var isTerminalMode = SystemProperty.IsTerminalMode;
            var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

            browserForFolder = null;

            if (false == String.IsNullOrEmpty(filePath))
            {
                this.form.txtFilePath.Text = filePath;
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        private void FormClose(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.Close();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormSave(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.form.txtUse.Text))
            {
                this.form.txtUse.Text = "2";
            }

            string filePath = this.form.txtFilePath.Text;
            if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
            {

                this.msgLogic.MessageBoxShow("E051", new string[] { "有効なファイル" });

                return;
            }

            // CTI情報保存
            if (this.CtiSaveSettings())
            {
                // 保存完了
                this.msgLogic.MessageBoxShowInformation("登録が完了しました。\n設定を有効にする場合は、環境将軍Rを再起動してください。");
            }
            else
            {
                // 保存失敗
                this.msgLogic.MessageBoxShowError("CTI連携設定ファイルの保存に失敗しました。");
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// CTI情報を取得する
        /// </summary>
        private void GetCtiSaveSettings()
        {
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();

            if (userProfile.Settings.CtiSettings != null
                && userProfile.Settings.CtiSettings.Values != null)
            {
                this.form.txtUse.Text = userProfile.Settings.CtiSettings.Values.Use;
                this.form.txtFilePath.Text = userProfile.Settings.CtiSettings.Values.FilePath;
                this.form.txtFileDetectTime.Text = userProfile.Settings.CtiSettings.Values.FileDetectTime;
            }
        }

        /// <summary>
        /// CTI情報保存
        /// </summary>
        private bool CtiSaveSettings()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var configXml = XElement.Load(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
                var valuesElem = configXml.XPathSelectElement("./Settings/CtiSettings/Values");
                if (valuesElem == null)
                {
                    valuesElem = new XElement("Values");
                    configXml.XPathSelectElement("./Settings").Add(new XElement("CtiSettings", valuesElem));
                }
                valuesElem.SetAttributeValue("Use", this.form.txtUse.Text);
                valuesElem.SetAttributeValue("FilePath", this.form.txtFilePath.Text);
                valuesElem.SetAttributeValue("FileDetectTime", this.form.txtFileDetectTime.Text.Replace(",", string.Empty));
                configXml.Save(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
            }
            catch (Exception ex)
            {

                LogUtility.Error("CtiSaveSettings", ex);// 保存失敗
                this.msgLogic.MessageBoxShowError("CTI連携設定ファイルの保存に失敗しました");

                LogUtility.DebugMethodEnd();
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

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
    }
}
