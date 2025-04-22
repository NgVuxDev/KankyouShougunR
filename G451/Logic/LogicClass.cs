using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using r_framework.Configuration;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.UserRestrict.URXmlDocument;
using System.Text;
using System.Data.SqlClient;
using Seasar.Framework.Exceptions;
using System.Diagnostics;
using r_framework.Logic;

namespace Shougun.Core.Common.Login
{
    /// <summary>
    /// ログインビジネスロジック
    /// </summary>
    internal class LogicClass
    {
        /// <summary>
        /// ログインフォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">ログインフォーム</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            // ユーザー利用制限対応 2014.10.14
            this.userLoginDao = DaoInitUtility.GetComponent<IT_USER_LOGINDao>();
            // ユーザー利用制限対応 2014.10.14

            LogUtility.DebugMethodEnd();
        }

        // ユーザー利用制限対応 2014.10.14
        /// <summary>
        /// T_USER_LOGINのDAO
        /// </summary>
        private IT_USER_LOGINDao userLoginDao;
        // ユーザー利用制限対応 2014.10.14

        /// <summary>
        /// ログインIDがある有効な社員マスター
        /// </summary>
        public M_SHAIN[] ShainAll { get; private set; }

        /// <summary>
        /// システム情報マスター
        /// </summary>
        public M_SYS_INFO SystemInformation { get; private set; }

        /// <summary>
        /// 会社情報マスター
        /// </summary>
        public M_CORP_INFO CorpInformation { get; private set; }

        /// <summary>
        /// メニュー権限マスター
        /// </summary>
        public M_MENU_AUTH[] MenuAuthorizationAll { get; private set; }

        // 20141211 ブン お気に入りモッド start
        /// <summary>
        /// お気に入りモッド
        /// </summary>
        public M_MY_FAVORITE[] BookmarkAll { get; private set; }
        // 20141211 ブン お気に入りモッド end

        /// <summary>
        /// 印刷設定帳票リストマスター
        /// </summary>
        public S_REPORTLISTPRINTERSETTINGS[] ReportListPrinterSettings { get; private set; }

        // ユーザー利用制限対応 2014.10.14
        /// <summary>
        /// 構成情報
        /// </summary>
        public M_USER_RESTRICT UserRestrict { get; private set; }

        /// <summary>
        /// 公開鍵
        /// </summary>
        public M_CUSTOMER_KEY CustomerKey { get; private set; }
        // ユーザー利用制限対応 2014.10.14

        /// <summary>
        /// 社員最大表示画面数マスター
        /// </summary>
        public M_SHAIN_MAX_WINDOW[] ShainMaxWindowAll { get; set; }

        /// <summary>
        /// ログインユーザーの詳細情報取得
        /// </summary>
        /// <param name="loginID">ログインID</param>
        /// <param name="passWord">パスワード</param>
        /// <returns>正しいログインIDとパスワードなら社員情報、正しくない場合はnull</returns>
        public M_SHAIN GetLoginUser(string loginID, string passWord)
        {
            LogUtility.DebugMethodStart(loginID, passWord);

            var shain = this.ShainAll.Where(s => s.LOGIN_ID == loginID && (s.PASSWORD ?? string.Empty) == passWord).FirstOrDefault();

            LogUtility.DebugMethodEnd(shain);

            return shain;
        }

        #region Communicate InxsSubApplication

        public void RefreshAllViews()
        {
            string sqlGetAllViews = @"SELECT DISTINCT Name AS ViewName
                                      FROM   sys.objects AS so
                                             INNER JOIN sys.sql_expression_dependencies AS sed
                                                ON so.object_id = sed.referencing_id
                                      WHERE  so.type = 'V' ";
            DataTable tbViews = this.userLoginDao.GetDateForStringSql(sqlGetAllViews);
            if (tbViews != null && tbViews.Rows.Count > 0)
            {
                StringBuilder sqlRefreshAllViews = new StringBuilder();
                foreach (DataRow row in tbViews.Rows)
                {
                    sqlRefreshAllViews.AppendFormat(" EXEC SP_REFRESHVIEW '{0}' ", row["ViewName"]);
                }
                sqlRefreshAllViews.Append(" select 1 AS IsOK ");
                this.userLoginDao.DeleteDateForStringSql(sqlRefreshAllViews.ToString());
            }
        }

