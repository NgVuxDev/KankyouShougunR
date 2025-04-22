using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;

namespace r_framework.CustomControl
{
    /// <summary>
    /// MultiRowカスタムコンボボックスセル
    /// ソートを行うときにヘッダー部にて使用
    /// </summary>
    public partial class GcCustomSortComboBox : ComboBoxCell
    {
        /// <summary>
        /// ソート方向フィールド
        /// </summary>
        private SortOrder[] sortOrderList = new SortOrder[] { SortOrder.None, SortOrder.Ascending, SortOrder.Descending };

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GcCustomSortComboBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ペイント処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        protected override void OnPaint(CellPaintingEventArgs e)
        {
            base.OnPaint(e);
        }

        /// <summary>
        /// 設定されている値に紐付くソート方向情報を取得する
        /// </summary>
        public SortOrder GetSortOrder()
        {
            return sortOrderList[Convert.ToInt32(this.Value)];
        }
    }
}
