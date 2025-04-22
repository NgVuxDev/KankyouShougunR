using System.ComponentModel;
using GrapeCity.Win.MultiRow;

namespace r_framework.CustomControl
{
    /// <summary>
    /// MultiRowようカスタムヘッダー
    /// </summary>
    public partial class GcCustomColumnHeader : ColumnHeaderCell
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GcCustomColumnHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(CellPaintingEventArgs e)
        {
            base.OnPaint(e);
        }

        /// <summary>
        /// コピー処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            GcCustomColumnHeader myHeaderCell = base.Clone() as GcCustomColumnHeader;

            myHeaderCell.ViewSearchItem = this.ViewSearchItem;

            return myHeaderCell;
        }

        #region Property

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("検索条件ポップアップに表示するか否かを選択してください")]
        public bool ViewSearchItem { get; set; }
        private bool ShouldSerializeViewSearchItem()
        {
            return this.ViewSearchItem != null;
        }

        #endregion
    }
}
