using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Collections;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Configuration;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Utility;
using r_framework.Dao;
using Shougun.UserRestrict.URXmlDocument;
using r_framework.Dto;
using r_framework.Logic;
using Shougun.Printing.Common;

namespace Shougun.Core.Common.Login.APP
{
    /// <summary>
    /// バージョン情報ダイアログ
    /// </summary>
    public partial class VersionInfoDialog : SuperForm
    {
        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        internal M_SYS_INFO mSysInfo;

        public VersionInfoDialog()
        {
            InitializeComponent();
        }

        public VersionInfoDialog(StringBuilder sb)
        {
            InitializeComponent();
            this.rtbVersionInfo.Text = sb.ToString();
            CreateRitchText();

            if (string.IsNullOrEmpty(AppConfig.LoginId))
            {
                this.btnLicense.Visible = false;
                this.btnLicense.Enabled = false;
            }
        }

        /// <summary>
        /// 画面読み込み時
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.rtbVersionInfo.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.btnProtect.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.btnCopy.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.btnEnd.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.btnLicense.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.btnSupport.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
 	        base.OnLoad(e);

            //プロテクト解除チェック
            this.CheckProtectM();
        }

        /// <summary>
        /// キーダウン
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F8:
                    this.btnProtect.PerformClick();
                    break;
                case Keys.F10:
                    this.btnLicense.PerformClick();
                    break;
                case Keys.F11:
                    this.btnCopy.PerformClick();
                    break;
                case Keys.F12:
                    this.btnEnd.PerformClick();
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        /// <summary>
        /// [F9]メール送信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMail_Click(object sender, EventArgs e)
        {
            // メーラーによって文字コードが異なる為、
            // メーラー起動 + 宛先 + タイトルまでがいいかも
            // 本文は[F10]コピーボタン -> 貼り付けで対応が良い

            Encoding enc = Encoding.GetEncoding("shift_jis");

            // メーラー名
            string name = (string)Microsoft.Win32.Registry.CurrentUser
                .OpenSubKey(@"Software\Clients\Mail").GetValue(String.Empty);
            if (name.Contains("Thunderbird"))
            {
                enc = Encoding.UTF8;
            }

            //宛先
            string to = "support@e-mall.co.jp";

            //題名
            string subject = "構成情報適用完了通知";
            subject = System.Web.HttpUtility.UrlEncode(subject, enc);
            
            //本文
            string body = AppConfig.VersionInfo.ToString();
            body = System.Web.HttpUtility.UrlEncode(body, enc);
            //body = System.Web.HttpUtility.UrlPathEncode(body);
            
            //CC
            //string cc = "cc@sample.com";

            //標準のメールクライアントを開く
            System.Diagnostics.Process.Start(
                string.Format("mailto:{0}?subject={1}&body={2}",to, subject, body));
        }

