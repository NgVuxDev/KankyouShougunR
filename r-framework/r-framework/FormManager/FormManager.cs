using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Menu;
using r_framework.OriginalException;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using r_framework.Dto;
using Shougun.Core.ExternalConnection.CommunicateLib;
using r_framework.CommunicateApp;
using r_framework.Xml;
using r_framework.Entity;

/// <summary>
/// 将軍フォームマネージャ
/// </summary>
namespace r_framework.FormManager
{
    /// <summary>
    /// 将軍フォームマネージャクラス
    /// </summary>
    public class FormManager
    {
        /// <summary>
        /// メニューから呼ばれた画面であることを示す文字列
        /// 画面引数の1つ目に入れてFormManagerに渡す
        /// </summary>
        public static readonly string CALLED_MENU = "CALLED_MENU";

        /// <summary>
        /// モーダル画面から呼び出し元に渡す値を保持するフィールド
        /// </summary>
        public static object[] ModalFormReturnParams { get; private set; }

        /// <summary>
        /// 指定シリーズの該当画面を有効化する
        /// </summary>
        /// <param name="seriesId">シリーズID</param>
        /// <retrun>利用可:True、利用不可:False</retrun>
        internal static void EnableSeriesForms(string seriesId)
        {
            initialize();
            instance.loadFormMapOfSeries(seriesId);
        }

        /// <summary>
        /// 指定オプションの該当画面を有効化する
        /// </summary>
        /// <param name="optionsIds">有効なオプションIDの配列</param>
        /// <retrun>利用可:True、利用不可:False</retrun>
        internal static void EnableOptionForms(string[] optionIds)
        {
            initialize();
            instance.loadFormMapOfOptions(optionIds);
        }

        /// <summary>
        /// FormOpen系で作成されたメインフレームウインドウがDispose時に呼び出す。
        /// FormManagerの管理から削除し早めにGCに回収させる目的。
        /// </summary>
        /// <param name="form">呼び出し元のForm</param>
        /// <retrun>なし</retrun>
        public static void NotifyDisposingForm(Form form)
        {
            if (form != null)
            {
                LogUtility.Info(form.Text);
                //QN_QUAN add 20210104 #158953 S
                var name_form = form.Text.Replace(" - " + r_framework.Dto.SystemProperty.NameFormName, "").Trim();
                if (!name_form.Equals("宛名ラベル") && !name_form.Equals(""))
                {
                    FormManager.InserDBLog(name_form);
                }
                //QN_QUAN add 20210104 #158953 E
            }
            initialize();
            instance.puageDisposingForm(form);
        }


        /// <summary>
        /// フォーム有効問い合わせ
        /// 指定された画面・帳票IDが有効か無効かを取得する。
        /// シリーズ・オプション定義で利用可能な場合はTrue、利用不可の場合はFalseを返却する。
        /// 未定義の場合は利用不可とする。
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// <retrun>利用可:True、利用不可:False</retrun>
        public static bool IsEnabledForm(string formID)
        {
            initialize();

            bool result = instance.isEnabledForm(formID);

            return result;
        }

