
using System.Collections.Generic;
namespace r_framework.Dto.PetternSettingDto
{
    public class PatternSettingDto
    {
        /// <summary>
        /// 表示するカラム名
        /// </summary>
        public string DisplayColumnName { get; set; }

        /// <summary>
        /// 表示するカラムの日本語名（ヘッダー）
        /// </summary>
        public string DisplayJapaneseName { get; set; }

        /// <summary>
        /// Join先のテーブル名
        /// </summary>
        public string JoinTableName { get; set; }

        /// <summary>
        /// Joinを行うカラム名
        /// </summary>
        public List<string> SendColumnName { get; set; }
    }
}
