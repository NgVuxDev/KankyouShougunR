using GrapeCity.Win.MultiRow;
using HaikiShuruiHoshu.Const;

namespace HaikiShuruiHoshu
{
    /// <summary>
    /// 業種保守一覧
    /// </summary>
    public sealed partial class HaikiShuruiHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HaikiShuruiHoshuDetail()
        {
            InitializeComponent();
            HaikiShuruiHoshuConstans.CD_MAXLENGTH = this.HAIKI_SHURUI_CD.MaxLength.ToString();
        }
    }
}
