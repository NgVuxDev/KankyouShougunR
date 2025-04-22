// $Id: LogicClass.cs 25072 2014-07-09 05:06:56Z j-kikuchi $
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using r_framework.BrowseForFolder;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.CustomControl;

namespace Shougun.Core.Common.MobileTsuushin
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region - Field -
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;
        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        private MessageBoxShowLogic msgLogic;
        /// <summary>
        /// モバイル設定ファイルPath
        /// </summary>
        private string iniFilePath;
        /// <summary>
        /// モバイル設定ファイル内容
        /// </summary>
        private string[] iniFileStrings;

        #endregion - Field -

        #region - Constructor -
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal LogicClass(UIForm targetForm)
        {
            // フィールドの初期化
            this.form = targetForm;
            this.msgLogic = new MessageBoxShowLogic();

            try
            {
                // モバイル設定ファイルPath取得
                this.iniFilePath = AppData.PrepareLocalAppDataFile(ConstClass.MOBILE_SETTING_INI);

                // モバイル設定ファイル読み込み
                this.iniFileStrings = File.ReadAllLines(this.iniFilePath, System.Text.Encoding.GetEncoding("Shift_JIS"));
            }
            catch(Exception)
            {
                LogUtility.Error(ConstClass.MOBILE_SETTING_INI + "読込失敗");
                throw;
            }
        }

        #endregion - Constructor -

        #region - Initialize -
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
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
        }

        /// <summary>
        /// ボタンのイベント初期化処理
        /// </summary>
        /// <returns></returns>
        private void eventInit()
        {
            //Functionボタンのイベント生成
            this.form.bt_func9.Click += this.bt_func9_Click;
            this.form.bt_func12.Click += this.bt_func12_Click;

            // 参照ボタンクリックイベント生成
            this.form.masterBrowse.Click += this.browseClick;
            this.form.transBrowse.Click += this.browseClick;
            this.form.inputBrowse.Click += this.browseClick;
            this.form.backUpBrowse.Click += this.browseClick;
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void formInit()
        {
            // タイトルラベル
            this.form.TITLE_LABEL.Text = WINDOW_ID.T_MOBILE_TSUUSHINSETTEI.ToTitleString();

            // Formタイトル
            this.form.Text = SystemProperty.CreateWindowTitle(this.form.TITLE_LABEL.Text);

            // 初期値は設定ファイルから読み込み
            this.form.masterOutputPath.Text = this.getMobilePath(ConstClass.MOBILE_OUTPUT_MASTER_PATH);
            this.form.transOutputPath.Text = this.getMobilePath(ConstClass.MOBILE_OUTPUT_TRANS_PATH);
            this.form.inputPath.Text = this.getMobilePath(ConstClass.MOBILE_INPUT_PATH);
            this.form.backUpPath.Text = this.getMobilePath(ConstClass.MOBILE_BACKUP_PATH);
        }

        #endregion - Initialze -

        #region - FunctionKeyEvent -
        /// <summary>
        /// F9 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            // 処理継続確認に「はい」を押下した場合
            if (msgLogic.MessageBoxShow("C044") == DialogResult.Yes)
            {
                // 設定ファイルに保存開始
                if (true == this.setMobilePath())
                {
                    // 保存完了
                    msgLogic.MessageBoxShow("I001", "モバイル設定ファイルの保存");
                }
                else
                {
                    // 保存失敗
                    MessageBox.Show("モバイル設定ファイルの保存に失敗しました",  "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// F12 キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            // フォームを閉じる
            this.form.Close();
        }

        #endregion - FunctionKeyEvent -

        #region - OtherEvent -
        /// <summary>
        /// 参照ボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void browseClick(object sender, EventArgs e)
        {
            // 各格納先にセット
            var ctrl = (CustomButton)sender;
            var initialPath = string.Empty;
            switch (ctrl.Name)
            {
                case "masterBrowse":
                    // マスタファイル出力先
                    initialPath = this.form.masterOutputPath.Text;
                    break;
                case "transBrowse":
                    // 配車ファイル出力先
                    initialPath = this.form.transOutputPath.Text;
                    break;
                case "inputBrowse":
                    // 実績データ取込先
                    initialPath = this.form.inputPath.Text;
                    break;
                case "backUpBrowse":
                    // バックアップ保存先
                    initialPath = this.form.backUpPath.Text;
                    break;
                default:
                    // DO NOTHING
                    break;
            }

            // フォルダ参照ポップアップ設定
            var windowHandle = this.form.Handle;
            var isFileSelect = false;

            if (string.IsNullOrEmpty(initialPath))
            {
                initialPath = @"C:\";
            }

            // フォルダ参照ポップアップ表示
            var filePath = this.DirSearch("", initialPath, windowHandle, isFileSelect);

            // フォルダが選択された場合
            if(false == string.IsNullOrEmpty(filePath))
            {
                switch(ctrl.Name)
                {
                    case "masterBrowse":
                        // マスタファイル出力先
                        this.form.masterOutputPath.Text = filePath;
                        break;
                    case "transBrowse":
                        // 配車ファイル出力先
                        this.form.transOutputPath.Text = filePath;
                        break;
                    case "inputBrowse":
                        // 実績データ取込先
                        this.form.inputPath.Text = filePath;
                        break;
                    case "backUpBrowse":
                        // バックアップ保存先
                        this.form.backUpPath.Text = filePath;
                        break;
                    default:
                        // DO NOTHING
                        break;
                }
            }
        }

        private string DirSearch(string title, string initialPath, IntPtr windowHandle, bool isFileSelect)
        {
            LogUtility.DebugMethodStart(title, initialPath, windowHandle, isFileSelect);

            var browserForFolder = new BrowseForFolder();
            var isTerminalMode = SystemProperty.IsTerminalMode;
            var filePath = "";

            try
            {
                filePath = browserForFolder.getFolderPath(title, initialPath, windowHandle, isFileSelect);
            }
            catch (Exception ex)
            {
                filePath = "";
                MessageBox.Show("ディレクトリの選択に失敗しました。" + Environment.NewLine + Environment.NewLine +
                                    "再試行してください。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            LogUtility.DebugMethodEnd();

            return filePath.ToString();
        }

        #endregion - OtherEvent -

        #region - Utility -
        /// <summary>
        /// 画面の内容を設定ファイルに保存する
        /// </summary>
        /// <returns name="bool">TRUE:保存完了, FALSE:保存失敗</returns>
        private bool setMobilePath()
        {
            bool success = false;

            try
            {
                // 登録する全てのPathに対して置換をかける
                foreach(var path in ConstClass.REGIST_PATH_TABLE)
                {
                    // ファイル内容検索
                    for(int i = 0; i < this.iniFileStrings.Length; i++)
                    {
                        var index = this.iniFileStrings[i].IndexOf(path);
                        if(index != -1)
                        {
                            var saveStr = path + "=";

                            if(path == ConstClass.MOBILE_OUTPUT_MASTER_PATH)
                            {
                                // マスタファイル出力先
                                saveStr += this.form.masterOutputPath.Text;
                            }
                            else if(path == ConstClass.MOBILE_OUTPUT_TRANS_PATH)
                            {
                                // 配車ファイル出力先
                                saveStr += this.form.transOutputPath.Text;
                            }
                            else if(path == ConstClass.MOBILE_INPUT_PATH)
                            {
                                // 実績データ取込先
                                saveStr += this.form.inputPath.Text;
                            }
                            else if(path == ConstClass.MOBILE_BACKUP_PATH)
                            {
                                // バックアップ保存先
                                saveStr += this.form.backUpPath.Text;
                            }

                            // 新Pathをにて置換
                            // ※余計な検索を行わないためにループから抜ける
                            if(true == saveStr.EndsWith("\\"))
                            {
                            	// そのまま置換
                                this.iniFileStrings[i] = saveStr;
                            }
                            else
                            {
                            	// 末尾に[\]が無ければ付与した上で置換
                                this.iniFileStrings[i] = saveStr + "\\";
                            }
                            break;
                        }
                    }
                }

                // モバイル設定ファイル書き込み
                File.WriteAllLines(this.iniFilePath, this.iniFileStrings, System.Text.Encoding.GetEncoding("Shift_JIS"));

                // 保存完了
                success = true;
            }
            catch(Exception ex)
            {
                LogUtility.Error(ConstClass.MOBILE_SETTING_INI + "保存失敗");
            }

            return success;
        }

        /// <summary>
        /// 指定された種別のモバイル設定Pathを取得
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string getMobilePath(string path)
        {
            string retPath = string.Empty;

            try
            {
                // ファイル内容検索
                foreach(var str in this.iniFileStrings)
                {
                    var index = str.IndexOf(path);
                    if(index != -1)
                    {
                        // 指定された種別のPathがあった場合「=」以降を取得し、Pathを返却
                        int num = str.IndexOf("=");
                        retPath = str.Substring(num + 1);
                    }
                }
            }
            catch(Exception)
            {
                LogUtility.Error(ConstClass.MOBILE_SETTING_INI + "読込失敗");
                throw;
            }

            // 該当Pathを返却
            return retPath;
        }

        #endregion - Utility -

        #region - IFmember -
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

        #endregion - IFmember -
    }
}
