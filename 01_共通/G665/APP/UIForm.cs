using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Common.HanyoCSVShutsuryoku.DTO;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Logic;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku
{
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        ///
        /// </summary>
        internal JokenDto Joken { get; set; }

        /// <summary>
        ///
        /// </summary>
        internal List<M_OUTPUT_CSV_PATTERN> Patterns { get; set; }

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
            : base(WINDOW_ID.T_HANYO_CSV_SHUTSURYOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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

                if (!this.logic.ParentShownBind(true))
                    return;

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.dgvPatterns != null)
                {
                    this.dgvPatterns.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
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
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void parentForm_Shown(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.logic.JokenLoad())
                    return;

                if (!this.logic.ParentShownBind(false))
                    return;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.JokenLoad();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.PatternLoad(WINDOW_TYPE.NEW_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.PatternLoad(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.PatternLoad(WINDOW_TYPE.DELETE_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.CsvOutput();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.logic.Search();
                this.logic.DispSourceRefresh();
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        ///
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
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPatterns_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (e.RowIndex >= 0)
                this.logic.PatternLoad(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        #endregion
    }
}