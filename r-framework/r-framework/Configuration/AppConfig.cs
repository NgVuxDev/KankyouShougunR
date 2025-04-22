using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using Shougun.UserRestrict.URXmlDocument;
using System.Diagnostics;
using System.Runtime.InteropServices;  // for DllImport

namespace r_framework.Configuration
{
    /// <summary>
    /// アプリケーション構成クラス
    /// </summary>
    public static class AppConfig
    {
        /// <summary>
        /// ターミナルモードを取得する
        /// </summary>
        public static bool IsTerminalMode
        {
            get
            {
                return System.Windows.Forms.SystemInformation.TerminalServerSession;
            }
        }
        
        /// <summary>
        /// ユーザーCAL数
        /// </summary>
        public static int UserCal { get; private set; }

        /// <summary>
        /// CAL数（モバイル将軍）
        /// </summary>
        public static int MobileCal { get; private set; }

        /// <summary>
        /// シリーズ識別子
        /// </summary>
        public static string Series { get; private set; }

        /// <summary>
        /// 識別子(クラウド用)
        /// </summary>
        public static string Identifier { get; private set; }

        // ユーザー利用制限対応 2014.10.14
        /// <summary>
        /// ライセンス種類
        /// </summary>
        public static string License { get; private set; }

        /// <summary>
        /// 顧客
        /// </summary>
        public static string Customer { get; private set; }

        /// <summary>
        /// バージョン情報
        /// </summary>
        public static StringBuilder VersionInfo { get; private set; }

        /// <summary>
        /// ログインID
        /// </summary>
        public static string LoginId { get; private set; }

        /// <summary>
        /// ログイン時間
        /// </summary>
        public static string LoginTime { get; private set; }

        /// <summary>
        /// ログインコンピュータ名
        /// </summary>
        public static string LoginComputerName { get; private set; }

        /// <summary>
        /// ログインコンピュータ名
        /// </summary>
        public static string LoginUserName { get; private set; }
        // ユーザー利用制限対応 2014.10.14

        /// <summary>
        /// アプリケーションタイプ列挙子
        /// </summary>
        public enum AppTypes
        {
            KankyouShougun,   // 環境将軍R
            HaishaShougun,    // 配車将軍
            ManifestShougun,  // マニフェスト将軍
            KeiryouShougun,   // 計量将軍
        }

        /// <summary>
        /// アプリケーションタイプ
        /// </summary>
        public static AppTypes AppType { get; private set; }

        /// <summary>
        /// アプリケーション名
        /// </summary>
        public static string AppName { get; private set; }

        /// <summary>
        /// 有効オプションIDリスト
        /// </summary>
        public static string[] EnableOptions { get; private set; }

        /// <summary>
        /// オプション判定フラグプロパティ
        /// </summary>
        public static AppOptions AppOptions { get; private set; }

        /// <summary>
        /// カスタマイズフラグ
        /// </summary>
        public static bool IsCustomized { get; private set; }

        /// <summary>
        /// ベースバージョン
        /// </summary>
        public static r_framework.Configuration.VersionInfo BaseVersion { get; private set; }

        /// <summary>
        /// カスタマイズバージョン
        /// </summary>
        public static r_framework.Configuration.VersionInfo CustomizeVersion { get; private set; }

