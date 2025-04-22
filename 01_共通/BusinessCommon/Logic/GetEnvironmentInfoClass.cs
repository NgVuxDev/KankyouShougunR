using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Configuration;
using System.Runtime.InteropServices;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    public class GetEnvironmentInfoClass
    {
        /// <summary>
        /// クライアントコンピューター名/ユーザーIDを取得します
        /// </summary>
        /// <returns>
        /// Item1 = clientComputerName
        /// Item2 = clientUserName
        /// </returns>
        public Tuple<string, string> GetComputerAndUserName()
        {
            string clientComputerName = string.Empty;
            string clientUserName = string.Empty;
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
            return Tuple.Create(clientComputerName, clientUserName);
        }

        #region クラウド版のクライアントコンピュータ名/ユーザーIDの取得処理
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
        #endregion クラウド版のクライアントコンピュータ名/ユーザーIDの取得処理
    }
}
