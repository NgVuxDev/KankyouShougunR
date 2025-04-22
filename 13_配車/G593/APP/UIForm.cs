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
using r_framework.Utility;

namespace Shougun.Core.Allocation.KaraContenaIchiranHyou
{
    /// <summary>
    /// コンテナ種類マスタの数量を計算し、何台のコンテナが残っているかを計算し帳票に出力する。
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region Fields
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region Constructors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_KARA_CONTENA_ICHIRAN_HYOU, WINDOW_TYPE.NONE)
        {
            LogUtility.DebugMethodStart();
            this.InitializeComponent();
            this.WindowId = WINDOW_ID.T_KARA_CONTENA_ICHIRAN_HYOU;
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region Inits
        /// <summary>画面Load処理</summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            this.logic.WindowInit();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F5 CSVイベント
        /// <summary>
        /// F5 CSVイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!this.logic.CheckAndInitCondition()) { return; }
            if (this.logic.CheckEmptyForContenaShuruiCd())
            {
                this.logic.CSVPrint();
            }
            Cursor.Current = Cursors.Arrow;
        }
        #endregion

        #region F7 表示イベント
        /// <summary>
        /// F7 表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!this.logic.CheckAndInitCondition()) { return; }
            if (this.logic.CheckEmptyForContenaShuruiCd())
            {
                this.logic.Search();
            }
            Cursor.Current = Cursors.Arrow;
        }
        #endregion

        #region F12 閉じるイベント
        /// <summary>
        /// F12 閉じるイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
            parentForm.Dispose();
            this.Dispose();
        }
        #endregion
    }
}
