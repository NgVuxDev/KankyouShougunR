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
using r_framework.APP.PopUp.Base;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.JissekiHokokuSyuseiPopup
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        public UIForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面で利用するパラメータ
        /// </summary>
        public object[] Params { get; set; }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.logic = new LogicClass(this, this.Params);
            //メッセージクラス
            messageShowLogic = new MessageBoxShowLogic();

            // 画面にデータをセット
            this.logic.SetDisplayData();
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                this.logic.windowClose();
            }
        }

        /// <summary>
        /// キャンセルボタン押下処理
        /// </summary>
        private void bt_cancer_Click(object sender, EventArgs e)
        {
            this.logic.windowClose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.cellDoubleClick(sender, e);
        }
    }
}