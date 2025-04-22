using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Common.BusinessCommon.Base.BaseForm
{
    public partial class BasePopForm : BusinessBaseForm
    {
        public BasePopForm(Form inForm, HeaderBaseForm headerForm)
            : base(inForm, headerForm)
        {
            InitializeComponent();

            base.IsPopupType = true; //ポップアップではリボンは非表示


            //baseでサイズ調整は行っている
            //this.Width = 1040;
            //this.Height = 780;
            //this.MaximizeBox = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //ポップアップでは、最小化と最大化、リサイズはできない。
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MinimizeBox = false;
            base.MaximizeBox = false;

        }
    }
}
