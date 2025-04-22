using System.Windows.Forms;
using r_framework.Const;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 画面タイプ通知用のラベル
    /// </summary>
    public partial class CustomWindowTypeLabel : Label
    {
        /// <summary>
        /// 画面タイプ保持フィールド
        /// </summary>
        public WINDOW_TYPE WindowType { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomWindowTypeLabel()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
        }
    }
}
