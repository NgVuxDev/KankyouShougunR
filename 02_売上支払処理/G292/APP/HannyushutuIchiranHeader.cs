using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.SalesPayment.HannyushutsuIchiran
{
    public partial class HannyushutsuIchiranHeader : HeaderBaseForm
    {
        /// <summary>
        /// ヘッダクラス
        /// </summary>
        public HannyushutsuIchiranHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// 起動時処理
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
