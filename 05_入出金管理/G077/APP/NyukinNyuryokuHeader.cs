using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Access;

namespace Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku
{
    public partial class HeaderSample : r_framework.APP.Base.HeaderBaseForm
    {
        public HeaderSample()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        //数字以外、効かない
        private void KYOTEN_CD_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar < '0' || '9' < e.KeyChar)
            //{
            //    //押されたキーが 0～9でない場合は、イベントをキャンセルする
            //    e.Handled = true;
            //}
        }
    }
}
