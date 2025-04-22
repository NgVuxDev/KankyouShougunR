using System.Diagnostics;
using r_framework.Const;
using System.IO;
using r_framework.Dto;
using r_framework.Utility;
using System.Web.Script.Serialization;
using System;
using r_framework.Dao;
using System.Windows.Forms;

namespace r_framework.CommunicateApp
{
    public class InxsLauncher
    {

        public static bool ExistsSubApplication()
        {
            if (!string.IsNullOrWhiteSpace(SystemProperty.InxsSettings.FilePath) && File.Exists(SystemProperty.InxsSettings.FilePath))
            {
                return true;
            }

            return false;
        }

        public static bool LaunchSupApp(LauncherDto launcherDto)
        {
            try
            {
                if (r_framework.Configuration.AppConfig.AppOptions.IsInxs()
                    && ExistsSubApplication())
                {
                    var sysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllDataForCode("0");
                    string settingSubappDBName = sysInfo != null ? sysInfo.DB_INXS_SUBAPP_CONNECT_NAME : string.Empty;
                    if (string.IsNullOrEmpty(settingSubappDBName))
                    {
                        MessageBox.Show("システム設定（初回登録）オプションタブ内のINXSサブアプリデータベースが未設定です。", Constans.WORNING_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogUtility.Warn("システム設定（初回登録）オプションタブ内のINXSサブアプリデータベースが未設定です。");
                        return false;
                    }

                    LogUtility.Info(string.Format("Run Inxs SubApplication - {0}", SystemProperty.InxsSettings.FilePath));
                    launcherDto.SubAppDBConnectName = settingSubappDBName;

                    launcherDto.IsInxsUketsuke = r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke();
                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                    launcherDto.IsInxsSeikyuusho = r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho();
                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                    launcherDto.IsInxsPayment = r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai();
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

                    // 20210225 【マージ】INXS委託契約書アップロード機能を環境将軍R V2.22にマージ依頼 #147341 start
                    launcherDto.IsInxsContract = r_framework.Configuration.AppConfig.AppOptions.IsInxsItaku();
                    // 20210225 【マージ】INXS委託契約書アップロード機能を環境将軍R V2.22にマージ依頼 #147341 end

                    // 20210225 【マージ】INXS許可証アップロード機能を環境将軍R V2.22にマージ依頼 #147342 start
                    launcherDto.IsInxsLicense = r_framework.Configuration.AppConfig.AppOptions.IsInxsKyokasho();
                    // 20210225 【マージ】INXS許可証アップロード機能を環境将軍R V2.22にマージ依頼 #147342 end

                    // 20210226 【マージ】INXSマニフェストシェア機能を環境将軍R V2.22にマージ依頼 #147340 start
                    launcherDto.IsInxsManifest = r_framework.Configuration.AppConfig.AppOptions.IsInxsManifest();
                    // 20210226 【マージ】INXSマニフェストシェア機能を環境将軍R V2.22にマージ依頼 #147340 end

                    launcherDto.IsInxsNoticeSupport = r_framework.Configuration.AppConfig.AppOptions.IsInxsOshirase();

                    string sendArgs = new JavaScriptSerializer().Serialize(launcherDto);
                    bool runResult = false;
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = SystemProperty.InxsSettings.FilePath;
                        process.StartInfo.Arguments = EncryptionUtility.Encrypt(sendArgs);
                        runResult = process.Start();                        
                    }
                    if (!runResult)
                    {
                        LogUtility.Error(string.Format("Run Inxs SubApplication FAIL - {0}", SystemProperty.InxsSettings.FilePath));
                    }
                    else
                    {
                        LogUtility.Info(string.Format("Run Inxs SubApplication SUCCESS - {0}", SystemProperty.InxsSettings.FilePath));
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Info(string.Format("Inxs SubApplication file path - {0}", SystemProperty.InxsSettings.FilePath));
                LogUtility.Error("LaunchSupApp FAIL", ex);
            }
            return false;
        }

        public static Process GetCurrentSubApp()
        {
            if (ExistsSubApplication())
            {
                var processName = Path.GetFileNameWithoutExtension(SystemProperty.InxsSettings.FilePath);
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes != null && processes.Length > 0)
                {
                    return processes[0];
                }
            }

            return null;
        }

        public static bool IsSubAppRunning()
        {
            Process process = GetCurrentSubApp();
            if (process != null)
            {
                return true;
            }
            return false;
        }

        public static void CloseSubApp()
        {
            if (!ExistsSubApplication()) return;
            
            Process[] procs = null;

            try
            {
                var processName = Path.GetFileNameWithoutExtension(SystemProperty.InxsSettings.FilePath);
                procs = Process.GetProcessesByName(processName);

                Process mspaintProc = procs[0];

                if (!mspaintProc.HasExited)
                {
                    mspaintProc.Kill();
                }
            }
            finally
            {
                if (procs != null)
                {
                    foreach (Process p in procs)
                    {
                        p.Dispose();
                    }
                }
            }
        }
    }
}
