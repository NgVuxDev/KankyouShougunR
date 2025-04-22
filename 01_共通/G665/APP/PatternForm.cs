using System;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Common.HanyoCSVShutsuryoku.APP.Panel;
using Shougun.Core.Common.HanyoCSVShutsuryoku.DTO;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Logic;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.APP
{
    public partial class PatternForm : SuperForm
    {
        #region フィールド

        /// <summary>
        ///
        /// </summary>
        public PatternDto Pattern { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public JokenDto Joken { get; set; }

        /// <summary>
        /// 更新済みフラグ
        /// </summary>
        public bool IsCommited { get; set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private PatternLogicClass logic;

        /// <summary>
        ///
        /// </summary>
        private WINDOW_TYPE wType = WINDOW_TYPE.ICHIRAN_WINDOW_FLAG;

        #endregion

        #region コンストラクタ

        /// <summary>
        ///
        /// </summary>
        /// <param name="column"></param>
        /// <param name="joken"></param>
        /// <param name="windowType"></param>
        public PatternForm(JokenDto joken, PatternDto pattern, WINDOW_TYPE windowType)
            : base(WINDOW_ID.T_HANYO_CSV_OUTPUT_KOUMOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            #region 上部パネル生成

            var patternPanel = PatternPanelFactory.Create(pattern.HaniKbn);
            if (patternPanel != null)
            {
                var panel = patternPanel as UserControl;
                foreach (var item in panel.Controls)
                    this.Controls.Add(item as Control);

                this.pnlPatternDetail.Location = new Point(this.pnlPatternDetail.Location.X, this.pnlPatternDetail.Location.Y + panel.Size.Height - 1);
                this.pnlPatternDetail.Size = new Size(this.pnlPatternDetail.Size.Width, this.pnlPatternDetail.Size.Height - panel.Size.Height + 1);
            }

            #endregion

            this.Joken = joken;
            this.Pattern = pattern;
            this.logic = new PatternLogicClass(this, patternPanel);

            // WindowTypeLabelの表示を回避するため、WindowTypeを一時変数に保存
            this.wType = windowType;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region イベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 初期化
            this.logic.WindowInit();

            // 初期化後WindowTypeを再設定
            this.WindowType = wType;

            // 選択項目、出力項目の読み込み
            this.logic.Search();
            this.logic.FormSet();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.OutputRowRemove();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.OutputRowAdd();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.OutputRowMove(-1);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.OutputRowMove(1);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
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
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.SelectRowJump();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            bool flg = true;

            switch (this.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // 登録(新規)を行う
                    if (flg)
                        flg = this.logic.RegistEx(true);
                    // 登録したら再検索を行う
                    if (flg)
                    {
                        this.logic.Search();
                        //this.logic.FormSet(); // 再設定の必要がないので、検索だけ
                    }
                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 登録(更新)を行う
                    if (flg)
                        flg = this.logic.UpdateEx(true);
                    // 登録したら再検索を行う
                    if (flg)
                    {
                        this.logic.Search();
                        //this.logic.FormSet(); // 再設定の必要がないので、検索だけ
                    }
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    // 削除を行う
                    if (flg)
                        flg = this.logic.DeleteEx();
                    // 削除したら画面を閉じる
                    if (flg)
                        this.logic.FormClose();
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
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
        private void txtOutputKbn_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.txtCondition.Text = string.Empty;

            this.logic.SelectSourceCreate();
            this.logic.OutputSourceClear(true); // 強制的にクリア
            this.logic.SelectSourceRefresh();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void patternPanel_DenshuKbnCheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.txtCondition.Text = string.Empty;

            this.logic.SelectSourceCreate();
            this.logic.OutputSourceClear();
            this.logic.SelectSourceRefresh();

            LogUtility.DebugMethodEnd();
        }

        #endregion
    }
}