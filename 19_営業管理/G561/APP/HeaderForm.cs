using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NyuukinsakiIchiran.APP
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
            this.lb_title.Text = "承認済電子申請一覧";
        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// 初回表出イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // 読込件数の初期化
            //this.ReadDataNumber.Text = "0";
        }

        /// <summary>
        /// アラート件数の検証後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AlertNumber_Validated(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.AlertNumber.Text))
            //{
                // 空だった場合0を入れる
            //    this.AlertNumber.Text = "0";
            //}
        }
    }
}
