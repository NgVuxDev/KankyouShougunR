// $Id: UIForm.cs 24123 2014-06-27 02:52:37Z sanbongi $
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

namespace Shougun.Core.PaperManifest.JissekiHokokuIchiran
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 設置コンテナ一覧画面ロジック
        /// </summary>
        private LogicCls logic;

        // <summary>
        /// header_new
        /// </summary>
        UIHeader header_new;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region UIForm
        // <summary>
        /// UIForm
        /// </summary>
        public UIForm( UIHeader headerForm)
            : base(WINDOW_ID.T_JISSEKIHOKOKU_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            //Header部の項目を初期化
            this.header_new = headerForm;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            //ロジックに、Header部情報を設定する
            logic.SetHeader(header_new);
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

        }
        #endregion

        /// <summary>
        /// 排出業者POPUP後処理
        /// </summary>
        public void PopupAfterSearchDate()
        {
            //this.cantxt_Nento.Text = DateTime.Parse(this.cantxt_Nento.Text).ToString("yyyy年度");

        }

        private void cantxt_Nento_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cantxt_Nento.Text))
            {
                if (!this.DataCheck())
                {
                    this.cantxt_Nento.Focus();
                    return;
                }

                int year = int.Parse(this.cantxt_Nento.Text);

                if (year == 1753)
                {
                    this.cbtn_Previous.Enabled = false;
                    this.cbtn_Next.Enabled = true;
                }
                else if (year == 9999)
                {
                    this.cbtn_Previous.Enabled = true;
                    this.cbtn_Next.Enabled = false;
                }
                else
                {
                    this.cbtn_Previous.Enabled = true;
                    this.cbtn_Next.Enabled = true;
                }
            }
        }

        private void cbtn_Previous_Click(object sender, EventArgs e)
        {
            string nento = this.cantxt_Nento.Text;
            if (!string.IsNullOrEmpty(nento))
            {
                this.cantxt_Nento.Text = (Convert.ToInt32(nento) - 1).ToString();

                if (Convert.ToInt32(nento) - 1 == 1753)
                {
                    this.cbtn_Previous.Enabled = false;
                }
                else
                {
                    this.cbtn_Previous.Enabled = true;
                }
                this.cbtn_Next.Enabled = true;
            }
        }

        private void cantxt_Nento_Enter(object sender, EventArgs e)
        {
            string nento = this.cantxt_Nento.Text;
            if (!string.IsNullOrEmpty(nento) && nento.Length >= 4)
            {
                nento = nento.Substring(0, 4);
                this.cantxt_Nento.Text = (Convert.ToInt32(nento)).ToString();
            }
        }

        private void cbtn_Next_Click(object sender, EventArgs e)
        {
            string nento = this.cantxt_Nento.Text;
            if (!string.IsNullOrEmpty(nento))
            {
                this.cantxt_Nento.Text = (Convert.ToInt32(nento) + 1).ToString();

                this.cbtn_Previous.Enabled = true;
                if (Convert.ToInt32(nento) + 1 == 9999)
                {
                    this.cbtn_Next.Enabled = false;
                }
                else
                {
                    this.cbtn_Next.Enabled = true;
                }
            }
        }

        /// 20141027 Houkakou 「実績報告書一覧」のクリック イベントを追加する　start
        private void Ichiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.Ichiran.SelectedRows.Count > 0)
            {
                string reportkbn = this.Ichiran.SelectedRows[0].Cells["REPORT_KBN"].Value.ToString();
                string systemid = this.Ichiran.SelectedRows[0].Cells["SYSTEM_ID"].Value.ToString();

                DataTable dt = this.logic.dao.GetDeleteJissekiHokokuData(systemid);
                if (dt.Rows.Count <= 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return;
                }

                if (reportkbn.Equals("1"))
                {

                    r_framework.FormManager.FormManager.OpenForm("G136", WINDOW_TYPE.UPDATE_WINDOW_FLAG, systemid);
                }
                else if (reportkbn.Equals("2"))
                {
                    r_framework.FormManager.FormManager.OpenForm("G604", WINDOW_TYPE.UPDATE_WINDOW_FLAG, systemid);
                }
                else if (reportkbn.Equals("3"))
                {
                    r_framework.FormManager.FormManager.OpenForm("G607", WINDOW_TYPE.UPDATE_WINDOW_FLAG, systemid);
                }
            }
        }
        /// 20141027 Houkakou 「実績報告書一覧」のクリック イベントを追加する　end

        /// <summary>
        /// 報告年度チェック
        /// </summary>
        /// <returns></returns>
        private bool DataCheck()
        {
            if (string.IsNullOrEmpty(this.cantxt_Nento.Text))
            {
                return true;
            }

            int year = int.Parse(this.cantxt_Nento.Text);
            if (year < 1753)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E078", "「1753年度」");
                return false;
            }

            return true;
        }

    }
}
