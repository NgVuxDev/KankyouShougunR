using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Shougun.Printing.Common.UI
{
    internal class DocumentPropertiesDialog
    {
        /// <summary>
        /// プリンタの「印刷設定」ダイアログを表示する。
        /// devModeInにnull指定した場合はデフォルト設定が表示される。
        /// </summary>
        public static DialogResult ShowDialog(IWin32Window window, string printerName, Byte [] devModeIn, out Byte [] devModeOut)
        {
            devModeOut = null;
            IntPtr hWnd = window.Handle;
            IntPtr hPrinter = IntPtr.Zero;
            IntPtr pDevModeInput = IntPtr.Zero;
            IntPtr pDevModeOutput = IntPtr.Zero;

            try
            {
                // プリンタのハンドル取得
                if (!Native.OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                {
                    throw new Win32Exception();
                }

                // DEVMODE構造体(現在のプロパティ)のハンドルを取得
                if (devModeIn != null)
                {
                    pDevModeInput = Marshal.AllocHGlobal(devModeIn.Length);
                    Marshal.Copy(devModeIn, 0, pDevModeInput, devModeIn.Length);
                }
               

                // プロパティの格納に必要なサイズを取得(fMode=0)
                int size = Native.DocumentProperties(hWnd, hPrinter, printerName,
                            IntPtr.Zero, IntPtr.Zero, 0);
                if (size < 0)
                {
                    throw new Win32Exception();
                }

                // DEVMODE構造体(新しいプロパティを格納する)メモリの確保
                pDevModeOutput = Marshal.AllocHGlobal(size);

                // プロパティダイアログの表示
                int fMode = Native.DM_IN_PROMPT | Native.DM_OUT_BUFFER;
                if (pDevModeInput != null)
                {
                    fMode |= Native.DM_IN_BUFFER;
                }
                int ret = Native.DocumentProperties(hWnd, hPrinter, printerName,
                            pDevModeOutput, pDevModeInput, fMode);
                if (ret == 1)       // IDOK
                {
                    devModeOut = new byte[size];
                    Marshal.Copy(pDevModeOutput, devModeOut, 0, size);

                    return DialogResult.OK;
                }
                else if (ret == 2)  //IDCANCEL
                {
                    return DialogResult.Cancel;
                }
                else
                {
                    throw new Win32Exception();
                }
            }
            finally
            {
                if (pDevModeInput != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pDevModeInput);
                }
                if (pDevModeOutput != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pDevModeOutput);
                }
                if (hPrinter != IntPtr.Zero)
                {
                    Native.ClosePrinter(hPrinter);
                }
            }
        }
    }
}
