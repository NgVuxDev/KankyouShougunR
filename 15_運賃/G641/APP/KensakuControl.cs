using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;

namespace Shougun.Core.Carriage.Unchinichiran.APP
{
    public partial class KensakuControl : UserControl
    {
        public KensakuControl(LogicClass logicclass)
        {
            InitializeComponent();
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = logicclass;
        }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        // 運搬業者CD
        private void UNNBANGYOUSYA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckUnpanGyousha();
        }
    }
}
