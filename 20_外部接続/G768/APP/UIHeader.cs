using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using Shougun.Core.ExternalConnection.SmsIchiran;
using Shougun.Core.ExternalConnection.SmsIchiran.Logic;

namespace Shougun.Core.ExternalConnection.SmsIchiran.APP
{
    public partial class UIHeader : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        public UIHeader(UIForm callForm)
        {
            InitializeComponent();

            base.windowTypeLabel.Visible = false;

            this.form = callForm;
        }

        private void SMS_SEND_JOKYO_TextChanged(object sender, EventArgs e)
        {
            this.form.logic.headerForm = this;

            // 送信状況の値より、各項目設定
            this.form.logic.SmsStatusSetting();
        }
    }
}
