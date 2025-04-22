using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace r_framework.Utility
{
    /// <summary>
    /// DataTable Utility
    /// </summary>
    public static class DataTableUtility
    {
        /// <summary>
        /// 行がオリジナルから変更されているかチェック
        /// </summary>
        public static bool IsDataRowChanged(DataRow row)
        {
            // 新規行は変更ありとする
            if (row.RowState == DataRowState.Added)
            {
                return true;
            }

            // 変更があるかチェック
            foreach (DataColumn column in row.Table.Columns)
            {
                if (!row[column, DataRowVersion.Current].Equals(
                    row[column, DataRowVersion.Original]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
