using System;
using r_framework.APP.Base;

namespace Shougun.Core.Common.CtiRenkeiSettei
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
        {
            // 初期化
            this.InitializeComponent();
            this.logic = new LogicClass(this);
        }
        #endregion

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);

            // 画面の初期化
            this.logic.WindowInit();
        }
    }
}
