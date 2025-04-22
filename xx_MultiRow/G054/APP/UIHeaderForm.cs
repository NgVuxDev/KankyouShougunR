using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku
{
    public partial class UIHeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        public LogicClass logic;    // No.3822

        public UIHeaderForm()
        {
            InitializeComponent();
        }

        // No.3822-->
        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (this.logic != null)
                {
                    var forward = (Control.ModifierKeys & Keys.Shift) != Keys.Shift;
                    if (ActiveControl != null)
                    {
						//20151026 hoanghm #13404 start
                        //this.SelectNextControl(ActiveControl, !forward, true, true, true);  // Activeが変更になっているため前の位置に戻す
                        var uiForm = ((UIForm)((BusinessBaseForm)this.Parent).inForm);
                        foreach (Control c in this.allControl)
                        {
                            if (c.Name.Equals(uiForm.beforbeforControlName))
                            {
                                this.ActiveControl = c;
                                break;
                            }
                        }
						//20151026 hoanghm #13404 end
                        this.logic.GotoNextControl(forward);
                    }
                    else
                    {
                        if (forward)
                        {
                            if (this.KYOTEN_CD.TabStop == true)
                            {
                                this.KYOTEN_CD.Focus();
                            }
                        }
                        else
                        {
                            if (this.KYOTEN_CD.TabStop == true)
                            {
                                // SHIFT+ENTERの場合特殊処理
                                if (e.KeyChar == (char)Keys.Tab)
                                {
                                    this.KYOTEN_CD.Focus();
                                }
                            }
                        }
						//20151026 hoanghm #13404 start
						//this.logic.GotoNextControl(forward);
                        if (this.KYOTEN_CD.TabStop == true)
                        {
                            this.logic.GotoNextControl(forward);
                        }
						//20151026 hoanghm #13404 end
                    }
                }
            }
        }
        // No.3822<--
    }
}
