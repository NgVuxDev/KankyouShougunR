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

namespace Shougun.Core.Common.IchiranSyu
{
    /// <summary>
    /// ヘッダーベースクラス
    /// </summary>
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIHeader()
            :base()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        //画面ロード
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}