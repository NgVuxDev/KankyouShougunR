using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.GenbamemoIchiran;
using r_framework.Const;

namespace Shougun.Core.ExternalConnection.GenbamemoIchiran
{
    public partial class UIHeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// タイトルラベルの最大横幅
        /// </summary>
        private int TitleMaxWidth = 290;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        public LogicCls logic;

        /// <summary>
        /// UIFrom
        /// </summary>
        public UIForm form;

        public UIHeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = ConstCls.Title;
            //読込データ件数の初期値設定
            this.readDataNumber.Text = "0";
        }

        /// <summary>
        /// ヘッダテキスト変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_title_TextChanged(object sender, EventArgs e)
        {
            ControlUtility.AdjustTitleSize(lb_title, this.TitleMaxWidth);
        }

        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
        }

    }
}
