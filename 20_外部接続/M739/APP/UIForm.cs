using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.ExternalConnection.MapRenkei.Logic;

namespace Shougun.Core.ExternalConnection.MapRenkei
{
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// LogicClass
        /// </summary>
        private LogicClass logic;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_MAP_RENKEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 完全に固定、ここには変更を入れない。
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region イベント

        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            base.OnLoad(e);

            try
            {
                if (!this.logic.WindowInit())
                    return;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.FormClose();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 参照ボタンクリックイベント(取込)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void InportFileRefClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.InportFileRefClick();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出力ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ExportClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.ExportClick();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取込ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void InportClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.InportClick();
            LogUtility.DebugMethodEnd();
        }

        #endregion

        internal void UIForm_KeyDown(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.KeyData == Keys.F9)
            {
                if (this.tabControl1.SelectedIndex == 0)
                {
                    this.logic.ExportClick();
                }
                else
                {
                    this.logic.InportClick();
                }
            }
            LogUtility.DebugMethodEnd();

        }
    }
}