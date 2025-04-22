// $Id: UIHeader.cs 47181 2015-04-13 07:00:26Z chenzz@oec-h.com $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Stock.ZaikoTyouseiIchiran
{
    public partial class UIHeader : r_framework.APP.Base.HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
            this.lb_title.Text = "在庫調整一覧";
        }

        #region イベント

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }
        #endregion
    }
}
