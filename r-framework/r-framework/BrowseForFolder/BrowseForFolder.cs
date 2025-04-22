using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using r_framework.Logic;

/// <summary>
/// フォルダ検索(API)クラス
/// </summary>
namespace r_framework.BrowseForFolder
{
    /// <summary>
    /// フォルダ検索(API)クラス
    /// </summary>
    public class BrowseForFolder : IDisposable 
    {
        // Constants for sending and receiving messages in BrowseCallBackProc
        /// <summary>
        /// 
        /// </summary>
        public struct BFFM
        {
            public const int WM_USER = 0x400;
            public const int BFFM_INITIALIZED = 1;
            public const int BFFM_SELCHANGED = 2;
            public const int BFFM_VALIDATEFAILEDA = 3;
            public const int BFFM_VALIDATEFAILEDW = 4;
            public const int BFFM_IUNKNOWN = 5;    // provides IUnknown to client. lParam: IUnknown*
            public const int BFFM_SETSTATUSTEXTA = WM_USER + 100;
            public const int BFFM_ENABLEOK = WM_USER + 101;
            public const int BFFM_SETSELECTIONA = WM_USER + 102;
            public const int BFFM_SETSELECTIONW = WM_USER + 103;
            public const int BFFM_SETSTATUSTEXTW = WM_USER + 104;
            public const int BFFM_SETOKTEXT = WM_USER + 105;    // Unicode only
            public const int BFFM_SETEXPANDED = WM_USER + 106;    // Unicode only
        }

        // Browsing for directory.
        /// <summary>
        /// 選択できる階層の絞込み
        /// </summary>
        public struct BIF
        {
            public const uint BIF_RETURNONLYFSDIRS = 0x1;       // 「コンピュータ」を選択できなくなる
            public const uint BIF_DONTGOBELOWDOMAIN = 0x2;      // ネットワークでつながっているコンピュータを表示しない
            public const uint BIF_STATUSTEXT = 0x4;             // 'ステータス文字列を表示（要:コールバック処理）
            // this flag is set.  Passing the message BFFM_SETSTATUSTEXTA to the hwnd can set the
            // rest of the text.  This is not used with BIF_USENEWUI and BROWSEINFO.lpszTitle gets
            // all three lines of text.
            public const uint BIF_RETURNFSANCESTORS = 0x8;      // ルートフォルダ以下のフォルダのみ選択
            public const uint BIF_EDITBOX = 0x10;               // エディットボックスをつける
            public const uint BIF_VALIDATE = 0x20;              // エディットボックスに無効な文字を入力した時に通知（要:コールバック処理）
            public const uint BIF_NEWDIALOGSTYLE = 0x40;        // 新しいユーザーインターフェースを使用
            // Caller needs to call OleInitialize() before using this API
            public const uint BIF_USENEWUI = 0x0040 + 0x10;     //(BIF_NEWDIALOGSTYLE | BIF_EDITBOX);
            public const uint BIF_BROWSEINCLUDEURLS = 0x80;     // Allow URLs to be displayed or entered. (Requires BIF_USENEWUI)
            public const uint BIF_UAHINT = 0x0100;              // Add a UA hint to the dialog, in place of the edit box. May not be combined with BIF_EDITBOX
            public const uint BIF_NONENEWFOLDERBUTTON = 0x200;  // Do not add the "New Folder" button to the dialog.  Only applicable with BIF_NEWDIALOGSTYLE.
            public const uint BIF_NOTRANSLATETARGETS = 0x400;   // don't traverse target as shortcut

            public const uint BIF_BROWSEFORCOMPUTER = 0x1000;   // 「コンピュータ」のみ選択可能
            public const uint BIF_BROWSEFORPRINTER = 0x2000;    // 「プリンタ」のみ選択可能
            public const uint BIF_BROWSEINCLUDEFILES = 0x4000;  // 全リソースの(ファイルも)選択可
            public const uint BIF_SHAREABLE = 0x8000;           // sharable resources displayed (remote shares, requires BIF_USENEWUI)
        }

        // Root Directory for display
        /// <summary>
        /// ルートディレクトリ
        /// </summary>
        public struct CSIDL
        {
            public const int DESKTOP            = 0x0;  // デスクトップ 
            public const int PROGRAMS           = 0x2;  // スタートメニューの「プログラム」 
            public const int CONTROLS           = 0x3;  // コントロールパネル 
            public const int PRINTERS           = 0x4;  // プリンタ 
            public const int PERSONAL           = 0x5;  // パーソナル 
            public const int FAVORITES          = 0x6;  // 「お気に入り」フォルダ 
            public const int STARTUP            = 0x7;  // 「スタートアップ」フォルダ 
            public const int RECENT             = 0x8;  // スタートメニューの「最近使ったファイル」 
            public const int SENDTO             = 0x9;  // 送る 
            public const int BITBUCKET          = 0xA;  // ごみ箱 
            public const int STARTMENU          = 0xB;  // スタートメニュー 
            public const int DESKTOPDIRECTORY   = 0x10; // デスクトップ 
            public const int DRIVES             = 0x11; // マイコンピュータ 
            public const int NETWORK            = 0x12; // ネットワークコンピュータ 
            public const int NETHOO             = 0x13; 　 
            public const int FONTS              = 0x14; // フォント 
            public const int TEMPLATES          = 0x15; // Shell New 
        }

