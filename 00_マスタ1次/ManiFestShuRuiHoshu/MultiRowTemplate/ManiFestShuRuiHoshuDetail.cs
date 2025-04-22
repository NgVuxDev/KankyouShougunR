// $Id: ManiFestShuRuiHoshuDetail.cs 679 2013-08-22 16:03:36Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using ManiFestShuRuiHoshu.Const;

namespace ManiFestShuRuiHoshu.MultiRowTemplate
{
    /// <summary>
    /// 拠点入力一覧
    /// </summary>
    public sealed partial class ManiFestShuRuiHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ManiFestShuRuiHoshuDetail()
        {
            InitializeComponent();
            ManiFestShuRuiHoshuConstans.CD_MAXLENGTH = this.MANIFEST_SHURUI_CD.MaxLength.ToString();
        }
    }
}
