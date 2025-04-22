// $Id: HaikiKbnHoshuDetail.cs 679 2013-08-22 16:03:36Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using HaikiKbnHoshu.Const;

namespace HaikiKbnHoshu.MultiRowTemplate
{
    /// <summary>
    /// 廃棄物区分入力一覧
    /// </summary>
    public sealed partial class HaikiKbnHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HaikiKbnHoshuDetail()
        {
            InitializeComponent();
            HaikiKbnHoshuConstans.CD_MAXLENGTH = this.HAIKI_KBN_CD.MaxLength.ToString();
        }
    }
}
