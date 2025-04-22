using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;

namespace Shougun.Core.Common.InsatsuSettei
{
    public partial class UIHeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        public UIHeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        
      
    }
}
