// $Id: GurupuNyuryokuDetail.cs 679 2013-08-22 16:03:36Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using GurupuNyuryoku.Const;

namespace GurupuNyuryoku.MultiRowTemplate
{
    /// <summary>
    /// 単位入力一覧
    /// </summary>
    public sealed partial class GurupuNyuryokuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GurupuNyuryokuDetail()
        {
            InitializeComponent();
            GurupuNyuryokuConstans.CD_MAXLENGTH = this.GURUPU_CD.MaxLength.ToString();
        }
    }
}
