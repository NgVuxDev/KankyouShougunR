using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using r_framework.CustomControl;
using System.Runtime.InteropServices;

namespace Shougun.Core.ElectronicManifest.CustomControls_Ex
{
    public partial class CustomPopupOpenButton_Ex : Button
    {
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYPRESS = 0x102;//KeyPress
        const int KEY_SPACE = 0x20;
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        #region  Properties
        [Category("EDISON 拡張 プロパティ"), Description("連動先のテキストボックス名前")]
        public String TextBox_ControlName { get; set; }

        #endregion  Properties  
        //=================================================================================
        public CustomPopupOpenButton_Ex()
            : base()
        {
            
        }
        private Control FindControl(string target)
        {
            Control ctl = null;
            ctl = FindControl(this.Parent, target);
            if (ctl == null)
            {
                if (this.Parent == null) return ctl;
                ctl = FindControl(this.Parent.Parent, target);
                if (ctl == null)
                {
                    if (this.Parent.Parent == null) return ctl;
                    ctl = FindControl(this.Parent.Parent.Parent, target);
                    if (ctl == null)
                    {
                        if (this.Parent.Parent.Parent == null) return ctl;
                        ctl = FindControl(this.Parent.Parent.Parent.Parent, target);
                        if (ctl == null)
                        {
                            if (this.Parent.Parent.Parent.Parent == null) return ctl;
                            ctl = FindControl(this.Parent.Parent.Parent.Parent.Parent, target);
                            if (ctl == null) return ctl;
                        }
                    }
                }
            }
            return ctl;
        }

        /// <summary>
        /// Find Control
        /// </summary>
        /// <param name="root">root control</param>
        /// <param name="target">controlName</param>
        /// <returns></returns>
        static Control FindControl(Control root, string target)
        {
            if (root == null) return null;
            if (root.Name.Equals(target)) return root;
            for (var i = 0; i < root.Controls.Count; ++i)
            {
                if (root.Controls[i].Name.Equals(target))
                    return root.Controls[i];
            }
            for (var i = 0; i < root.Controls.Count; ++i)
            {
                Control result;
                for (var k = 0; k < root.Controls[i].Controls.Count; ++k)
                {
                    result = FindControl(root.Controls[i].Controls[k], target);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }      
         /// <summary>
        /// ボタンクリック処理
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBox_ControlName))
            {
                Control ctl = FindControl(this.TextBox_ControlName);
                if ((ctl as TextBox)!=null){
                    //spaceキーダウンイベントをメッセージを発送
                    SendMessage(ctl.Handle, WM_KEYPRESS, KEY_SPACE, 0);
                    SendMessage(ctl.Handle, WM_KEYDOWN, KEY_SPACE, 0);    
                }
            }
            base.OnClick(e);
        }

    }
}
