
using System.Collections.Generic;
namespace r_framework.Dto
{
    public class TablePrimaryKeyDto
    {
        /// <summary>
        /// テーブル物理名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// プライマリーキー物理名
        /// </summary>
        public List<string> PrimaryKey { get; set; }
    }
}
