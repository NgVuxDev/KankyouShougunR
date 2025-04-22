// $Id: UIForm.cs 24687 2014-07-04 04:30:57Z j-kikuchi $
using System;
using r_framework.APP.Base;

namespace Shougun.Core.Common.MobileTsuushin
{
    /// <summary>
    /// モバイル通信設定画面
    /// </summary>
    public partial class UIForm : SuperForm, IDisposable
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        #endregion フィールド

        #region コンストラクタ
        /// <summary>
        /// コンストラクター
        /// </summary>
        public UIForm()
        {
            // コンポーネントの初期化
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }

        #endregion コンストラクタ

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
