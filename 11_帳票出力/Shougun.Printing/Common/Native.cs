using System;
using System.Runtime.InteropServices;

namespace Shougun.Printing.Common
{
    /// <summary>
    /// Windows API/グローバルメモリ関連
    /// </summary>
    public static class Native
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GlobalFree(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalUnlock(IntPtr hMem);


        public const Int32 WM_USER = 0x400;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #region WINSPOOLAPI
        /// <summary>
        /// fMode 設定値
        /// </summary>
        public const int DM_OUT_BUFFER = 0x2;
        public const int DM_PROMPT = 0x4;
        public const int DM_IN_PROMPT = DM_PROMPT;
        public const int DM_IN_BUFFER = 0x8;

        /// <summary>
        /// OpenPrinter
        /// </summary>
        /// <param name="pPrinterName"></param>
        /// <param name="phPrinter"></param>
        /// <param name="pDefault"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenPrinter
            ([In, MarshalAs(UnmanagedType.LPWStr)] string pPrinterName,
             out IntPtr phPrinter, IntPtr pDefault);

        /// <summary>
        /// ClosePrinter
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", SetLastError = true)]
        public static extern int ClosePrinter(IntPtr hPrinter);

       
        /// <summary>
        /// DocumentPropertiesダイアログを表示（またはdevmodeのサイズ取得）
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="hPrinter"></param>
        /// <param name="pDeviceName"></param>
        /// <param name="pDevModeOutput"></param>
        /// <param name="pDevModeInput"></param>
        /// <param name="fMode"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int DocumentProperties
            (IntPtr hwnd, IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pDeviceName,
             IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

        #endregion

        #region WTSAPI
        //"C:\Program Files\Microsoft SDKs\Windows\v7.0A\Include\WtsApi32.h"

        public static IntPtr WTS_CURRENT_SERVER_HANDLE = (IntPtr)null;
        public static int WTS_CURRENT_SESSION = -1;
        /// <summary>
        /// 指定したターミナルサーバー上の、指定したセッションの情報を取得します。。
        /// </summary>
        [DllImport("wtsapi32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool WTSQuerySessionInformation(
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
        public static extern void WTSFreeMemory(IntPtr pMemory);

        /// <summary>
        /// WTSQuerySessionInformation で要求できる情報の種類
        /// </summary>
        public enum WTS_INFO_CLASS 
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
        #endregion
    }
}
