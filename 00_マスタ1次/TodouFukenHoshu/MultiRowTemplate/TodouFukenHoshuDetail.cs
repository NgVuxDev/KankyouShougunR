// $Id: TodouFukenHoshuDetail.cs 679 2013-08-22 16:03:36Z tecs_suzuki $
using GrapeCity.Win.MultiRow;
using TodouFukenHoshu.Const;

namespace TodouFukenHoshu.MultiRowTemplate
{
    /// <summary>
    /// 都道府県入力一覧
    /// </summary>
    public sealed partial class TodouFukenHoshuDetail : Template
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TodouFukenHoshuDetail()
        {
            InitializeComponent();
            TodouFukenHoshuConstans.CD_MAXLENGTH = this.TODOUFUKEN_CD.MaxLength.ToString();
        }
    }
}
