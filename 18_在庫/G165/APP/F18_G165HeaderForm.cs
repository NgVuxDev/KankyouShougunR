// $Id: F18_G165HeaderForm.cs 4506 2013-10-23 02:50:20Z sys_dev_18 $

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.Const;

namespace Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.APP
{
    public partial class F18_G165HeaderForm : HeaderBaseForm
    {
        public F18_G165HeaderForm()
        {
            InitializeComponent();

            // 区分非表示
            //base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // ヘッダ文字列設定
            //base.lb_title.Text = ConstCls.FORM_TITLE;
        }
    }
}