        [DllImport("shell32.dll")]
        static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);

        // Note that the BROWSEINFO object's pszDisplayName only gives you the name of the folder.
        // To get the actual path, you need to parse the returned PIDL
        [DllImport("shell32.dll", CharSet=CharSet.Unicode)]
        // static extern uint SHGetPathFromIDList(IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)]
        //StringBuilder pszPath);
        static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);

        [DllImport("user32.dll", PreserveSig = true)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

        private string _initialPath;

        public delegate int BrowseCallBackProc(IntPtr hwnd, int msg, IntPtr lp, IntPtr wp);
        struct BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            public string pszDisplayName;
            public string lpszTitle;
            public uint ulFlags;
            public BrowseCallBackProc lpfn;
            public IntPtr lParam;
            public int iImage;
        }

        public int OnBrowseEvent(IntPtr hWnd, int msg, IntPtr lp, IntPtr lpData)
        {
            switch(msg)
            {
                case BFFM.BFFM_INITIALIZED: // Required to set initialPath
                {
                    //Win32.SendMessage(new HandleRef(null, hWnd), BFFM_SETSELECTIONA, 1, lpData);
                    // Use BFFM_SETSELECTIONW if passing a Unicode string, i.e. native CLR Strings.
                    SendMessage(new HandleRef(null, hWnd), BFFM.BFFM_SETSELECTIONW, 1, _initialPath);
                    break;
                }
                case BFFM.BFFM_SELCHANGED:
                {
                    IntPtr pathPtr = Marshal.AllocHGlobal((int)(260 * Marshal.SystemDefaultCharSize));
                    if (SHGetPathFromIDList(lp, pathPtr))
                    {
                        SendMessage(new HandleRef(null, hWnd), BFFM.BFFM_SETSTATUSTEXTW, 0, pathPtr);
                        Marshal.FreeHGlobal(pathPtr);
                    }
                    break;
                }
            }

            return 0;
        }

        public string SelectFolder(string caption, string initialPath, IntPtr parentHandle)
        {
            //initialPathとparentHandleはDialog側で再設定するため破棄。
            string selectFolder = "";

            FolderSelectPopup fsp;
            fsp = new FolderSelectPopup();
            fsp.captionSetting = caption;
            fsp.StartPosition = FormStartPosition.CenterParent;

            //渡されているparentHandleが親フォームなので、親フォームの中央に表示する。
            fsp.ShowDialog(Control.FromHandle(parentHandle));

            selectFolder = fsp.getDirPath;

            fsp.Dispose();
            fsp = null;

            return selectFolder;
        }

        public string SelectFolder(string caption, string initialPath, IntPtr parentHandle,bool isFileSelect)
        {
            string selectFolder = "";

            if (isFileSelect == true)
            {
                selectFolder = getFolderPath(caption, initialPath, parentHandle, isFileSelect);
            }
            else
            {
                selectFolder = SelectFolder(caption, initialPath, parentHandle);
            }

            return selectFolder;
        }

        public string getFolderPath(string caption, string initialPath, IntPtr parentHandle)
        {

            _initialPath = initialPath;
            StringBuilder sb = new StringBuilder(1024);
            IntPtr bufferAddress = Marshal.AllocHGlobal(1024); ;
            IntPtr pidl = IntPtr.Zero;
            BROWSEINFO bi = new BROWSEINFO();
            bi.hwndOwner = parentHandle;
            bi.pidlRoot = IntPtr.Zero;
            bi.lpszTitle = caption;
            bi.ulFlags = BIF.BIF_NEWDIALOGSTYLE | BIF.BIF_SHAREABLE;
            bi.lpfn = new BrowseCallBackProc(OnBrowseEvent);
            bi.lParam = IntPtr.Zero;
            bi.iImage = 0;

            try
            {
                pidl = SHBrowseForFolder(ref bi);
                if (true != SHGetPathFromIDList(pidl, bufferAddress))
                {
                    return null;
                }
                sb.Append(Marshal.PtrToStringAuto(bufferAddress));
            }
            finally
            {
                // Caller is responsible for freeing this memory.
                Marshal.FreeCoTaskMem(pidl);
            }

            return sb.ToString();
        }

        /// <summary>
        /// フォルダ指定ダイアログを表示
        /// </summary>
        /// <param name="caption">ヘッダに表示する文字列(string)</param>
        /// <param name="initialPath">初期表示するディレクトリ文字列(string)</param>
        /// <param name="parentHandle">呼び出し元のWindowHandle</param>
        /// <param name="isFileSelect">ファイル指定ダイアログの場合Trueを指定</param>
        /// <returns>選択したファイル・ディレクトリのフルパス、またキャンセルした場合はブランク</returns>
        public string getFolderPath(string caption, string initialPath, IntPtr parentHandle,bool isFileSelect)
        {
            //ターミナルモードの取得
            bool isTerminalMode;
            isTerminalMode = r_framework.Dto.SystemProperty.IsTerminalMode;

            _initialPath = initialPath;
            StringBuilder sb = new StringBuilder(1024);
            IntPtr bufferAddress = Marshal.AllocHGlobal(1024); ;
            IntPtr pidl = IntPtr.Zero;
            BROWSEINFO bi = new BROWSEINFO();
            bi.hwndOwner = parentHandle;
            if (caption == "")
            {
                if (isFileSelect == true)
                {
                    bi.lpszTitle = "ファイルを選択してください";
                }
                else
                {
                    bi.lpszTitle = "フォルダを選択してください";
                }
            }
            else
            {
                bi.lpszTitle = caption;
            }
            if (isTerminalMode == true)
            {
                bi.pidlRoot = new IntPtr(CSIDL.DRIVES);
                if (isFileSelect == true)
                {
                    bi.ulFlags = BIF.BIF_BROWSEINCLUDEFILES + BIF.BIF_DONTGOBELOWDOMAIN + BIF.BIF_RETURNONLYFSDIRS + BIF.BIF_RETURNFSANCESTORS;
                }
                else
                {
                    bi.ulFlags = BIF.BIF_DONTGOBELOWDOMAIN + BIF.BIF_RETURNONLYFSDIRS + BIF.BIF_RETURNFSANCESTORS;
                }
            }
            else
            {
                bi.pidlRoot = IntPtr.Zero;
                if (isFileSelect == true)
                {
                    bi.ulFlags = BIF.BIF_BROWSEINCLUDEFILES;
                }
                else
                {
                    bi.ulFlags = 0;
                }
            }
            bi.lpfn = new BrowseCallBackProc(OnBrowseEvent);
            bi.lParam = IntPtr.Zero;
            bi.iImage = 0;

            try
            {
                if (isFileSelect == true)
                {
                    bool isFile = false;
                    //ファイルが選択されるかキャンセルされるまでループする
                    do
                    {
                        //値の初期化
                        sb.Clear();

                        pidl = SHBrowseForFolder(ref bi);

                        if (pidl.Equals(IntPtr.Zero))
                        {
                            //キャンセルされた場合は抜ける
                            break;
                        }

                        //通さないとsb.ToString()が最適化されない
                        SHGetPathFromIDList(pidl, bufferAddress);

                        sb.Append(Marshal.PtrToStringAuto(bufferAddress));
                        if (System.IO.File.Exists(sb.ToString()) == true)
                        {
                            //選択したアイテムがファイル
                            isFile = true;
                        }
                        else
                        {
                            //選択したアイテムがファイルでない
                            isFile = false;
                        }

                        if (isFile != true)
                        {
                            var messageBoxShowLogic = new MessageBoxShowLogic();

                            DialogResult result;
                            result = messageBoxShowLogic.MessageBoxShow("E051","有効なファイル");
                        }
                    }
                    while (isFile != true);
                }
                else
                {
                    bool isDirectory = false;
                    do
                    {
                        //値の初期化
                        sb.Clear();

                        pidl = SHBrowseForFolder(ref bi);

                        if (pidl.Equals(IntPtr.Zero))
                        {
                            //キャンセルされた場合は抜ける
                            break;
                        }

                        //通さないとsb.ToString()が最適化されない
                        SHGetPathFromIDList(pidl, bufferAddress);

                        sb.Append(Marshal.PtrToStringAuto(bufferAddress));
                        if (System.IO.Directory.Exists(sb.ToString()) == true)
                        {
                            isDirectory = true;
                        }
                        else
                        {
                            isDirectory = false;
                        }

                        if (isDirectory != true)
                        {
                            var messageBoxShowLogic = new MessageBoxShowLogic();

                            DialogResult result;
                            result = messageBoxShowLogic.MessageBoxShow("E051","有効なフォルダ");
                        }
                    }
                    while (isDirectory != true);
                }
            }
            finally
            {
                // Caller is responsible for freeing this memory.
                Marshal.FreeCoTaskMem(pidl);
            }

            return sb.ToString();
        }

        public void Dispose()
        {
        }

    }
}