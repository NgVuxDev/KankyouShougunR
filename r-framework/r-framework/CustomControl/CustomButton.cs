using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using r_framework.Const;

namespace r_framework.CustomControl
{
    /// <summary>
    /// カスタムボタン
    /// ボタン名が入力されている場合のみ応答状態とする
    /// </summary>
    public partial class CustomButton : Button
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 名称変更時に応答可非を切り替える
        /// </summary>
        public void TextChange(object sender, System.EventArgs e)
        {
            // ボタン名が変更され、名前が設定されている場合は応答、されていない場合は非応答とする
            this.Enabled = !string.IsNullOrEmpty(this.Text);
        }

        #region Property

        [Category("EDISONプロパティ_処理区分")]
        [Description("対応する処理区分を記述してください。")]
        public PROCESS_KBN ProcessKbn { get; set; }
        private bool ShouldSerializeProcessKbn()
        {
            return this.ProcessKbn != null;
        }

        [Category("EDISONプロパティ")]
        [Browsable(false)]
        public Color DefaultBackColor { get; set; }
        private bool ShouldSerializeDefaultBackColor()
        {
            return this.DefaultBackColor != null;
        }

        #endregion

        /// <summary>
        /// クリックイベント処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        protected override void OnClick(System.EventArgs e)
        {
            base.OnClick(e);
        }

        /// <summary>
        /// PreviewKeyDownイベントを発生させます
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                // 親フォームでキーイベントをハンドリング出来るようにするため
                e.IsInputKey = true;
            }
            
            base.OnPreviewKeyDown(e);
        }
    }
}