        /// <summary>
        /// フォームオープン(モードレス)
        /// 指定された画面IDのFormを生成・表示する。
        /// 呼び出し元から渡された可変引数がそのまま渡される。
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <retrun>成功:true、失敗:false</retrun>
        public static bool OpenForm(string formID, params object[] args)
        {
            LogUtility.DebugMethodStart(formID, args);
            initialize();

            //bool canExecute = FormManager.CanExecute(formID, args);
            //bool result = canExecute ? instance.openForm(formID, args) : false;
            bool result = instance.openForm(formID, args);

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// フォームオープン(モーダル)
        /// 指定された画面IDのFormを生成・表示する。
        /// 呼び出し元から渡された可変引数がそのまま渡される。
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <retrun>成功:true、失敗:false</retrun>
        public static bool OpenFormModal(string formID, params object[] args)
        {
            LogUtility.DebugMethodStart(formID, args);
            initialize();

            //bool canExecute = FormManager.CanExecute(formID, args);
            //bool result = canExecute ? instance.openFormModal(formID, args) : false;
            bool result = instance.openFormModal(formID, args);

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// ダイアログオープン
        /// 指定された画面IDのFormを生成・表示する。
        /// 呼び出し元から渡された可変引数がそのまま渡される。
        /// </summary>
        /// <param name="formID">画面ID(ex."R160")</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <retrun>成功:true、失敗:false</retrun>
        public static bool OpenDialog(string formID, params object[] args)
        {
            LogUtility.DebugMethodStart(formID, args);
            initialize();

            //bool canExecute = FormManager.CanExecute(formID, args);
            //bool result = canExecute ? instance.openDialog(formID, args) : false;
            bool result = instance.openDialog(formID, args);

            LogUtility.DebugMethodEnd(result);
            return result;
        }
        
        #region 権限対応版

        /// <summary>
        /// フォームオープン(モードレス)
        /// 指定された画面IDのFormを生成・表示する。
        /// 呼び出し元から渡された可変引数がそのまま渡される。
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// <param name="windowType">モード</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <retrun>成功:true、失敗:false</retrun>
        public static bool OpenFormWithAuth(string formID, WINDOW_TYPE windowType, params object[] args)
        {
            return OpenFormWithAuth(formID, WINDOW_ID.NONE, windowType, args);
        }

        /// <summary>
        /// フォームオープン(モードレス)
        /// 指定された画面IDのFormを生成・表示する。
        /// 呼び出し元から渡された可変引数がそのまま渡される。
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// <param name="windowId">画面ID</param>
        /// <param name="windowType">モード</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <retrun>成功:true、失敗:false</retrun>
        public static bool OpenFormWithAuth(string formID, WINDOW_ID windowId, WINDOW_TYPE windowType, params object[] args)
        {
            LogUtility.DebugMethodStart(formID, windowId, windowType, args);
            initialize();

            bool canExecute = Authority.Manager.CheckAuthority(formID, windowType, windowId);
            bool result = canExecute ? instance.openForm(formID, args) : false;

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// フォームオープン(モーダル)
        /// 指定された画面IDのFormを生成・表示する。
        /// 呼び出し元から渡された可変引数がそのまま渡される。
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// /// <param name="windowType">モード</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <retrun>成功:true、失敗:false</retrun>
        public static bool OpenFormModalWithAuth(string formID, WINDOW_TYPE windowType, params object[] args)
        {
            return OpenFormModalWithAuth(formID, WINDOW_ID.NONE, windowType, args);
        }

        /// <summary>
        /// フォームオープン(モーダル)
        /// 指定された画面IDのFormを生成・表示する。
        /// 呼び出し元から渡された可変引数がそのまま渡される。
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// <param name="windowId">画面ID</param>
        /// /// <param name="windowType">モード</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <retrun>成功:true、失敗:false</retrun>
        public static bool OpenFormModalWithAuth(string formID, WINDOW_ID windowId, WINDOW_TYPE windowType, params object[] args)
        {
            LogUtility.DebugMethodStart(formID, windowId, windowType, args);
            initialize();

            bool canExecute = Authority.Manager.CheckAuthority(formID, windowType, windowId);
            bool result = canExecute ? instance.openFormModal(formID, args) : false;

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// ダイアログオープン
        /// 指定された画面IDのFormを生成・表示する。
        /// 呼び出し元から渡された可変引数がそのまま渡される。
        /// </summary>
        /// <param name="formID">画面ID(ex."R160")</param>
        /// /// <param name="windowType">モード</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <retrun>成功:true、失敗:false</retrun>
        public static bool OpenDialogWithAuth(string formID, WINDOW_TYPE windowType, params object[] args)
        {
            return OpenDialogWithAuth(formID, WINDOW_ID.NONE, windowType, args);
        }

        /// <summary>
        /// ダイアログオープン
        /// 指定された画面IDのFormを生成・表示する。
        /// 呼び出し元から渡された可変引数がそのまま渡される。
        /// </summary>
        /// <param name="formID">画面ID(ex."R160")</param>
        /// <param name="windowId">画面ID</param>
        /// /// <param name="windowType">モード</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <retrun>成功:true、失敗:false</retrun>
        public static bool OpenDialogWithAuth(string formID, WINDOW_ID windowId, WINDOW_TYPE windowType, params object[] args)
        {
            LogUtility.DebugMethodStart(formID, windowId, windowType, args);
            initialize();

            bool canExecute = Authority.Manager.CheckAuthority(formID, windowType, windowId);
            bool result = canExecute ? instance.openDialog(formID, args) : false;

            LogUtility.DebugMethodEnd(result);
            return result;
        }
        #endregion

        #region Communicate InxsAubApplication

        /// <summary>
        /// Open InxsSubApp Form
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// <param name="windowType">モード</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        public static void OpenFormSubApp(string formID, WINDOW_TYPE windowType, params object[] args)
        {
            if (!r_framework.Configuration.AppConfig.AppOptions.IsInxs())
            {
                var messageBoxShowLogic = new MessageBoxShowLogic();
                messageBoxShowLogic.MessageBoxShow("E294");
            }
            else
            {
                if (string.IsNullOrEmpty(formID))
                {
                    return;
                }
                CommunicateAppDto communicateAppDto = new CommunicateAppDto()
                {
                    FormID = formID,
                    WindowType = windowType,
                    ShainCD = SystemProperty.Shain.CD,
                    Type = Shougun.Core.ExternalConnection.CommunicateLib.Enums.NotificationType.OpenForm,
                    Args = args
                };
                RemoteAppCls remoteAppCls = new RemoteAppCls();
                if (!remoteAppCls.OpenForm(Constans.StartFormText, communicateAppDto))
                {
                    LauncherDto launcherDto = new LauncherDto()
                    {
                        ShainCD = SystemProperty.Shain.CD,
                        ShougunConfigPath = r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath,
                        Args = communicateAppDto
                    };
                    InxsLauncher.LaunchSupApp(launcherDto);
                }
            }
        }

        /// <summary>
        /// Open InxsSubApp Dialog
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// <param name="windowType">モード</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        public static void OpenDialogSubApp(string formID, WINDOW_TYPE windowType, params object[] args)
        {
            if (!r_framework.Configuration.AppConfig.AppOptions.IsInxs())
            {
                var messageBoxShowLogic = new MessageBoxShowLogic();
                messageBoxShowLogic.MessageBoxShow("E294");
            }
            else
            {
                if (string.IsNullOrEmpty(formID))
                {
                    return;
                }
                CommunicateAppDto communicateAppDto = new CommunicateAppDto()
                {
                    FormID = formID,
                    WindowType = windowType,
                    ShainCD = SystemProperty.Shain.CD,
                    Type = Shougun.Core.ExternalConnection.CommunicateLib.Enums.NotificationType.OpenDialog,
                    Args = args
                };
                RemoteAppCls remoteAppCls = new RemoteAppCls();
                if (!remoteAppCls.OpenDialog(Constans.StartFormText, communicateAppDto))
                {
                    LauncherDto launcherDto = new LauncherDto()
                    {
                        ShainCD = SystemProperty.Shain.CD,
                        ShougunConfigPath = r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath,
                        Args = communicateAppDto
                    };
                    InxsLauncher.LaunchSupApp(launcherDto);
                }
            }
        }

        #endregion Communicate InxsAubApplication


        /// <summary>
        /// フォーム更新
        /// 指定された画面IDのForm表示内容を更新する。
        /// 修正画面で登録確定した場合に一覧画面を指定して呼び出す。
        /// 指定画面のSgFormInterface.UpdateForm()が呼び出される。
        /// </summary>
        /// <param name="formID">画面ID(ex."G001")</param>
        /// <retrun>成功:true、失敗:false</retrun>>
        public static bool UpdateForm(string formID)
        {
            LogUtility.DebugMethodStart(formID);
            initialize();
            bool result = instance.updateForm(formID);
            LogUtility.DebugMethodEnd(result);
            return result;
        }

        // No3953-->
        [DllImport("user32.dll")]
        extern static bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// 再起動処理の為Mutex解除を移動
        /// </summary>
        public static System.Threading.Mutex gMutex = null;

        /// <summary>
        /// 管理下のフォームを全て終了
        /// アプリケーションの再起動
        /// Loginフォームが表示するまで終了待機
        /// </summary>
        /// <param name="form">オーナー画面</param>
        public static void CloseAllForm(Form form)
        {
            initialize();
            try
            {
                // ログアウト確認
                var messageShowLogic = new MessageBoxShowLogic();
                if (DialogResult.Yes != messageShowLogic.MessageBoxShow("C055", "ログアウト"))
                {
                    return;
                }

                Exception ex;
                int errorNumber;
                if (!instance.UpdateLoginCounter(out errorNumber, out ex))
                {
                    LogUtility.Error(string.Format("ログインカウンタの更新処理でエラーが発生しました。社員CD : {0}",
                                     Dto.SystemProperty.Shain.CD), ex);

                    if (errorNumber == -2)
                    {
                        // タイムアウト
                        MessageBox.Show("タイムアウトしました。時間を置いてから再度お試しください。",
                                        Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (MessageBox.Show("ログアウト処理でエラーが発生しました。" +
                                        Environment.NewLine +
                                        "再度ログアウト処理を行う場合は「はい」を選択後、" +
                                        "メニューから「ログアウト」→「ログアウト」を選択してください。",
                                        Constans.ERROR_TITLE,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        return;
                    }
                }

                // 管理下のフォームを終了
                instance.closeAllForm();

                // 新しいプロセスを起動するためMutexを解除
                if (gMutex != null)
                {
                    gMutex.ReleaseMutex();
                }

                // 新しいプロセスの起動
                Process process = new Process();
                process.StartInfo.FileName = Application.ExecutablePath;
                process.Start();

                // 新しいプロセスのフォーム（ログイン画面）が表示されるまで待機
                process.WaitForInputIdle();
                while (!IsWindowVisible(process.MainWindowHandle))
                {
                    System.Threading.Thread.Sleep(10);
                    System.Windows.Forms.Application.DoEvents();
                }

                // 自プロセスの終了
                Application.Exit();
            }
            catch (Exception ex)
            {
                // 例外発生時はログを残して終了する。
                LogUtility.Fatal(string.Empty, ex);
                Application.Exit();
            }
        }
        // No3953<--

        /// <summary>
        /// アプリケーション終了手続き
        /// </summary>
        /// <param name="form">オーナー画面</param>
        public static void ExitShougunR(Form form)
        {
            try
            {
                var messageShowLogic = new MessageBoxShowLogic();
                if (DialogResult.Yes == messageShowLogic.MessageBoxShow("C055", "「" + GetApplicationTitle() + "」を終了"))
                {
                    Exception ex;
                    int errorNumber;
                    if (!instance.UpdateLoginCounter(out errorNumber, out ex))
                    {
                        LogUtility.Error(string.Format("ログインカウンタの更新処理でエラーが発生しました。社員CD : {0}",
                                         Dto.SystemProperty.Shain.CD), ex);

                        if (errorNumber == -2)
                        {
                            // タイムアウト
                            MessageBox.Show("タイムアウトしました。時間を置いてから再度お試しください。",
                                            Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (MessageBox.Show("終了処理でエラーが発生しました。" +
                                            Environment.NewLine +
                                            "再度終了処理を行う場合は「はい」を選択後、" +
                                            "メニューから「ログアウト」→「終了」を選択してください。",
                                            Constans.ERROR_TITLE,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            return;
                        }
                    }
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                // 例外発生時はログを残して終了する。
                LogUtility.Fatal(ex);
                Application.Exit();
            }
        }

        public static string pathAfterDownload = "";
        public static void DownLoadShougunR(Form form)
        {
            try
            {
                /*string url = @"http://download.teamviewer.com/download/TeamViewer_Setup_vi-iod.exe";

                if (File.Exists(@"D:\Temp\linhtinh\TeamViewer_Setup_vi-iod.exe"))
                {
                    System.Diagnostics.Process.Start(@"D:\Temp\linhtinh\TeamViewer_Setup_vi-iod.exe");
                }
                else
                {
                    // Create an instance of WebClient
                    WebClient client = new WebClient();
                    // Hookup DownloadFileCompleted Event
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    // Start the download and copy the file to c:\temp
                    client.DownloadFileAsync(new Uri(url), @"D:\Temp\linhtinh\TeamViewer_Setup_vi-iod.exe");
                }*/

                string url = @"http://download.teamviewer.com/download/TeamViewer_Setup_vi-iod.exe";
                var dialog = new SaveFileDialog();
                dialog.Filter = "files (*.exe)|*.exe|All files (*.*)|*.*";
                dialog.FileName = "TeamViewer_Setup_vi-iod.exe";
                //dialog.Filter = "Archive (*.rar)|*.rar";

                var result = dialog.ShowDialog(); //shows save file dialog
                if (result == DialogResult.OK)
                {
                    pathAfterDownload = dialog.FileName;
                    WebClient client = new WebClient();
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new Uri(url), dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
            }
        }

        public static void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("File downloaded");
            //System.Diagnostics.Process.Start(@"D:\Temp\linhtinh\TeamViewer_Setup_vi-iod.exe");
            System.Diagnostics.Process.Start(pathAfterDownload);
            pathAfterDownload = "";
        }

        /// <summary>
        /// ModalFormReturnParamsに値をセットします。
        /// OpenFormModal()によって呼び出された画面からのみ呼出可能です。
        /// それ以外は例外をスローします。
        /// </summary>
        /// <param name="callForm">呼出画面のインスタンス</param>
        /// <param name="args">セットする値の可変長配列</param>
        public static void SetReturnParams(Form callForm, params object[] args)
        {
            Form modalForm = instance.openedFormInfoList.FirstOrDefault(n => n.Form.Modal && callForm.Equals(n.Form)).Form;

            if (modalForm == null)
            {
                // メソッド呼出元画面がFormManagerから呼ばれたMoal画面でない場合、例外をスローします。
                throw new Exception();
            }

            // 値をセット
            ModalFormReturnParams = args;
        }

        /// <summary>
        /// <para>アセンブリ内でIShougunFormを使用している最初のクラス名を取得</para>
        /// <para>1アセンブリ、1FormIDが前提</para>
        /// </summary>
        /// <param name="exeAssm">System.Reflection.Assembly.GetExecutingAssembly()</param>
        /// <returns>FormID(Gxxx/Rxxx)</returns>
        public static string GetFormID(System.Reflection.Assembly exeAssm)
        {
            var inf = exeAssm.GetTypes()
                             .Where(s => s.GetInterfaces().Contains(typeof(r_framework.FormManager.IShougunForm)))
                             .FirstOrDefault();
            return inf == null ? String.Empty : inf.Name;
        }

        #region private

        static FormManager instance = null;

        /// <summary>
        /// 初期化。インスタンスをひとつ生成する。
        /// </summary>
        private static void initialize()
        {
            if (instance == null)
            {
                instance = new FormManager();
            }
        }

        /// <summary>
        /// フォーム/アセンブリ定義マップ
        /// ShogunFormAssembly.xmlの内容を格納する。
        /// </summary>
        private Dictionary<string, FormAssembly> formAssembliesMap = null;

        /// <summary>
        /// フォーム/インタフェース読み込みマップ
        /// フォーム識別子ごとのインタフェースを保持しておく。
        /// </summary>
        private Dictionary<string, IShougunForm> formInterfacesMap = null;

        /// <summary>
        /// オープン中フォームのリスト
        /// OepnFormで作成されたフォームを格納し管理する。
        /// </summary>
        private List<FormInfo> openedFormInfoList = null;

        /// <summary>
        /// フォーム有効無効辞書
        /// シリーズ・オプション定義からフォーム識別子ごとの有効無効状態を保持しておく。
        /// </summary>
        private Dictionary<string, bool> enabledFormMap = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private FormManager()
        {
            this.loadShogunFormAssemblyXml();
            this.enabledFormMap = new Dictionary<string, bool>();
            this.formInterfacesMap = new Dictionary<string, IShougunForm>();
            this.openedFormInfoList = new List<FormInfo>();
        }

        /// <summary>
        /// ShogunFormAssembly.xmlを読み込みMapに格納する
        /// </summary>
        private void loadShogunFormAssemblyXml()
        {
            // menu.xmlから読み込み
            // TODO:id属性を追加して読み込みたいがうまくいかない
            //var menu = new Setting.MenuSetting();
            //var ribbonDto = menu.LoadMenuSetting();

            // 組み込みのXMLファイルから読み込む
            this.formAssembliesMap = new Dictionary<string, FormAssembly>();
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(FormAssembly[]));
            var myAssembly = Assembly.GetExecutingAssembly();
            using (var stream = myAssembly.GetManifestResourceStream("r_framework.FormManager.FormAssembly.xml"))
            {
                using (var reader = new StreamReader(stream))
                {
                    FormAssembly[] array = (FormAssembly[])serializer.Deserialize(reader);
                    foreach (var item in array)
                    {
                        this.formAssembliesMap.Add(item.FormID, item);
                    }
                }
            }
        }

        /// <summary>
        /// シリーズに対応した有効画面マップを読み込む
        /// </summary>
        private void loadFormMapOfSeries(string seriesId)
        {
            // TODO:メニュー一本化の際にシリーズ別の有効画面一覧リソースを作成し下記コードを有効にする 
            /*
            var mapResourceName = "r_framework.FormManager.FormMapOfSeries" + seriesId + ".txt";
            
            var myAssembly = Assembly.GetExecutingAssembly();

            using (var stream = myAssembly.GetManifestResourceStream(mapResourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var formId = reader.ReadLine().Trim();
                        formId = formId.Remove(formId.IndexOf("//")).Trim();
                        if (formId.Length > 0)
                        {
                            this.enabledFormMap.Add(formId, true);
                        }
                    }
                }
            }
            */
        }

        /// <summary>
        /// 有効オプションに対応した有効画面マップを読み込む
        /// </summary>
        private void loadFormMapOfOptions(string[] enableOptionIds)
        {
            this.enabledFormMap.Clear();
            var mapResourceName = "r_framework.FormManager.FormMapOfOptions.txt";
            var myAssembly = Assembly.GetExecutingAssembly();
            using (var stream = myAssembly.GetManifestResourceStream(mapResourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    string optionId = null;
                    bool enable = false;
                    while (!reader.EndOfStream)
                    {
                        var formId = reader.ReadLine().Trim();
                        formId = formId.Remove(formId.IndexOf("//")).Trim();
                        if (formId.Length > 3 && formId[0] == '[')
                        {
                            optionId = formId.Substring(1, formId.Length - 2); // []の2文字削る
                            enable = enableOptionIds.Contains(optionId);
                        }
                        else if (optionId != null && formId.Length > 0)
                        {
                            // 20210312 マスタメニューの「将軍-INXS」の表示不具合 #147886 start
                            //this.enabledFormMap
                            if (this.enabledFormMap.ContainsKey(formId))
                            {
                                if (enable)
                                {
                                    this.enabledFormMap[formId] = enable;
                                }
                            }
                            else
                            {
                                this.enabledFormMap.Add(formId, enable);
                            }
                            // 20210312 マスタメニューの「将軍-INXS」の表示不具合 #147886 end
                        }
                    }
                }
            }
        }

        /// <summary>
        /// フォーム有効問い合わせ
        /// 指定された画面・帳票IDが有効か無効かを取得する。
        /// シリーズ・オプション定義で利用可能な場合はTrue、利用不可の場合はFalseを返却する。
        /// 未定義の場合は利用不可とする。
        /// </summary>
        /// <param name="formID">画面ID(ex."G160")</param>
        /// <retrun>利用可:True、利用不可:False</retrun>
        public bool isEnabledForm(string formID)
        {
            bool enabled = true;
            this.enabledFormMap.TryGetValue(formID, out enabled);
            return enabled;
        }

        /// <summary>
        /// フォームが実装されているかチェックする。
        /// </summary>
        /// <param name="FormID">画面識別子 ex) "G160", "M123"</param>
        /// <returns>true:存在する false:存在しない</returns>
        private bool isImplementForm(string FormID)
        {
            return true;
        }

        /// <summary>
        /// 指定されたフォームのフォームマップからの参照をクリアする
        /// （FormのDisposeから呼び出される。参照をクリアすることでGCに回収させる目的）
        /// </summary>
        private void puageDisposingForm(Form form)
        {
            foreach (var info in this.openedFormInfoList)
            {
                if (info.Form == form)
                {
                    info.Form = null;
                }
            }
        }

        /// <summary>
        /// フォームマップから非活性のフォームを掃除する。
        /// </summary>
        private void clearInactiveForms()
        {
            foreach (var info in this.openedFormInfoList)
            {
                Form form = info.Form as Form;
                if (form == null || form.IsDisposed)
                {
                    info.Form = null;
                }
            }

            this.openedFormInfoList.RemoveAll(info => info.Form == null);

            GC.Collect();
            LogUtility.Info(string.Format("GC.GetTotalMemory()={0}", GC.GetTotalMemory(false)));
        }

        /// <summary>
        /// 画面番号からインタフェースを取得する。
        /// </summary>
        private IShougunForm getFormInterface(string formID, out string caption)
        {
            caption = "";

            // 画面識別子に対応したアセンブリ名を解決
            if (!this.formAssembliesMap.ContainsKey(formID))
            {
                return null;
            }

            var formAssembly = this.formAssembliesMap[formID];
            caption = formAssembly.Caption;

            // 既にインタフェースを作成済みの場合はそれを返す
            if (this.formInterfacesMap.ContainsKey(formID))
            {
                return this.formInterfacesMap[formID];
            }

            // "namespace.(画面番号)"をクラス名としてインタフェースを作成
            IShougunForm formInterface = null;
            var assembly = Assembly.LoadFrom(formAssembly.AssemblyName);
            if (assembly != null)
            {
                var className = formAssembly.Namespace + "." + formID;
                formInterface = (IShougunForm)assembly.CreateInstance(
                        className, // 名前空間を含めたクラス名
                        true, // 大文字小文字を無視するかどうか
                        BindingFlags.CreateInstance, // インスタンスを生成
                        null, null, null, null);
                if (formInterface != null)
                {
                    this.formInterfacesMap.Add(formID, formInterface);
                }
            }
            return formInterface;
        }

        /// <summary>
        /// フォームオープン(モードレス)。指定された識別子のFormを生成・表示する。
        /// </summary>
        /// <param name="formID">画面識別子 ex) "G160", "M123"</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        private bool openForm(string formID, params object[] args)
        {
            this.clearInactiveForms();
            string caption;
            var formInterface = this.getFormInterface(formID, out caption);
            Form targetForm = null;

            // メニューから呼ばれたどうか調べる
            bool isCalledMenu = false;
            if (args.Count() > 0)
            {
                if (args.LastOrDefault().ToString() == FormManager.CALLED_MENU)
                {
                    args = args.Take(args.Length - 1).ToArray();
                    isCalledMenu = true;
                }
            }

            // 同じ画面番号、同じ内容を表示している画面がいるか調べる
            foreach (var info in this.openedFormInfoList)
            {
                if (formID.Equals(info.FormID))
                {
                    Form form = info.Form as Form; // インスタンスの生存確認
                    if (form != null && !form.IsDisposed && form.Visible)
                    {
                        // インタフェースで問い合わせ
                        if (formInterface.IsSameContentForm(form, args))
                        {
                            targetForm = form;
                            break;
                        }
                    }
                }
            }

            // 新規追加モードまたはオープン中の画面がなければ新規作成する
            if (targetForm == null)
            {
                // 最大数に達していた場合は
                // 20150714 メニューから呼出すか問わず、全体的に4画面以上開けないように修正 Start
                //if (this.openedFormInfoList.Count(n => n.IsCalledMenu) >= r_framework.Const.Constans.MAX_WINDOW_COUNT && isCalledMenu)
                // 計算する時、当該画面も数える(+1)
                //if (this.openedFormInfoList.Count + 1 >= r_framework.Const.Constans.MAX_WINDOW_COUNT)
                // 20150714 メニューから呼出すか問わず、全体的に4画面以上開けないように修正 End
                if (this.openedFormInfoList.Count + 1 >= r_framework.Dto.SystemProperty.Shain.MAX_WINDOW_COUNT)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E136");
                    return false;
                }

                targetForm = formInterface.CreateForm(args);
                if (targetForm != null)
                {
                    this.openedFormInfoList.Add(new FormInfo(formID, targetForm, formInterface, caption, isCalledMenu));
                    Form activeForm = Form.ActiveForm;
                    if (activeForm != null)
                    {
                        // マルチディスプレイ時に新規ウィンドウの表示を呼び出し元にする
                        targetForm.StartPosition = FormStartPosition.Manual;
                        // Location(X,Y)を親Formと同じにする
                        targetForm.SetBounds(activeForm.Bounds.Location.X,
                                             activeForm.Bounds.Location.Y,
                                             targetForm.Width,
                                             targetForm.Height);
                    }
                    else
                    {
                        targetForm.StartPosition = FormStartPosition.CenterParent;
                    }
                    // 20150902 katen #12048 「システム日付」の基準作成、適用 start
                    GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
                    System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
                    if (dt.Rows.Count > 0)
                    {
                        //取得した場合(基本的に取得できないのはありえない)
                        DateTime sysDate = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);//取得した結果をDateTimeに転換する
                        BaseBaseForm form = targetForm as BaseBaseForm;//画面フォームをベースフォームに転換する
                        if (form != null)
                        {
                            //念のため、転換結果をチェックする
                            form.sysDate = sysDate;//画面フォームにDBサーバ日付を設定する
                        }
                    }
                    // 20150902 katen #12048 「システム日付」の基準作成、適用 end
                    targetForm.Show();
                }
            }

            // 見つかった、または作成したフォームを前面に表示
            if (targetForm != null)
            {
                //if (formID == "M0317")
                //{
                //    targetForm.Text = caption.ToString();
                //}
                if (targetForm.WindowState == FormWindowState.Minimized)
                {
                    targetForm.WindowState = FormWindowState.Normal;
                }
                targetForm.BringToFront();
                targetForm.Activate();
                return true;
            }
            return false;
        }

        /// <summary>
        /// フォームオープン(モーダル)。指定された識別子のFormを生成・表示する。
        /// </summary>
        /// <param name="formID">画面識別子 ex) "G160", "M123"</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        private bool openFormModal(string formID, params object[] args)
        {
            this.clearInactiveForms();
            string caption;
            ModalFormReturnParams = null; // 戻り値をnullで初期化
            var formInterface = this.getFormInterface(formID, out caption);

            // メニューから呼ばれたどうか、今後必要になる可能性あり
            bool isCalledMenu = false;
            if (args.Count() > 0)
            {
                if (args.LastOrDefault().ToString() == FormManager.CALLED_MENU)
                {
                    args = args.Take(args.Length - 1).ToArray();
                    isCalledMenu = true;
                }
            }

            using (Form targetForm = formInterface.CreateForm(args))
            {
                if (targetForm != null)
                {
                    this.openedFormInfoList.Add(new FormInfo(formID, targetForm, formInterface, caption, isCalledMenu));
                    Form activeForm = Form.ActiveForm;
                    if (activeForm != null)
                    {
                        // マルチディスプレイ時に新規ウィンドウの表示を呼び出し元にする
                        targetForm.StartPosition = FormStartPosition.Manual;
                        // Location(X,Y)を親Formと同じにする
                        targetForm.SetBounds(activeForm.Bounds.Location.X,
                                             activeForm.Bounds.Location.Y,
                                             targetForm.Width,
                                             targetForm.Height);
                    }
                    else
                    {
                        targetForm.StartPosition = FormStartPosition.CenterParent;
                    }
                    // 20150902 katen #12048 「システム日付」の基準作成、適用 start
                    GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
                    System.Data.DataTable dt = dao.GetDateForStringSql("SELECT CONVERT(DATE, GETDATE()) AS DATE_TIME");//DBサーバ日付を取得する
                    if (dt.Rows.Count > 0)
                    {
                        //取得した場合(基本的に取得できないのはありえない)
                        DateTime sysDate = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);//取得した結果をDateTimeに転換する
                        BaseBaseForm form = targetForm as BaseBaseForm;//画面フォームをベースフォームに転換する
                        if (form != null)
                        {
                            //念のため、転換結果をチェックする
                            form.sysDate = sysDate;//画面フォームにDBサーバ日付を設定する
                        }
                    }
                    // 20150902 katen #12048 「システム日付」の基準作成、適用 end

                    targetForm.ShowDialog();
                    //targetForm.Dispose();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// モーダル画面を呼び出す。指定された識別子のFormを生成・表示する。
        /// </summary>
        /// <param name="formID">画面識別子 ex) "G160", "M123"</param>
        /// <param name="args">フォームのコンストラクタに渡す可変引数</param>
        /// <returns>True:成功, False:失敗</returns>
        private bool openDialog(string formID, params object[] args)
        {
            ModalFormReturnParams = null; // 戻り値をnullで初期化
            List<AssemblyItem> assemblyItems = GetAssemblyItems();
            var assemblyItem = assemblyItems.FirstOrDefault(n => n.FormID == formID);
            if (assemblyItem == null)
            {
                // メニューに登録されていなければそのまま返す
                return false;
            }

            // メニューから呼ばれたかどうか、今後必要になる可能性あり
            bool isCalledMenu = false;
            if (args.Count() > 0)
            {
                if (args.LastOrDefault().ToString() == FormManager.CALLED_MENU)
                {
                    args = args.Take(args.Length - 1).ToArray();
                    isCalledMenu = true;
                }
            }

            // アセンブリをロードし、コンストラクタを呼ぶ
            var assembly = Assembly.LoadFrom(assemblyItem.AssemblyName);
            using (var targetForm = (Form)assembly.CreateInstance(
                    assemblyItem.NameSpace + "." + assemblyItem.ClassName, // 名前空間を含めたクラス名
                    true, // 大文字小文字を無視するかどうか
                    BindingFlags.CreateInstance, // インスタンスを生成
                    null,
                    args, // コンストラクタの引数
                    null,
                    null
                  ))
            {
                if (targetForm != null)
                {
                    this.openedFormInfoList.Add(new FormInfo(formID, targetForm, null, assemblyItem.Name, isCalledMenu));
                    // 親のセンターに表示
                    targetForm.StartPosition = FormStartPosition.CenterParent;
                    targetForm.ShowDialog();
                    targetForm.Dispose();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="formID">更新するフォームのフォーム識別子</param>
        private bool updateForm(string formID)
        {
            this.clearInactiveForms();
            foreach (var info in this.openedFormInfoList)
            {
                Form form = info.Form as Form;
                if (form != null && !form.IsDisposed && form.Visible)
                {
                    if (formID.Equals(info.FormID))
                    {
                        info.FormInterface.UpdateForm(form);
                    }
                }
            }
            return true;
        }

        private void closeAllForm()
        {
            foreach (var info in this.openedFormInfoList)
            {
                Form form = info.Form as Form;
                if (form != null && !form.IsDisposed)
                {
                    form.Close();
                }
            }
        }

        /// <summary>
        /// メニューアイテムをAssemblyItemのListに変換する
        /// </summary>
        /// <returns>AssemblyItemのリスト</returns>
        private static List<AssemblyItem> GetAssemblyItems()
        {
            List<AssemblyItem> assemblyItems = UserRibbonMenu.menuItems.SelectMany(n => ((GroupItem)n).SubItems).SelectMany(n => ((SubItem)n).AssemblyItems).
                                                    Where(n => n.Name.ToLower() != "separator").ToList<AssemblyItem>();
            return assemblyItems;
        }

        /// <summary>
        /// 環境将軍のタイトルを取得します
        /// </summary>
        /// <returns>タイトル</returns>
        private static string GetApplicationTitle()
        {
            string result = string.Empty;
            Form mainMenu = Application.OpenForms.Cast<Form>().FirstOrDefault(n => n.Name == "ShougunMenu");
            if (mainMenu != null && mainMenu.Tag != null)
            {
                result = mainMenu.Tag.ToString();
            }
            else
            {
                result = "環境将軍R";
            }

            return result;
        }

        /// <summary>
        /// 権限チェック
        /// </summary>
        /// <param name="formID">OpenForm/OpenFormModal/OpenDialogの引数:formID</param>
        /// <param name="args">OpenForm/OpenFormModal/OpenDialogの引数:args</param>
        /// <returns>権限あり:true 権限なし:false</returns>
        //private static bool CanExecute(string formID, params object[] args)
        //{
        //    WINDOW_TYPE windowType = args.Where(s => s.GetType() == typeof(WINDOW_TYPE)).OfType<WINDOW_TYPE>().FirstOrDefault();
        //    if (windowType != WINDOW_TYPE.NONE)
        //    {
        //        // モード指定があればその権限をチェック
        //        if (windowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG
        //                && !Dto.SystemProperty.Shain.GetAuth(formID).HasFlag(AuthMethodFlag.Read))
        //        {
        //            // 参照権限なし
        //            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E158", "参照");
        //            return false;
        //        }
        //        else if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG
        //                    && !Dto.SystemProperty.Shain.GetAuth(formID).HasFlag(AuthMethodFlag.Add))
        //        {
        //            // 新規権限なし
        //            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E158", "新規");
        //            return false;
        //        }
        //        else if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
        //                    && !Dto.SystemProperty.Shain.GetAuth(formID).HasFlag(AuthMethodFlag.Edit))
        //        {
        //            // 修正権限なし
        //            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E158", "修正");
        //            return false;
        //        }
        //        else if (windowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
        //                    && !Dto.SystemProperty.Shain.GetAuth(formID).HasFlag(AuthMethodFlag.Delete))
        //        {
        //            // 削除権限なし
        //            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E158", "削除");
        //            return false;
        //        }
        //    }
        //    else if (Dto.SystemProperty.Shain.GetAuth(formID) == AuthMethodFlag.None)
        //    {
        //        // モード指定が無ければ、何かしらの権限があるかだけチェック
        //        Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E158", "");
        //        return false;
        //    }

        //    // 基本は権限あり
        //    return true;
        //}

        #endregion

        #region システム
        /// <summary>
        /// リボンメニューインスタンス
        /// ログイン時に作成されたリボンメニューを共有するための静的プロパティ
        /// BusinessBaseFormのコンストラクタ内で参照します。各画面では参照しないでください。
        /// </summary>
        public static RibbonMainMenu UserRibbonMenu { get; set; }
        #endregion

        /// <summary>
        /// LOGIN_COUNTERの更新(ゼロクリア)
        /// </summary>
        [Transaction]
        public virtual bool UpdateLoginCounter(out int errorNumber, out Exception ex)
        {
            ex = null;
            errorNumber = 0;
            int recodeCnt = 0;
            bool result = false;

            try
            {
                using (TransactionUtility tran = new TransactionUtility())
                {
                    recodeCnt = DaoInitUtility.GetComponent<IT_USER_LOGINDao>().UpdateLoginCounter(AppConfig.LoginId, AppConfig.LoginComputerName, AppConfig.LoginUserName, r_framework.Configuration.AppConfig.IsTerminalMode);
                    //if (recodeCnt == 1) { result = true; }
                    result = true;
                    tran.Commit();
                }
            }
            catch (Seasar.Framework.Exceptions.SQLRuntimeException sqlRuntimeEx)
            {
                if (sqlRuntimeEx.InnerException is System.Data.SqlClient.SqlException)
                {
                    System.Data.SqlClient.SqlException sqlEx
                        = (System.Data.SqlClient.SqlException)sqlRuntimeEx.InnerException;
                    errorNumber = sqlEx.Number;
                    ex = sqlEx;
                }
                else
                {
                    ex = sqlRuntimeEx;
                }
            }
            catch (Exception e)
            {
                ex = e;
            }

            return result;
        }

        // 20150723 障害#11443一時対応 Start
        [Obsolete("画面起動最大数一時対応用、カウント中で除外すべき画面ID(画面表示前の選択用ポップアップなど)。")]
        private static string[] excludeFormIds = { "M001" };

        // 一部画面の起動はFormManagerで管理されていないので、
        // 一時的に該当画面情報をFormManagerに追加し、画面閉じる時情報を削除する。
        [Obsolete("画面起動最大数一時対応用、BusinessBaseForm.Show()だけ利用する。")]
        public static void OpenNoneIdForm(Form noneId)
        {
            LogUtility.DebugMethodStart(noneId);
            initialize();
            instance.openNoneIdForm(noneId);
            LogUtility.DebugMethodEnd();
        }
        private void openNoneIdForm(Form noneId)
        {
            this.clearInactiveForms();

            // 最大数に達していた場合は
            //if (this.openedFormInfoList.Count + 1 >= r_framework.Const.Constans.MAX_WINDOW_COUNT)
            if (this.openedFormInfoList.Count + 1 >= r_framework.Dto.SystemProperty.Shain.MAX_WINDOW_COUNT)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E136");
                return;
            }

            var dummy = new FormInfo("dummy", noneId, null, "", false);
            this.openedFormInfoList.Add(dummy);

            noneId.Show();
        }

        [Obsolete("画面起動最大数一時対応用、BusinessBaseForm.ShowDialog()だけ利用する。")]
        public static DialogResult OpenNoneIdFormModal(Form noneId)
        {
            LogUtility.DebugMethodStart(noneId);
            initialize();
            var result = instance.openNoneIdFormModal(noneId);
            LogUtility.DebugMethodEnd(result);
            return result;
        }
        private DialogResult openNoneIdFormModal(Form noneId)
        {
            this.clearInactiveForms();

            var dummy = new FormInfo("dummy", noneId, null, "", false);
            this.openedFormInfoList.Add(dummy);

            var result = noneId.ShowDialog();
            //QN_QUAN add 20210104 #158953 Ss
            var name_form = noneId.Text.Replace(" - " + r_framework.Dto.SystemProperty.NameFormName, "").Trim();
            if (!name_form.Equals("コンテナ指定") && !name_form.Equals("伝票発行")
                && !name_form.Equals("伝票発行（代納）") && !name_form.Equals("パターン一覧") && !name_form.Equals(""))
            {
                noneId.Dispose();
            }       
            return result;
        }
        // 20150723 障害#11443一時対応 End
        //QN_QUAN add 20210104 #158953 S
        private static void InserDBLog(string caption)
        {
            DBConnectionLogLogic db = new DBConnectionLogLogic();
            if (!db.CanConnectDB() || !db.ConnectDB())
            {
                return;
            }
            db.InserDBLog(caption);
        }
        //QN_QUAN add 20210104 #158953 E
    }
}
