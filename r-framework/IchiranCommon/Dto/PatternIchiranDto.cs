using System;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.IchiranCommon.Dto
{
    public class PatternIchiranDto
    {
        /// <summary>
        /// 検索条件  :PATTERN_NAME
        /// </summary>
        public String Patern_Name { get; set; }
        /// <summary>
        /// 検索条件 : DENSHU_KBN_CD
        /// </summary>
        public String Denshu_Kbn_Cd{ get; set; }
        /// <summary>
        /// 検索条件 : SHAIN_CD
        /// </summary>
        public String Shain_Cd { get; set; }
        /// <summary>
        /// 検索条件 : SYSTEM_ID
        /// </summary>
        public SqlInt64 System_Id { get; set; }
        /// <summary>
        /// 検索条件 : DELETE_FLG
        /// </summary>
        public String Delete_Flg { get; set; }
        /// <summary>
        /// 検索条件 : DEFAULT_KBN
        /// </summary>
        public String Default_Kbn { get; set; }
    }
}
