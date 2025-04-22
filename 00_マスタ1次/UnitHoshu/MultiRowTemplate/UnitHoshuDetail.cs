// $Id: UnitHoshuDetail.cs 679 2013-08-22 16:03:36Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using UnitHoshu.Const;

namespace UnitHoshu.MultiRowTemplate
{
    /// <summary>
    /// 単位入力一覧
    /// </summary>
    public sealed partial class UnitHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnitHoshuDetail()
        {
            InitializeComponent();
            UnitHoshuConstans.CD_MAXLENGTH = this.UNIT_CD.MaxLength.ToString();
        }
    }
}
