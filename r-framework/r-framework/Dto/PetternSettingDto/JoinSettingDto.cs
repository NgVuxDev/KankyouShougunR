using System.Collections.Generic;

namespace r_framework.Dto.PetternSettingDto
{
    public class JoinSettingDto
    {
        /// <summary>
        /// Joinを行うテーブル
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Joinを行うKeyカラム
        /// </summary>
        public List<string> KeyColumn { get; set; }
    }
}
