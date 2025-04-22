using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.Master.ChiikiIkkatsu
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        public HeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }
    }
}
