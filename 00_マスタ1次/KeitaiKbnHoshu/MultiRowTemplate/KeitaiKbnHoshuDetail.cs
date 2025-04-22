// $Id: KeitaiKbnHoshuDetail.cs 679 2013-08-22 16:03:36Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using KeitaiKbnHoshu.Const;

namespace KeitaiKbnHoshu.MultiRowTemplate
{
    /// <summary>
    /// 形態区分保守一覧
    /// </summary>
    public sealed partial class KeitaiKbnHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KeitaiKbnHoshuDetail()
        {
            InitializeComponent();
            KeitaiKbnHoshuConstans.CD_MAXLENGTH = this.KEITAI_KBN_CD.MaxLength.ToString();
        }
    }
}
