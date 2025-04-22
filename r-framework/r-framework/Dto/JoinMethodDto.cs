

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using r_framework.Const;
namespace r_framework.Dto
{
    [Serializable]
    public class JoinMethodDto
    {
        public JoinMethodDto()
        {
            if (this.SearchCondition == null)
            {
                this.SearchCondition = new Collection<SearchConditionsDto>();
            }
        }

        public JOIN_METHOD Join { get; set; }

        [TypeConverter(typeof(SearchConditionsDto))]
        public Collection<SearchConditionsDto> SearchCondition { get; set; }

        public string LeftTable { get; set; }

        public string LeftKeyColumn { get; set; }

        /// <summary>
        /// LeftTableに指定したテーブルについて
        /// 有効なレコードのチェックを行うかどうか
        /// True：行う, False：行わない
        /// </summary>
        [DefaultValue(true)]
        [Description("Trueの場合、LeftTableから有効なレコードのみ取得します。")]
        public bool? IsCheckLeftTable { get; set; }

        public string RightTable { get; set; }

        public string RightKeyColumn { get; set; }

        /// <summary>
        /// RightTableに指定したテーブルについて
        /// 有効なレコードのチェックを行うかどうか
        /// True：行う, False：行わない
        /// </summary>
        [DefaultValue(true)]
        [Description("Trueの場合、RightTableから有効なレコードのみ取得します。")]
        public bool? IsCheckRightTable { get; set; }
    }
}
