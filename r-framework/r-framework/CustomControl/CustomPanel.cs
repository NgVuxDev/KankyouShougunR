using System.ComponentModel;
using System.Windows.Forms;

namespace r_framework.CustomControl
{
    /// <summary>
    /// ヘッダー生成用のカスタムパネル
    /// </summary>
    public partial class CustomPanel : Panel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ペイント処理
        /// </summary>
        /// <param name="pe">イベントハンドラ</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        public string LinkedDataGridViewName { get; set; }
        private bool ShouldSerializeLinkedDataGridViewName()
        {
            return this.LinkedDataGridViewName != null;
        }
    }
}
