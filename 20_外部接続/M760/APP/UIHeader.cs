using System;
using r_framework.APP.Base;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.DenshiBunshoHoshu
{
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// システムID
        /// </summary>
        public long SystemId { get; set; }

        public UIHeader()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            base.OnLoad(e);
            base.lb_title.Text = "電子文書詳細入力";
            LogUtility.DebugMethodEnd();
        }
    }
}