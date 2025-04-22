using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Shougun.Printing.Manager
{
    public class WebBrowserReader
    {
        /// <summary>
        /// Mapbox連携で使用
        /// インストールされたブラウザ、既定のブラウザの情報を読み込む
        /// </summary>
        /// <returns></returns>
        public List<BrowserDto> browserInfoRead()
        {
            // インストールされているWebブラウザのリスト作成
            List<BrowserDto> _browserList = CreateLocalBrowsersList();
            return _browserList;
        }

        // 先頭と末尾の「"」を削除
        private string StripQuotes(string s)
        {
            if (s.EndsWith("\"") && s.StartsWith("\""))
            {
                return s.Substring(1, s.Length - 2);
            }
            else
            {
                return s;
            }
        }

        #region ブラウザのリストを作成
        // ブラウザーのリスト作成
        private List<BrowserDto> CreateLocalBrowsersList()
        {
            try
            {
                List<BrowserDto> _browserList = new List<BrowserDto>();

                /* Microsoft Edge 以外のブラウザー情報を取得する
                 ************************************************************************/

                // HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Clients\StartMenuInternetを開く
                RegistryKey browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");

                if (browserKeys == null)
                {
                    // 上記のキーでレジストリが開けない場合はHKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternetを開く
                    browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
                }

                // サブキーを取得
                var subKeyNames = browserKeys.GetSubKeyNames();

                foreach (var browserName in subKeyNames)
                {
                    var browser = new BrowserDto();

                    // ブラウザーの名前
                    RegistryKey browserKey = browserKeys.OpenSubKey(browserName);
                    browser.Name = (string)browserKey.GetValue(null);

                    // ブラウザーのパス
                    RegistryKey browserKeyPath = browserKey.OpenSubKey(@"shell\open\command");
                    browser.Path = StripQuotes(browserKeyPath.GetValue(null).ToString());

                    // ブラウザーのバージョン
                    if (browser.Path != null)
                    {
                        browser.Version = FileVersionInfo.GetVersionInfo(browser.Path).FileVersion;
                    }
                    else
                    {
                        browser.Version = "unknown";
                    }

                    // ブラウザーの追加
                    _browserList.Add(browser);
                }

                /* Microsoft Edge のブラウザー情報を取得する
                 ************************************************************************/

                RegistryKey edgeKey = Registry.CurrentUser.OpenSubKey(
                    @"SOFTWARE\Classes\Local Settings\Software\Microsoft\Windows\CurrentVersion\AppModel\SystemAppData\Microsoft.MicrosoftEdge_8wekyb3d8bbwe");
                if (edgeKey != null)
                {
                    string version = "unknown";

                    // ブラウザーのバージョン
                    RegistryKey edgeSchemaKey = edgeKey.OpenSubKey("Schemas");
                    if (edgeSchemaKey != null)
                    {
                        string packageFullName = edgeSchemaKey.GetValue("PackageFullName").ToString();
                        packageFullName = StripQuotes(packageFullName);
                        Match match = Regex.Match(packageFullName, "(((([0-9.])\\d)+){1})");
                        if (match.Success)
                        {
                            version = match.Value;
                        }
                    }

                    // ブラウザーの追加
                    _browserList.Add(new BrowserDto
                    {
                        Name = "Microsoft Edge",
                        Path = @"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe",
                        Version = version
                    });
                }

                return _browserList;
            }
            catch
            {
                // 例外は無視する
                return null;
            }
        }
        #endregion

        #region 既定のブラウザを取得
        private string GetDefaultBrowserExePath()
        {
            // これだとvista以前でないと機能しない
            //return _GetDefaultExePath(@"http\shell\open\command");
            return _GetDefaultExePath(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice");
        }

        private string _GetDefaultExePath(string keyPath)
        {
            string path = "";

            // レジストリ・キーを開く
            // 「HKEY_CLASSES_ROOT\xxxxx\shell\open\command」
            // これだとvista以前のOSでないと機能しない
            // 「HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice」
            // こっちを利用
            RegistryKey rKey = Registry.CurrentUser.OpenSubKey(keyPath);
            if (rKey != null)
            {
                // レジストリの値を取得する
                string command = (string)rKey.GetValue("ProgId");
                if (command == null)
                {
                    return path;
                }
                // 前後の余白を削る
                command = command.Trim();
                if (command.Length == 0)
                {
                    return path;
                }

                // 「"」で始まる長いパス形式かどうかで処理を分ける
                if (command[0] == '"')
                {
                    // 「"～"」間の文字列を抽出
                    int endIndex = command.IndexOf('"', 1);
                    if (endIndex != -1)
                    {
                        // 抽出開始を「1」ずらす分、長さも「1」引く
                        path = command.Substring(1, endIndex - 1);
                    }
                }
                else
                {
                    // 「（先頭）～（スペース）」間の文字列を抽出
                    int endIndex = command.IndexOf(' ');
                    if (endIndex != -1)
                    {
                        path = command.Substring(0, endIndex);
                    }
                    else
                    {
                        path = command;
                    }
                }
            }

            return path;
        }
        #endregion
    }
}