        public DataTable GetAppSettingInxs(string shainCd)
        {
            DataTable tbData = null;
            try
            {
                LogUtility.DebugMethodStart(shainCd);
                string sql = @"SELECT * FROM M_APP_SETTING_INXS";
                tbData = this.userLoginDao.GetDateForStringSql(sql);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetAppSettingInxs", ex);
                tbData = null;
            }
            finally
            {
                LogUtility.DebugMethodEnd(tbData);
            }
            return tbData;
        }

        #endregion

        public M_SHAIN_MAX_WINDOW GetMaxWindowCountByShain(string shainCd)
        {
            LogUtility.DebugMethodStart(shainCd);

            var shainMaxWindow = this.ShainMaxWindowAll.Where(s => s.SHAIN_CD == shainCd).FirstOrDefault();

            LogUtility.DebugMethodEnd(shainMaxWindow);

            return shainMaxWindow;
        }

        /// <summary>
        /// DAO経由でマスターデータの読み込み
        /// </summary>
        public void LoadMasterData()
        {
            LogUtility.DebugMethodStart();

            // ログインIDがあるユーザーのみ
            this.ShainAll = DaoInitUtility.GetComponent<IM_SHAINDao>().GetAllValidData(new M_SHAIN()).Where(s => !string.IsNullOrWhiteSpace(s.LOGIN_ID)).ToArray();

            this.ShainMaxWindowAll = DaoInitUtility.GetComponent<IM_SHAIN_MAX_WINDOWDao>().GetAllValidData(new M_SHAIN_MAX_WINDOW());

            this.SystemInformation = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllDataForCode("0");

            this.CorpInformation = DaoInitUtility.GetComponent<IM_CORP_INFODao>().GetAllData().FirstOrDefault();

            this.MenuAuthorizationAll = DaoInitUtility.GetComponent<IM_MENU_AUTHDao>().GetAllValidData(new M_MENU_AUTH());

            // 20141211 ブン お気に入りモッド start
            this.BookmarkAll = DaoInitUtility.GetComponent<IM_MY_FAVORITEDao>().GetAllValidData(new M_MY_FAVORITE());
            // 20141211 ブン お気に入りモッド end

            this.ReportListPrinterSettings = DaoInitUtility.GetComponent<IS_REPORTLISTPRINTERSETTINGSDao>().GetAllReportList(false);

            // ユーザー利用制限対応 2014.10.14
            this.UserRestrict = DaoInitUtility.GetComponent<IM_USER_RESTRICTDao>().GetSign();

            this.CustomerKey = DaoInitUtility.GetComponent<IM_CUSTOMER_KEYDao>().GetCustomerKey();
            // ユーザー利用制限対応 2014.10.14

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// XMLからデフォルトログイン情報取得
        /// </summary>
        /// <returns></returns>
        public CommonDefine.LoginInfo GetLoginInfo()
        {
            LogUtility.DebugMethodStart();

            var info = new CommonDefine.LoginInfo();
            try
            {
                var userElement = XmlManager.GetUserElement();

                info.Code = userElement.Attribute("Code").Value;
                info.Pwd = userElement.Attribute("Pwd").Value;
                info.PwdSaved = userElement.Attribute("PwdSaved").Value == "1";
            }
            catch
            {
                LogUtility.Error(string.Format("{0}の形式が正しくありません。", Path.GetFileName(XmlManager.UserConfigFile)));
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(info);
            }

            return info;
        }

        /// <summary>
        /// XMLからDB接続情報取得
        /// </summary>
        /// <returns></returns>
        public List<DBConnectionDTO> GetDBConnectionList()
        {
            LogUtility.DebugMethodStart();

            List<DBConnectionDTO> connectionList = null;

            try
            {
                var dbconfig = XmlManager.GetDatabaseConnectList();

                connectionList = dbconfig.Select(s => XmlManager.GetDbConnectionDto(s)).ToList();
            }
            catch
            {
                LogUtility.Error(string.Format("{0}の形式が正しくありません。", Path.GetFileName(XmlManager.DBConfigFile)));
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(connectionList);
            }

            return connectionList;
        }

        /// <summary>
        /// XMLのデフォルト選択DB情報更新
        /// </summary>
        /// <param name="selectedIndex">選択インデックス</param>
        public void UpdateDBConnectionList(int selectedIndex)
        {
            LogUtility.DebugMethodStart(selectedIndex);


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// XMLのデフォルトログイン情報更新
        /// </summary>
        /// <param name="loginID">ログインID</param>
        /// <param name="passWord">パスワード</param>
        /// <param name="passWordSaved">パスワード保存フラグ</param>
        public void UpdateXmlLoginInfo(string loginID, string passWord, bool passWordSaved)
        {
            LogUtility.DebugMethodStart(loginID, passWord, passWordSaved);

            var userConfig = XDocument.Load(XmlManager.UserConfigFile);
            var userElement = XmlManager.GetUserElement(userConfig);

            userElement.Attribute("Code").SetValue(loginID);
            userElement.Attribute("Pwd").SetValue(passWordSaved ? passWord : string.Empty);
            userElement.Attribute("PwdSaved").SetValue(passWordSaved ? "1" : "0");

            userConfig.Save(XmlManager.UserConfigFile);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public DataTable GetPopUpShainData(IEnumerable<string> displayCol)
        {
            LogUtility.DebugMethodStart(displayCol);

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = EntityUtility.EntityToDataTable(this.ShainAll);
            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            var sortedDt = new DataTable();
            foreach (var col in displayCol)
            {
                // 表示対象の列だけを順番に追加
                sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
            }

            foreach (DataRow r in dt.Rows)
            {
                sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
            }

            LogUtility.DebugMethodEnd(sortedDt);
            return sortedDt;
        }

        /// <summary>
        /// クラウド時のリダイレクトプリンタ番号を使用できる番号に書き換え
        /// </summary>
        public void ReplaceRedirectPrinterName()
        {
            LogUtility.DebugMethodStart();

            var reg = new System.Text.RegularExpressions.Regex(@" \(リダイレクト [\d]{1,}\)$");

            var userConfig = XDocument.Load(XmlManager.UserConfigFile);
            var configPrinters = userConfig.Element("CurrentUserCustomConfigProfile").Element("Settings").Element("PrintReport").Elements("PrintSettings")
                                            .SelectMany(s => s.Elements("Name").Attributes("Printer"))
                                            .Where(s => reg.IsMatch(s.Value));

            var printerName = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Cast<string>().FirstOrDefault(s => reg.IsMatch(s));
            if (printerName != null)
            {
                var currentPrt = reg.Match(printerName).Value;
                foreach (var prt in configPrinters)
                {
                    prt.SetValue(reg.Replace(prt.Value, currentPrt));
                }

                userConfig.Save(XmlManager.UserConfigFile);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// クラウド時にマニフェスト帳票のテンプレートパスを書き換え
        /// </summary>
        public void ReplaceTemplatePathToCloud()
        {
            LogUtility.DebugMethodStart();

            // マニ帳票はクラウド用テンプレートファイル名の末尾を「C」とする
            this.ReportListPrinterSettings.Where(s => s.REPORT_DISP_NAME.Contains("マニフェスト")).ToList()
                                          .ForEach(s => s.REPORT_BUTSURI_NAME = s.REPORT_BUTSURI_NAME.Replace(".xml", "C.xml"));

            LogUtility.DebugMethodEnd();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        const int WM_CLOSE = 0x10;
        const int WM_NCDESTROY = 0x0082;

        const int popupCloseTimeLimit = 10000;   //msec

        /* ユーザー利用制限対応 2014.10.14 */
        /// <summary>
        /// ログイン可能か判断し結果を返す
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <param name="errorMessage">メッセージ</param>
        /// <returns>true  : ログイン許可, false : ログイン却下</returns>
        [Transaction]
        public virtual bool CanLogin(string loginId, out string errorMessage)
        {
            bool result = false;

            string signString = string.Empty;
            string publicKeyString = string.Empty;

            string clientComputerName = string.Empty;
            string clientUserName = string.Empty;

            errorMessage = string.Empty;

            try
            {
                if (this.UserRestrict == null)
                {
                    errorMessage = "データベースに構成情報が設定されていない為、ログインできません。";
                    return result;
                }

                if (this.CustomerKey == null)
                {
                    errorMessage = "データベースに鍵情報が設定されていない為、ログインできません。";
                    return result;
                }

                // 圧縮&Base64エンコードを元の文字列に戻す
                signString = StringFromBase64EncodeAndCompress(this.UserRestrict.URINFO);
                publicKeyString = StringFromBase64EncodeAndCompress(this.CustomerKey.CUSTOMER_KEY);

                URXmlDocument URXmlDoc = new URXmlDocument();
                URXmlDoc.LoadXml(signString);

                // クラウド版の場合、クライアントコンピュータ名/ユーザーIDの取得
                if (AppConfig.IsTerminalMode)
                {
                    clientComputerName = getClientInfo(WTS_INFO_CLASS.WTSClientName);
                    clientUserName = getClientInfo(WTS_INFO_CLASS.WTSUserName);
                }
                else
                {
                    clientComputerName = Environment.MachineName;
                    clientUserName = Environment.UserName;
                }

                // AppConfigに格納する処理
                if (!AppConfig.setUserRestrictItems(URXmlDoc, loginId, DateTime.Now.ToString(), clientComputerName, clientUserName, ref errorMessage))
                {
                    return result;
                }

                // 検証処理
                if (!URXmlDoc.Verify(publicKeyString))
                {
                    errorMessage = "データベースに登録されている署名と鍵情報が一致しない為、ログインできません。";
                    return result;
                }

                // menu.xmlの存在チェック
                if (!File.Exists(XmlManager.MenuFile))
                {
                    errorMessage = "設定ファイルが見つかりませんでした。" + Environment.NewLine + XmlManager.MenuFile;
                    return result;
                }

                // セッションの制御でログイン制限を行う場合、以下は不要になる
                if (AppConfig.IsTerminalMode) { return true; }

                // CAL数チェック、ログイン情報テーブルへの登録、ログインカウンタ更新
                // RETURN_CODE =  1 : CAL数内でログイン許可
                // RETURN_CODE =  0 : CAL数OVERでログイン却下
                // RETURN_CODE = -9 : 再ログインを行うかポップアップ表示
                using (Transaction tran = new Transaction())
                {
                    T_USER_LOGIN resultUserLogin = userLoginDao.UpdOrInsLoginInfo(r_framework.Configuration.AppConfig.UserCal,
                                                                                  loginId,
                                                                                  clientComputerName,
                                                                                  clientUserName,
                                                                                  AppConfig.IsTerminalMode);

                    if (resultUserLogin.RETURN_CODE == 1)
                    {
                        result = true;
                    }
                    else if (resultUserLogin.RETURN_CODE == -9)
                    {
                        string msg = string.Empty;
                        if (AppConfig.IsTerminalMode)
                        {
                            msg = "リモートユーザー [" + clientUserName + "] は既にログインされています。" + Environment.NewLine + "再ログインしますか?"
                                                + Environment.NewLine
                                                + Environment.NewLine
                                                + "※このダイアログは、表示されてから [" + popupCloseTimeLimit / 1000 + "秒] で"
                                                + Environment.NewLine + "　自動的にキャンセルされます。";
                        }
                        else
                        {
                            msg = "コンピュータ名 [" + resultUserLogin.CLIENT_COMPUTER_NAME + "] は既にログインされています。" + Environment.NewLine + "再ログインしますか?"
                                                + Environment.NewLine
                                                + Environment.NewLine
                                                + "※このダイアログは、表示されてから [" + popupCloseTimeLimit / 1000 + "秒] で"
                                                + Environment.NewLine + "　自動的にキャンセルされます。";
                        }


                        using (var timer = new System.Threading.Timer(s =>
                        {
                            var hWnd = FindWindow(null, "再ログインの確認");
                            if (hWnd != IntPtr.Zero)
                            {
                                SendMessage(hWnd, WM_NCDESTROY, IntPtr.Zero, IntPtr.Zero);
                            }
                        }, null, popupCloseTimeLimit, System.Threading.Timeout.Infinite))

                        // 再ログインするか確認
                        if (System.Windows.Forms.MessageBox.Show(msg, "再ログインの確認", System.Windows.Forms.MessageBoxButtons.YesNo,
                                                                 System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)

                        {
                            // LOGIN_COUNTER更新
                            int cnt = userLoginDao.UpdateLoginCounterReLogin(loginId, clientComputerName, clientUserName, AppConfig.IsTerminalMode);
                            if (cnt > 0) { result = true; }
                            else { throw new Exception(); }
                        }
                    }
                    else
                    {
                        errorMessage = "ログイン可能ユーザー数を超えている為、ログインできません。"
                                     + Environment.NewLine
                                     + "時間を置いてから再度お試しください。";
                    }
                    tran.Commit();
                }
            }
            catch (Seasar.Framework.Exceptions.SQLRuntimeException sqlRuntimeEx)
            {
                if (sqlRuntimeEx.InnerException is System.Data.SqlClient.SqlException)
                {
                    System.Data.SqlClient.SqlException sqlEx
                        = (System.Data.SqlClient.SqlException)sqlRuntimeEx.InnerException;
                    if (sqlEx.Number == -2)
                    {
                        // タイムアウト
                        errorMessage = "タイムアウトしました。時間を置いてから再度お試しください。";
                    }
                    else
                    {
                        errorMessage = "ログイン情報テーブル操作中にエラーが発生しました。" +
                                       Environment.NewLine + sqlEx.Message;
                    }
                }
                else
                {
                    errorMessage = "ログイン情報テーブル操作中にエラーが発生しました。" +
                                   Environment.NewLine + sqlRuntimeEx.Message;
                }
            }
            catch (Exception e)
            {
                errorMessage = "ログイン検証処理でエラーが発生しました。" + Environment.NewLine + e.Message;
            }

            return result;
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
        /// クラウドでのリモート接続のクライアント情報取得
        /// </summary>
        /// <param name="target">WTS_INFO_CLASSの種類</param>
        /// <returns>引数で指定したクライアント情報の値</returns>
        private string getClientInfo(WTS_INFO_CLASS target)
        {
            string targetValue = "";

            IntPtr ppBuffer = IntPtr.Zero;
            uint size = 0;

            try
            {
                WTSQuerySessionInformation(
                    WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION,
                    target,
                    out ppBuffer, out size);
                if (ppBuffer != IntPtr.Zero)
                {
                    targetValue = Marshal.PtrToStringAnsi(ppBuffer);
                }
            }
            catch
            {
            }
            finally
            {
                if (ppBuffer != IntPtr.Zero)
                {
                    WTSFreeMemory(ppBuffer);
                }
            }
            return targetValue;
        }

        private static IntPtr WTS_CURRENT_SERVER_HANDLE = (IntPtr)null;
        private static int WTS_CURRENT_SESSION = -1;

        /// <summary>
        /// 指定したターミナルサーバー上の、指定したセッションの情報を取得します。。
        /// </summary>
        [DllImport("wtsapi32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern bool WTSQuerySessionInformation(
            IntPtr hServer,
            int SessionId,
            WTS_INFO_CLASS WTSInfoClass,
            out IntPtr ppBuffer,
            out uint pBytesReturned
            );

        /// <summary>
        /// ターミナルサービス関数で確保したメモリを解放します。
        /// </summary>
        /// <param name="memory"></param>
        [DllImport("wtsapi32.dll", ExactSpelling = true, SetLastError = false)]
        private static extern void WTSFreeMemory(IntPtr pMemory);

        /// <summary>
        /// WTSQuerySessionInformation で要求できる情報の種類
        /// </summary>
        private enum WTS_INFO_CLASS
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingBytes,
            WTSOutgoingBytes,
            WTSIncomingFrames,
            WTSOutgoingFrames,
            WTSClientInfo,
            WTSSessionInfo,
            WTSSessionInfoEx,
            WTSConfigInfo,
            WTSValidationInfo,
            WTSSessionAddressV4,
            WTSIsRemoteSession
        };
        /* ユーザー利用制限対応 2014.10.14 */

        internal bool ShellCmd(out string errorMessage)
        {
            // query userコマンドを実行しても、ローカルでは値が返ってこない
            // リモート実行だと結果が返ってくるため、検証が面倒だがこのまま進める

            errorMessage = string.Empty;

            long cal = AppConfig.UserCal;       // CAL数 本来はAppConfig.UserCal
            string un = AppConfig.Identifier.ToLower();   // ユーザー名 本来はAppConfig.Identifier(仮) 鍵の構成情報に追加する必要もある
            long i = 0;                         // 有効セッション数のカウンタ
            string command = "query user";      // 実行するコマンド
            string CheckString = string.Empty;  // コマンドで発行された標準出力保持用

            try
            {
                // Processオブジェクトを作成
                Process p = new Process();

                p.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");   //ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
                p.StartInfo.Arguments = "/c " + command;        // 実行するファイル名(コマンド) ※"/c"は実行後閉じるために必要
                p.StartInfo.UseShellExecute = false;            // シェル機能を利用しない
                p.StartInfo.RedirectStandardOutput = true;      // 標準出力のリダイレクトする
                p.StartInfo.CreateNoWindow = true;              // ウィンドウを表示しないようにする

                // 起動
                p.Start();

                // 出力を読み取る
                string results = p.StandardOutput.ReadToEnd();

                // プロセス終了まで待機する
                // WaitForExitはReadToEndの後である必要がある
                // (親プロセス、子プロセスでブロック防止のため)
                p.WaitForExit();

                p.Close();

                StringReader rs = new StringReader(results);
                while (rs.Peek() > -1)
                {
                    // 一行ずつ読み込む
                    CheckString = rs.ReadLine().ToLower();

                    // ユーザー名チェック
                    if (CheckString.Contains(un))
                    {
                        // rdp-tcp#を含むかチェック
                        if (CheckString.Contains("rdp-tcp#"))
                        {
                            // ここまできたらカウント
                            i++;
                        }
                    }
                }
                rs.Close();

                if (cal < i)
                {
                    errorMessage = "ログイン可能ユーザー数を超えている為、ログインできません。"
                                 + Environment.NewLine
                                 + "時間を置いてから再度お試しください。";
                    return false;
                }
            }
            catch(Exception ex)
            {
                errorMessage = "ログイン検証処理でエラーが発生しました。" + Environment.NewLine + ex.Message;
            }
            return true;
        }
        internal bool ConnectToLogDB()
        {
            LogUtility.DebugMethodStart();

            if (SystemInformation != null && !string.IsNullOrEmpty(SystemInformation.DB_LOG_CONNECT))
            {
                return new DBConnectionLogLogic().ConnectDB();
            }

            LogUtility.DebugMethodEnd();
            return false;
        }
    }
}
