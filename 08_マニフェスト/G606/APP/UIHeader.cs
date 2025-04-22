using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpan
{
    /// <summary>
    /// 実績報告書（運搬実績）ヘッダー部
    /// </summary>
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIHeader()
        {
            InitializeComponent();
            base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
