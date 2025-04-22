using System;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Shougun.Core.PaperManifest.ManifestImport
{
    /// <summary>
    /// マニフェストデータインポート画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// マニフェストデータインポート画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        UIHeader headerForm;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm(UIHeader headerForm)
            : base(WINDOW_ID.T_MANIFEST_DATA_IMPORT, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);
            this.logic.WindowInit();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
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
    }
}
