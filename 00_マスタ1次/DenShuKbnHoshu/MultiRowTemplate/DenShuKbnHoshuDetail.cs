// $Id: DenShuKbnHoshuDetail.cs 679 2013-08-22 16:03:36Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using DenShuKbnHoshu.Const;

namespace DenShuKbnHoshu.MultiRowTemplate
{
    /// <summary>
    /// 伝種区分一覧
    /// </summary>
    public sealed partial class DenShuKbnHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenShuKbnHoshuDetail()
        {
            InitializeComponent();
            DenShuKbnHoshuConstans.CD_MAXLENGTH = this.DENSHU_KBN_CD.MaxLength.ToString();
        }
    }
}
