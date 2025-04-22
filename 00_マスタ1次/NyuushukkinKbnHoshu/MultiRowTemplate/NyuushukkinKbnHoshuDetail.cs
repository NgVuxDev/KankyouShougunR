// $Id: NyuushukkinKbnHoshuDetail.cs 679 2013-08-22 16:03:36Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using NyuushukkinKbnHoshu.Const;

namespace NyuushukkinKbnHoshu.MultiRowTemplate
{
    /// <summary>
    /// 入出金区分保守一覧
    /// </summary>
    public sealed partial class NyuushukkinKbnHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NyuushukkinKbnHoshuDetail()
        {
            InitializeComponent();
            NyuushukkinKbnHoshuConstans.CD_MAXLENGTH = this.NYUUSHUKKIN_KBN_CD.MaxLength.ToString();
        }
    }
}
