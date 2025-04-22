using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Shougun.Printing.Common;

namespace Shougun.Printing.Common
{
    static class ServerSettings
    {
        static string clientName = null;
        public static string GetClientComputerName()
        {
            LastError.Clear();

            if (!string.IsNullOrEmpty(ServerSettings.clientName))
            {
                return ServerSettings.clientName;
            }

            IntPtr ppBuffer = IntPtr.Zero;
            uint size = 0;

            try
            {
                Native.WTSQuerySessionInformation(
                    Native.WTS_CURRENT_SERVER_HANDLE, Native.WTS_CURRENT_SESSION,
                    Native.WTS_INFO_CLASS.WTSClientName,
                    out ppBuffer, out size);
                if (ppBuffer != IntPtr.Zero)
                {
                    ServerSettings.clientName = Marshal.PtrToStringAnsi(ppBuffer);
                }
            }
            catch (Exception ex)
            {
                LastError.Exception = ex;
            }
            finally
            {
                if (ppBuffer != IntPtr.Zero)
                {
                    Native.WTSFreeMemory(ppBuffer);
                }
            }

            if (string.IsNullOrEmpty(ServerSettings.clientName))
            {
                LastError.Message = "接続元のコンピュータ名が取得できません。";
            }
            return ServerSettings.clientName;
        }

        private static string clientSidePrintingDirectory = null;
        public static string GetClientSidePrintingDirectory()
        {
            LastError.Clear();

            if (!string.IsNullOrEmpty(ServerSettings.clientSidePrintingDirectory))
            {
                return ServerSettings.clientSidePrintingDirectory;
            }

            string clientName = GetClientComputerName();
            if (string.IsNullOrEmpty(clientName))
            {
                return null;
            }

            // ファイル名末尾にクライアントPC名を付けて接続元別にする。
            string settingsFile = Path.Combine(
                        LocalDirectories.SettingsDirectory,
                        "ServerPrintSettings_" + clientName);

            if (!File.Exists(settingsFile))
            {
                // 過去Verとの互換性のためファイルが見つからなかったらクライアントPC名の付いていないファイル名に置き換える
                settingsFile = Path.Combine(
                        LocalDirectories.SettingsDirectory,
                        "ServerPrintSettings");
            }

            if (File.Exists(settingsFile))
            {
                try
                {
                    string directory = File.ReadAllText(settingsFile, Encoding.UTF8);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        if (Directory.Exists(directory))
                        {
                            ServerSettings.clientSidePrintingDirectory = directory;
                            return ServerSettings.clientSidePrintingDirectory;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LastError.Exception = ex;
                }
            }

            LastError.Message = "リダイレクトローカルドライブの帳票出力先ディレクトリが存在しません。\r\nマスタ / システムメニューの「印刷設定」で設定を確認してください。";
            return null;
        }

        public static bool SetPrintingDirectory(string direcory)
        {
            LastError.Clear();
            ServerSettings.clientSidePrintingDirectory = null;

            // ファイル名末尾にクライアントPC名を付けて接続元別にする。
            string clientName = GetClientComputerName();
            if (!string.IsNullOrEmpty(clientName))
            {
                string settingsFile = Path.Combine(
                        LocalDirectories.SettingsDirectory,
                        "ServerPrintSettings_" + clientName);

                try
                {
                    File.WriteAllText(settingsFile, direcory, Encoding.UTF8);
                    ServerSettings.clientSidePrintingDirectory = direcory;
                    ReportSettingsSyncThread.Stop();
                    ReportSettingsSyncThread.Start();
                    return true;
                }
                catch (Exception ex)
                {
                    LastError.Exception = ex;
                }
            }

            LastError.Message = "印刷設定ファイルの書き込みに失敗しました。";
            return false;
        }

        public static string GetClientSettingsDirectory()
        {
            LastError.Clear();
            var directory = ServerSettings.GetClientSidePrintingDirectory();
            if (!string.IsNullOrEmpty(directory))
            {
                directory = Path.Combine(Path.GetDirectoryName(directory), "Settings");
            }
            return directory;
        }

        public static string GetServerSettingsDirectory()
        {
            LastError.Clear();
            string clientName = GetClientComputerName();
            if (string.IsNullOrEmpty(clientName))
            {
                return null;
            }

            // SettingsフォルダにクライアントPC名でサブフォルダにする。
            var directory = Path.Combine(
                        LocalDirectories.SettingsDirectory, clientName);

            return directory;
        }

        #region mapbox連携

        private static string clientSideMapsDirectory = null;
        public static string GetClientSideMapsDirectory()
        {
            LastError.Clear();

            if (!string.IsNullOrEmpty(ServerSettings.clientSideMapsDirectory))
            {
                return ServerSettings.clientSideMapsDirectory;
            }

            string clientName = GetClientComputerName();
            if (string.IsNullOrEmpty(clientName))
            {
                return null;
            }

            // ファイル名末尾にクライアントPC名を付けて接続元別にする。
            string settingsFile = Path.Combine(
                        LocalDirectories.SettingsDirectory,
                        "ServerPrintSettings_" + clientName);

            if (!File.Exists(settingsFile))
            {
                // 過去Verとの互換性のためファイルが見つからなかったらクライアントPC名の付いていないファイル名に置き換える
                settingsFile = Path.Combine(
                        LocalDirectories.SettingsDirectory,
                        "ServerPrintSettings");
            }

            if (File.Exists(settingsFile))
            {
                try
                {
                    string directory = File.ReadAllText(settingsFile, Encoding.UTF8);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        directory = Path.GetDirectoryName(directory);
                        directory = Path.Combine(directory, "Maps");
                        return directory;
                    }
                }
                catch (Exception ex)
                {
                    LastError.Exception = ex;
                }
            }

            LastError.Message = "リダイレクトローカルドライブの帳票出力先ディレクトリが存在しません。\r\nマスタ / システムメニューの「印刷設定」で設定を確認してください。";
            return null;
        }

        #endregion
    }
}
