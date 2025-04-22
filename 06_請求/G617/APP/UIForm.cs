using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.Billing.GetsujiShori
{
    public partial class UIForm : SuperForm
    {
        #region Field

        /// <summary>ヘッダーフォーム</summary>
        private UIHeader headerForm;

        /// <summary>ロジッククラス</summary>
        private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        /// <param name="type"></param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE type)
            : base(WINDOW_ID.T_GETSUJI, type)
        {
            InitializeComponent();

            this.logic = new LogicClass(headerForm, this);
            this.headerForm = headerForm;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region Event

        /// <summary>
        /// 画面Loadイベント
        /// </summary>
        /// <param name="e">EventArgs</param>
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
    }
}