        /// <summary>
        /// [F10]ライセンス更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLicense_Click(object sender, EventArgs e)
        {
            try
            {
                // 構成情報
                M_USER_RESTRICT UserRestrict = DaoInitUtility.GetComponent<IM_USER_RESTRICTDao>().GetSign();
                // 公開鍵
                M_CUSTOMER_KEY CustomerKey = DaoInitUtility.GetComponent<IM_CUSTOMER_KEYDao>().GetCustomerKey();

                // ファイル選択ダイアログ
                OpenFileDialog ofd = ShowOpenFileDialog();
                if (ofd.ShowDialog() != DialogResult.OK) { return; }

                // Scriptから構成情報の部分を取得
                string userRestrictSqlValue = File.ReadAllText(ofd.FileName, Encoding.UTF8);
                int confStartIndex = userRestrictSqlValue.IndexOf("'") + 1;
                int confEndIndex = userRestrictSqlValue.IndexOf("'", confStartIndex);
                string confValueBase64Compress 
                    = userRestrictSqlValue.Substring(confStartIndex, confEndIndex - confStartIndex);
                // 圧縮&Base64戻し処理
                string newConfigValue = StringFromBase64EncodeAndCompress(confValueBase64Compress);
                string publicKeyValue = StringFromBase64EncodeAndCompress(CustomerKey.CUSTOMER_KEY);

                // DBに登録されている公開鍵を使い、検証
                URXmlDocument NewConfURXmlDoc = new URXmlDocument();
                NewConfURXmlDoc.LoadXml(newConfigValue);
                if (!NewConfURXmlDoc.Verify(publicKeyValue))
                {
                    MessageBox.Show("選択したScriptファイルの構成情報とデータベースに登録されている公開鍵が一致しません。", 
                                    Constans.ERROR_TITLE, 
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 既存構成情報
                URXmlDocument URXmlDoc = new URXmlDocument();
                URXmlDoc.LoadXml(StringFromBase64EncodeAndCompress(UserRestrict.URINFO));
                Dictionary<int, Dictionary<string, string>> oldConfDictionary = GetConfDictionary(URXmlDoc);
                // 新規構成情報
                Dictionary<int, Dictionary<string, string>> newConfDictionary = GetConfDictionary(NewConfURXmlDoc);
                // 差分表示
                if (!ShowVersionInfoDialogDiff(oldConfDictionary, newConfDictionary))
                {
                    return;
                }

                // DB登録実行
                int insCnt = DaoInitUtility.GetComponent<IM_USER_RESTRICTDao>().Insert(userRestrictSqlValue);
                if (insCnt > 0)
                {
                    string errorMessage = string.Empty;
                    AppConfig.setUserRestrictItems(NewConfURXmlDoc, string.Empty, string.Empty, string.Empty, string.Empty ,ref errorMessage);
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        MessageBox.Show(errorMessage, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    // 適用した構成情報表示
                    this.rtbVersionInfo.Clear();
                    this.rtbVersionInfo.Text = AppConfig.VersionInfo.ToString();
                    CreateRitchText();

                    // ログにVersion情報出力
                    LogUtility.Info(Environment.NewLine +
                                    "================= バージョン情報 (構成情報更新) =================" +
                                    Environment.NewLine +
                                    AppConfig.VersionInfo +
                                    "================= バージョン情報 (構成情報更新) =================");

                    // Seriesとかオプションの関係で将軍の再起動が必要な気がする
                    /*
                    if (MessageBox.Show("新しい構成情報の適用が完了しました。" + Environment.NewLine +
                                        "Series、オプション変更の適用には環境将軍Rの再ログインが必要です。" + Environment.NewLine + 
                                        "再起動しますか?", Constans.CONFIRM_TITLE, 
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        r_framework.FormManager.FormManager.ExitShougunR(this);

                        // 新しいプロセスを起動するためMutexを解除
                        if (r_framework.FormManager.FormManager.gMutex != null)
                        {
                            r_framework.FormManager.FormManager.gMutex.ReleaseMutex();
                        }

                        // 新しいプロセスの起動
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo.FileName = Application.ExecutablePath;
                        process.Start();
                    }
                    else
                    {
                        MessageBox.Show("Series、オプションの変更は次回ログイン時から反映されます。",
                                        "インフォメーション", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    */

                    MessageBox.Show("新しい構成情報の適用が完了しました。" + Environment.NewLine +
                                    "Series、オプション変更の適用には環境将軍Rの再ログインが必要です。",
                                    "インフォメーション", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //this.btnMail.Enabled = true;
                    //this.btnMail.Visible = true;
                }
                else
                {
                    // 登録件数が0件の場合、失敗
                    MessageBox.Show("新しい構成情報の適用に失敗しました。" + 
                                    Environment.NewLine +
                                    "再度お試しください。",
                                    Constans.ERROR_TITLE, 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// [F11]コピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.rtbVersionInfo.Text);
        }

        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// リッチテキストボックスに表示する文字の修飾
        /// </summary>
        private void CreateRitchText()
        {
            string[] line = rtbVersionInfo.Lines;
            int start = rtbVersionInfo.SelectionStart;
            int fromLength = 0;
            int toLength = 0;

            foreach (string str in line)
            {
                toLength += str.Length;

                // 【】で囲った箇所
                if (str.IndexOf("】") > 0)
                {
                    rtbVersionInfo.SelectionStart = fromLength;
                    rtbVersionInfo.SelectionLength = str.Length;
                    // 文字背景色
                    rtbVersionInfo.SelectionBackColor = Color.ForestGreen;
                    // 文字色
                    rtbVersionInfo.SelectionColor = Color.White;
                    Font fnt = new Font(rtbVersionInfo.SelectionFont.FontFamily,
                                        rtbVersionInfo.SelectionFont.Size,
                                        rtbVersionInfo.SelectionFont.Style | FontStyle.Bold);
                    rtbVersionInfo.SelectionFont = fnt;
                    fnt.Dispose();
                    rtbVersionInfo.SelectionFont.Dispose();
                }

                // 項目
                int valueFindIndex = str.IndexOf(":");
                if (valueFindIndex > 0)
                {
                    // 項目名
                    rtbVersionInfo.SelectionStart = fromLength;
                    rtbVersionInfo.SelectionLength = valueFindIndex + 1;
                    rtbVersionInfo.SelectionColor = Color.ForestGreen;
                    Font fnt = new Font(rtbVersionInfo.SelectionFont.FontFamily,
                                        rtbVersionInfo.SelectionFont.Size,
                                        rtbVersionInfo.SelectionFont.Style | FontStyle.Regular);
                    rtbVersionInfo.SelectionFont = fnt;

                    // 値
                    rtbVersionInfo.SelectionStart = fromLength + valueFindIndex + 1;
                    rtbVersionInfo.SelectionLength = str.Length - valueFindIndex;
                    rtbVersionInfo.SelectionFont = fnt;
                    fnt.Dispose();
                    rtbVersionInfo.SelectionFont.Dispose();
                    rtbVersionInfo.Select(0, start);
                }

                fromLength += str.Length + 1;
            }
        }

        /// <summary>
        /// ファイル選択ダイアログ表示
        /// </summary>
        /// <returns>OpenFileDialog</returns>
        private OpenFileDialog ShowOpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "構成情報のScriptファイルを選択してください。";
            ofd.FileName = "Shougun.UserRestrict.sql";
            ofd.Filter = "SQLファイル(.sql)|*.sql";

            return ofd;
        }

        /// <summary>
        /// 圧縮&Base64エンコードした文字列を元の文字列に戻す
        /// </summary>
        /// <param name="compressString">圧縮&Base64エンコード文字列</param>
        /// <returns>圧縮&Base64エンコード前の文字列</returns>
        private string StringFromBase64EncodeAndCompress(string compressString)
        {
            byte[] byteCompressString = System.Convert.FromBase64String(compressString);

            // 入出力用のストリームを生成します
            MemoryStream memoryStream = new MemoryStream(byteCompressString);
            MemoryStream writeMemoryStream = new MemoryStream();

            DeflateStream compressedStream = new DeflateStream(memoryStream, CompressionMode.Decompress);

            //　MemoryStream に展開します
            while (true)
            {
                int readByte = compressedStream.ReadByte();
                // 読み終わったとき while 処理を抜けます
                if (readByte == -1)
                {
                    break;
                }
                // メモリに展開したデータを読み込みます
                writeMemoryStream.WriteByte((byte)readByte);
            }

            string result = System.Text.Encoding.Unicode.GetString(writeMemoryStream.ToArray());

            return result;
        }

        /// <summary>
        /// 差分ダイアログ表示
        /// </summary>
        /// <param name="oldConf">既存構成情報Dictionary</param>
        /// <param name="newConf">新規構成情報Dictionary</param>
        /// <returns>
        /// true  : 登録する
        /// false : キャンセル または 既に最新の為、登録の必要なし
        /// </returns>
        private bool ShowVersionInfoDialogDiff(Dictionary<int, Dictionary<string, string>> oldConfDic,
                                               Dictionary<int, Dictionary<string, string>> newConfDic)
        {
            bool result = false;

            var diffDialog = new VersionInfoDialogDiff(oldConfDic, newConfDic);
            if (!diffDialog.hasDiff)
            {
                MessageBox.Show("既に最新の構成情報です。",
                                "インフォーメーション",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }

            DialogResult diagResult = diffDialog.ShowDialog();
            diffDialog.Dispose();
            diffDialog = null;
            
            if (diagResult == DialogResult.OK)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 構成情報の設定値をDictionaryで返す
        /// </summary>
        /// <returns>
        ///  Disctionary<int, Dictionary<string, string>>
        ///    key   -> index(EnumItemsで取得したID配列のIndex)
        ///    value -> Dictionary<strin, string>
        ///               key   -> caption(説明)
        ///               value -> itemValue(値)
        /// </returns>
        private Dictionary<int, Dictionary<string, string>> GetConfDictionary(URXmlDocument URXmlDoc)
        {
            Dictionary<int, Dictionary<string, string>> returnDic
                = new Dictionary<int, Dictionary<string, string>>();

            string[] idArray = URXmlDoc.EnumItems(string.Empty);

            for (int i = 0; i < idArray.Length; i++)
            {
                if (URXmlDoc.GetItem(idArray[i]).group != "comment")
                {
                    Dictionary<string, string> nestDic = new Dictionary<string, string>();

                    if (idArray[i] == "license")
                    {
                        // ID = ライセンスの場合、ライセンス名を取得し、Dictionaryに詰める
                        nestDic.Add(URXmlDoc.GetItem(idArray[i]).caption,
                                   AppConfig.GetLicenseName(URXmlDoc.GetItemValue(idArray[i]).ToString()));
                    }
                    else if (URXmlDoc.GetItem(idArray[i]).group == "option")
                    {
                        // オプションの場合、値がBoolean型なのでON/OFFに変換
                        nestDic.Add(URXmlDoc.GetItem(idArray[i]).caption,
                                   Convert.ToBoolean(URXmlDoc.GetItemValue(idArray[i]).ToString()) ? "ON" : "OFF");
                    }
                    else
                    {
                        // 上記以外
                        nestDic.Add(URXmlDoc.GetItem(idArray[i]).caption,
                                   URXmlDoc.GetItemValue(idArray[i]).ToString());
                    }
                    returnDic.Add(i, nestDic);
                }
            }
            return returnDic;
        }

        /// <summary>
        /// TeamViewerのダウンロード先のリンクによってローカル端末のブラウザを表示します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupport_Click(object sender, EventArgs e)
        {
            try
            {
                var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

                string url = this.mSysInfo.SUPPORT_TOOL_URL_PATH;
                if (string.IsNullOrEmpty(url))
                    return;

                if (SystemProperty.IsTerminalMode)
                {
                    if (string.IsNullOrEmpty(Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectoryNonMsg()))
                    {
                        MessageBox.Show("表示を行う前に、印刷設定の出力先フォルダを指定してください。",
                                        "アラート",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        return;
                    }
                    string clientFilePathInfo = Path.Combine(Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectory(), "ClientFilePathInfo2.txt");

                    //5回ファイル作成を試す					
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            using (var writer = new StreamWriter(clientFilePathInfo, false, Encoding.UTF8))
                            {
                                writer.Write(url);
                            }
                            break;
                        }
                        catch (Exception exc)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Process.Start(url);
                }
            }
            catch (Exception ex)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is Win32Exception)
                {
                    // SQLエラー用メッセージを出力
                    msgLogic.MessageBoxShow("E200");
                    return;
                }
                else
                {
                    LogUtility.Error("btnSupport_Click", ex);
                    msgLogic.MessageBoxShow("E245", "");
                    return;
                }
            }
        }

        /// <summary>
        /// プロテクトキー解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProtect_Click(object sender, EventArgs e)
        {

            //プロテクト解除ボタンが使用不可の場合は開かない
            if (this.btnProtect.Enabled == false)
            {
                return;
            }

            try
            {
                //R将軍起動フォルダ内にある、ShogunProtect_Keyk.exeを管理者権限で起動
                var proc = new System.Diagnostics.Process();
                var lockDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ShougunProtect.Program.ProtectExe);
                proc.StartInfo.Arguments = "/K";
                proc.StartInfo.FileName = lockDir;
                proc.StartInfo.Verb = "RunAs";          //Verbへ"RunAs"を指定する
                proc.StartInfo.UseShellExecute = true;  //動詞(Verb)を利用する場合は、UseShellExecuteがTrue
                proc.Start();
            }
            catch (Exception ex)
            {
                //起動確認ダイアログでいいえを選択するとエラーになる為。
            }
        }

        /// <summary>
        /// プロテクト解除のボタンの使用可不可
        /// 使用可→ライセンスが正規版）限定メニュー起動。または、KEY登録無し(30日の体験版期間を想定)
        /// 使用不可→ライセンスが正規版以外又は、通常起動（プロテクト実行無し。ターミナルモード。プロテクト解除後。）
        /// </summary>
        private void  CheckProtectM()
        {

            //ライセンスが正規版のみ、プロテクト解除可とする
            if ((AppConfig.License) != "1")
            {
                this.btnProtect.Enabled = false;
                return;
            }

            if (AppConfig.ProtectMode)
            {
                //通常起動は、起動不可
                this.btnProtect.Enabled = false;

                //通常起動でもkey無しは起動可(30日体験版期間内)
                //プロテクト実行→実行しない場合は抜ける
                if (!AppConfig.IsProtectRun)
                {
                    return;
                }
                //オンプレチェック(クラウド環境は抜ける)
                if (AppConfig.IsTerminalMode)
                {
                    return;
                }
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Royknak");
                if (key.GetValue(ShougunProtect.Program.KeyV, "") == "")
                {
                    this.btnProtect.Enabled = true;
                }
            }
            else
            {
                //限定メニューの時は起動可
                this.btnProtect.Enabled = true;
            }
        }
    }
}
