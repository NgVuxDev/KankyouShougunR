// $Id: UIHeaderForm.cs 7927 2013-11-22 06:57:47Z sys_dev_22 $

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Reception.UketsukeKuremuNyuuryoku
{
    public partial class UIHeaderForm : HeaderBaseForm
    {
        public UIHeaderForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

    }
}
