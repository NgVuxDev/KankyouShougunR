using GrapeCity.Win.MultiRow;

namespace r_framework.Logic
{
    /// <summary>
    /// </summary>
    internal class MultiRowControlLogic
    {
        /// <summary>
        /// 設定を行うコントロールの配列を取得する
        /// </summary>
        /// <param name="cellCollect"></param>
        /// <param name="setField"></param>
        /// <returns></returns>
        internal object[] CreateSetControls(CellCollection cellCollect, string setField)
        {
            if (string.IsNullOrEmpty(setField))
            {
                return null;
            }

            object[] obj = new object[setField.Split(',').Length];

            int i = 0;
            foreach (var field in setField.Split(','))
            {
                foreach (var cell in cellCollect)
                {
                    if (cell.DataField == field.Replace(" ", ""))
                    {
                        obj[i] = cell;
                        break;
                    }
                }
                i++;
            }

            return obj;
        }

        /// <summary>
        /// 設定を行うコントロールの配列をコントロール名により取得する
        /// </summary>
        /// <param name="cellCollect"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        internal object[] CreateSetControlsByNames(CellCollection cellCollect, string names)
        {
            if (string.IsNullOrEmpty(names))
            {
                return null;
            }

            object[] obj = new object[names.Split(',').Length];

            int i = 0;
            foreach (var field in names.Split(','))
            {
                foreach (var cell in cellCollect)
                {
                    if (cell.Name == field.Replace(" ", ""))
                    {
                        obj[i] = cell;
                        break;
                    }
                }
                i++;
            }

            return obj;
        }
    }

    internal static class MultiRowControlExtLogic
    {
        /// <summary>
        /// インデックスより Cell 及び Row を取得する
        /// </summary>
        /// <param name="multiRow"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        /// <param name="cell"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        internal static bool TryGetCellAndRow(this GcMultiRow multiRow, int rowIndex, int cellIndex, out Cell cell, out Row row)
        {
            cell = null;
            row = null;

            if (rowIndex < 0 || cellIndex < 0)
            {
                return false;
            }

            if (multiRow.RowCount <= rowIndex || multiRow.RowCount == 0)
            {
                return false;
            }
            row = multiRow.Rows[rowIndex];

            if (row.Cells.Count <= cellIndex || row.Cells.Count == 0)
            {
                return false;
            }
            cell = row.Cells[cellIndex];

            return true;
        }

        /// <summary>
        /// インデックスより Cell を取得する
        /// </summary>
        /// <param name="multiRow"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        internal static bool TryGetCell(this GcMultiRow multiRow, int rowIndex, int cellIndex, out Cell cell)
        {
            Row row;
            return TryGetCellAndRow(multiRow, rowIndex, cellIndex, out cell, out row);
        }
    }
}