// $Id: KyotenHoshuDetail.cs 679 2013-08-22 16:03:36Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using KyotenHoshu.Const;

namespace KyotenHoshu.MultiRowTemplate
{
    /// <summary>
    /// 拠点入力一覧
    /// </summary>
    public sealed partial class KyotenHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KyotenHoshuDetail()
        {
            InitializeComponent();
            KyotenHoshuConstans.CD_MAXLENGTH = this.KYOTEN_CD.MaxLength.ToString();
        }
    }
}
