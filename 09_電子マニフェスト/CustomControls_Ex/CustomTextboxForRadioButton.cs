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
    public partial class CustomTextboxForRadioButton : r_framework.CustomControl.CustomNumericTextBox2
    {
        #region  Properties  
        [Category("EDISON 拡張 プロパティ"), Description("連動先のRadioボタン名称配列")]
        public string[] RadioBtn_ControlNames { get; set; }
        #endregion  Properties  
  
        //===============================================================================================  
        private string _lastValue = string.Empty;
        public string GetlastValue() { return _lastValue; }
        // Constructor  
        public CustomTextboxForRadioButton(): base()
        {
            this.MaxLength = 1;
        }
        
        /// <summary>
        /// Find Control
        /// </summary>
        /// <param name="root">root control</param>
        /// <param name="target">controlName</param>
        /// <returns></returns>
        static Control FindControl(Control root, string target)
        {
            if (root == null){
                return null;
            }
            if (root.Name.Equals(target))
                return root;
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
        public bool HaveBeChanged() { return (_lastValue != this.Text); }

        #region  OverrideMethods  

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if( _lastValue != this.Text){
                _lastValue = this.Text;
            }
            if (RadioBtn_ControlNames == null || RadioBtn_ControlNames.Length ==0) return;

            if (string.IsNullOrEmpty(this.Text))
            {
                for (int i = 0; i < RadioBtn_ControlNames.Length; i++)
                {
                    if (!string.IsNullOrEmpty(RadioBtn_ControlNames[i])){
                        Control ctl = FindControl(this.Parent, RadioBtn_ControlNames[i]);
                        if (ctl != null){
                            if ((ctl as RadioButton).Checked){
                                (ctl as RadioButton).Checked = false;
                                break;   
                            }
                        }
                    }
                }
                return;
            }
            try
            {
                int nVal = Int16.Parse(this.Text);
                for (int i = 0; i < RadioBtn_ControlNames.Length; i++)
                {
                    if (!string.IsNullOrEmpty(RadioBtn_ControlNames[i]))
                    {
                        Control ctl = FindControl(this.Parent, RadioBtn_ControlNames[i]);
                        if (ctl != null)
                        {
                            if (nVal == i + 1)
                            {
                                if (!(ctl as RadioButton).Checked)
                                {
                                    (ctl as RadioButton).Checked = true;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch{
                this.Text = "1";
                return;
            }
            
            
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (RadioBtn_ControlNames == null || 
                RadioBtn_ControlNames.Length == 0 || 
                RadioBtn_ControlNames.Length > 9 || 
                e.KeyChar == 8) return;

            if(!char.IsDigit(e.KeyChar)){
                e.Handled = true;
                return;
            }
            int cnt = RadioBtn_ControlNames.Length;
            if (e.KeyChar < 49 || e.KeyChar > cnt + 48)
            {
                e.Handled = true;
                return;
            }
        }  
        #endregion  OverrideMethods  
 
    }  
}  

