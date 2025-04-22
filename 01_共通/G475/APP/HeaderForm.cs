// $Id: HeaderForm.cs 6980 2013-11-14 08:42:19Z sys_dev_27 $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;


namespace Shougun.Core.Common.ItakuKeiyakuSearch
{
    public partial class HeaderForm : HeaderBaseForm
    {
        public HeaderForm()
        {
            InitializeComponent();
            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //base.lb_title.Text = "受注目標件数入力";
            
        }

        /// <summary>
        /// 拠点コード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KYOTEN_CD_Leave(object sender, EventArgs e)
        {
            //int i;
            //if (!int.TryParse(this.KYOTEN_CD.Text, out i))
            //{
            //    this.KYOTEN_CD.Text = string.Empty;
            //}
        }
    }
}
