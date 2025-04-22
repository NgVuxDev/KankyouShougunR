using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Dto;
using r_framework.Utility;

namespace r_framework.BrowseForFolder
{
    public partial class FolderSelectPopup : Form , IDisposable
    {
        #region 外部による設定
        private string caption = "";
        public string captionSetting
        {
            set
            {
                caption = value.ToString();
            }
        }
        #endregion

        #region 外部への設定
        private string dirPath = "";
        public string getDirPath
        {
            get
            {
                return dirPath;
            }
        }
        #endregion

        /// <summary>
        /// 開始
        /// </summary>
        public FolderSelectPopup()
        {
            InitializeComponent();

            setEventHandler();

        }

        #region 起動処理
        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderSelectPopup_Load(object sender, EventArgs e)
        {
            setDispSetting();
        }

        /// <summary>
        /// 画面の状態設定
        /// </summary>
        private void setDispSetting()
        {
            this.Text = caption;
            this.lbl_Title.Text = "出力先確認";
            this.lbl_OutputDir.Text = "出力先";

            this.txtOutputDirectory.Text = getBeforeDirectory().ToString();

            this.bt_DirSearch.Text = "参照";
            this.bt_Func9.Text = "[F9]" + Environment.NewLine + "出力";
            this.bt_Func12.Text = "[F12]" + Environment.NewLine + "閉じる";

        }
        #endregion

        #region Event設定

        /// <summary>
        /// 画面に存在するコントロールにイベントを設定
        /// </summary>
        private void setEventHandler()
        {
            this.KeyPreview = true;
            this.KeyUp += new KeyEventHandler(this.Form_KeyUp);

            this.Load += new System.EventHandler(this.FolderSelectPopup_Load);
            this.bt_DirSearch.Click += new System.EventHandler(this.bt_DirSearch_Click);
            this.bt_Func9.Click += new System.EventHandler(this.bt_Func9_Click);
            this.bt_Func12.Click += new System.EventHandler(this.bt_Func12_Click);

        }

        /// <summary>
        /// Form_KeyUpイベントの追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            switch (e.KeyCode)
            {
                case Keys.F9:
                    bt_Func9_Click(sender, e);
                    break;

                case Keys.F12:
                    bt_Func12_Click(sender, e);
                    break;

                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.D0:
                case Keys.Delete:
                case Keys.Back:
                case Keys.Tab:
                case Keys.Shift:
                case Keys.Enter:
                    break;

                default:
                    e.Handled = true;
                    break;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ControlEvent
        private void bt_DirSearch_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            
            this.txtOutputDirectory.Text = this.DirSearch();

            LogUtility.DebugMethodEnd();
        }

        private void bt_Func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Commit(this.txtOutputDirectory.Text, true);

            LogUtility.DebugMethodEnd();
        }
        private void bt_Func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Commit("", false);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 処理
        /// <summary>
        /// BrowseForFolderを利用してディレクトリの検索を行う
        /// </summary>
        private string DirSearch()
        {
            LogUtility.DebugMethodStart();

            var browserForFolder = new BrowseForFolder();
            var title = caption;
            var initialPath = this.txtOutputDirectory.Text;
            var windowHandle = this.Handle;
            var isFileSelect = false;
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

        /// <summary>
        /// ディレクトリの許容チェック
        /// </summary>
        /// <param name="checkDir"></param>
        private Boolean DirAcceptCheck(string checkDir)
        {
            LogUtility.DebugMethodStart(checkDir);

            Boolean checkBool = true;

            try
            {
                if (checkBool == true && checkDir == "")
                {
                    MessageBox.Show("フォルダが指定されていません。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    checkBool = false;
                }

                if (checkBool == true && System.IO.Directory.Exists(checkDir) == false)
                {
                    MessageBox.Show("選択されているディレクトリは存在しません。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    checkBool = false;
                }

                if (checkBool == true && System.IO.Directory.GetAccessControl(checkDir).AreAccessRulesProtected == true)
                {
                    MessageBox.Show("選択されているディレクトリにアクセス権がありません。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    checkBool = false;
                }

                if (checkBool == true && checkDir.Length >= 240)
                {
                    //PathTooLongExceptionの回避
                    MessageBox.Show("ディレクトリが長すぎます。240文字以内で指定してください。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    checkBool = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("選択したディレクトリは無効です。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                checkBool = false;
            }

            LogUtility.DebugMethodEnd(checkBool);

            return checkBool;
        }

        /// <summary>
        /// 現在表示中のディレクトリを選択したディレクトリとして確定するか、戻さない
        /// </summary>
        /// <param name="dPath">確定したいディレクトリ</param>
        /// <param name="noReturn">true:値を戻す false:値を戻さない</param>
        private void Commit(string dPath, bool noReturn)
        {
            LogUtility.DebugMethodStart(dPath, noReturn);

            //外部への戻り値を初期化する
            this.dirPath = "";

            if (noReturn == true)
            {
                //ディレクトリの許容チェック
                if (DirAcceptCheck(dPath) == false)
                {
                    //ディレクトリが許容されない場合はキャンセルする
                    return;
                }

                //外部への戻り値を設定する
                this.dirPath = dPath;

                //ディレクトリを保存する
                this.SaveDirectory(dPath);
            }

            this.Close();

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 現在表示中のディレクトリをUser.Configに保存する
        /// </summary>
        /// <param name="dPath"></param>
        private void SaveDirectory(string dPath)
        {
            LogUtility.DebugMethodStart(dPath);

            try
            {
                Properties.Settings.Default.LastOutputDirectory = dPath;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ディレクトリの保存に失敗しました。" + Environment.NewLine +
                                    "今回選択したディレクトリは保存されません。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 前回のディレクトリパスの読み込みと返却
        /// </summary>
        /// <returns></returns>
        private string getBeforeDirectory()
        {
            LogUtility.DebugMethodStart();

            string dir = "";

            try
            {
                dir = Properties.Settings.Default.LastOutputDirectory;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ディレクトリの読込に失敗しました。" + Environment.NewLine +
                                    "前回選択していたディレクトリは読込されません。" + Environment.NewLine + Environment.NewLine +
                                    "新しく指定し直してください。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            LogUtility.DebugMethodEnd();

            return dir;

        }
        #endregion

        #region 終了処理

        public void Dispose()
        {
        }

        #endregion

    }
}