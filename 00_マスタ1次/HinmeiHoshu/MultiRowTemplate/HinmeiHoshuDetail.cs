// $Id: HinmeiHoshuDetail.cs 1263 2013-09-04 09:47:18Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using HinmeiHoshu.Const;

namespace HinmeiHoshu.MultiRowTemplate
{
    /// <summary>
    /// 品名保守一覧
    /// </summary>
    public sealed partial class HinmeiHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HinmeiHoshuDetail()
        {
            InitializeComponent();
            HinmeiHoshuConstans.CD_MAXLENGTH = this.DENSHU_KBN_CD.MaxLength.ToString();
        }
    }
}
