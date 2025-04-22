using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using Shougun.Core.ExternalConnection.SmsResult;
using Shougun.Core.ExternalConnection.SmsResult.Logic;

namespace Shougun.Core.ExternalConnection.SmsResult.APP
{
    public partial class UIHeader : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        public UIHeader()
        {
            InitializeComponent();

            base.windowTypeLabel.Visible = false;
        }
    }
}