        /// <summary>
        /// シリーズ識別子がマニフェスト将軍Lite(C8/D2/D4)か判定
        /// </summary>
        public static bool IsManiLite
        {
            get
            {
                if ("C8".Equals(Series) || "D2".Equals(Series) || "D4".Equals(Series))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// プロテクトチェックを行うか
        /// return true :チェックを行う
        /// return false :チェックを行わない
        /// で、returnの設定を変更する
        /// </summary>
        public static bool IsProtectRun
        {
            get
            {
                //return false; //チェックを行わない
                return true;    //チェックを行う
            }
        }

        /// <summary>
        /// プロテクトモード(true:通常、false:ログアウトのみ)
        /// </summary>
        public static bool ProtectMode { get; private set; }

        /// <summary>
        /// プロテクトモードセット
        /// </summary>
        /// <param name="ProtectM"></param>
        public static void ProtectModeSet(bool ProtectM)
        {
            AppConfig.ProtectMode = ProtectM;
        }

        /// <summary>
        /// バージョンファイルを読み込み、構成情報/バージョンプロパティを設定する。
        /// アプリ起動時に一度だけ呼び出すこと。
        /// このクラスではログは一切出力しない。
        /// エラーの場合、errorMessageに表示用のエラーメッセージが格納され戻り値はfalseが返却される。
        /// 
        /// </summary>
        /// <param name="path">バージョンファイルのパス</param>
        /// <param name="path">エラー時の表示メッセージ</param>
        /// <returns></returns>
        public static bool LoadVersionFile(string path, out string errorMessage)
        {
            errorMessage = "";
            Dictionary<string, string> data;

            if (AppConfig.readVersionFile(path, out data, ref errorMessage))
            {
                //if (AppConfig.setConfigPropaties(data, ref errorMessage))
                //{
                    VersionInfo baseVer;
                    if (AppConfig.setVersionPropaties("[Base Version]", data, out baseVer, ref errorMessage))
                    {
                        VersionInfo customizeVer;
                        if (AppConfig.setVersionPropaties("[Customize Version]", data, out customizeVer, ref errorMessage))
                        {
                            AppConfig.BaseVersion = baseVer;
                            AppConfig.CustomizeVersion = customizeVer;
                            AppConfig.IsCustomized = (customizeVer.CustomerName.Length > 0);

                            switch (AppConfig.Series)
                            {
                                default:
                                    AppConfig.AppType = AppTypes.KankyouShougun;
                                    AppConfig.AppName = "環境将軍R";
                                    break;
                                case "C6":
                                    AppConfig.AppType = AppTypes.HaishaShougun;
                                    AppConfig.AppName = "配車将軍";
                                    break;
                                case "C7":
                                case "C8":
                                    AppConfig.AppType = AppTypes.ManifestShougun;
                                    AppConfig.AppName = "マニフェスト将軍";
                                    break;
                                case "C9":
                                    AppConfig.AppType = AppTypes.KeiryouShougun;
                                    AppConfig.AppName = "計量将軍";
                                    break;
                            }

                            // 有効オプションIDリスト
                            // TODO: #355オプション正式対応時にバージョンファイルから構成情報の参照(ログイン後)に変更すること。
                            /*
				            var enableOptionList = new List<string>();
                            var key = "[Options] workflow";
                            string value;
                            if (data.TryGetValue(key, out value))
                            {
                                if (value.Equals("1"))
                                {
                                    enableOptionList.Add("workflow");
                                }
                            }
                            AppConfig.EnableOptions = enableOptionList.ToArray();

                            AppConfig.AppOptions = new AppOptions(AppConfig.EnableOptions);
                            FormManager.FormManager.EnableOptionForms(AppConfig.EnableOptions);
                            FormManager.FormManager.EnableSeriesForms(AppConfig.Series);
                            */
                            AppConfig.VersionInfo = AppConfig.CreateVersionInfo();

                            return true;
                        }
                    }
                //}
            }
            return false;
        }

        private static bool readVersionFile(string path, out Dictionary<string, string> data, ref string errorMessage)
        {
            data = new Dictionary<string, string>();

            try
            {
                if (File.Exists(path))
                {
                    using (var reader = new StreamReader(path, true))
                    {
                        string section = "";
                        while (!reader.EndOfStream)
                        {
                            var buf = reader.ReadLine().Trim();
                            if (buf.IndexOf("[") == 0)
                            {
                                section = buf;
                            }
                            else
                            {
                                var array = buf.Split('=');
                                if (array.Length > 1)
                                {
                                    var key = array[0].Trim(' ', '\"', '\'', '\t');
                                    var value = array[1].Trim(' ', '\"', '\'', '\t');
                                    data.Add(string.Format("{0} {1}", section, key), value);
                                }
                            }
                        }
                        return true;
                    }
                }
                else
                {
                    errorMessage = "ファイルがありません。" + path;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return false;
        }

        /// <summary>
        /// LoadVersionFileの下請け処理。バージョンファイル[Configration]セクションから構成情報プロパティを設定する。
        /// 必須項目のチェックも行う。
        /// </summary>
        /// <param name="data">バージョンファイルの読み込みデータ(辞書構造)</param>
        /// <param name="errorMessage">エラー時のメッセージ</param>
        /// <returns></returns>
        private static bool setConfigPropaties(Dictionary<string, string> data, ref string errorMessage)
        {
            try
            {
                string value;
                string key;
                
                key = "[Configuration] User Cal";
                if (data.TryGetValue(key, out value))
                {
                    AppConfig.UserCal = int.Parse(value);
                    // TODO:値のチェックを追加する
                }
                else
                {
                    errorMessage = string.Format("User Cal数が未定義です:{0}", key);
                    return false;
                }

                key = "[Configuration] Series";
                if (data.TryGetValue(key, out value))
                {
                    AppConfig.Series = value;
                    // TODO:値のチェックを追加する
                }
                else
                {
                    errorMessage = string.Format("シリーズ識別子が未定義です:{0}", key);
                    return false;
                }
            }
            catch (Exception e)
            {
                errorMessage = string.Format("[Configuration]の読み込みに失敗しました。\r\n{0}", e.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// LoadVersionFileの下請け処理。バージョンファイルのデータからバージョン情報のプロパティを設定する。 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="data"></param>
        /// <param name="versionInfo"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool setVersionPropaties(string section, Dictionary<string, string> data, out VersionInfo versionInfo, ref string errorMessage)
        {
            versionInfo = new VersionInfo();
            string key = "";
            string value;
            try
            {
                string productName = "";
                key = section + " Product Name";
                data.TryGetValue(key, out productName);
                
                string productInfo = "";
                key = section + " Product Info";
                data.TryGetValue(key, out productInfo);
                
                string venderName = "";
                key = section + " Vender Name";
                data.TryGetValue(key, out venderName);
                
                string venderInfo = "";
                key = section + " Vender Info";
                data.TryGetValue(key, out venderInfo);

                string customerName = "";
                key = section + " Customer Name";
                data.TryGetValue(key, out customerName);
                
                string description = "";
                key = section + " Description";
                data.TryGetValue(key, out description);
                
                key = section + " Major Version";
                data.TryGetValue(key, out value);
                var major = int.Parse(value);

                key = section + " Minor Version";
                data.TryGetValue(key, out value);
                var minor = int.Parse(value);
                
                key = section + " Build Number";
                data.TryGetValue(key, out value);
                var build = int.Parse(value);

                key = section + " Revision";
                data.TryGetValue(key, out value);
                var revision = int.Parse(value);
            
                key = section + " Build Date";
                data.TryGetValue(key, out value);
                var date = DateTime.Parse(value);
            
                versionInfo.SetProductInfo(productName, productInfo);
                versionInfo.SetVenderInfo(venderName, venderInfo);
                versionInfo.SetCustomerName(customerName);
                versionInfo.SetDescription(description);
                versionInfo.SetVersionNumber(major, minor, build, revision, date);
            }
            catch (Exception e)
            {
                errorMessage = string.Format("{0}の読み込みに失敗しました。\r\n{1}", key, e.Message);
                return false;
            }

            return true;
        }

        /* ユーザー利用制限対応 2014.10.14 */
        /// <summary>
        /// ライセンス種類名を返す
        /// </summary>
        /// <returns>ライセンス種類名</returns>
        public static string GetLicenseName(string licenseNumber)
        {
            string licenseName = string.Empty;

            switch (int.Parse(licenseNumber ?? "0"))
            {
                case 0:
                    licenseName = "試用版";
                    break;
                case 1:
                    licenseName = "正規版";
                    break;
                case 2:
                    licenseName = "開発版";
                    break;
            }
            return licenseName;
        }

        /// <summary>
        /// 構成情報の設定値を取得し、AppConfingに格納する
        /// </summary>
        /// <param name="URXmlDocument">構成情報を読み込んだXmlDocument</param>
        public static bool setUserRestrictItems(URXmlDocument URXmlDoc, string loginId, string loginTime, string loginComputerName, string loginUserName, ref string errorMessage)
        {
            string key = string.Empty;

            try
            {
                // Basic Group
                // Series
                key = "series";
                if (URXmlDoc.GetItemValue(key) == null)
                {
                    errorMessage = string.Format("Seriesが未定義です:{0}", key);
                    return false;
                }
                AppConfig.Series = URXmlDoc.GetItemValue(key).ToString();
                switch (AppConfig.Series)
                {
                    default:
                        AppConfig.AppType = AppTypes.KankyouShougun;
                        AppConfig.AppName = "環境将軍R";
                        break;
                    case "C6":
                        AppConfig.AppType = AppTypes.HaishaShougun;
                        AppConfig.AppName = "配車将軍";
                        break;
                    case "C7":
                    case "C8":
                        AppConfig.AppType = AppTypes.ManifestShougun;
                        AppConfig.AppName = "マニフェスト将軍";
                        break;
                    case "C9":
                        AppConfig.AppType = AppTypes.KeiryouShougun;
                        AppConfig.AppName = "計量将軍";
                        break;
                }

                // CAL数
                key = "cal";
                if (URXmlDoc.GetItemValue(key) == null)
                {
                    errorMessage = string.Format("UserCal数が未定義です:{0}", key);
                    return false;
                }
                AppConfig.UserCal = int.Parse(URXmlDoc.GetItemValue(key).ToString());

                // CAL数（モバイル将軍）
                key = "calMobile";
                if (URXmlDoc.GetItemValue(key) == null)
                {
                    AppConfig.MobileCal = 0;
                }
                else
                {
                    AppConfig.MobileCal = int.Parse(URXmlDoc.GetItemValue(key).ToString());
                }

                // Identifier
                if (AppConfig.IsTerminalMode)
                {
                    key = "identifier";
                    if (URXmlDoc.GetItemValue(key) == null)
                    {
                        errorMessage = string.Format("識別子が未定義です:{0}", key);
                        return false;
                    }
                    AppConfig.Identifier = URXmlDoc.GetItemValue(key).ToString();
                }

                // License
                key = "license";
                if (URXmlDoc.GetItemValue(key) == null)
                {
                    errorMessage = string.Format("License種類が未定義です:{0}", key);
                    return false;
                }
                AppConfig.License = URXmlDoc.GetItemValue(key).ToString();
                
                // Customer
                key = "customer";
                if (URXmlDoc.GetItemValue(key) == null)
                {
                    errorMessage = string.Format("顧客名が未定義です:{0}", key);
                    return false;
                }
                AppConfig.Customer = URXmlDoc.GetItemValue(key).ToString();

                // Option Group
                AppConfig.AppOptions = new AppOptions(URXmlDoc);

                var enableOptionList = new List<string>();
                foreach (string id in AppConfig.AppOptions.OptionDictionary.Keys)
                {
                    if (Convert.ToBoolean(AppConfig.AppOptions.OptionDictionary[id].value))
                    {
                        // 電子申請オプションのみ有効IDリストに格納する場合は
                        // 以下のIF文のコメントをはずす
                        // ～2014.10.20時点で未確定～
                        //if (id.Equals("workflow"))
                        //{
                            enableOptionList.Add(id);
                        //}
                    }
                }

                if (!string.IsNullOrEmpty(loginId)) { AppConfig.LoginId = loginId; }
                if (!string.IsNullOrEmpty(loginTime)) { AppConfig.LoginTime = loginTime; }
                if (!string.IsNullOrEmpty(loginComputerName)) { AppConfig.LoginComputerName = loginComputerName; }
                if (!string.IsNullOrEmpty(loginUserName)) { AppConfig.LoginUserName = loginUserName; }

                AppConfig.EnableOptions = enableOptionList.ToArray();
                FormManager.FormManager.EnableOptionForms(AppConfig.EnableOptions);
                FormManager.FormManager.EnableSeriesForms(AppConfig.Series);

                AppConfig.VersionInfo = AppConfig.CreateVersionInfo();
            }
            catch (Exception e)
            {
                errorMessage = string.Format("{0}の読み込みに失敗しました。\r\n{1}", key, e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Version情報の作成
        /// </summary>
        /// <returns></returns>
        private static StringBuilder CreateVersionInfo()
        {
            StringBuilder buf = new System.Text.StringBuilder();

            VersionInfo b = AppConfig.BaseVersion;
            VersionInfo c = AppConfig.CustomizeVersion;

            buf.AppendFormat("【{0}】", b.ProductName).Append(Environment.NewLine);

            if (AppConfig.IsCustomized)
            {
                buf.AppendFormat("  ユーザー               : {0}", c.CustomerName).Append(Environment.NewLine);
                buf.AppendFormat("  バージョン             : {0}.{1}.{2}.{3}",
                    c.MajorVersion, c.MinorVersion, c.BuildNumber, c.Revision).Append(Environment.NewLine);
                buf.AppendFormat("  作成日時               : {0}", c.BuildDate.ToString("yyyy/MM/dd HH:mm")).Append(Environment.NewLine);
                buf.Append(Environment.NewLine);
                buf.AppendFormat("【基本パッケージ】").Append(Environment.NewLine);
            }

            buf.AppendFormat("  バージョン             : {0}.{1}.{2}.{3}",
                b.MajorVersion, b.MinorVersion, b.BuildNumber, b.Revision).Append(Environment.NewLine);
            buf.AppendFormat("  作成日時               : {0}", b.BuildDate.ToString("yyyy/MM/dd HH:mm")).Append(Environment.NewLine);
            buf.AppendFormat("  製品情報               : {0}", b.ProductInfo).Append(Environment.NewLine);
            buf.Append(Environment.NewLine);

            // 構成情報
            buf.AppendFormat("【構成情報】").Append(Environment.NewLine);
            buf.AppendFormat("  アプリケーション名     : {0}", AppConfig.AppName).Append(Environment.NewLine);
            buf.AppendFormat("  Series                 : {0}", AppConfig.Series).Append(Environment.NewLine);

            string userCal = string.Empty;
            if (AppConfig.UserCal != 0) { userCal = AppConfig.UserCal.ToString(); }
            buf.AppendFormat("  CAL数                  : {0}", userCal).Append(Environment.NewLine);
            userCal = string.Empty;
            if (AppConfig.MobileCal != 0) { userCal = AppConfig.MobileCal.ToString(); }
            buf.AppendFormat("  CAL数(モバイル将軍)    : {0}", userCal).Append(Environment.NewLine);
            buf.AppendFormat("  顧客名                 : {0}", AppConfig.Customer).Append(Environment.NewLine);
            buf.AppendFormat("  接続モード             : {0}", (AppConfig.IsTerminalMode ? "クラウド" : "オンプレミス")).Append(Environment.NewLine);
            buf.AppendFormat("  ライセンス種類         : {0}", GetLicenseName(AppConfig.License)).Append(Environment.NewLine);

            buf.Append(Environment.NewLine);

            // Option
            if (AppConfig.AppOptions != null)
            {
                buf.AppendFormat("【オプション】").Append(Environment.NewLine);
                foreach (string id in AppConfig.AppOptions.OptionDictionary.Keys)
                {
                    // 電子申請オプションのみ表示する場合は
                    // 以下のIF文のコメントをはずす
                    // ～2014.10.20時点で未確定～
                    //if (id.Equals("workflow"))
                    //{
                    string caption = AppConfig.AppOptions.OptionDictionary[id].caption;
                    object value = AppConfig.AppOptions.OptionDictionary[id].value;

                    buf.AppendFormat("  {0}: {1}", caption.PadRight(23 -
                                     (Encoding.GetEncoding("shift_jis").GetByteCount(caption) - caption.Length), ' '),
                                     Convert.ToBoolean(value) ? "ON" : "OFF")
                                     .Append(Environment.NewLine);
                    //}
                }
                buf.Append(Environment.NewLine);
            }

            // ログイン情報
            if (!string.IsNullOrEmpty(AppConfig.LoginId))
            {
                buf.AppendFormat("【ログイン情報】").Append(Environment.NewLine);
                buf.AppendFormat("  ログインID             : {0}", AppConfig.LoginId).Append(Environment.NewLine);
                buf.AppendFormat("  ログインユーザー名     : {0}", Dto.SystemProperty.Shain.Name).Append(Environment.NewLine);
                buf.AppendFormat("  ログイン時間           : {0}", AppConfig.LoginTime).Append(Environment.NewLine);
                buf.AppendFormat("  接続DB                 : {0}", AppData.LastLoginDbName).Append(Environment.NewLine);
            }

            return buf;
        }
        /* ユーザー利用制限対応 2014.10.14 */

        /// <summary>
        /// フォアグラウンドかどうかを判断
        /// </summary>
        /// <returns>
        ///  true  : フォアグラウンド
        ///  false : フォアグラウンドじゃない
        /// </returns>
        public static bool IsForeground()
        {
            bool result = true;

            IntPtr hWnd = GetForegroundWindow();
            int id;
            GetWindowThreadProcessId(hWnd, out id);

            // フォアグランドウィンドウが将軍自身ではない場合
            if (id != Process.GetCurrentProcess().Id)
            {
                result = false;
            }

            return result;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        /// <summary>
        /// Localディレクトリの取得
        /// </summary>
        /// <param name="formID"></param>
        /// <param name="computerName"></param>
        /// <returns></returns>
        public static string GetLocalSettings(string formID)
        {
            string result = string.Empty;

            var configFilePath = ConfigurationManager.OpenExeConfiguration(
                                  ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            var appData = Path.GetDirectoryName(configFilePath);
            appData = Path.GetDirectoryName(appData);
            appData = Path.GetDirectoryName(appData);
            appData = Path.Combine(appData, "LocalSettings");

            string filePath = string.Empty;
            if (IsTerminalMode)
            {
                filePath = Path.Combine(appData, formID + "_" + LoginComputerName);
            }
            else
            {
                filePath = Path.Combine(appData, formID);
            }

            if (File.Exists(filePath))
            {
                result = File.ReadAllText(filePath, Encoding.UTF8);
            }
            return result;
        }

        /// <summary>
        /// 記録しておきたいファイルパスの保存
        /// </summary>
        /// <param name="formID"></param>
        /// <param name="value"></param>
        /// <param name="computerName"></param>
        public static void SaveLocalFilePath(string formID, string value)
        {
            var configFilePath = ConfigurationManager.OpenExeConfiguration(
                      ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            var appData = Path.GetDirectoryName(configFilePath);
            appData = Path.GetDirectoryName(appData);
            appData = Path.GetDirectoryName(appData);
            appData = Path.Combine(appData, "LocalSettings");

            if (!Directory.Exists(appData))
            {
                Directory.CreateDirectory(appData);
            }

            string filePath = string.Empty;
            if (IsTerminalMode)
            {
                filePath = Path.Combine(appData, formID + "_" + LoginComputerName);
            }
            else
            {
                filePath = Path.Combine(appData, formID);
            }

            try
            {
                File.WriteAllText(filePath, value, Encoding.UTF8);
            }
            catch
            {

            }
        }
    }
}
