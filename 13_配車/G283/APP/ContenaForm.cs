using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using Shougun.Core.Allocation.MobileShougunTorikomi.Logic;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.APP
{
    public partial class ContenaForm : SuperPopupForm
    {
        // デリゲートの宣言
        public delegate void CloseEventHandler(int Index);

        // イベントデリゲートの宣言
        public event CloseEventHandler CloseEvent = null;

        /// <summary>
        /// 親データ取得用
        /// </summary>
        private int index;
        private DataTable dataResult;
        private DataGridViewRow dr;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private ContenaLogic ContenaLogic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ContenaForm(DataTable oyaDataResult, DataGridViewRow oyaDr, int index)
        {
            InitializeComponent();

            this.index = index;
            this.dataResult = oyaDataResult;
            this.dr = oyaDr;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.ContenaLogic = new ContenaLogic(this);
        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面情報の初期化
            this.ContenaLogic.WindowInit(this.dataResult, this.dr);
        }

        /// <summary>
        /// 「閉じるボタン」イベント
        /// </summary>
        /// <param name="e">イベント</param>
        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
            // Close処理
            this.CloseEvent(this.index);

            this.ContenaLogic.WindowFinal();
        }
    }
}
