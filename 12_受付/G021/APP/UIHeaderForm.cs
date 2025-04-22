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
using Shougun.Core.Reception.UketukeiIchiran;
using r_framework.Const;

namespace Shougun.Core.Reception.UketukeiIchiran
{
    public partial class UIHeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        // VUNGUYEN 20150703 START
        /// <summary>
        /// タイトルラベルの最大横幅
        /// </summary>
        private int TitleMaxWidth = 290;
        // VUNGUYEN 20150703 END

        /// <summary>
        /// 画面ロジック
        /// </summary>
        public LogicCls logic;    // No.3123

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
        /// 伝票日付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radbtnDenpyouHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnDenpyouHiduke.Checked)
            {
                this.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_DenPyou;
            }
        }

        /// <summary>
        /// 入力日付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radbtnNyuuryokuHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnNyuuryokuHiduke.Checked)
            {
                this.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_NyuuRyoku;
            }
        }

        /// <summary>
        /// 作業日
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radbtnSagyoubi_CheckedChanged(object sender, EventArgs e)
        {
            // 日付のヒントテキスト
            if (this.radbtnSagyoubi.Checked)
            {
                this.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_Sagyou;
            }
        }

        /// <summary>
        /// 日付選択フォーカス移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_HidukeSentaku_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_HidukeSentaku.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", ConstCls.HidukeCD_DenPyou, ConstCls.HidukeCD_Sagyou);
                e.Cancel = true;
            }
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

        // No.3123-->
        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
        }
        // No.3123<--

        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end
    }
}
