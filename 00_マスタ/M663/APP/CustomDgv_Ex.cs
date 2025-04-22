using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using r_framework.CustomControl;
using System.Windows.Forms;

namespace Shougun.Core.Master.CourseIchiran
{
    /// <summary>
    /// 継承したデータグリッドビーユー
    /// </summary>
    public partial class CustomDgv_Ex : CustomDataGridView
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomDgv_Ex()
        {
            InitializeComponent();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public CustomDgv_Ex(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// セル境界線ダブルクリック
        /// </summary>
        /// <param name="e"></param>
        public void OnColumnDividerDoubleClick(DataGridViewColumnDividerDoubleClickEventArgs e)
        {
            base.OnColumnDividerDoubleClick(e);
            this.AutoResizeColumnHeadersHeight();
        }
    }
}
