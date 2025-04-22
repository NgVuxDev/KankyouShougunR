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
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Shougun.Core.SalesManagement.UrikakekinItiranHyo
{
    /// <summary>
    /// 売掛金一覧表画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 売掛金一覧表画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        UIHeader headerForm;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm(UIHeader headerForm)
            : base(WINDOW_ID.T_URIGAKEKIN_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            // コンポーネントの初期化
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 一覧画面再表示
        /// </summary>
        public void IchiranUpdate()
        {
            // 一覧画面再表示
            this.logic.SetIchiran();
        }

           /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);

            // 画面の初期化
            this.logic.WindowInit();
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.Shown += new EventHandler(this.OnShown);

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.UrikakekinItiranHyo != null)
            {
                this.UrikakekinItiranHyo.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        internal virtual void OnShown(object sender, EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            // 範囲指定条件画面表示
            this.Search(null, e);
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.Shown -= new EventHandler(this.OnShown);
        }

        /// <summary>
        /// 印刷
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void Print(object sender, EventArgs e)
        {
            // 印刷出力
            this.logic.Print();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void CSV(object sender, EventArgs e)
        {
            // CSV出力
            this.logic.CSV();
        }

        /// <summary>
        /// 範囲条件指定
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void Search(object sender, EventArgs e)
        {
            // 範囲条件指定画面表示
            this.logic.ShowPopUp(Shougun.Core.Common.Kakepopup.Const.UIConstans.DispType.URIKAKE);
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            // Formクローズ
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }
        /// <summary>
        /// ソート比較イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UrikakekinItiranHyo_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            // 締日にNULLがあるとDataGridViewの仕様でエラーになるので、ここでソート順を決めて止める
            if (e.Column.HeaderText.Contains("締日"))
            {
                if (string.IsNullOrEmpty(e.CellValue1.ToString()))
                {
                    e.SortResult = -1;
                    e.Handled = true;
                }
                else if (string.IsNullOrEmpty(e.CellValue2.ToString()))
                {
                    e.SortResult = 1;
                    e.Handled = true;
                }
            }
        }

    }
}
