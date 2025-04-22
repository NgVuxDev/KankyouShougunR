using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Const;

namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomi
{
    public partial class UIHeader : HeaderBaseForm
    {
        //アラート件数（初期値）
        public Int32 InitialNumberAlert = 0;

        //アラート件数
        public Int32 NumberAlert = 0;

        //Form
        //public UIForm form;

        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        #region イベント

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //base.lb_title.Text = "入金消込履歴一覧";
            //base.lb_title.Text =WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_NYUKIN_KESHIKOMI_RIREKI);

        }

        //2013.12.23 naitou upd start
        private void alertNumber_Validating(object sender, CancelEventArgs e)
        {
            if (this.alertNumber.Text == "") this.alertNumber.Text = "0";
            this.NumberAlert = Int32.Parse(this.alertNumber.Text);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("HOGE");
        //}

        //private void radbtnDenpyouHiduke_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.radbtnDenpyouHiduke.Checked)
        //    {
        //        this.lbl_DenpyoDate.Text = "伝票日付";
        //        this.form.Nyuukin_CD.Enabled = false;
        //        this.form.SEIKYUU_NUMBER.Enabled = false;
        //        this.HIDUKE_FROM.Enabled = true;
        //        this.HIDUKE_TO.Enabled = true;
        //        this.form.Nyuukin_CD.Text = "";
        //        this.form.SEIKYUU_NUMBER.Text = "";

        //        if (Properties.Settings.Default.SET_HIDUKE_FROM != "")
        //        {
        //            this.HIDUKE_FROM.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_FROM);
        //        }
        //        else
        //        {
        //            this.HIDUKE_FROM.Value = DateTime.Now.Date;
        //        }
        //        if (Properties.Settings.Default.SET_HIDUKE_TO != "")
        //        {
        //            this.HIDUKE_TO.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_TO);
        //        }
        //        else
        //        {
        //            this.HIDUKE_TO.Value = DateTime.Now.Date;
        //        }
        //    }
        //}

        //private void radbtnNyuuryokuHiduke_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.radbtnNyuuryokuHiduke.Checked)
        //    {
        //        this.lbl_DenpyoDate.Text = "請求日付";
        //        this.form.Nyuukin_CD.Enabled = false;
        //        this.form.SEIKYUU_NUMBER.Enabled = false;
        //        this.HIDUKE_FROM.Enabled = true;
        //        this.HIDUKE_TO.Enabled = true;
        //        this.form.Nyuukin_CD.Text = "";
        //        this.form.SEIKYUU_NUMBER.Text = "";

        //        if (Properties.Settings.Default.SET_HIDUKE_FROM != "")
        //        {
        //            this.HIDUKE_FROM.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_FROM);
        //        }
        //        else
        //        {
        //            this.HIDUKE_FROM.Value = DateTime.Now.Date;
        //        }
        //        if (Properties.Settings.Default.SET_HIDUKE_TO != "")
        //        {
        //            this.HIDUKE_TO.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_TO);
        //        }
        //        else
        //        {
        //            this.HIDUKE_TO.Value = DateTime.Now.Date;
        //        }
        //    }
        //}

        //private void radbtnHidukeNasi_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.radbtnHidukeNasi.Checked)
        //    {
        //        this.lbl_DenpyoDate.Text = "日付なし";
        //        this.form.Nyuukin_CD.Enabled = true;
        //        this.form.SEIKYUU_NUMBER.Enabled = true;
        //        this.HIDUKE_FROM.Enabled = false;
        //        this.HIDUKE_TO.Enabled = false;
        //        this.HIDUKE_FROM.Text = "";
        //        this.HIDUKE_TO.Text = "";
        //    }
        //}

        ////deleteキー制約
        //private void txtNum_HidukeSentaku_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
        //    {
        //        txtNum_HidukeSentaku.Text = "";
        //        radbtnDenpyouHiduke.Checked = false;
        //        radbtnNyuuryokuHiduke.Checked = false;
        //    }   
        //}

        ////backspaceキー制約
        //private void txtNum_HidukeSentaku_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar < '0' || '2' < e.KeyChar)
        //    {
        //        //押されたキーが 0～9でない場合は、イベントをキャンセルする
        //        e.Handled = true;
        //    }
        //    if (e.KeyChar == '1')
        //    {
        //        radbtnDenpyouHiduke.Checked = true;
        //        txtNum_HidukeSentaku.SelectAll();
        //        e.Handled = true;
        //    }
        //    if (e.KeyChar == '2')
        //    {
        //        radbtnNyuuryokuHiduke.Checked = true;
        //        txtNum_HidukeSentaku.SelectAll();
        //        e.Handled = true;
        //    }
        //}

        //private void KYOTEN_CD_Leave(object sender, EventArgs e)
        //{
        //    int i;
        //    if (!int.TryParse(this.KYOTEN_CD.Text, out i))
        //    {
        //        this.KYOTEN_CD.Text = string.Empty;
        //    }
        //}
        //2013.12.23 naitou upd end

        #endregion
    }
}
