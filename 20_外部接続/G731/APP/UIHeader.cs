using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.ExternalConnection.FileUploadIchiran.APP
{
    public partial class UIHeader : r_framework.APP.Base.HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();

            base.windowTypeLabel.Visible = false;
        }
    }
}
