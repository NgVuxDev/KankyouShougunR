using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Data;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;


namespace Shougun.Core.ElectronicManifest.CustomControls_Ex
{
    public partial class CustomForSyaryoNoKyoutuuPopup_Ex : CustomAlphaNumTextBox
    {
        #region  Properties  
        [Category("EDISON 電子加入者プロパティ"), Description("加入者番号所属コントロール名")]
        public String Gyousha_CD_ControlName { get; set; }
        [Category("EDISON 電子加入者プロパティ"), Description("加入者番号が必須入力チェックフラグ")]
        public bool IsInputMustbeCheck { get; set; }
      
        #endregion  Properties  
        
        // Constructor  
        public CustomForSyaryoNoKyoutuuPopup_Ex()
            : base()  
        {
            this.IsInputMustbeCheck = false;
        }
        
        /// <summary>
        /// 事業者コントロールのFind
        /// </summary>
        /// <param name="target">事業者CDコントロール名</param>
        /// <returns></returns>
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
                    if (ctl == null){
                        if (this.Parent.Parent.Parent == null) return ctl;
                        ctl = FindControl(this.Parent.Parent.Parent.Parent, target);
                        if (ctl == null){
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
        #region  OverrideMethods  

        /// <summary>前回のフォーカス</summary>
        private string prevControlName = string.Empty;

        /// <summary>
        /// OnEnter Event
        /// 汎用的なコントロールであるため、前回値の他に前回フォーカスしたコントロールを保持する。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            this.prevControlName = this.Name;
            base.OnEnter(e);
        }

        /// <summary>
        /// PreviewKeyDownイベントのオウバーリード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //必須入力チェックフラグの判断
            if (IsInputMustbeCheck)
            {
                if (!string.IsNullOrEmpty(this.Gyousha_CD_ControlName))
                {
                    Control ctl = FindControl(this.Gyousha_CD_ControlName);
                    if (ctl != null)
                    {
                        if (string.IsNullOrEmpty((ctl as TextBox).Text))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if(e.KeyChar==(Char)Keys.Tab){
                return;
            }
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //業者コード入力必須チェックの判断
            if (IsInputMustbeCheck){
                if (!string.IsNullOrEmpty(this.Gyousha_CD_ControlName))
                {
                    Control ctl = FindControl(this.Gyousha_CD_ControlName);
                    if (ctl != null){
                        if (string.IsNullOrEmpty((ctl as TextBox).Text)){
                            msgLogic.MessageBoxShow("E001","業者CD");
                            e.Handled = true;
                            //(ctl as TextBox).Focus();
                            return;
                        }
                    }
                }
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        /// OnValidating Event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            //編集不可の場合は、チェックを行わない
            if (this.ReadOnly)
            {
                return;
            }

            if (this.Name.Equals(this.prevControlName)
                && this.Text.Equals(this.prevText))
            {
                return;
            }

            base.OnValidating(e);
        }

        #endregion  OverrideMethods  
    }  
}  