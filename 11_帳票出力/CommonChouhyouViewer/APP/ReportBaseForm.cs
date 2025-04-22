using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.ReportOutput.CommonChouhyouViewer
{
    public partial class ReportBaseForm : BusinessBaseForm
    {
        public ReportBaseForm(Form inForm, HeaderBaseForm headerForm)
            : base(inForm, headerForm)
        {
            InitializeComponent();

            //ポップアップではリボンは非表示
            base.IsPopupType = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //// フォームタイトル設定
            //((HeaderBaseForm)base.headerForm).lb_title.Text += "／一覧";

            //// Windowタイトル
            //this.Text += "／一覧";

            // サイズ調整
            if (base.headerForm == null)
            {
                base.Width = base.inForm.Width + 30;
            }
            else
            {
                base.Width = (base.headerForm.Width > base.inForm.Width ? base.headerForm.Width : base.inForm.Width) + 30;
            }

            //ポップアップでは、最小化と最大化、リサイズはできない。
            //base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

        }
    }
}
