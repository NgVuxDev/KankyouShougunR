using r_framework.APP.PopUp.Base;
using Shougun.Core.Common.BusinessCommon.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;

namespace Shougun.Core.BusinessManagement.DenshiShinseiNyuuryoku
{
    public partial class KessaiPopupForm : SuperPopupForm
    {
        #region Field

        /// <summary>コメント</summary>
        public string Comment { get; set; }

        /// <summary>ステータス</summary>
        private DenshiShinseiUtility.DENSHI_SHINSEI_STATUS Status { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KessaiPopupForm(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS status)
        {
            this.Comment = string.Empty;
            this.Status = status;

            InitializeComponent();
        }

        #region Event

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            this.SetHeaderText();
            this.EventInit();
        }

        /// <summary>
        /// [F9]決定ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            var msgBox = new MessageBoxShowLogic();

            if (DialogResult.Yes == msgBox.MessageBoxShow("C046", "入力されている内容で登録"))
            {
                this.Comment = KESSAI_COMMENT.Text;
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// [F11]取消ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Screen Initialization

        /// <summary>
        /// HeaderTitleを設定します
        /// </summary>
        private void SetHeaderText()
        {
            if (DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL == this.Status)
            {
                this.lb_title.Text = "承認決裁";
            }
            else
            {
                this.lb_title.Text = "否認決裁";
            }
        }

        /// <summary>
        /// イベントの初期化を行います
        /// </summary>
        private void EventInit()
        {
            this.bt_func9.Click += new EventHandler(bt_func9_Click);
            this.bt_func11.Click += new EventHandler(bt_func11_Click);
        }

        #endregion
    }
}
