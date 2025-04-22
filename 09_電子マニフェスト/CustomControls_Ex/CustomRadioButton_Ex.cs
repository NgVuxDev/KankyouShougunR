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


namespace Shougun.Core.ElectronicManifest.CustomControls_Ex
{
    public partial class CustomRadioButton_Ex : r_framework.CustomControl.CustomRadioButton
    {
        #region  Properties  
        [Category("EDISON 拡張 プロパティ"), Description("連動先のテキストボックス名前")]
        public String TextBox_ControlName  { get; set; }

        #endregion  Properties  
  
        //===============================================================================================  

        // Constructor  
        public CustomRadioButton_Ex()
            : base()  {}

        private Control FindControl(string target)
        {
            Control ctl = null;
            ctl = FindControl(this.Parent, target);
            if (ctl == null){
                if (this.Parent == null) return ctl;
                ctl = FindControl(this.Parent.Parent, target);
                if (ctl == null){
                    if (this.Parent.Parent == null) return ctl;
                    ctl = FindControl(this.Parent.Parent.Parent, target);
                    if (ctl==null){
                        if (this.Parent.Parent.Parent == null) return ctl;
                        ctl = FindControl(this.Parent.Parent.Parent.Parent, target);
                        if (ctl==null){
                            if (this.Parent.Parent.Parent.Parent == null)return ctl;
                            ctl = FindControl(this.Parent.Parent.Parent.Parent.Parent, target);
                            if (ctl==null) return ctl;
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
        #region  OverrideMethods  

        protected override void OnCheckedChanged(EventArgs e)  
        { 
            base.OnCheckedChanged(e);
            if (!this.Checked) return;
            if (string.IsNullOrEmpty(this.TextBox_ControlName)) return;
            Control ctlTextbox = FindControl(this.TextBox_ControlName);
            if (ctlTextbox == null) return;
            if (ctlTextbox.GetType().ToString() != "Shougun.Core.ElectronicManifest.CustomControls_Ex.CustomTextboxForRadioButton") return;
            CustomTextboxForRadioButton myTextCtl = ctlTextbox as CustomTextboxForRadioButton;
            if (myTextCtl.RadioBtn_ControlNames == null || myTextCtl.RadioBtn_ControlNames.Length == 0) return;
            for (int i = 0; i < myTextCtl.RadioBtn_ControlNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(myTextCtl.RadioBtn_ControlNames[i])){
                    Control ctlRadio = FindControl(this.Parent, myTextCtl.RadioBtn_ControlNames[i]);
                    if (ctlRadio != null){
                        if (ctlRadio.Equals(this)){
                            if ((ctlRadio as RadioButton).Checked){
                                myTextCtl.Text = (i + 1).ToString();
                                return;
                            }
                        }
                    }
                }
             }
           
        }
        #endregion  OverrideMethods 
 
    }  
}  

