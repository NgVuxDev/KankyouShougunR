using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;  
using r_framework.CustomControl;

namespace Shougun.Core.ElectronicManifest.CustomControls_Ex
{
    public partial class CustomTextboxForFullWordsPaste : r_framework.CustomControl.CustomTextBox
    {
        #region  Properties  
        [Category("EDISON 拡張 プロパティ"), Description("全角文字の貼付け機能の可否フラグ設定")]
        public bool IsFullWordsPasteEnable { get; set; }
        #endregion  Properties  
  
        //===============================================================================================  

        #region Constants
        /// <summary>
        /// Windows message that is sent when a paste event occurs.
        /// </summary>
        public const int WM_PASTE = 0x0302;

        #endregion

        // Constructor  
        public CustomTextboxForFullWordsPaste()
            : base()
        {
            IsFullWordsPasteEnable = false;
        }

        #region  OverrideMethods  
        /// <summary>
        /// Windowsのメッセージの絞込みで貼付けメッセージ処理だけ
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                if(!this.IsFullWordsPasteEnable)
                {
                    string clipboardText = string.Empty;
                    try
                    {
                        clipboardText = Clipboard.GetText(TextDataFormat.Text);
                        if (clipboardText.Length > 0) // Does clipboard contain text?
                        {
                            int charSBC = Regex.Matches(clipboardText, @"[^\x00-\xff]").Count;
                            //全角文字ある場合、貼付けしない
                            if (charSBC > 0){
                                m.Result = IntPtr.Zero;
                                return;
                            }
                        }
                    }
                    catch
                    {
                        base.WndProc(ref m);
                    }
                }
            }
            base.WndProc(ref m);
        }
       
        #endregion  OverrideMethods  
 
    }  
}  

